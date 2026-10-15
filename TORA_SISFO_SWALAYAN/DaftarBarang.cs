using System.Collections.Generic;

namespace TORA_SISFO_SWALAYAN;

// Menyimpan semua barang dan bisa mencarinya berdasarkan kode
public class DaftarBarang
{
    private readonly List<Barang> _semua = new List<Barang>
    {
        new Barang("pp",   "PULPEN",            1000),
        new Barang("pshb", "PINSIL HB",          500),
        new Barang("ps2b", "PINSIL 2B",          800),
        new Barang("bt30", "BUKU TULIS 30 Lbr", 1500),
        new Barang("bt50", "BUKU TULIS 50 Lbr", 3000),
        new Barang("bga2", "BUKU GAMBAR A2",    2500)
    };

    public IReadOnlyList<Barang> Semua => _semua;

    // Hasilnya null kalau kode tidak ditemukan
    public Barang? Cari(string kode)
    {
        string kodeCari = kode.Trim().ToLower();

        foreach (Barang b in _semua)
        {
            if (b.Kode.ToLower() == kodeCari)
            {
                return b;
            }
        }

        return null;
    }
}