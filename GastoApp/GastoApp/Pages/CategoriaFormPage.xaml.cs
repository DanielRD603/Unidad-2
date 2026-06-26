using GastoApp.Models;
using GastoApp.Services;

namespace GastoApp.Pages;

[QueryProperty(nameof(CategoriaId), "CategoriaId")]
public partial class CategoriaFormPage : ContentPage
{
    private int _categoriaId;
    private Categoria? _enEdicion;

    public int CategoriaId
    {
        get => _categoriaId;
        set { _categoriaId = value; if (_categoriaId > 0) CargarCategoria(_categoriaId); }
    }

    public CategoriaFormPage() => InitializeComponent();

    private void CargarCategoria(int id)
    {
        var c = CategoriaService.Instancia.ObtenerPorId(id);
        if (c is null) return;
        _enEdicion = c;
        Title = "Editar Categoría";
        BtnEliminar.IsVisible = true;
        EntryNombre.Text      = c.Nombre;
        EntryIcono.Text       = c.Icono;
        EntryColor.Text       = c.Color;
        EditorDescripcion.Text = c.Descripcion;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryNombre.Text))
        { await DisplayAlertAsync("Requerido", "El nombre es obligatorio.", "OK"); return; }

        if (string.IsNullOrWhiteSpace(EntryIcono.Text))
        { await DisplayAlertAsync("Requerido", "El ícono es obligatorio.", "OK"); return; }

        if (_enEdicion is null)
        {
            CategoriaService.Instancia.Agregar(new Categoria
            {
                Nombre      = EntryNombre.Text.Trim(),
                Icono       = EntryIcono.Text.Trim(),
                Color       = EntryColor.Text?.Trim() ?? "#888780",
                Descripcion = EditorDescripcion.Text ?? string.Empty
            });
            await DisplayAlertAsync("✓ Guardado", "Categoría registrada.", "OK");
        }
        else
        {
            _enEdicion.Nombre      = EntryNombre.Text.Trim();
            _enEdicion.Icono       = EntryIcono.Text.Trim();
            _enEdicion.Color       = EntryColor.Text?.Trim() ?? "#888780";
            _enEdicion.Descripcion = EditorDescripcion.Text ?? string.Empty;
            await DisplayAlertAsync("✓ Actualizado", "Categoría actualizada.", "OK");
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        if (_enEdicion is null) return;
        bool ok = await DisplayAlertAsync("Eliminar", "¿Eliminar esta categoría?", "Sí", "Cancelar");
        if (!ok) return;
        CategoriaService.Instancia.Eliminar(_enEdicion);
        await Shell.Current.GoToAsync("..");
    }
}
