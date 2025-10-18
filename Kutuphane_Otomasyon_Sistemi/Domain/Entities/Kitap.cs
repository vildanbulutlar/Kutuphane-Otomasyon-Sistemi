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
    /// Kütüphanedeki tüm kitap tiplerinin temel sınıfı.
    /// Yeni tür eklemek için bu sınıftan türetin (ör. CocukKitabi, FelsefeKitabi...).
    /// </summary>
    public abstract class Kitap
    {
        /// <summary>Uluslararası standart kitap numarası (unique).</summary>
        public string ISBN { get; }

        /// <summary>Kitap adı/başlık.</summary>
        public string Baslik { get; }

        /// <summary>Yazar adı.</summary>
        public string Yazar { get; }

        /// <summary>Yayın yılı (pozitif olmalı).</summary>
        public int YayinYili { get; }

        /// <summary>Kitabın ödünç durumu.</summary>
        public Durum Durum { get; private set; }

        /// <summary>Tür adı (alt sınıflar override edebilir).</summary>
        public virtual string Tur => "Genel";

        protected Kitap(string isbn, string baslik, string yazar, int yayinYili, Durum durum = Durum.OduncAlabilir)
        {
            // Doğrulamalar ortak helper üzerinden
            ISBN = ValidationHelper.NotEmpty(isbn, nameof(isbn));
            Baslik = ValidationHelper.NotEmpty(baslik, nameof(baslik));
            Yazar = ValidationHelper.NotEmpty(yazar, nameof(yazar));
            YayinYili = ValidationHelper.PositiveInt(yayinYili, nameof(yayinYili));
            Durum = durum;
        }

        /// <summary>Kitabın durumunu değiştirir.</summary>
        public void DurumGuncelle(Durum yeniDurum) => Durum = yeniDurum;

        public override string ToString()
            => $"[{Tur}] {Baslik} — {Yazar} ({YayinYili}) | ISBN: {ISBN} | Durum: {Durum}";
    }
}

