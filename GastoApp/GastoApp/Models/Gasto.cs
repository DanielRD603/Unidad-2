namespace GastoApp.Models;


public class Gasto
{
    public int Id { get; set; }

    
    public string Descripcion { get; set; } = string.Empty;

   
    public decimal Monto { get; set; }

   
    public DateTime Fecha { get; set; } = DateTime.Now;

   
    public string Categoria { get; set; } = "Otro";


    public string? Notas { get; set; }

  
    public string Usuario { get; set; } = string.Empty;
}
