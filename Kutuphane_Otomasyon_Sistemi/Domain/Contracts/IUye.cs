using Kutuphane_Otomasyon_Sistemi.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Kutuphane_Otomasyon_Sistemi.Domain.Contracts
{
    /// <summary>Üye davranış sözleşmesi.</summary>
    public interface IUye
    {
        /// <summary>Üyeye sistemce verilen benzersiz numara.</summary>
        Guid UyeNo { get; }

        /// <summary>Üyenin adı.</summary>
        string Ad { get; }

        /// <summary>Üyenin soyadı.</summary>
        string Soyad { get; }

        /// <summary>Üyenin telefon numarası.</summary>
        string Telefon { get; }   // ✅ yeni eklendi

        /// <summary>Üyenin elindeki (ödünç aldığı) kitaplar (salt okunur koleksiyon).</summary>
        IReadOnlyCollection<Kitap> OduncAldiklari { get; }

        /// <summary>Üye ödünç kitap alır.</summary>
        void OduncAl(Kitap kitap);

        /// <summary>Üye elindeki kitabı iade eder.</summary>
        void IadeEt(Kitap kitap);
    }
}