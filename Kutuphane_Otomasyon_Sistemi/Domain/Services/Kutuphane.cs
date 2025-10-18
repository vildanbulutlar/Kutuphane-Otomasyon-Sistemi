using Kutuphane_Otomasyon_Sistemi.Domain.Contracts;
using Kutuphane_Otomasyon_Sistemi.Domain.Entities;
using Kutuphane_Otomasyon_Sistemi.Domain.Enum;

using Kutuphane_Otomasyon_Sistemi.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kutuphane_Otomasyon_Sistemi.Domain.Services
{
    public class Kutuphane
    {
        private readonly Dictionary<Guid, IUye> _uyeler = new();
        private readonly Dictionary<string, Kitap> _kitaplarByIsbn = new(StringComparer.OrdinalIgnoreCase);

        // --- Üye ---
        public Guid UyeEkle(string ad, string soyad, string telefon)
        {
            var uye = new Uye(
                ValidationHelper.NotEmpty(ad, nameof(ad)),
                ValidationHelper.NotEmpty(soyad, nameof(soyad)),
                ValidationHelper.NotEmpty(telefon, nameof(telefon)));

            ValidationHelper.EnsureUnique(_uyeler, uye.UyeNo, "Üye");
            _uyeler.Add(uye.UyeNo, uye);
            return uye.UyeNo;
        }

        public IUye UyeBul(Guid uyeNo)
            => _uyeler.TryGetValue(uyeNo, out var u) ? u : throw new KeyNotFoundException("Üye bulunamadı.");

        // --- Kitap ---
        public void KitapEkle(Kitap kitap)
        {
            if (kitap is null) throw new ArgumentNullException(nameof(kitap));
            ValidationHelper.EnsureUnique(_kitaplarByIsbn, kitap.ISBN, "Kitap");
            _kitaplarByIsbn.Add(kitap.ISBN, kitap);
        }

        public Kitap KitapBul(string isbn)
            => _kitaplarByIsbn.TryGetValue(ValidationHelper.NotEmpty(isbn, nameof(isbn)), out var k)
               ? k : throw new KeyNotFoundException("Kitap bulunamadı.");

        // --- İş Akışları (tek satır) ---
        public void OduncVer(Guid uyeNo, string isbn)
            => UyeBul(uyeNo).OduncAl(KitapBul(isbn));

        public void IadeAl(Guid uyeNo, string isbn)
            => UyeBul(uyeNo).IadeEt(KitapBul(isbn));

        public void GuncelleDurum(string isbn, Durum yeniDurum)
            => KitapBul(isbn).DurumGuncelle(yeniDurum);

        public IReadOnlyCollection<Kitap> UyeninOduncleri(Guid uyeNo)
            => UyeBul(uyeNo).OduncAldiklari.ToList().AsReadOnly();

        // --- Menü yardımcıları (kısa) ---
        public IReadOnlyCollection<Kitap> TumKitaplar()
            => _kitaplarByIsbn.Values.ToList().AsReadOnly();

        public IReadOnlyCollection<IUye> TumUyeler()
            => _uyeler.Values.ToList().AsReadOnly();

        public (IReadOnlyList<Kitap> musait, IReadOnlyList<Kitap> oduncte, IReadOnlyList<Kitap> yok) DurumOzet()
        {
            IReadOnlyList<Kitap> F(Durum d)
                => _kitaplarByIsbn.Values.Where(k => k.Durum == d).ToList().AsReadOnly();

            return (F(Durum.OduncAlabilir), F(Durum.OduncVerildi), F(Durum.MevcutDegil));
        }

        // (Opsiyonel) String rapor yerine sayılar döndürüp UI'da yazdırmak daha temiz:
        public (int toplam, int oduncte, int alinabilir, int yok, int uyeSayisi) RaporSayilar()
        {
            int toplam     = _kitaplarByIsbn.Count;
            int oduncte    = _kitaplarByIsbn.Values.Count(k => k.Durum == Durum.OduncVerildi);
            int alinabilir = _kitaplarByIsbn.Values.Count(k => k.Durum == Durum.OduncAlabilir);
            int yok        = _kitaplarByIsbn.Values.Count(k => k.Durum == Durum.MevcutDegil);
            return (toplam, oduncte, alinabilir, yok, _uyeler.Count);
        }
    }
}