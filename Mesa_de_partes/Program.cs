using System;

class Program
{
    static void Main()
    {
        string expediente;
        string entrada;
        string asunto = "";
        string tramite = "";
        string categoria = "";
        string area = "";
        int opcionAsunto;
        int opcionTramite = 0;
        bool tramiteManual = false;

        Console.WriteLine("=== CLASIFICACION Y DERIVACION DE TRAMITES ===");

        // 1. Ingresar expediente
        Console.Write("Ingrese el numero de expediente: ");
        expediente = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(expediente))
        {
            Console.WriteLine("Error: debe ingresar el numero de expediente.");
            return;
        }

        // 2. Seleccionar asunto
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
        entrada = Console.ReadLine() ?? "";

        if (!int.TryParse(entrada, out opcionAsunto))
        {
            Console.WriteLine("Error: debe ingresar un numero.");
            return;
        }

        if (opcionAsunto < 1 || opcionAsunto > 10)
        {
            Console.WriteLine("Error: seleccione una opcion del 1 al 10.");
            return;
        }

        // 3. Relacionar el asunto con el tramite y el area
        switch (opcionAsunto)
        {
            case 1:
                asunto = "Reparacion de pistas y veredas";
                tramite = "Obras y pistas";
                categoria = "Obras publicas";
                area = "Gerencia de Desarrollo Urbano";
                opcionTramite = 1;
                break;

            case 2:
                asunto = "Licencia de edificacion";
                tramite = "Licencias de edificacion";
                categoria = "Licencias y edificaciones";
                area = "Subgerencia de Obras Privadas";
                opcionTramite = 2;
                break;

            case 3:
                asunto = "Habilitacion urbana";
                tramite = "Habilitacion urbana";
                categoria = "Habilitacion urbana";
                area = "Subgerencia de Planeamiento Urbano";
                opcionTramite = 3;
                break;

            case 4:
                asunto = "Zonificacion o catastro";
                tramite = "Zonificacion y catastro";
                categoria = "Zonificacion y catastro";
                area = "Subgerencia de Catastro";
                opcionTramite = 4;
                break;

            case 5:
                asunto = "Impuesto predial";
                tramite = "Asuntos tributarios";
                categoria = "Asuntos tributarios";
                area = "Gerencia de Administracion Tributaria";
                opcionTramite = 5;
                break;

            case 6:
                asunto = "Limpieza publica";
                tramite = "Limpieza publica";
                categoria = "Limpieza publica";
                area = "Gerencia de Servicios Publicos";
                opcionTramite = 6;
                break;

            case 7:
                asunto = "Mantenimiento de parques y jardines";
                tramite = "Parques y jardines";
                categoria = "Parques y jardines";
                area = "Gerencia de Servicios Publicos";
                opcionTramite = 7;
                break;

            case 8:
                asunto = "Licencia de funcionamiento de negocio";
                tramite = "Formalizacion de negocios";
                categoria = "Formalizacion de negocios";
                area = "Subgerencia de Comercializacion";
                opcionTramite = 8;
                break;

            case 9:
                asunto = "Senalizacion vial";
                tramite = "Transito y senalizacion";
                categoria = "Transito y senalizacion vial";
                area = "Gerencia de Transporte";
                opcionTramite = 9;
                break;


            case 10:
                Console.Write("Escriba el asunto que necesita: ");
                asunto = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(asunto))
                {
                    Console.WriteLine("Error: debe escribir un asunto.");
                    return;
                }

                tramiteManual = true;
                break;
        }

        // 4. Si el asunto no tiene un tramite preparado,
        // permitir que el usuario indique el tramite manualmente
        if (tramiteManual)
        {
            Console.WriteLine("\nNo hay un tramite preparado para ese asunto.");
            Console.WriteLine("");
            Console.WriteLine("=== INGRESE EL TRAMITE MANUALMENTE ===");
            Console.Write("Escriba el tipo de tramite: ");
            tramite = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(tramite))
            {
                Console.WriteLine("Error: debe escribir el tipo de tramite.");
                return;
            }

            categoria = "Pendiente de clasificacion";
            area = "Mesa de Partes para evaluacion";
            opcionTramite = 10;
        }

        // 5. Mostrar resultado
        Console.WriteLine("\n=== RESULTADO DEL TRAMITE ===");
        Console.WriteLine("Expediente: " + expediente);
        Console.WriteLine("Asunto: " + asunto);
        Console.WriteLine("Tipo de tramite: " + tramite);
        Console.WriteLine("Categoria: " + categoria);
        Console.WriteLine("Area de destino: " + area);
        Console.WriteLine("Estado: Pendiente de derivacion");
    }
}


