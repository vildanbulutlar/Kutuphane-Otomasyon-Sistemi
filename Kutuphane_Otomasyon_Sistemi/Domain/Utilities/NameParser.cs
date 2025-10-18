using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Otomasyon_Sistemi.Domain.Utilities
{
    public static class NameParser
    {
        public static (string Ad, string Soyad) Parse(string adSoyad)
        {
            if (string.IsNullOrWhiteSpace(adSoyad))
                throw new ArgumentException("Ad Soyad boş olamaz", nameof(adSoyad));

            var parts = adSoyad.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1) return (parts[0].Trim(), "");

            var soyad = parts[^1].Trim();
            var ad = string.Join(" ", parts, 0, parts.Length - 1).Trim();
            return (ad, soyad);
        }
    }
}