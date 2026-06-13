namespace GastoApp.Pages;

public partial class GastosListPage : ContentPage
{
    public GastosListPage()
    {
        InitializeComponent();
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
       
        await Shell.Current.GoToAsync(nameof(GastoFormPage));
    }

    private async void OnGastoSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        
        await Shell.Current.GoToAsync(nameof(GastoFormPage));

        
        ((CollectionView)sender).SelectedItem = null;
    }

    private void OnBuscar(object sender, EventArgs e)
    {
      
    }

    private void OnBuscarTextChanged(object sender, TextChangedEventArgs e)
    {
    }

    private void OnFiltroClicked(object sender, EventArgs e)
    {
        
    }
}
