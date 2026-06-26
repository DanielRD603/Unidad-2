using GastoApp.Models;
using GastoApp.Services;

namespace GastoApp.Pages;

public partial class GastosListPage : ContentPage
{
    public GastosListPage()
    {
        InitializeComponent();
        Lista.ItemsSource = GastoService.Instancia.Gastos;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        int n = GastoService.Instancia.Gastos.Count;
        decimal total = GastoService.Instancia.Total();
        LblConteo.Text = $"{n} registro(s) · Total: RD$ {total:N2}";
    }

    private async void OnAgregarClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(GastoFormPage));

    private async void OnSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Gasto g)
            await Shell.Current.GoToAsync($"{nameof(GastoFormPage)}?GastoId={g.Id}");
        ((CollectionView)sender).SelectedItem = null;
    }
}
