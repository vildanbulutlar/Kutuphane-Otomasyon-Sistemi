using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Otomasyon_Sistemi.Domain.Validation
{
    /// <summary>
    /// Basit ve tekrar kullanılabilir doğrulamalar.
    /// Her metod, geçersiz veri gelirse açıklayıcı bir istisna fırlatır.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Metnin boş/whitespace olmamasını garanti eder, trim’ler ve geri döner.
        /// </summary>
        public static string NotEmpty(string? value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{fieldName} boş olamaz.", fieldName);
            return value.Trim();
        }

        /// <summary>
        /// Pozitif tamsayıyı zorunlu kılar (örn. yıl, adet, sayfa sayısı).
        /// </summary>
        public static int PositiveInt(int value, string fieldName)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(fieldName, $"{fieldName} pozitif olmalı.");
            return value;
        }
        /// <summary>
        /// Verilen sözlükte anahtar zaten varsa hata fırlatır.
        /// IDictionary kullanıldığı için Dictionary ve türevleriyle uyumludur.
        /// </summary>
        public static void EnsureUnique<TKey, TValue>(
            IDictionary<TKey, TValue> dict, TKey key, string fieldName)
        {
            if (dict is null) throw new ArgumentNullException(nameof(dict));
            if (key is null) throw new ArgumentNullException(nameof(key));

            if (dict.ContainsKey(key))
                throw new InvalidOperationException($"{fieldName} zaten mevcut.");
        }
    }
}
