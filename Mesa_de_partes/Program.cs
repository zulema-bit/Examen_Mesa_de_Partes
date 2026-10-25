using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== CLASIFICACION Y DERIVACION DE TRAMITES ===");

        string expediente;
        string asunto;
        string entrada;
        int opcion;
        string categoria = "";

        Console.Write("Ingrese el numero de expediente: ");
        expediente = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(expediente))
        {
            Console.WriteLine("Error: debe ingresar el numero de expediente.");
            return;
        }

        Console.Write("Ingrese el asunto del tramite: ");
        asunto = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(asunto))
        {
            Console.WriteLine("Error: debe ingresar el asunto del tramite.");
            return;
        }

        Console.WriteLine("\nSeleccione el tipo de tramite:");
        Console.WriteLine("1. Construccion o reparacion de pistas y veredas");
        Console.WriteLine("2. Licencia de edificacion o remodelacion");
        Console.WriteLine("3. Habilitacion urbana");
        Console.WriteLine("4. Zonificacion o catastro");
        Console.WriteLine("5. Impuesto predial o consulta tributaria");
        Console.WriteLine("6. Limpieza publica");
        Console.WriteLine("7. Parques y jardines");
        Console.WriteLine("8. Licencia o formalizacion de negocio");
        Console.WriteLine("9. Transito o señalizacion vial");
        Console.WriteLine("10. Otro tramite");

        Console.Write("Seleccione una opcion del 1 al 10: ");
        entrada = Console.ReadLine() ?? "";

        if (!int.TryParse(entrada, out opcion))
        {
            Console.WriteLine("Error: debe ingresar un numero.");
            return;
        }

        if (opcion < 1 || opcion > 10)
        {
            Console.WriteLine("Error: la opcion debe estar entre 1 y 10.");
            return;
        }

        // Clasificacion automatica segun el asunto
        asunto = asunto.ToLower();

        if (asunto.Contains("obra") ||
            asunto.Contains("construccion") ||
            asunto.Contains("pista") ||
            asunto.Contains("vereda"))
        {
            categoria = "Obras publicas";
        }
        else if (asunto.Contains("licencia") ||
                 asunto.Contains("edificacion") ||
                 asunto.Contains("remodelacion"))
        {
            categoria = "Licencias y edificaciones";
        }
        else if (asunto.Contains("habilitacion urbana"))
        {
            categoria = "Habilitacion urbana";
        }
        else if (asunto.Contains("zonificacion") ||
                 asunto.Contains("catastro"))
        {
            categoria = "Zonificacion y catastro";
        }
        else if (asunto.Contains("impuesto") ||
                 asunto.Contains("tributaria") ||
                 asunto.Contains("predial"))
        {
            categoria = "Asuntos tributarios";
        }
        else if (asunto.Contains("limpieza"))
        {
            categoria = "Limpieza publica";
        }
        else if (asunto.Contains("parque") ||
                 asunto.Contains("jardin"))
        {
            categoria = "Parques y jardines";
        }
        else if (asunto.Contains("negocio") ||
                 asunto.Contains("formalizacion"))
        {
            categoria = "Formalizacion de negocios";
        }
        else if (asunto.Contains("transito") ||
                 asunto.Contains("señalizacion") ||
                 asunto.Contains("vial"))
        {
            categoria = "Transito y señalizacion vial";
        }
        else
        {
            categoria = "Otro tramite";
        }

        Console.WriteLine("\n=== RESULTADO DE LA CLASIFICACION ===");
        Console.WriteLine("Expediente: " + expediente);
        Console.WriteLine("Asunto: " + asunto);
        Console.WriteLine("Opcion seleccionada: " + opcion);
        Console.WriteLine("Categoria identificada: " + categoria);
        Console.WriteLine("Estado: Pendiente de derivacion");
    }
}

