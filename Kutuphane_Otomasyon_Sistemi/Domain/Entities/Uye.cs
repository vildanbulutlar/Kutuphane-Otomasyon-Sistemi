using Kutuphane_Otomasyon_Sistemi.Domain.Contracts;
using Kutuphane_Otomasyon_Sistemi.Domain.Enum;

using Kutuphane_Otomasyon_Sistemi.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Otomasyon_Sistemi.Domain.Entities
{
    /// <summary>
    /// Tek tip üye modeli. IUye sözleşmesini uygular.
    /// </summary>
    public class Uye : IUye
    {
        private readonly List<Kitap> _odunc = new();

        public Guid UyeNo { get; }
        public string Ad { get; }
        public string Soyad { get; }
        public string Telefon { get; }   // ✅ Yeni alan

        public IReadOnlyCollection<Kitap> OduncAldiklari => _odunc.AsReadOnly();

        public Uye(string ad, string soyad, string telefon)
        {
            Ad = ValidationHelper.NotEmpty(ad, nameof(ad));
            Soyad = ValidationHelper.NotEmpty(soyad, nameof(soyad));
            Telefon = ValidationHelper.NotEmpty(telefon, nameof(telefon)); // Boş olamaz
            UyeNo = Guid.NewGuid();
        }


        public void OduncAl(Kitap kitap)
        {
            // Tüm iş kuralları ayrı sınıfta:
            OduncValidation.EnsureVerilebilir(kitap);
            OduncValidation.EnsureUyeninElindeDegil(this, kitap);

            _odunc.Add(kitap);
            kitap.DurumGuncelle(Durum.OduncVerildi);
        }

        public void IadeEt(Kitap kitap)
        {
            // İade kuralları ayrı sınıfta:
            OduncValidation.EnsureUyeninElinde(this, kitap);

            _odunc.Remove(kitap);
            kitap.DurumGuncelle(Durum.OduncAlabilir);
        }

        public override string ToString() => $"{Ad} {Soyad} ({UyeNo}) | Ödünç: {_odunc.Count}";
    }
}
