namespace GastoApp.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Clave { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public string Iniciales =>
        Nombre.Length >= 2 ? Nombre[..2].ToUpper() : Nombre.ToUpper();
}
