namespace GastoApp.Models;

public class Presupuesto
{
    public int Id { get; set; }
    public int Mes { get; set; } = DateTime.Now.Month;
    public int Año { get; set; } = DateTime.Now.Year;
    public decimal MontoLimite { get; set; }
    public string Categoria { get; set; } = "General";
    public string Usuario { get; set; } = string.Empty;

    public string MesNombre =>
        new DateTime(Año, Mes, 1).ToString("MMMM yyyy");
}
