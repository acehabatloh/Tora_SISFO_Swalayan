using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Styling;

namespace TORA_SISFO_SWALAYAN;

public static class Pesan
{
    public static async Task Tampilkan(Window pemilik, string judul, string isi)
    {
        Button tombol = new Button
        {
            Content = "OK",
            Width = 90,
            HorizontalAlignment = HorizontalAlignment.Right,
            HorizontalContentAlignment = HorizontalAlignment.Center
        };

        Window dialog = new Window
        {
            Title = judul,
            Width = 420,
            SizeToContent = SizeToContent.Height,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            RequestedThemeVariant = ThemeVariant.Light,
            Content = new StackPanel
            {
                Margin = new Thickness(20),
                Spacing = 18,
                Children =
                {
                    new TextBlock { Text = isi, TextWrapping = Avalonia.Media.TextWrapping.Wrap },
                    tombol
                }
            }
        };

        tombol.Click += (s, e) => dialog.Close();
        await dialog.ShowDialog(pemilik);
    }
}