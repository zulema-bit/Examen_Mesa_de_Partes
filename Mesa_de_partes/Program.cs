using System;

namespace Mesa_De_Partes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   SISTEMA DE MESA DE PARTES - MUNI     ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Documento y Generar Expediente");
            Console.WriteLine("2. Salir");
            Console.Write("Ingrese una opción: ");
            
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.WriteLine("\n--- DATOS DEL SOLICITANTE ---");
                Console.Write("Ingrese DNI (8 dígitos): ");
                string dni = Console.ReadLine();

                // Validación del DNI
                if (string.IsNullOrEmpty(dni) || dni.Length != 8)
                {
                    Console.WriteLine("Error: El DNI debe tener exactamente 8 dígitos.");
                    return;
                }
                
                Console.Write("Ingrese Nombres y Apellidos: ");
                string nombres = Console.ReadLine();

                Console.WriteLine("\n--- DATOS DEL DOCUMENTO ---");
                Console.Write("Ingrese el Tipo de Documento (Ej. Oficio, Solicitud): ");
                string tipoDocumento = Console.ReadLine();

                Console.Write("Ingrese el Asunto o Motivo: ");
                string asunto = Console.ReadLine();

                Random random = new Random();
                int numeroExpediente = random.Next(1000, 9999);
                DateTime fechaRegistro = DateTime.Now;

                Console.WriteLine("\n========================================");
                Console.WriteLine("      EXPEDIENTE GENERADO CON ÉXITO     ");
                Console.WriteLine("========================================");
                Console.WriteLine($"Nro. Expediente : EXP-{numeroExpediente}");
                Console.WriteLine($"Fecha y Hora    : {fechaRegistro}");
                Console.WriteLine($"Solicitante     : {nombres} (DNI: {dni})");
                Console.WriteLine($"Documento       : {tipoDocumento}");
                Console.WriteLine($"Asunto          : {asunto}");
                Console.WriteLine("========================================");
            }
            else
            {
                Console.WriteLine("Saliendo del sistema...");
            }
        }
    }
}