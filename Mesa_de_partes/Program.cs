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
                string dni = "";
                
                while (true)
                {
                    Console.Write("Ingrese DNI (8 dígitos): ");
                    dni = Console.ReadLine();

                    if (dni != null && dni.Length == 8)
                    {
                        break;
                    }
                    Console.WriteLine("Error: El DNI debe tener exactamente 8 dígitos. Intente nuevamente.");
                }
                
                string nombres = "";
                while (true)
                {
                    Console.Write("Ingrese Nombres y Apellidos: ");
                    nombres = Console.ReadLine();

                    if (nombres != null && nombres.Trim().Length > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Error: El nombre y apellido no puede estar vacío. Intente nuevamente.");
                }

                Console.WriteLine("\n--- DATOS DEL DOCUMENTO ---");
                string tipoDocumento = "";
                while (true)
                {
                    Console.Write("Ingrese el Tipo de Documento (Ej. Oficio, Solicitud): ");
                    tipoDocumento = Console.ReadLine();

                    if (tipoDocumento != null && tipoDocumento.Trim().Length > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Error: El tipo de documento no puede estar vacío.");
                }

                string asunto = "";
                while (true)
                {
                    Console.Write("Ingrese el Asunto o Motivo: ");
                    asunto = Console.ReadLine();

                    if (asunto != null && asunto.Trim().Length > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Error: El asunto no puede estar vacío.");
                }

                Console.Write("\n¿Desea confirmar el registro de este documento? (S/N): ");
                string confirmacion = Console.ReadLine()?.Trim().ToUpper();

                if (confirmacion == "S")
                {
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
                    Console.WriteLine("\nRegistro cancelado por el usuario.");
                }
            }
            else
            {
                Console.WriteLine("Saliendo del sistema...");
            }
        }
    }
}