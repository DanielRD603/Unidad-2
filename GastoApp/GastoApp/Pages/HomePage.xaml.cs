namespace GastoApp.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnNuevoGastoClicked(object sender, EventArgs e)
    {
        
        await Shell.Current.GoToAsync(nameof(GastoFormPage));
    }

    private async void OnVerTodosClicked(object sender, EventArgs e)
    {
        
        await Shell.Current.GoToAsync("//gastos");
    }
}
