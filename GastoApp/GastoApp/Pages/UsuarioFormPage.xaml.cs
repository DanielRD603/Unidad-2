using GastoApp.Models;
using GastoApp.Services;

namespace GastoApp.Pages;

[QueryProperty(nameof(UsuarioId), "UsuarioId")]
public partial class UsuarioFormPage : ContentPage
{
    private int _usuarioId;
    private Usuario? _enEdicion;

    public int UsuarioId
    {
        get => _usuarioId;
        set { _usuarioId = value; if (_usuarioId > 0) CargarUsuario(_usuarioId); }
    }

    public UsuarioFormPage() => InitializeComponent();

    private void CargarUsuario(int id)
    {
        var u = UsuarioService.Instancia.ObtenerPorId(id);
        if (u is null) return;
        _enEdicion = u;
        Title = "Editar Usuario";
        BtnEliminar.IsVisible = true;
        EntryNombre.Text = u.Nombre;
        EntryCorreo.Text = u.Correo;
        EntryClave.Text  = u.Clave;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryNombre.Text))
        { await DisplayAlertAsync("Requerido", "El nombre es obligatorio.", "OK"); return; }

        if (string.IsNullOrWhiteSpace(EntryCorreo.Text))
        { await DisplayAlertAsync("Requerido", "El correo es obligatorio.", "OK"); return; }

        if (string.IsNullOrWhiteSpace(EntryClave.Text) || EntryClave.Text.Length < 4)
        { await DisplayAlertAsync("Contraseña inválida", "Mínimo 4 caracteres.", "OK"); return; }

        if (_enEdicion is null)
        {
            UsuarioService.Instancia.Agregar(new Usuario
            {
                Nombre = EntryNombre.Text.Trim(),
                Correo = EntryCorreo.Text.Trim(),
                Clave  = EntryClave.Text
            });
            await DisplayAlertAsync("✓ Guardado", "Usuario registrado.", "OK");
        }
        else
        {
            _enEdicion.Nombre = EntryNombre.Text.Trim();
            _enEdicion.Correo = EntryCorreo.Text.Trim();
            _enEdicion.Clave  = EntryClave.Text;
            await DisplayAlertAsync("✓ Actualizado", "Usuario actualizado.", "OK");
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        if (_enEdicion is null) return;
        bool ok = await DisplayAlertAsync("Eliminar", "¿Eliminar este usuario?", "Sí", "Cancelar");
        if (!ok) return;
        UsuarioService.Instancia.Eliminar(_enEdicion);
        await Shell.Current.GoToAsync("..");
    }
}
