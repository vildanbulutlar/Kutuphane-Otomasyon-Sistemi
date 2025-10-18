using Kutuphane_Otomasyon_Sistemi.Domain.Contracts;
using Kutuphane_Otomasyon_Sistemi.Domain.Entities;
using Kutuphane_Otomasyon_Sistemi.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_Otomasyon_Sistemi.Domain.Validation
{
    public static class OduncValidation
    {
        /// <summary>Kitap null olmamalı ve ödünç verilebilir durumda olmalı.</summary>
        public static void EnsureVerilebilir(Kitap kitap)
        {
            if (kitap is null) throw new ArgumentNullException(nameof(kitap));
            if (kitap.Durum != Durum.OduncAlabilir)
                throw new InvalidOperationException($"Kitap ödünç verilemez. Mevcut durum: {kitap.Durum}");
        }

        /// <summary>Üye null olmamalı ve bu kitabı zaten elinde bulundurmamalı.</summary>
        public static void EnsureUyeninElindeDegil(IUye uye, Kitap kitap)
        {
            if (uye is null) throw new ArgumentNullException(nameof(uye));
            if (kitap is null) throw new ArgumentNullException(nameof(kitap));
            if (uye.OduncAldiklari.Contains(kitap))
                throw new InvalidOperationException("Üye bu kitabı zaten ödünç almış.");
        }

        /// <summary>İade için: Üyenin elinde bu kitap bulunmalı.</summary>
        public static void EnsureUyeninElinde(IUye uye, Kitap kitap)
        {
            if (uye is null) throw new ArgumentNullException(nameof(uye));
            if (kitap is null) throw new ArgumentNullException(nameof(kitap));
            if (!uye.OduncAldiklari.Contains(kitap))
                throw new InvalidOperationException("Bu kitap üyede kayıtlı görünmüyor.");
        }
    }
}