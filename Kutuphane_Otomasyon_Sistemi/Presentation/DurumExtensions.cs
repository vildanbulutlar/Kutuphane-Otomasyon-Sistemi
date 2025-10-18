using Kutuphane_Otomasyon_Sistemi.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Otomasyon_Sistemi.Presentation
{
    namespace Kutuphane_Otomasyon_Sistemi.Presentation
    {
        public static class DurumExtensions
        {
            public static string FriendlyName(this Durum d) => d switch
            {
                Durum.OduncAlabilir => "Ödünç Alınabilir",
                Durum.OduncVerildi => "Ödünçte",
                Durum.MevcutDegil => "Mevcut Değil",
                _ => d.ToString()
            };
        }
    }
}
