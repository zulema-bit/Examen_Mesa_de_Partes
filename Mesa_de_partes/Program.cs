using System;

namespace Mesa_De_Partes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Menú principal
            Console.WriteLine("========================================");
            Console.WriteLine("   SISTEMA DE MESA DE PARTES - MUNI     ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Documento y Generar Expediente");
            Console.WriteLine("2. Salir");
            Console.Write("Ingrese una opción: ");
            
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                // Captura de datos del ciudadano
                Console.WriteLine("\n--- DATOS DEL SOLICITANTE ---");
                Console.Write("Ingrese DNI (8 dígitos): ");
                string dni = Console.ReadLine();
                
                Console.Write("Ingrese Nombres y Apellidos: ");
                string nombres = Console.ReadLine();

                // Captura de datos del documento (Commit 3)
                Console.WriteLine("\n--- DATOS DEL DOCUMENTO ---");
                Console.Write("Ingrese el Tipo de Documento (Ej. Oficio, Solicitud): ");
                string tipoDocumento = Console.ReadLine();

                Console.Write("Ingrese el Asunto o Motivo: ");
                string asunto = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Saliendo del sistema...");
            }
        }
    }
}