using System.Collections.ObjectModel;
using GastoApp.Models;

namespace GastoApp.Services;

public class CategoriaService
{
    private static CategoriaService? _instancia;
    public static CategoriaService Instancia => _instancia ??= new CategoriaService();

    public ObservableCollection<Categoria> Categorias { get; } = new();
    private int _siguienteId = 1;

    private CategoriaService()
    {
        Agregar(new Categoria { Nombre = "Comida",     Icono = "🍽️", Color = "#854F0B", Descripcion = "Alimentos y restaurantes" });
        Agregar(new Categoria { Nombre = "Transporte", Icono = "⛽",  Color = "#185FA5", Descripcion = "Gasolina, transporte público" });
        Agregar(new Categoria { Nombre = "Ocio",       Icono = "🎬",  Color = "#993556", Descripcion = "Entretenimiento y diversión" });
        Agregar(new Categoria { Nombre = "Servicios",  Icono = "💡",  Color = "#3B6D11", Descripcion = "Luz, agua, internet" });
        Agregar(new Categoria { Nombre = "Salud",      Icono = "💊",  Color = "#085041", Descripcion = "Médicos y medicamentos" });
    }

    public void Agregar(Categoria c)  { c.Id = _siguienteId++; Categorias.Add(c); }
    public void Eliminar(Categoria c) { Categorias.Remove(c); }
    public Categoria? ObtenerPorId(int id) => Categorias.FirstOrDefault(c => c.Id == id);
}
