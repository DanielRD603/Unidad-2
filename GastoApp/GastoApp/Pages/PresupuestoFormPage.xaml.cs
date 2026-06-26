using GastoApp.Models;
using GastoApp.Services;

namespace GastoApp.Pages;

[QueryProperty(nameof(PresupuestoId), "PresupuestoId")]
public partial class PresupuestoFormPage : ContentPage
{
    private int _presupuestoId;
    private Presupuesto? _enEdicion;

    public int PresupuestoId
    {
        get => _presupuestoId;
        set { _presupuestoId = value; if (_presupuestoId > 0) CargarPresupuesto(_presupuestoId); }
    }

    public PresupuestoFormPage()
    {
        InitializeComponent();
        PickerMes.SelectedItem = DateTime.Now.Month.ToString();
        EntryAño.Text         = DateTime.Now.Year.ToString();
    }

    private void CargarPresupuesto(int id)
    {
        var p = PresupuestoService.Instancia.ObtenerPorId(id);
        if (p is null) return;
        _enEdicion = p;
        Title = "Editar Presupuesto";
        BtnEliminar.IsVisible    = true;
        PickerCategoria.SelectedItem = p.Categoria;
        EntryMonto.Text          = p.MontoLimite.ToString("0.00");
        PickerMes.SelectedItem   = p.Mes.ToString();
        EntryAño.Text           = p.Año.ToString();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (PickerCategoria.SelectedItem is null)
        { await DisplayAlertAsync("Requerido", "Selecciona una categoría.", "OK"); return; }

        if (!decimal.TryParse(EntryMonto.Text, out decimal monto) || monto <= 0)
        { await DisplayAlertAsync("Monto inválido", "Ingresa un monto mayor a cero.", "OK"); return; }

        if (!int.TryParse(EntryAño.Text, out int año) || año < 2024)
        { await DisplayAlertAsync("Año inválido", "Ingresa un año válido.", "OK"); return; }

        int mes = int.Parse(PickerMes.SelectedItem?.ToString() ?? "1");

        if (_enEdicion is null)
        {
            PresupuestoService.Instancia.Agregar(new Presupuesto
            {
                Categoria   = PickerCategoria.SelectedItem.ToString()!,
                MontoLimite = monto,
                Mes         = mes,
                Año        = año,
                Usuario     = "admin"
            });
            await DisplayAlertAsync("✓ Guardado", "Presupuesto registrado.", "OK");
        }
        else
        {
            _enEdicion.Categoria   = PickerCategoria.SelectedItem.ToString()!;
            _enEdicion.MontoLimite = monto;
            _enEdicion.Mes         = mes;
            _enEdicion.Año        = año;
            await DisplayAlertAsync("✓ Actualizado", "Presupuesto actualizado.", "OK");
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        if (_enEdicion is null) return;
        bool ok = await DisplayAlertAsync("Eliminar", "¿Eliminar este presupuesto?", "Sí", "Cancelar");
        if (!ok) return;
        PresupuestoService.Instancia.Eliminar(_enEdicion);
        await Shell.Current.GoToAsync("..");
    }
}
