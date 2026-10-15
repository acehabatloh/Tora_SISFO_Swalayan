using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TORA_SISFO_SWALAYAN;

// Mengubah angka jadi tulisan rupiah, contoh: 1500 -> "Rp 1.500"
public static class Rupiah
{
    public static string Format(int angka)
    {
        return "Rp " + angka.ToString("N0", CultureInfo.InvariantCulture).Replace(",", ".");
    }
}

// Satu baris belanjaan
public class ItemTransaksi
{
    public int No { get; }
    public Barang Barang { get; }
    public int Jumlah { get; }
    public int TotalHarga => Barang.Harga * Jumlah;

    public ItemTransaksi(int no, Barang barang, int jumlah)
    {
        No = no;
        Barang = barang;
        Jumlah = jumlah;
    }
}

// Satu kali belanja (kumpulan item)
public class Transaksi
{
    private readonly List<ItemTransaksi> _daftarItem = new List<ItemTransaksi>();

    public int JumlahItem => _daftarItem.Count;
    public int NomorBerikutnya => _daftarItem.Count + 1;
    public int Total => _daftarItem.Sum(i => i.TotalHarga);

    public ItemTransaksi Tambah(Barang barang, int jumlah)
    {
        ItemTransaksi item = new ItemTransaksi(NomorBerikutnya, barang, jumlah);
        _daftarItem.Add(item);
        return item;
    }

    public int HitungKembali(int bayar)
    {
        return bayar - Total;
    }

    public void Reset()
    {
        _daftarItem.Clear();
    }
}