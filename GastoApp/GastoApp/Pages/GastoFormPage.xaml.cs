namespace GastoApp.Pages;


[QueryProperty(nameof(GastoId), "GastoId")]
public partial class GastoFormPage : ContentPage
{
  
    private int _gastoId;
    public int GastoId
    {
        get => _gastoId;
        set
        {
            _gastoId = value;
            
            if (_gastoId > 0)
            {
                Title = "Editar Gasto";
                BtnEliminar.IsVisible = true;
               
            }
        }
    }

    
    private string _categoriaSeleccionada = "Comida";

    public GastoFormPage()
    {
        InitializeComponent();
    }

    
    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        
        if (string.IsNullOrWhiteSpace(EntryDescripcion.Text))
        {
            await DisplayAlert("Campo requerido",
                "Debes ingresar una descripción.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(EntryMonto.Text))
        {
            await DisplayAlert("Campo requerido",
                "Debes ingresar el monto.", "OK");
            return;
        }

      

        await DisplayAlert("Guardado ✓",
            "Gasto registrado correctamente.", "OK");
        await Shell.Current.GoToAsync("..");
    }

  
    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Eliminar gasto",
            "¿Estás seguro de que deseas eliminar este gasto?",
            "Sí, eliminar", "Cancelar");

        if (!confirmar) return;

        // TODO Unidad 2: llamar al servicio de eliminación
        await DisplayAlert("Eliminado", "El gasto fue eliminado.", "OK");
        await Shell.Current.GoToAsync("..");
    }

 
    private void OnCategoriaSeleccionada(object sender, TappedEventArgs e)
    {
        _categoriaSeleccionada = e.Parameter?.ToString() ?? "Otro";
        ActualizarChips();
    }

    private void ActualizarChips()
    {
        
        var chips = new Dictionary<string, Frame>
        {
            { "Comida",     ChipComida     },
            { "Transporte", ChipTransporte },
            { "Ocio",       ChipOcio       },
            { "Servicios",  ChipServicios  },
            { "Salud",      ChipSalud      },
            { "Otro",       ChipOtro       }
        };

        foreach (var kv in chips)
        {
            bool activo = kv.Key == _categoriaSeleccionada;
            kv.Value.BackgroundColor = activo
                ? Color.FromArgb("#E1F5EE")   
                : Color.FromArgb("#FFFFFF");   
            kv.Value.BorderColor = activo
                ? Color.FromArgb("#1D9E75")  
                : Color.FromArgb("#D3D1C7");   
           
            var label = (kv.Value.Content as VerticalStackLayout)?
                            .Children.OfType<Label>()
                            .LastOrDefault();
            if (label is not null)
            {
                label.TextColor = activo
                    ? Color.FromArgb("#1D9E75")
                    : Color.FromArgb("#5F5E5A");
                label.FontAttributes = activo
                    ? FontAttributes.Bold
                    : FontAttributes.None;
            }
        }
    }
}
