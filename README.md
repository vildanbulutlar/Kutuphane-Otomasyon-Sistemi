# 📚 Kütüphane Otomasyon Sistemi

Bu proje, bir **kütüphane yönetim sistemi** simülasyonudur.  
Amaç, kütüphane üyeleri, kitaplar ve ödünç işlemleri üzerinde temel CRUD operasyonlarını yapmaktır.  
Uygulama **katmanlı mimari** prensibine göre geliştirilmiştir (Domain – Application – Presentation).

---

## 🚀 Proje Özellikleri

- Üye ekleme, listeleme, ödünç verilen kitapları görüntüleme  
- Kitap ekleme, durumu güncelleme (ödünç verildi / alındı)  
- Kitap türlerine göre (Bilim, Roman, Tarih vb.) sınıflandırma  
- Konsol tabanlı menü sistemi  
- Validation kontrolü (geçersiz girişlerde uyarı mesajı)  
- Enum yapısıyla kitap durum yönetimi  

---

## 🧩 Katmanlı Yapı
```md
📦 Kutuphane_Otomasyon_Sistemi
 ┣ 📂 Domain
 ┃ ┣ 📂 Contracts
 ┃ ┃ ┗ IUye.cs
 ┃ ┣ 📂 Entities
 ┃ ┃ ┣ Kitap.cs
 ┃ ┃ ┣ KitapRoman.cs
 ┃ ┃ ┣ KitapBilim.cs
 ┃ ┃ ┣ KitapTarih.cs
 ┃ ┃ ┗ Uye.cs
 ┃ ┣ 📂 Enum
 ┃ ┃ ┗ Durum.cs
 ┃ ┣ 📂 Services
 ┃ ┃ ┗ Kutuphane.cs
 ┃ ┣ 📂 Validation
 ┃ ┃ ┣ OduncValidation.cs
 ┃ ┃ ┗ ValidationHelper.cs
 ┃ ┗ 📂 Utilities
 ┣ 📂 Presentation
 ┃ ┣ DurumExtensions.cs
 ┃ ┣ KütüphaneMenü.cs
 ┃ ┗ Program.cs


## ⚙️ Teknolojiler

- C# (.NET 8.0)
- Nesne Yönelimli Programlama (OOP)
- Katmanlı Mimari
- Enum, Interface, Validation yapıları
- Console Application

---

## 👩‍💻 Geliştirici

**Vildan Bulutlar**  
📍 Türkiye  
📫 [GitHub Profili](https://github.com/vildanbulutlar)

---

## 📄 Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.
 
