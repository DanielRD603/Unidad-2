using GastoApp.Models;
using GastoApp.Services;

namespace GastoApp.Pages;

public partial class CategoriasListPage : ContentPage
{
    public CategoriasListPage()
    {
        InitializeComponent();
        Lista.ItemsSource = CategoriaService.Instancia.Categorias;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LblConteo.Text = $"{CategoriaService.Instancia.Categorias.Count} categoría(s)";
    }

    private async void OnAgregarClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(CategoriaFormPage));

    private async void OnSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Categoria c)
            await Shell.Current.GoToAsync($"{nameof(CategoriaFormPage)}?CategoriaId={c.Id}");
        ((CollectionView)sender).SelectedItem = null;
    }
}
