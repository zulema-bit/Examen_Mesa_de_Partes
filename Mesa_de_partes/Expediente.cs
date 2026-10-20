namespace Mesa_de_partes;

public class Expediente
{
    public int Numero { get; set; }
    public string Codigo { get; set; } = "";
    public string DniRuc { get; set; } = "";
    public string Solicitante { get; set; } = "";
    public string TipoDocumento { get; set; } = "";
    public string Asunto { get; set; } = "";
    public DateTime FechaRegistro { get; set; }
    public string Estado { get; set; } = "Pendiente de derivacion";
    public string Area { get; set; } = "Sin derivar";
    public string? RutaArchivo { get; set; }
    public string? ObservacionArchivo { get; set; }
    public List<string> Historial { get; set; } = [];
}
