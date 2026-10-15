using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace TORA_SISFO_SWALAYAN;

public partial class FrmLogin : Window
{
    private int _percobaan;   // menghitung berapa kali salah

    public FrmLogin()
    {
        InitializeComponent();
    }

    // Enter di User ID -> pindah ke Password
    private void TxtID_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) TxtPas.Focus();
    }

    // Enter di Password -> sama seperti klik Proses
    private void TxtPas_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) BtnProses_Click(sender, e);
    }

    private async void BtnProses_Click(object? sender, RoutedEventArgs e)
    {
        // Ganti user dan password sesuai keinginanmu
        if (TxtID.Text == "tora" && TxtPas.Text == "12345")
        {
            MainWindow menu = new MainWindow();

            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = menu;
            }

            menu.Show();
            Close();
            return;
        }

        _percobaan++;
        TxtID.Text = "";
        TxtPas.Text = "";

        if (_percobaan >= 3)
        {
            await Pesan.Tampilkan(this, "Peringatan", "Salah 3 kali. Program akan ditutup.");
            Close();
            return;
        }

        await Pesan.Tampilkan(this, "Peringatan",
            "Data yang Anda masukkan salah.\nCoba cek username dan password Anda.\nKesempatan tersisa: " + (3 - _percobaan));
        TxtID.Focus();
    }
}
