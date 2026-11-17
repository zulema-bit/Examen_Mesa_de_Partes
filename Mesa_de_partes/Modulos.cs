namespace Mesa_de_partes;

public static class Modulos
{
    public static void Registrar(AlmacenExpedientes datos)
    {
        var dni = Pedir("DNI (8 digitos) o RUC (11 digitos): ", x =>
            (x.Length == 8 || x.Length == 11) && x.All(char.IsDigit));
        var nombre = Pedir("Nombre o razon social: ", x => !string.IsNullOrWhiteSpace(x));
        var tipo = Pedir("Tipo de documento: ", x => !string.IsNullOrWhiteSpace(x));
        var asunto = Pedir("Asunto: ", x => !string.IsNullOrWhiteSpace(x));
        Console.Write("Ruta del archivo PDF o imagen: ");
        var ruta = Console.ReadLine()?.Trim().Trim('"') ?? "";
        if (!ValidarArchivo(ruta)) return;
        var confirmacion = Pedir("Confirma el registro? (S/N): ", x =>
            x.Equals("S", StringComparison.OrdinalIgnoreCase) ||
            x.Equals("N", StringComparison.OrdinalIgnoreCase));
        if (confirmacion.Equals("N", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Registro cancelado por el usuario.");
            return;
        }
        var numero = datos.SiguienteNumero();
        var nuevo = new Expediente
        {
            Numero = numero, Codigo = $"EXP-{numero:D4}", DniRuc = dni,
            Solicitante = nombre, TipoDocumento = tipo, Asunto = asunto,
            FechaRegistro = DateTime.Now, Estado = "Registrado",
            RutaArchivo = Path.GetFullPath(ruta),
            ObservacionArchivo = "Archivo comprobado al registrar; OCR no implementado"
        };
        nuevo.Historial.Add(new Movimiento
       {
        Fecha = nuevo.FechaRegistro,
        Area = "Mesa de Partes",
        Descripcion = "Expediente registrado"
       });

       datos.Expedientes.Add(nuevo);

        if (!datos.Guardar())
       {
         datos.Expedientes.Remove(nuevo);
         Console.WriteLine("Registro cancelado: no se pudo guardar.");
         return;
       }

         Console.WriteLine($"Registrado: {nuevo.Codigo} | {nuevo.FechaRegistro:dd/MM/yyyy HH:mm} | Estado: {nuevo.Estado}");
    }
   
    
         public static void Digitalizar(AlmacenExpedientes datos)
       {
          string opcion;

        do
    {
        Console.WriteLine("\n=== DIGITALIZACION Y OCR ===");
        Console.WriteLine("1. Digitalizar documento");
        Console.WriteLine("2. Ver documentos digitalizados");
        Console.WriteLine("3. Ver resumen");
        Console.WriteLine("0. Volver al menu principal");
        Console.Write("Elige una opcion: ");

        opcion = Console.ReadLine()?.Trim() ?? "";

        switch (opcion)
        {
            case "1":
                var exp = PedirExpediente(datos);
                if (exp is null) break;

                if (exp.FechaDigitalizacion != null)
                {
                    Console.WriteLine("Este expediente ya fue digitalizado.");
                    break;
                }

                var ruta = exp.RutaArchivo;

                if (string.IsNullOrWhiteSpace(ruta))
                {
                    Console.Write("Ruta del documento PDF o imagen: ");
                    ruta = Console.ReadLine()?.Trim().Trim('"') ?? "";
                }
                else
                {
                    Console.WriteLine($"Archivo registrado: {ruta}");
                }

                if (!ValidarArchivo(ruta)) break;

                var rutaCompleta = Path.GetFullPath(ruta);

                bool duplicado = datos.Expedientes.Any(x =>
                    x.Numero != exp.Numero &&
                    x.FechaDigitalizacion != null &&
                    string.Equals(
                        x.RutaArchivo,
                        rutaCompleta,
                        StringComparison.OrdinalIgnoreCase));

                if (duplicado)
                {
                    Console.WriteLine("Este archivo ya fue digitalizado en otro expediente.");
                    break;
                }

                var anteriorRuta = exp.RutaArchivo;
                var anteriorObservacion = exp.ObservacionArchivo;
                var anteriorEstado = exp.Estado;
                var anteriorTexto = exp.TextoExtraido;
                var anteriorFecha = exp.FechaDigitalizacion;
                var anteriorValidado = exp.Validado;

                exp.RutaArchivo = rutaCompleta;
                exp.TextoExtraido =
                    $"[OCR SIMULADO] Texto de ejemplo del archivo {Path.GetFileName(rutaCompleta)}";
                exp.FechaDigitalizacion = DateTime.Now;
                exp.Validado = true;
                exp.ObservacionArchivo = "Documento conforme. OCR simulado.";
                exp.Estado = "En revision";

                exp.Historial.Add(new Movimiento
                {
                    Fecha = DateTime.Now,
                    Area = exp.Area,
                    Descripcion = "Documento digitalizado y validado (OCR simulado)"
                });

                if (!datos.Guardar())
                {
                    exp.RutaArchivo = anteriorRuta;
                    exp.ObservacionArchivo = anteriorObservacion;
                    exp.Estado = anteriorEstado;
                    exp.TextoExtraido = anteriorTexto;
                    exp.FechaDigitalizacion = anteriorFecha;
                    exp.Validado = anteriorValidado;
                    exp.Historial.RemoveAt(exp.Historial.Count - 1);

                    Console.WriteLine("No se pudieron guardar los cambios.");
                    break;
                }

                Console.WriteLine($"\nDocumento digitalizado correctamente.");
                Console.WriteLine($"Expediente: {exp.Codigo}");
                Console.WriteLine($"Documento: {Path.GetFileName(rutaCompleta)}");
                Console.WriteLine($"Texto extraido: {exp.TextoExtraido}");
                Console.WriteLine("Estado: VALIDO");
                break;

            case "2":
                var digitalizados = datos.Expedientes
                    .Where(x => x.FechaDigitalizacion != null)
                    .ToList();

                if (digitalizados.Count == 0)
                {
                    Console.WriteLine("Aun no hay documentos digitalizados.");
                    break;
                }

                Console.WriteLine("\n--- DOCUMENTOS DIGITALIZADOS ---");

                foreach (var documento in digitalizados)
                {
                    Console.WriteLine(
                        $"{documento.Codigo} | {documento.Solicitante} | " +
                        $"{documento.FechaDigitalizacion:dd/MM/yyyy HH:mm} | " +
                        $"{(documento.Validado ? "VALIDO" : "RECHAZADO")}");
                }
                break;

            case "3":
                int total = datos.Expedientes.Count(
                    x => x.FechaDigitalizacion != null);

                int validos = datos.Expedientes.Count(
                    x => x.FechaDigitalizacion != null && x.Validado);

                int rechazados = total - validos;

                Console.WriteLine("\n--- RESUMEN DE DIGITALIZACION ---");
                Console.WriteLine($"Total digitalizados: {total}");
                Console.WriteLine($"Validos: {validos}");
                Console.WriteLine($"Rechazados: {rechazados}");
                break;

            case "0":
                Console.WriteLine("Volviendo al menu principal...");
                break;

            default:
                Console.WriteLine("Opcion no valida.");
                break;
        }

    } while (opcion != "0");
}


    public static void Clasificar(AlmacenExpedientes datos)
    {
        var exp = PedirExpediente(datos);
        if (exp is null) return;
        Console.WriteLine("1. Obras; 2. Desarrollo Urbano; 3. Otra area");
        Console.Write("Seleccione el tramite: ");
        var area = Console.ReadLine()?.Trim() switch
        {
            "1" => "Subgerencia de Obras",
            "2" => "Desarrollo Urbano",
            "3" => "Area correspondiente",
            _ => null
        };
        if (area is null) { Console.WriteLine("Opcion no valida."); return; }
        var anteriorArea = exp.Area;
        var anteriorEstado = exp.Estado;
        exp.Area = area;
        exp.Estado = "En proceso";
        exp.Historial.Add(new Movimiento
      {
        Fecha = DateTime.Now,
        Area = area,
        Descripcion = $"Expediente derivado a {area}"
      });

      if (!datos.Guardar())
       {
         exp.Area = anteriorArea;
         exp.Estado = anteriorEstado;
         exp.Historial.RemoveAt(exp.Historial.Count - 1);

         Console.WriteLine("Cambio cancelado: no se pudo guardar.");
         return;
       }

         Console.WriteLine($"Expediente derivado a {area}.");
    }

    public static void Consultar(AlmacenExpedientes datos)
    {
        var exp = PedirExpediente(datos);
        if (exp is null) return;
        Console.Write("DNI/RUC del solicitante: ");
        var dni = Console.ReadLine()?.Trim() ?? "";
        if (exp.DniRuc != dni) { Console.WriteLine("DNI/RUC no corresponde al expediente."); return; }
        Console.WriteLine("1. Estado; 2. Area; 3. Historial; 4. Datos completos");
        Console.Write("Seleccione el tipo de consulta: ");
        switch (Console.ReadLine()?.Trim())
        {
            case "1": Console.WriteLine($"{exp.Codigo}: {exp.Estado}"); break;
            case "2": Console.WriteLine($"{exp.Codigo}: {exp.Area}"); break;
            case "3":
                foreach (var evento in exp.Historial)
            {
               Console.WriteLine($"{evento.Fecha:dd/MM/yyyy HH:mm} | {evento.Area} | {evento.Descripcion}");
            }
             break;
            case "4": Mostrar([exp]); break;
            default: Console.WriteLine("Opcion no valida."); break;
        }
    }

    public static void Mostrar(IEnumerable<Expediente> expedientes)
    {
        var lista = expedientes.ToList();
        if (lista.Count == 0) { Console.WriteLine("No hay expedientes registrados."); return; }
        foreach (var x in lista)
            Console.WriteLine($"{x.Codigo} | {x.Solicitante} | {x.TipoDocumento} | {x.Asunto} | {x.FechaRegistro:dd/MM/yyyy HH:mm} | {x.Estado} | {x.Area}");
    }

    public static void Ordenar(AlmacenExpedientes datos)
    {
        Console.WriteLine("Ordenar por codigo: 1. Ascendente; 2. Descendente");
        Console.Write("Seleccione el orden: ");
        var opcion = Console.ReadLine()?.Trim();
        if (opcion is not ("1" or "2"))
        {
            Console.WriteLine("Opcion no valida.");
            return;
        }
        var ordenados = opcion == "1"
            ? datos.Expedientes.OrderBy(x => x.Numero)
            : datos.Expedientes.OrderByDescending(x => x.Numero);
        Console.WriteLine(opcion == "1" ? "Orden ascendente:" : "Orden descendente:");
        Mostrar(ordenados);
    }

    private static Expediente? PedirExpediente(AlmacenExpedientes datos)
    {
        Console.Write("Codigo del expediente (por ejemplo, EXP-1001 o 1001): ");
        var exp = datos.Buscar(Console.ReadLine() ?? "");
        if (exp is null) Console.WriteLine("Expediente no encontrado.");
        return exp;
    }

    private static bool ValidarArchivo(string ruta)
    {
        try
        {
            if (!File.Exists(ruta)) { Console.WriteLine("El archivo no existe."); return false; }
            var extension = Path.GetExtension(ruta).ToLowerInvariant();
            if (extension is not (".pdf" or ".jpg" or ".jpeg" or ".png"))
            { Console.WriteLine("Formato no permitido."); return false; }
            if (new FileInfo(ruta).Length > 5 * 1024 * 1024)
            { Console.WriteLine("El archivo supera 5 MB."); return false; }
            return true;
        }
        catch (Exception ex) when (ex is ArgumentException or IOException or UnauthorizedAccessException or NotSupportedException)
        {
            Console.WriteLine($"No se pudo comprobar el archivo: {ex.Message}");
            return false;
        }
    }

    private static string Pedir(string mensaje, Func<string, bool> valido)
    {
        while (true)
        {
            Console.Write(mensaje);
            var valor = Console.ReadLine()?.Trim() ?? "";
            if (valido(valor)) return valor;
            Console.WriteLine("Dato no valido. Intente nuevamente.");
        }
    }
}
