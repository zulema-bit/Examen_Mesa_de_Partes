using System.Text.Json;

namespace Mesa_de_partes;

public sealed class AlmacenExpedientes
{
    private readonly string ruta;
    public List<Expediente> Expedientes { get; }

    public AlmacenExpedientes(string ruta)
    {
        this.ruta = ruta;
        if (!File.Exists(ruta)) { Expedientes = []; return; }
        try { Expedientes = JsonSerializer.Deserialize<List<Expediente>>(File.ReadAllText(ruta)) ?? []; }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            throw new InvalidDataException($"No se pudo leer {ruta}: {ex.Message}", ex);
        }
    }

    public Expediente? Buscar(string codigo)
    {
        var entrada = codigo.Trim();
        if (int.TryParse(entrada, out var numero))
            return Expedientes.Find(x => x.Numero == numero);
        return Expedientes.Find(x =>
            string.Equals(x.Codigo, entrada, StringComparison.OrdinalIgnoreCase));
    }

    public int SiguienteNumero() => Expedientes.Select(x => x.Numero).DefaultIfEmpty(1000).Max() + 1;

    public bool Guardar()
    {
        try
        {
            var temporal = ruta + ".tmp";
            File.WriteAllText(temporal, JsonSerializer.Serialize(Expedientes,
                new JsonSerializerOptions { WriteIndented = true }));
            File.Move(temporal, ruta, true);
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Console.WriteLine($"Error al guardar: {ex.Message}");
            return false;
        }
    }
}
