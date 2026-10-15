namespace Mesa_de_partes;

public class DocumentoDigital
{
    public string Nombre { get; set; } = "";
    public string Ruta { get; set; } = "";
    public long TamanoKb { get; set; }
    public string TextoExtraido { get; set; } = "";
    public bool Validado { get; set; }
    public string Observacion { get; set; } = "";
}

public static class DigitalizacionOcr
{
    static readonly string[] ExtensionesPermitidas = { ".pdf", ".jpg", ".jpeg", ".png" };
    const long TamanoMaximoKb = 5120; // 5 MB
    static readonly List<DocumentoDigital> Digitalizados = new();

    public static void Ejecutar()
    {
        Console.Write("Ruta del documento a digitalizar: ");
        string ruta = Console.ReadLine() ?? "";

        var doc = Digitalizar(ruta);
        ExtraerTextoOcr(doc);
        Validar(doc);

        Digitalizados.Add(doc);
        Console.WriteLine($"Documento: {doc.Nombre}");
        Console.WriteLine($"Texto extraído: {doc.TextoExtraido}");
        Console.WriteLine($"Estado: {(doc.Validado ? "VÁLIDO" : "RECHAZADO")} - {doc.Observacion}");
    }

    static DocumentoDigital Digitalizar(string ruta)
    {
        var info = new FileInfo(ruta);
        return new DocumentoDigital
        {
            Ruta = ruta,
            Nombre = info.Name,
            TamanoKb = info.Exists ? info.Length / 1024 : 0
        };
    }

    static void ExtraerTextoOcr(DocumentoDigital doc)
    {
        // OCR simulado
        doc.TextoExtraido = $"Texto reconocido del archivo {doc.Nombre}";
    }

    static void Validar(DocumentoDigital doc)
    {
        string ext = Path.GetExtension(doc.Ruta).ToLower();

        if (!File.Exists(doc.Ruta))
            doc.Observacion = "El archivo no existe.";
        else if (!ExtensionesPermitidas.Contains(ext))
            doc.Observacion = "Formato no permitido.";
        else if (doc.TamanoKb > TamanoMaximoKb)
            doc.Observacion = "El archivo supera el tamaño máximo.";
        else if (string.IsNullOrWhiteSpace(doc.TextoExtraido))
            doc.Observacion = "No se pudo extraer texto.";
        else
        {
            doc.Validado = true;
            doc.Observacion = "Documento conforme.";
        }
    }
}