using System;

namespace TORA_SISFO_SWALAYAN;

// Nama hari dari sebuah tanggal
public static class Hari
{
    public static string Nama(DateTime tanggal)
    {
        string[] nama = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jum'at", "Sabtu" };
        return nama[(int)tanggal.DayOfWeek];
    }
}

// Aturan hadiah point reward
public static class HadiahPoint
{
    public static (string Teks, string Hadiah) Cek(int point)
    {
        if (point <= 499)
            return ("Maaf, Point Anda Belum Mencapai 500.", "Anda Belum Mendapat Hadiah.");

        string hadiah;
        if (point <= 1000) hadiah = "Mug Cantik";
        else if (point <= 1500) hadiah = "Boneka Cantik";
        else if (point <= 2000) hadiah = "Kipas Angin";
        else if (point <= 2500) hadiah = "Dispenser";
        else hadiah = "Dispenser + Sisa Point: " + (point - 2500);

        return ("Anda Mendapatkan Hadiah:", hadiah);
    }
}

// Aturan zodiac dan batu zodiac
public static class Zodiak
{
    public static (string Nama, string Batu) Cari(int tgl, int bln)
    {
        if ((tgl >= 21 && bln == 1) || (tgl <= 19 && bln == 2)) return ("AQUARIUS", "AMETHYST");
        if ((tgl >= 20 && bln == 2) || (tgl <= 20 && bln == 3)) return ("PISCES", "ZIRCON");
        if ((tgl >= 21 && bln == 3) || (tgl <= 20 && bln == 4)) return ("ARIES", "DIAMOND (INTAN)");
        if ((tgl >= 21 && bln == 4) || (tgl <= 22 && bln == 5)) return ("TAURUS", "EMERALD (ZAMRUD)");
        if ((tgl >= 23 && bln == 5) || (tgl <= 21 && bln == 6)) return ("GEMINI", "MOONSTONE (WIDURI BULAN)");
        if ((tgl >= 22 && bln == 6) || (tgl <= 22 && bln == 7)) return ("CANCER", "RUBY");
        if ((tgl >= 23 && bln == 7) || (tgl <= 22 && bln == 8)) return ("LEO", "SARDONIK");
        if ((tgl >= 23 && bln == 8) || (tgl <= 23 && bln == 9)) return ("VIRGO", "SAPPHIRE (SAFIR)");
        if ((tgl >= 24 && bln == 9) || (tgl <= 22 && bln == 10)) return ("LIBRA", "OVAL (OPAL)");
        if ((tgl >= 23 && bln == 10) || (tgl <= 21 && bln == 11)) return ("SCORPIO", "TOPAZ");
        if ((tgl >= 22 && bln == 11) || (tgl <= 22 && bln == 12)) return ("SAGITARIUS", "ALEXANDRITE");
        return ("CAPRICON", "GARNET");   // 23-12 s/d 20-01
    }
}