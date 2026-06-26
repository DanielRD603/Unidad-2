using GastoApp.Models;
using GastoApp.Services;

namespace GastoApp.Pages;

public partial class UsuariosListPage : ContentPage
{
    public UsuariosListPage()
    {
        InitializeComponent();
        Lista.ItemsSource = UsuarioService.Instancia.Usuarios;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LblConteo.Text = $"{UsuarioService.Instancia.Usuarios.Count} usuario(s)";
    }

    private async void OnAgregarClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(UsuarioFormPage));

    private async void OnSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Usuario u)
            await Shell.Current.GoToAsync($"{nameof(UsuarioFormPage)}?UsuarioId={u.Id}");
        ((CollectionView)sender).SelectedItem = null;
    }
}
