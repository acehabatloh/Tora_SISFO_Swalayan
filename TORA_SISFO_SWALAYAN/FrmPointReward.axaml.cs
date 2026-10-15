using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace TORA_SISFO_SWALAYAN;

public partial class FrmPointReward : Window
{
    public FrmPointReward()
    {
        InitializeComponent();
    }

    // Tekan Enter = sama seperti klik tombol Cek Hadiah
    private void TxtPoint_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) BtnCekHadiah_Click(sender, e);
    }

    private async void BtnCekHadiah_Click(object? sender, RoutedEventArgs e)
    {
        LblTeks.Text = "";
        LblHadiah.Text = "";

        if (!int.TryParse(TxtPoint.Text, out int point))
        {
            await Pesan.Tampilkan(this, "Warning", "Isi point dengan angka.");
            TxtPoint.Focus();
            return;
        }

        if (point < 0)
        {
            await Pesan.Tampilkan(this, "Warning",
                "Point yang anda masukkan salah.\nIsilah Point dengan angka 0 (nol) keatas.");
            TxtPoint.Focus();
            return;
        }

        var hasil = HadiahPoint.Cek(point);
        LblTeks.Text = hasil.Teks;
        LblHadiah.Text = hasil.Hadiah;
        TxtPoint.Focus();
    }
}
