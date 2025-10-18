using Kutuphane_Otomasyon_Sistemi.Domain.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Otomasyon_Sistemi.Domain.Entities
{
    /// <summary>Roman türü kitap.</summary>
    public class KitapRoman : Kitap
    {
        public override string Tur => "Roman";

        public KitapRoman(
            string isbn,
            string baslik,
            string yazar,
            int yayinYili,
            Durum durum = Durum.OduncAlabilir ) : base(isbn, baslik, yazar, yayinYili, durum) { }
    }
}