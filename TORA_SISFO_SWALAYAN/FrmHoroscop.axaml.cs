using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TORA_SISFO_SWALAYAN;

public partial class FrmHoroscop : Window
{
    public FrmHoroscop()
    {
        InitializeComponent();

        // Mengisi angka pada ComboBox
        CboTgl.ItemsSource = Enumerable.Range(1, 31).ToList();
        CboBln.ItemsSource = Enumerable.Range(1, 12).ToList();
        CboThn.ItemsSource = Enumerable.Range(1930, 83).Reverse().ToList();   // 2012 ... 1930
    }

    private async void BtnProses_Click(object? sender, RoutedEventArgs e)
    {
        if (CboTgl.SelectedItem is not int tgl ||
            CboBln.SelectedItem is not int bln ||
            CboThn.SelectedItem is not int thn)
        {
            await Pesan.Tampilkan(this, "Info", "Pilih tanggal, bulan, dan tahun lahir dulu.");
            return;
        }

        if (tgl > DateTime.DaysInMonth(thn, bln))
        {
            await Pesan.Tampilkan(this, "Info", "Tanggal tidak valid, bulan itu tidak punya tanggal " + tgl + ".");
            return;
        }

        var zodiak = Zodiak.Cari(tgl, bln);

        LblSalam.Text = string.IsNullOrWhiteSpace(TxtNama.Text) ? "" : "Halo, " + TxtNama.Text.Trim() + "!";
        LblHariLahir.Text = Hari.Nama(new DateTime(thn, bln, tgl));
        LblZodiac.Text = zodiak.Nama;
        LblBatu.Text = zodiak.Batu;
    }
}
