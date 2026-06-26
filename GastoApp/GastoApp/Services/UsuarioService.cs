using System.Collections.ObjectModel;
using GastoApp.Models;

namespace GastoApp.Services;

public class UsuarioService
{
    private static UsuarioService? _instancia;
    public static UsuarioService Instancia => _instancia ??= new UsuarioService();

    public ObservableCollection<Usuario> Usuarios { get; } = new();
    private int _siguienteId = 1;

    
    public Usuario? UsuarioActual { get; set; }

    private UsuarioService()
    {
        Agregar(new Usuario { Nombre = "Carlos García", Correo = "admin", Clave = "1234" });
        Agregar(new Usuario { Nombre = "María López",   Correo = "maria@correo.com", Clave = "5678" });
    }

    public void Agregar(Usuario u)  { u.Id = _siguienteId++; Usuarios.Add(u); }
    public void Eliminar(Usuario u) { Usuarios.Remove(u); }
    public Usuario? ObtenerPorId(int id) => Usuarios.FirstOrDefault(u => u.Id == id);
    public Usuario? ValidarLogin(string correo, string clave) =>
        Usuarios.FirstOrDefault(u => u.Correo == correo && u.Clave == clave);
}
