using GastoApp.Services;

namespace GastoApp.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var usuario = UsuarioService.Instancia.UsuarioActual;
        int hora = DateTime.Now.Hour;
        string saludo = hora < 12 ? "Buenos días" : hora < 18 ? "Buenas tardes" : "Buenas noches";
        LblSaludo.Text = $"{saludo}, {usuario?.Nombre?.Split(' ')[0] ?? "Usuario"} 👋";
        LblFecha.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy");

        decimal total = GastoService.Instancia.Total();
        LblTotalMes.Text = $"RD$ {total:N2}";

        var presupuestoGeneral = PresupuestoService.Instancia.Presupuestos
            .FirstOrDefault(p => p.Categoria == "General");
        decimal limite = presupuestoGeneral?.MontoLimite ?? 0;
        LblPresupuesto.Text = $"RD$ {limite:N2}";
        LblDisponible.Text  = $"RD$ {Math.Max(0, limite - total):N2}";

        LblCantGastos.Text     = GastoService.Instancia.Gastos.Count.ToString();
        LblCantCategorias.Text = CategoriaService.Instancia.Categorias.Count.ToString();

      
        StackRecientes.Children.Clear();
        var recientes = GastoService.Instancia.Gastos
            .OrderByDescending(g => g.Fecha).Take(3).ToList();

        foreach (var g in recientes)
        {
            var fila = new Grid { ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition(GridLength.Auto),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)),
                Padding = new Thickness(16, 12), ColumnSpacing = 12 };

            fila.Add(new Label { Text = "💳", FontSize = 20,
                VerticalOptions = LayoutOptions.Center }, 0, 0);

            var info = new VerticalStackLayout { VerticalOptions = LayoutOptions.Center, Spacing = 2 };
            info.Add(new Label { Text = g.Descripcion, FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#1A1A2E") });
            info.Add(new Label { Text = $"{g.Fecha:dd MMM} · {g.Categoria}",
                FontSize = 12, TextColor = Color.FromArgb("#9CA3AF") });
            fila.Add(info, 1, 0);

            fila.Add(new Label { Text = $"-RD$ {g.Monto:N0}",
                FontSize = 14, FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#EF4444"),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End }, 2, 0);

            StackRecientes.Children.Add(fila);

            if (recientes.Last() != g)
                StackRecientes.Children.Add(new BoxView
                    { HeightRequest = 1, Color = Color.FromArgb("#F3F4F6"),
                      Margin = new Thickness(16, 0) });
        }

        if (!recientes.Any())
            StackRecientes.Children.Add(new Label
            { Text = "No hay gastos aún",
              FontSize = 14,
              TextColor = Color.FromArgb("#9CA3AF"),
              HorizontalOptions = LayoutOptions.Center,
              Margin = new Thickness(0, 20) });
    }

    private async void OnNuevoGastoClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(GastoFormPage));

    private async void OnVerGastosClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("//gastos");
}
