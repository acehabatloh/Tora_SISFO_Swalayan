using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TORA_SISFO_SWALAYAN;

public partial class MainWindow : Window
{
    // Mencatat jendela yang sedang terbuka supaya tidak terbuka dobel
    private readonly Dictionary<Type, Window> _terbuka = new Dictionary<Type, Window>();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Buka<T>(string nama) where T : Window, new()
    {
        if (_terbuka.TryGetValue(typeof(T), out Window? sudahAda))
        {
            sudahAda.Activate();
            return;
        }

        T baru = new T();
        _terbuka[typeof(T)] = baru;
        baru.Closed += (s, e) => _terbuka.Remove(typeof(T));
        baru.Show(this);
        TxtStatus.Text = "Menu " + nama + " dibuka.";
    }

    private void MenuTransaksi_Click(object? sender, RoutedEventArgs e) => Buka<FrmTransaksi>("Transaksi");
    private void MenuPointReward_Click(object? sender, RoutedEventArgs e) => Buka<FrmPointReward>("Hadiah Point Reward");
    private void MenuHoroscop_Click(object? sender, RoutedEventArgs e) => Buka<FrmHoroscop>("Horoscop");

    private async void MenuDataBarang_Click(object? sender, RoutedEventArgs e)
    {
        DaftarBarang daftar = new DaftarBarang();
        string isi = "";
        foreach (Barang b in daftar.Semua)
        {
            isi += b.Kode + "  -  " + b.Nama + "  -  " + Rupiah.Format(b.Harga) + "\n";
        }
        await Pesan.Tampilkan(this, "Data Barang", isi.TrimEnd());
    }

    private async void MenuCaraJalan_Click(object? sender, RoutedEventArgs e)
    {
        await Pesan.Tampilkan(this, "Menjalankan Program",
            "1. Login dengan user ID dan password.\n" +
            "2. File > Transaksi: ketik kode barang lalu Enter, isi jumlah lalu Enter, klik Add.\n" +
            "3. Isi uang bayar lalu Enter untuk melihat kembalian. Klik New untuk transaksi baru.\n" +
            "4. Info > Hadiah Point Reward: cek hadiah dari point.\n" +
            "5. Permainan > Horoscop: cek zodiac dari tanggal lahir.");
    }

    private async void MenuTentang_Click(object? sender, RoutedEventArgs e)
    {
        await Pesan.Tampilkan(this, "Tentang Program",
            "Sistem Informasi Swalayan\nSwalayan Kita Semua\n\nDibuat dengan C#, .NET, dan Avalonia.");
    }

    private void MenuKeluar_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
