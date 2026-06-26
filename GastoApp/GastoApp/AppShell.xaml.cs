namespace GastoApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        
        Routing.RegisterRoute(nameof(Pages.GastoFormPage),       typeof(Pages.GastoFormPage));
        Routing.RegisterRoute(nameof(Pages.CategoriaFormPage),   typeof(Pages.CategoriaFormPage));
        Routing.RegisterRoute(nameof(Pages.PresupuestoFormPage), typeof(Pages.PresupuestoFormPage));
        Routing.RegisterRoute(nameof(Pages.UsuarioFormPage),     typeof(Pages.UsuarioFormPage));

        
        var usuario = Services.UsuarioService.Instancia.UsuarioActual;
        if (usuario is not null)
        {
            LblNombreUsuario.Text = usuario.Nombre;
            LblCorreoUsuario.Text = usuario.Correo;
            LblIniciales.Text     = usuario.Iniciales;
        }
    }

    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        bool ok = await DisplayAlertAsync("Cerrar sesión",
            "¿Deseas cerrar tu sesión?", "Sí", "Cancelar");
        if (!ok) return;

        Services.UsuarioService.Instancia.UsuarioActual = null;
        Application.Current!.Windows[0].Page =
            new NavigationPage(new Pages.LoginPage());
    }
}
