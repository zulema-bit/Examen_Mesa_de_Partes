using System;

class Program
{
    static void Main()
    {
        ConsultaExpedientes modulo = new ConsultaExpedientes();
        modulo.ConsultarYSeguir();
    }
}

class ConsultaExpedientes
{
    public void ConsultarYSeguir()
    {
        Console.WriteLine("=== CONSULTA Y SEGUIMIENTO DE EXPEDIENTES ===");

        Console.Write("Ingrese el numero de expediente: ");
        string expediente = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(expediente))
        {
            Console.WriteLine("Error: el expediente es obligatorio.");
            return;
        }

               Console.Write("Ingrese DNI del solicitante (8 digitos): ");
        string dni = Console.ReadLine() ?? "";

        if (dni.Length != 8 || !dni.All(char.IsDigit))
        {
            Console.WriteLine("Error: el DNI debe tener 8 digitos numericos.");
            return;
        }
        
        Console.WriteLine("\nSeleccione el tipo de consulta:");
        Console.WriteLine("1. Estado actual del expediente");
        Console.WriteLine("2. Area donde se encuentra");
        Console.WriteLine("3. Historial de movimientos (seguimiento)");

        Console.Write("Ingrese una opcion (1-3): ");
        string entrada = Console.ReadLine() ?? "";

        if (!int.TryParse(entrada, out int opcion) ||
            opcion < 1 || opcion > 3)
        {
            Console.WriteLine("Error: ingrese una opcion valida del 1 al 3.");
            return;
        }
        
        Console.WriteLine("\nConsulta registrada.");
        Console.WriteLine($"Expediente: {expediente}");
        Console.WriteLine($"DNI del solicitante: {dni}");
        Console.WriteLine($"Opcion seleccionada: {opcion}");
    }
}