namespace GastoApp.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Icono { get; set; } = "📦";
    public string Color { get; set; } = "#888780";
    public string Descripcion { get; set; } = string.Empty;
}
