using GastoApp.Services;

namespace GastoApp.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string correo = EntryCorreo.Text?.Trim() ?? string.Empty;
        string clave  = EntryPassword.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(clave))
        {
            await DisplayAlertAsync("Campos requeridos",
                "Debes ingresar usuario y contraseña.", "OK");
            return;
        }

        var usuario = UsuarioService.Instancia.ValidarLogin(correo, clave);

        if (usuario is not null)
        {
            UsuarioService.Instancia.UsuarioActual = usuario;
            Application.Current!.Windows[0].Page = new AppShell();
        }
        else
        {
            await DisplayAlertAsync("Acceso denegado",
                "Usuario o contraseña incorrectos.", "OK");
        }
    }
}
