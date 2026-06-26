using System.Collections.ObjectModel;
using GastoApp.Models;

namespace GastoApp.Services;

public class GastoService
{
    private static GastoService? _instancia;
    public static GastoService Instancia => _instancia ??= new GastoService();

    public ObservableCollection<Gasto> Gastos { get; } = new();
    private int _siguienteId = 1;

    private GastoService()
    {
        Agregar(new Gasto { Descripcion = "Almuerzo",      Monto = 350,  Categoria = "Comida",     Usuario = "admin", Fecha = DateTime.Now });
        Agregar(new Gasto { Descripcion = "Gasolina",      Monto = 900,  Categoria = "Transporte", Usuario = "admin", Fecha = DateTime.Now.AddDays(-1) });
        Agregar(new Gasto { Descripcion = "Luz eléctrica", Monto = 2100, Categoria = "Servicios",  Usuario = "admin", Fecha = DateTime.Now.AddDays(-3) });
    }

    public void Agregar(Gasto g)  { g.Id = _siguienteId++; Gastos.Add(g); }
    public void Eliminar(Gasto g) { Gastos.Remove(g); }
    public Gasto? ObtenerPorId(int id) => Gastos.FirstOrDefault(g => g.Id == id);
    public decimal Total() => Gastos.Sum(g => g.Monto);
}
