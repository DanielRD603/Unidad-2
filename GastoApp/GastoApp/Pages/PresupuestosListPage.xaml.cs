using GastoApp.Models;
using GastoApp.Services;

namespace GastoApp.Pages;

public partial class PresupuestosListPage : ContentPage
{
    public PresupuestosListPage()
    {
        InitializeComponent();
        Lista.ItemsSource = PresupuestoService.Instancia.Presupuestos;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LblConteo.Text = $"{PresupuestoService.Instancia.Presupuestos.Count} presupuesto(s)";
    }

    private async void OnAgregarClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(PresupuestoFormPage));

    private async void OnSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Presupuesto p)
            await Shell.Current.GoToAsync($"{nameof(PresupuestoFormPage)}?PresupuestoId={p.Id}");
        ((CollectionView)sender).SelectedItem = null;
    }
}
