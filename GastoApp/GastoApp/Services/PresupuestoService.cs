using System.Collections.ObjectModel;
using GastoApp.Models;

namespace GastoApp.Services;

public class PresupuestoService
{
    private static PresupuestoService? _instancia;
    public static PresupuestoService Instancia => _instancia ??= new PresupuestoService();

    public ObservableCollection<Presupuesto> Presupuestos { get; } = new();
    private int _siguienteId = 1;

    private PresupuestoService()
    {
        Agregar(new Presupuesto { Mes = DateTime.Now.Month, Año = DateTime.Now.Year, MontoLimite = 25000, Categoria = "General",    Usuario = "admin" });
        Agregar(new Presupuesto { Mes = DateTime.Now.Month, Año = DateTime.Now.Year, MontoLimite = 8000,  Categoria = "Comida",     Usuario = "admin" });
        Agregar(new Presupuesto { Mes = DateTime.Now.Month, Año = DateTime.Now.Year, MontoLimite = 5000,  Categoria = "Transporte", Usuario = "admin" });
    }

    public void Agregar(Presupuesto p)  { p.Id = _siguienteId++; Presupuestos.Add(p); }
    public void Eliminar(Presupuesto p) { Presupuestos.Remove(p); }
    public Presupuesto? ObtenerPorId(int id) => Presupuestos.FirstOrDefault(p => p.Id == id);
}
