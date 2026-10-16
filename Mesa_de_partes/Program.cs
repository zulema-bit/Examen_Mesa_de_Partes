using System;

class Program
{
    static void Main()
    {
        ClasificacionDerivacion modulo = new ClasificacionDerivacion();
        modulo.ClasificarYDerivar();
    }
}

class ClasificacionDerivacion
{
    public void ClasificarYDerivar()
    {
        Console.WriteLine("=== CLASIFICACION Y DERIVACION DE TRAMITES ===");

        Console.Write("Ingrese el numero de expediente: ");
        string expediente = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(expediente))
        {
            Console.WriteLine("Error: el expediente es obligatorio.");
            return;
        }

        Console.Write("Ingrese el tipo de documento: ");
        string tipoDoc = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(tipoDoc))
        {
            Console.WriteLine("Error: el tipo de documento es obligatorio.");
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

        Console.Write("Ingrese una opcion (1-10): ");
        string entrada = Console.ReadLine() ?? "";

        if (!int.TryParse(entrada, out int opcion) ||
            opcion < 1 || opcion > 10)
        {
            Console.WriteLine("Error: ingrese una opcion valida del 1 al 10.");
            return;
        }

        Console.WriteLine("\nRegistro inicial completado.");
        Console.WriteLine($"Expediente: {expediente}");
        Console.WriteLine($"Tipo de documento: {tipoDoc}");
        Console.WriteLine($"Opcion seleccionada: {opcion}");
    }
}

