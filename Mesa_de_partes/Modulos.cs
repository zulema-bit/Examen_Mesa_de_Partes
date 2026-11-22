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

    Console.WriteLine("\n=== SELECCIONE EL ASUNTO ===");
    Console.WriteLine("1. Reparacion de pistas y veredas");
    Console.WriteLine("2. Licencia de edificacion");
    Console.WriteLine("3. Habilitacion urbana");
    Console.WriteLine("4. Zonificacion o catastro");
    Console.WriteLine("5. Impuesto predial");
    Console.WriteLine("6. Limpieza publica");
    Console.WriteLine("7. Mantenimiento de parques y jardines");
    Console.WriteLine("8. Licencia de funcionamiento de negocio");
    Console.WriteLine("9. Senalizacion vial");
    Console.WriteLine("10. Escribir asunto manualmente");

    Console.Write("Seleccione una opcion (1-10): ");

    if (!int.TryParse(Console.ReadLine(), out int opcion) ||
        opcion < 1 || opcion > 10)
    {
        Console.WriteLine("Error: seleccione una opcion del 1 al 10.");
        return;
    }

    string asunto = "";
    string tramite = "";
    string categoria = "";
    string area = "";
    string estado = "En proceso";

    switch (opcion)
    {
        case 1:
            asunto = "Reparacion de pistas y veredas";
            tramite = "Obras y pistas";
            categoria = "Obras publicas";
            area = "Gerencia de Desarrollo Urbano";
            break;

        case 2:
            asunto = "Licencia de edificacion";
            tramite = "Licencias de edificacion";
            categoria = "Licencias y edificaciones";
            area = "Subgerencia de Obras Privadas";
            break;

        case 3:
            asunto = "Habilitacion urbana";
            tramite = "Habilitacion urbana";
            categoria = "Habilitacion urbana";
            area = "Subgerencia de Planeamiento Urbano";
            break;

        case 4:
            asunto = "Zonificacion o catastro";
            tramite = "Zonificacion y catastro";
            categoria = "Zonificacion y catastro";
            area = "Subgerencia de Catastro";
            break;

        case 5:
            asunto = "Impuesto predial";
            tramite = "Asuntos tributarios";
            categoria = "Asuntos tributarios";
            area = "Gerencia de Administracion Tributaria";
            break;

        case 6:
            asunto = "Limpieza publica";
            tramite = "Limpieza publica";
            categoria = "Limpieza publica";
            area = "Gerencia de Servicios Publicos";
            break;

        case 7:
            asunto = "Mantenimiento de parques y jardines";
            tramite = "Parques y jardines";
            categoria = "Parques y jardines";
            area = "Gerencia de Servicios Publicos";
            break;

        case 8:
            asunto = "Licencia de funcionamiento de negocio";
            tramite = "Formalizacion de negocios";
            categoria = "Formalizacion de negocios";
            area = "Subgerencia de Comercializacion";
            break;

        case 9:
            asunto = "Senalizacion vial";
            tramite = "Transito y senalizacion";
            categoria = "Transito y senalizacion vial";
            area = "Gerencia de Transporte";
            break;

        case 10:
            Console.Write("Escriba el asunto: ");
            asunto = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(asunto))
            {
                Console.WriteLine("Error: debe escribir un asunto.");
                return;
            }

            Console.Write("Escriba el tipo de tramite: ");
            tramite = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(tramite))
            {
                Console.WriteLine("Error: debe escribir el tipo de tramite.");
                return;
            }

            categoria = "Pendiente de clasificacion";
            area = "Mesa de Partes";
            estado = "Pendiente de evaluacion y derivacion";
            break;
    }

    var anteriorAsunto = exp.Asunto;
    var anteriorTramite = exp.Tramite;
    var anteriorCategoria = exp.Categoria;
    var anteriorArea = exp.Area;
    var anteriorEstado = exp.Estado;

    exp.Asunto = asunto;
    exp.Tramite = tramite;
    exp.Categoria = categoria;
    exp.Area = area;
    exp.Estado = estado;

    exp.Historial.Add(new Movimiento
    {
        Fecha = DateTime.Now,
        Area = area,
        Descripcion = $"Asunto: {asunto} | Tramite: {tramite} | Categoria: {categoria} | Estado: {estado}"
    });

    if (!datos.Guardar())
    {
        exp.Asunto = anteriorAsunto;
        exp.Tramite = anteriorTramite;
        exp.Categoria = anteriorCategoria;
        exp.Area = anteriorArea;
        exp.Estado = anteriorEstado;
        exp.Historial.RemoveAt(exp.Historial.Count - 1);

        Console.WriteLine("Cambio cancelado: no se pudo guardar.");
        return;
    }

    Console.WriteLine("\n=== RESULTADO DEL TRAMITE ===");
    Console.WriteLine($"Expediente: {exp.Codigo}");
    Console.WriteLine($"Asunto: {exp.Asunto}");
    Console.WriteLine($"Tipo de tramite: {exp.Tramite}");
    Console.WriteLine($"Categoria: {exp.Categoria}");
    Console.WriteLine($"Area de destino: {exp.Area}");
    Console.WriteLine($"Estado: {exp.Estado}");

    if (opcion == 10)
    {
        Console.WriteLine("\nEl asunto queda pendiente de clasificacion.");
        Console.WriteLine("Mesa de Partes debe evaluarlo antes de derivarlo.");
    }
    else
    {
        Console.WriteLine("\nEl tramite fue clasificado y derivado correctamente.");
    }
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
            Console.WriteLine($"{x.Codigo} | {x.Solicitante} | {x.TipoDocumento} | {x.Asunto} | Tramite: {x.Tramite} | Categoria: {x.Categoria} | {x.FechaRegistro:dd/MM/yyyy HH:mm} | {x.Estado} | {x.Area}");
    }

    
        public static void Ordenar(AlmacenExpedientes datos)
   {
           Console.WriteLine("Ordenar por codigo: 1. Ascendente; 2. Descendente");
           Console.Write("Seleccione el orden: ");

            string opcion = Console.ReadLine()?.Trim() ?? "";

             if (opcion != "1" && opcion != "2")
        {
              Console.WriteLine("Opcion no valida.");
             return;
        }

            Expediente[] ordenados = datos.Expedientes.ToArray();

          Array.Sort(ordenados, (a, b) =>
             opcion == "1"
            ? a.Numero.CompareTo(b.Numero)
            : b.Numero.CompareTo(a.Numero));

            Console.WriteLine(opcion == "1"
               ? "Orden ascendente:"
               : "Orden descendente:");

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
