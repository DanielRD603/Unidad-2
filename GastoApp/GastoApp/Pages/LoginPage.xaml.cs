namespace GastoApp.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

   
    private async void OnLoginClicked(object sender, EventArgs e)
    {
      
        Application.Current!.MainPage = new AppShell();
    }

    private async void OnOlvidéClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Recuperar contraseña",
            "Función disponible en la próxima unidad.", "OK");
    }
}
