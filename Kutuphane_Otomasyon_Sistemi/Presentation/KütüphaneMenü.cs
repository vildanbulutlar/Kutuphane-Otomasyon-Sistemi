using Kutuphane_Otomasyon_Sistemi.Domain.Contracts;
using Kutuphane_Otomasyon_Sistemi.Domain.Entities;
using Kutuphane_Otomasyon_Sistemi.Domain.Enum;
using Kutuphane_Otomasyon_Sistemi.Domain.Services;
using Kutuphane_Otomasyon_Sistemi.Presentation.Kutuphane_Otomasyon_Sistemi.Presentation;
namespace Kutuphane_Otomasyon_Sistemi.Presentation
{
    internal static class KutuphaneMenü
    {
        // ---- MENÜ İŞLEMLERİ ----
        public static void Menu_UyeEkle(Kutuphane lib)
        {
            var ad = Prompt("Ad");
            var soyad = Prompt("Soyad");
            var telefon = Prompt("Telefon");  // ✅ Artık telefon da isteniyor

            var uyeNo = lib.UyeEkle(ad, soyad, telefon);
            Console.WriteLine($"✔ Eklendi: {ad} {soyad} | Tel: {telefon} | No: {uyeNo}");
        }

        public static void Menu_KitapEkle(Kutuphane lib)
        {
            var isbn = Prompt("ISBN");
            var baslik = Prompt("Başlık");
            var yazar = Prompt("Yazar");
            var yil = PromptInt("Yayın yılı", 1, 3000);

            Console.WriteLine("Tür seçin: 1) Bilim  2) Roman  3) Tarih");
            var tur = Prompt("Tür (1/2/3)");

            Kitap k = tur switch
            {
                "1" => new KitapBilim(isbn, baslik, yazar, yil),
                "2" => new KitapRoman(isbn, baslik, yazar, yil),
                "3" => new KitapTarih(isbn, baslik, yazar, yil),
                _ => throw new InvalidOperationException("Geçersiz tür.")
            };

            lib.KitapEkle(k);
            Console.WriteLine("✔ Kitap eklendi.");
        }

        public static void Menu_OduncVer(Kutuphane lib)
        {
            var isbn = Prompt("ISBN");
            var uye = SelectUye(lib);
            lib.OduncVer(uye.UyeNo, isbn);
            Console.WriteLine($"✔ \"{isbn}\" ödünç verildi → {uye.Ad} {uye.Soyad} | Tel: {uye.Telefon}");
        }

        public static void Menu_IadeAl(Kutuphane lib)
        {
            var isbn = Prompt("ISBN");
            var uye = SelectUye(lib);
            lib.IadeAl(uye.UyeNo, isbn);
            Console.WriteLine($"\"{isbn}\" iade alındı ← {uye.Ad} {uye.Soyad}");
        }

        public static void Menu_DurumGuncelle(Kutuphane lib)
        {
            var isbn = Prompt("ISBN");
            var kitap = lib.KitapBul(isbn);

            var values = (Durum[])Enum.GetValues(typeof(Durum));

            Console.WriteLine("Yeni durum seçin:");
            for (int i = 0; i < values.Length; i++)
                Console.WriteLine($"{i + 1}) {values[i].FriendlyName()}");

            var secimText = Prompt($"Seçim (1-{values.Length})");
            if (!int.TryParse(secimText, out var secim) || secim < 1 || secim > values.Length)
                throw new InvalidOperationException("Geçersiz seçim.");

            var yeni = values[secim - 1];
            lib.GuncelleDurum(kitap.ISBN, yeni);
            Console.WriteLine($"✔ Durum güncellendi: {yeni.FriendlyName()}");
        }

        public static void Menu_KitapListele(Kutuphane lib)
        {
            var list = lib.TumKitaplar();
            if (!list.Any()) { Console.WriteLine("Kitap yok."); return; }

            foreach (var k in list)
                Console.WriteLine($" - {k.ISBN} | {k.Baslik} — {k.Yazar} | {k.Durum.FriendlyName()}");
        }

        public static void Menu_UyeListele(Kutuphane lib)
        {
            var list = lib.TumUyeler();
            if (!list.Any()) { Console.WriteLine("Üye yok."); return; }

            foreach (var u in list)
                Console.WriteLine($" - {u.Ad} {u.Soyad} | Tel: {u.Telefon} | ID: {u.UyeNo}");
        }
        public static void Menu_UyeninOduncleri(Kutuphane lib)
        {
            var uye = SelectUye(lib);
            var list = lib.UyeninOduncleri(uye.UyeNo);
            if (!list.Any()) { Console.WriteLine("Ödünç yok."); return; }

            Console.WriteLine($"{uye.Ad} {uye.Soyad} → " +
                              string.Join(", ", list.Select(k => $"{k.Baslik} ({k.Durum.FriendlyName()})")));
        }

        public static void Menu_DurumOzet(Kutuphane lib)
        {
            var (musait, oduncte, yok) = lib.DurumOzet();
            Console.WriteLine($"{Durum.OduncAlabilir.FriendlyName()}: {musait.Count} | " +
                              $"{Durum.OduncVerildi.FriendlyName()}: {oduncte.Count} | " +
                              $"{Durum.MevcutDegil.FriendlyName()}: {yok.Count}");
        }

        // ---- YARDIMCILAR ----
        private static IUye SelectUye(Kutuphane lib)
        {
            var uyeler = lib.TumUyeler().ToList();
            if (!uyeler.Any())
                throw new InvalidOperationException("Hiç üye yok.");

            Console.WriteLine("Üyeler:");
            for (int i = 0; i < uyeler.Count; i++)
                Console.WriteLine($"{i + 1}) {uyeler[i].Ad} {uyeler[i].Soyad} | Tel: {uyeler[i].Telefon}");

            int secim = PromptInt($"Üye seç (1-{uyeler.Count})", 1, uyeler.Count);
            return uyeler[secim - 1];
        }

        private static string Prompt(string label)
        {
            Console.Write($"{label}: ");
            var s = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(s))
                throw new InvalidOperationException($"{label} boş olamaz.");
            return s;
        }

        private static int PromptInt(string label, int min, int max)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                if (int.TryParse(Console.ReadLine(), out var v) && v >= min && v <= max)
                    return v;
                Console.WriteLine($"⚠ Geçerli sayı giriniz ({min}-{max}).");
            }
        }

        public static void Seed(Kutuphane lib)
        {
            // Üyeler
            lib.UyeEkle("Ayşe", "Yılmaz", "0505 111 22 33");
            lib.UyeEkle("Mehmet", "Arslan", "0532 444 55 66");
            lib.UyeEkle("Ayşe", "Yılmaz", "0506 777 88 99");   // aynı isim, farklı telefon
            lib.UyeEkle("Zeynep", "Çelik", "0543 123 45 67");

            // Kitaplar
            lib.KitapEkle(new KitapBilim("978-0001", "Zamanın Kısa Tarihi", "S. Hawking", 1988));
            lib.KitapEkle(new KitapRoman("978-0002", "Kürk Mantolu Madonna", "S. Ali", 1943));
            lib.KitapEkle(new KitapTarih("978-0003", "Nutuk", "M. K. Atatürk", 1927));
            lib.KitapEkle(new KitapBilim("978-0004", "Kozmos", "Carl Sagan", 1980));
        }
    }
}
