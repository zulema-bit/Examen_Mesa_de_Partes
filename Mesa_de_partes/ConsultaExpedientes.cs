namespace Mesa_de_partes;

public static class ConsultaExpedientes
{
    public static void Ejecutar()
    {
        Console.WriteLine("=== CONSULTA Y SEGUIMIENTO DE EXPEDIENTES ===");

        Console.Write("Ingrese el numero de expediente: ");
        string expediente = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (string.IsNullOrWhiteSpace(expediente))
        {
            Console.WriteLine("Error: el expediente es obligatorio.");
            return;
        }

        Console.Write("Ingrese DNI del solicitante (8 digitos): ");
        string dni = (Console.ReadLine() ?? "").Trim();

        if (dni.Length != 8 || !dni.All(char.IsDigit))
        {
            Console.WriteLine("Error: el DNI debe tener 8 digitos numericos.");
            return;
        }

        if (RepositorioExpedientes.Cantidad() == 0)
        {
            Console.WriteLine("No hay expedientes registrados todavia.");
            return;
        }

        var exp = RepositorioExpedientes.Buscar(expediente);

        if (exp == null)
        {
            Console.WriteLine("Error: no se encontro el expediente.");
            return;
        }

        if (exp.Dni != dni)
        {
            Console.WriteLine("Error: el DNI no coincide con el solicitante del expediente.");
            return;
        }

        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\nSeleccione el tipo de consulta:");
            Console.WriteLine("1. Estado actual del expediente");
            Console.WriteLine("2. Area donde se encuentra");
            Console.WriteLine("3. Historial de movimientos (seguimiento)");
            Console.WriteLine("4. Salir");
            Console.Write("Ingrese una opcion (1-4): ");

            if (!int.TryParse(Console.ReadLine(), out opcion) ||
                opcion < 1 || opcion > 4)
            {
                Console.WriteLine("Error: ingrese una opcion valida del 1 al 4.");
                opcion = 0;
                continue;
            }

            if (opcion == 4)
            {
                Console.WriteLine("Saliendo de la consulta de expedientes...");
                break;
            }

            Console.WriteLine($"\nExpediente: {exp.Numero} | Solicitante: {exp.Solicitante}");

            switch (opcion)
            {
                case 1:
                    Console.WriteLine($"Estado actual: {exp.Estado}");
                    break;
                case 2:
                    Console.WriteLine($"Area actual: {exp.AreaActual}");
                    break;
                case 3:
                    Console.WriteLine("Historial de movimientos:");
                    foreach (var mov in exp.Historial.OrderBy(m => m.Fecha))
                    {
                        Console.WriteLine($"  {mov.Fecha:dd/MM/yyyy HH:mm} | {mov.Area} | {mov.Descripcion}");
                    }
                    break;
            }
        }
    }
}