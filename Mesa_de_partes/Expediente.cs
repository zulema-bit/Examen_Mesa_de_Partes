namespace Mesa_de_partes;

public class Movimiento
{
    public DateTime Fecha { get; set; }
    public string Area { get; set; } = "";
    public string Descripcion { get; set; } = "";
}

public class Expediente
{
    public string Numero { get; set; } = "";
    public string Dni { get; set; } = "";
    public string Solicitante { get; set; } = "";
    public string Estado { get; set; } = "";
    public string AreaActual { get; set; } = "";
    public List<Movimiento> Historial { get; set; } = new();
}

public static class RepositorioExpedientes
{
    private static readonly List<Expediente> _expedientes = new()
    {
        new Expediente
        {
            Numero = "EXP-001",
            Dni = "12345678",
            Solicitante = "Juan Perez",
            Estado = "En tramite",
            AreaActual = "Gerencia de Administracion",
            Historial = new List<Movimiento>
            {
                new Movimiento { Fecha = DateTime.Now.AddDays(-2), Area = "Mesa de Partes", Descripcion = "Expediente registrado y digitalizado" },
                new Movimiento { Fecha = DateTime.Now.AddDays(-1), Area = "Gerencia de Administracion", Descripcion = "Derivado para evaluacion" }
            }
        },
        new Expediente
        {
            Numero = "EXP-002",
            Dni = "87654321",
            Solicitante = "Maria Lopez",
            Estado = "Atendido",
            AreaActual = "Archivo Central",
            Historial = new List<Movimiento>
            {
                new Movimiento { Fecha = DateTime.Now.AddDays(-3), Area = "Mesa de Partes", Descripcion = "Ingreso de documento" },
                new Movimiento { Fecha = DateTime.Now, Area = "Archivo Central", Descripcion = "Resolucion emitida y archivada" }
            }
        }
    };

    public static int Cantidad() => _expedientes.Count;

    public static Expediente? Buscar(string numero)
    {
        return _expedientes.FirstOrDefault(e => e.Numero.Equals(numero, StringComparison.OrdinalIgnoreCase));
    }

    public static void Agregar(Expediente expediente)
    {
        _expedientes.Add(expediente);
    }
}