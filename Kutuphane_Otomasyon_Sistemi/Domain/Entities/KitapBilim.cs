using Kutuphane_Otomasyon_Sistemi.Domain.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Otomasyon_Sistemi.Domain.Entities
{
    /// <summary>Bilim türü kitap.</summary>
    public class KitapBilim : Kitap
    {
        public override string Tur => "Bilim";

        public KitapBilim(
            string isbn,
            string baslik,
            string yazar,
            int yayinYili,

            Durum durum = Durum.OduncAlabilir) : base(isbn, baslik, yazar, yayinYili, durum) { }
    }
}
