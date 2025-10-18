using System;
using Kutuphane_Otomasyon_Sistemi.Domain.Services;
using Kutuphane_Otomasyon_Sistemi.Presentation;

namespace Kutuphane_Otomasyon_Sistemi
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var lib = new Kutuphane();
            KutuphaneMenü.Seed(lib); // başlangıç verisi

            // ✅ Program başında üyeleri ve kitapları listele
            Console.WriteLine("=== Başlangıç Üyeleri ===");
            KutuphaneMenü.Menu_UyeListele(lib);

            Console.WriteLine("\n=== Başlangıç Kitapları ===");
            KutuphaneMenü.Menu_KitapListele(lib);

            // ✅ Menü döngüsü
            while (true)
            {
                Console.WriteLine("\n=== KÜTÜPHANE MENÜ ===");
                Console.WriteLine("1) Üye Ekle");
                Console.WriteLine("2) Kitap Ekle");
                Console.WriteLine("3) Ödünç Ver");
                Console.WriteLine("4) İade Al");
                Console.WriteLine("5) Kitap Durumu Güncelle");
                Console.WriteLine("6) Tüm Kitapları Listele");
                Console.WriteLine("7) Tüm Üyeleri Listele");
                Console.WriteLine("8) Üyenin Ödünç Aldıkları");
                Console.WriteLine("9) Kütüphane Durum Özeti");
                Console.WriteLine("0) Çıkış");
                Console.Write("Seçim: ");

                var secim = Console.ReadLine()?.Trim();
                Console.WriteLine();

                try
                {
                    switch (secim)
                    {
                        case "1": KutuphaneMenü.Menu_UyeEkle(lib); break;
                        case "2": KutuphaneMenü.Menu_KitapEkle(lib); break;
                        case "3": KutuphaneMenü.Menu_OduncVer(lib); break;
                        case "4": KutuphaneMenü.Menu_IadeAl(lib); break;
                        case "5": KutuphaneMenü.Menu_DurumGuncelle(lib); break;
                        case "6": KutuphaneMenü.Menu_KitapListele(lib); break;
                        case "7": KutuphaneMenü.Menu_UyeListele(lib); break;
                        case "8": KutuphaneMenü.Menu_UyeninOduncleri(lib); break;
                        case "9": KutuphaneMenü.Menu_DurumOzet(lib); break;
                        case "0": return;
                        default: Console.WriteLine("Geçersiz seçim."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
        }
    }
}