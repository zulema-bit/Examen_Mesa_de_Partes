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
        int opcion;
        do
        {
            Console.WriteLine("\n=== DIGITALIZACIÓN Y OCR ===");
            Console.WriteLine("1. Digitalizar documento");
            Console.WriteLine("2. Ver documentos digitalizados");
            Console.WriteLine("0. Volver");
            Console.Write("Elige una opción: ");
            int.TryParse(Console.ReadLine(), out opcion);

            switch (opcion)
            {
                case 1: DigitalizarDocumento(); break;
                case 2: ListarDigitalizados(); break;
                case 0: break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        } while (opcion != 0);
    }

    static void DigitalizarDocumento()
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

    static void ListarDigitalizados()
    {
        if (Digitalizados.Count == 0)
        {
            Console.WriteLine("Aún no hay documentos digitalizados.");
            return;
        }

        Console.WriteLine("\n--- Documentos digitalizados ---");
        foreach (var d in Digitalizados)
            Console.WriteLine($"- {d.Nombre} | {d.TamanoKb} KB | {(d.Validado ? "VÁLIDO" : "RECHAZADO")}");
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
        if (!File.Exists(doc.Ruta)) return;
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