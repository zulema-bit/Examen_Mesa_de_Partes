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

        Console.WriteLine($"Expediente a consultar: {expediente}");
    }
}