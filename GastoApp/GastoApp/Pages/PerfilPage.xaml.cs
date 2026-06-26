namespace GastoApp.Pages;

public partial class PerfilPage : ContentPage
{
    public PerfilPage() => InitializeComponent();

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        bool ok = await DisplayAlertAsync("Cerrar sesión",
            "¿Deseas cerrar tu sesión?", "Sí", "Cancelar");
        if (ok)
            Application.Current!.Windows[0].Page = new NavigationPage(new LoginPage());
    }
}