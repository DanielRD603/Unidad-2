using GastoApp.Models;
using GastoApp.Services;

namespace GastoApp.Pages;

[QueryProperty(nameof(GastoId), "GastoId")]
public partial class GastoFormPage : ContentPage
{
    private int _gastoId;
    private Gasto? _enEdicion;

    public int GastoId
    {
        get => _gastoId;
        set { _gastoId = value; if (_gastoId > 0) CargarGasto(_gastoId); }
    }

    public GastoFormPage()
    {
        InitializeComponent();
        DatePickerFecha.Date = DateTime.Now;
        TimePickerHora.Time  = DateTime.Now.TimeOfDay;
    }

    private void CargarGasto(int id)
    {
        var g = GastoService.Instancia.ObtenerPorId(id);
        if (g is null) return;
        _enEdicion = g;
        Title = "Editar Gasto";
        BtnEliminar.IsVisible = true;
        EntryDescripcion.Text = g.Descripcion;
        EntryMonto.Text       = g.Monto.ToString("0.00");
        DatePickerFecha.Date  = g.Fecha.Date;
        TimePickerHora.Time   = g.Fecha.TimeOfDay;
        EditorNotas.Text      = g.Notas;
        PickerCategoria.SelectedItem = g.Categoria;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryDescripcion.Text))
        { await DisplayAlertAsync("Requerido", "La descripción es obligatoria.", "OK"); return; }

        if (!decimal.TryParse(EntryMonto.Text, out decimal monto) || monto <= 0)
        { await DisplayAlertAsync("Monto inválido", "Ingresa un monto mayor a cero.", "OK"); return; }

        if (PickerCategoria.SelectedItem is null)
        { await DisplayAlertAsync("Requerido", "Selecciona una categoría.", "OK"); return; }

        DateTime fecha = (DatePickerFecha.Date ?? DateTime.Now)
                         .Add(TimePickerHora.Time ?? DateTime.Now.TimeOfDay);

        if (_enEdicion is null)
        {
            GastoService.Instancia.Agregar(new Gasto
            {
                Descripcion = EntryDescripcion.Text.Trim(),
                Monto       = monto,
                Fecha       = fecha,
                Categoria   = PickerCategoria.SelectedItem.ToString()!,
                Notas       = EditorNotas.Text,
                Usuario     = "admin"
            });
            await DisplayAlertAsync("✓ Guardado", "Gasto registrado.", "OK");
        }
        else
        {
            _enEdicion.Descripcion = EntryDescripcion.Text.Trim();
            _enEdicion.Monto       = monto;
            _enEdicion.Fecha       = fecha;
            _enEdicion.Categoria   = PickerCategoria.SelectedItem.ToString()!;
            _enEdicion.Notas       = EditorNotas.Text;
            await DisplayAlertAsync("✓ Actualizado", "Gasto actualizado.", "OK");
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        if (_enEdicion is null) return;
        bool ok = await DisplayAlertAsync("Eliminar", "¿Eliminar este gasto?", "Sí", "Cancelar");
        if (!ok) return;
        GastoService.Instancia.Eliminar(_enEdicion);
        await Shell.Current.GoToAsync("..");
    }
}
