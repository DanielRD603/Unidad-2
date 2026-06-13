namespace GastoApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        
        Routing.RegisterRoute(nameof(Pages.GastoFormPage), typeof(Pages.GastoFormPage));
    }
}
