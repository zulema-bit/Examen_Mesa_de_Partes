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

        Console.WriteLine($"Expediente a consultar: {expediente}");
    }
}