namespace GastoApp.Pages;
public partial class PerfilPage : ContentPage
{
    public PerfilPage() => InitializeComponent();

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        bool ok = await DisplayAlert("Cerrar sesión",
            "¿Deseas cerrar tu sesión?", "Sí", "Cancelar");
        if (ok)
            Application.Current!.MainPage = new NavigationPage(new LoginPage());
    }
}
