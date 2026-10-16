using System;

namespace Mesa_de_partes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine("       SISTEMA DE MESA DE PARTES");
                Console.WriteLine("======================================");
                Console.WriteLine("1. Registrar nuevo documento");
                Console.WriteLine("2. Digitalizar y validar documento");
                Console.WriteLine("3. Clasificar y derivar expediente");
                Console.WriteLine("4. Consultar expediente");
                Console.WriteLine("5. Salir");
                Console.WriteLine("======================================");
                Console.Write("Seleccione una opcion: ");

              
                string entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada) ||!int.TryParse(entrada, out opcion))
                {
               
                opcion = 0;
                Console.WriteLine("Error: ingrese un numero del 1 al 5.");

                }

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Modulo de registro de documentos.");
                        break;

                    case 2:
                        Console.WriteLine("Modulo de digitalizacion y validacion.");
                        break;

                    case 3:
                        Console.WriteLine("Modulo de clasificacion y derivacion.");
                        break;
                        
                    case 4:
                        Console.WriteLine("Modulo de consulta de expedientes.");
                        break;

                    case 5:
                        Console.WriteLine("Saliendo del sistema...");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida. Intente nuevamente.");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 5);
        }
    }
}
