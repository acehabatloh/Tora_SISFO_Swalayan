using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;

namespace TORA_SISFO_SWALAYAN;

public partial class FrmTransaksi : Window
{
    private readonly DaftarBarang _daftarBarang = new DaftarBarang();
    private readonly Transaksi _transaksi = new Transaksi();
    private Barang? _barangDipilih;
    private readonly DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

    public FrmTransaksi()
    {
        InitializeComponent();

        LblHari.Text = Hari.Nama(DateTime.Today);
        LblTgl.Text = DateTime.Today.ToString("dd-MM-yyyy");
        TampilJam();

        // Timer: memperbarui jam setiap 1 detik
        _timer.Tick += (s, e) => TampilJam();
        _timer.Start();
        Closed += (s, e) => _timer.Stop();

        Segarkan();
    }

    private void TampilJam()
    {
        LblJam.Text = DateTime.Now.ToString("HH:mm:ss");
    }

    // Memperbarui "Item Ke", "Jumlah Item", dan "Total Transaksi"
    private void Segarkan()
    {
        LblNoItem.Text = _transaksi.NomorBerikutnya.ToString();
        LblJlhItem.Text = _transaksi.JumlahItem.ToString();
        TxtTT.Text = _transaksi.JumlahItem == 0 ? "" : Rupiah.Format(_transaksi.Total);
    }

    // Kode barang: tekan Enter untuk mencari barang
    private async void TxtKB_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;

        // Kode kosong tapi sudah ada belanjaan -> pindah ke kolom Bayar
        if (string.IsNullOrWhiteSpace(TxtKB.Text))
        {
            if (_transaksi.JumlahItem > 0) TxtB.Focus();
            return;
        }

        Barang? barang = _daftarBarang.Cari(TxtKB.Text);
        if (barang == null)
        {
            await Pesan.Tampilkan(this, "Info Kode Barang", "Kode barang tidak ada");
            TxtKB.Focus();
            return;
        }

        _barangDipilih = barang;
        TxtNB.Text = barang.Nama;
        TxtHS.Text = Rupiah.Format(barang.Harga);
        TxtJ.Focus();
    }

    // Jumlah: tekan Enter untuk menghitung total harga
    private async void TxtJ_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;

        if (_barangDipilih == null)
        {
            TxtKB.Focus();
            return;
        }

        if (!int.TryParse(TxtJ.Text, out int jumlah) || jumlah <= 0)
        {
            await Pesan.Tampilkan(this, "Info Jumlah", "Jumlah harus berupa angka lebih dari 0.");
            TxtJ.Focus();
            return;
        }

        TxtTH.Text = Rupiah.Format(_barangDipilih.Harga * jumlah);
        BtnAdd.Focus();
    }

    // Tombol Add: masukkan barang ke daftar transaksi
    private async void BtnAdd_Click(object? sender, RoutedEventArgs e)
    {
        if (_barangDipilih == null || !int.TryParse(TxtJ.Text, out int jumlah) || jumlah <= 0)
        {
            await Pesan.Tampilkan(this, "Info", "Isi kode barang dan jumlah dulu (tekan Enter di tiap kolom).");
            TxtKB.Focus();
            return;
        }

        ItemTransaksi item = _transaksi.Tambah(_barangDipilih, jumlah);
        TambahBaris(item);
        Segarkan();
        BersihkanInput();
    }

    // Membuat satu baris tabel untuk satu item
    private void TambahBaris(ItemTransaksi item)
    {
        Grid baris = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("50,110,*,110,70,120"),
            Background = item.No % 2 == 0 ? new SolidColorBrush(Color.Parse("#F4F7FB")) : Brushes.White
        };

        baris.Children.Add(Sel(item.No.ToString(), 0, HorizontalAlignment.Center));
        baris.Children.Add(Sel(item.Barang.Kode, 1, HorizontalAlignment.Left));
        baris.Children.Add(Sel(item.Barang.Nama, 2, HorizontalAlignment.Left));
        baris.Children.Add(Sel(Rupiah.Format(item.Barang.Harga), 3, HorizontalAlignment.Right));
        baris.Children.Add(Sel(item.Jumlah.ToString(), 4, HorizontalAlignment.Right));
        baris.Children.Add(Sel(Rupiah.Format(item.TotalHarga), 5, HorizontalAlignment.Right));

        PanelItem.Children.Add(baris);
    }

    private static TextBlock Sel(string teks, int kolom, HorizontalAlignment rata)
    {
        TextBlock tb = new TextBlock
        {
            Text = teks,
            Margin = new Thickness(8, 6),
            HorizontalAlignment = rata
        };
        Grid.SetColumn(tb, kolom);
        return tb;
    }

    private void BersihkanInput()
    {
        _barangDipilih = null;
        TxtKB.Text = "";
        TxtNB.Text = "";
        TxtHS.Text = "";
        TxtJ.Text = "";
        TxtTH.Text = "";
        TxtKB.Focus();
    }

    // Bayar: tekan Enter untuk menghitung kembalian
    private async void TxtB_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) await Proses();
    }

    private async void BtnProses_Click(object? sender, RoutedEventArgs e)
    {
        await Proses();
    }

    private async Task Proses()
    {
        if (_transaksi.JumlahItem == 0)
        {
            await Pesan.Tampilkan(this, "Info", "Belum ada barang yang dibeli.");
            TxtKB.Focus();
            return;
        }

        if (!int.TryParse(TxtB.Text, out int bayar))
        {
            await Pesan.Tampilkan(this, "Info Pembayaran", "Isi uang bayar dengan angka.");
            TxtB.Focus();
            return;
        }

        int kembali = _transaksi.HitungKembali(bayar);
        if (kembali < 0)
        {
            await Pesan.Tampilkan(this, "Info Pembayaran", "Uang bayar kurang " + Rupiah.Format(-kembali));
            TxtB.Focus();
            return;
        }

        TxtK.Text = Rupiah.Format(kembali);
        BtnNew.Focus();
    }

    // Tombol New: bersihkan semua untuk transaksi baru
    private void BtnNew_Click(object? sender, RoutedEventArgs e)
    {
        _transaksi.Reset();
        PanelItem.Children.Clear();
        TxtB.Text = "";
        TxtK.Text = "";
        Segarkan();
        BersihkanInput();
    }
}
