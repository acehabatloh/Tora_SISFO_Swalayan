namespace TORA_SISFO_SWALAYAN;

// Satu objek Barang = satu barang di swalayan
public class Barang
{
    public string Kode { get; }
    public string Nama { get; }
    public int Harga { get; }

    public Barang(string kode, string nama, int harga)
    {
        Kode = kode;
        Nama = nama;
        Harga = harga;
    }
}