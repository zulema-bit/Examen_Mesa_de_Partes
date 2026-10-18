using System;

namespace Mesa_De_Partes
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("   SISTEMA DE MESA DE PARTES - MUNI     ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Registrar Documento y Generar Expediente");
                Console.WriteLine("2. Salir");
                Console.Write("Ingrese una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            RegistrarDocumento();
                            break;
                        case 2:
                            Console.WriteLine("\nSaliendo del sistema. ¡Hasta luego!");
                            break;
                        default:
                            Console.WriteLine("\nOpción no válida. Presione una tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nPor favor, ingrese un número válido. Presione una tecla para continuar...");
                    Console.ReadKey();
                }

                if (opcion != 2)
                {
                    Console.WriteLine("\nPresione una tecla para volver al menú...");
                    Console.ReadKey();
                }

            } while (opcion != 2);
        }

        static void RegistrarDocumento()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       REGISTRO DE DOCUMENTOS           ");
            Console.WriteLine("========================================");

            string dni = "";
            while (true)
            {
                Console.Write("Ingrese DNI (8 dígitos): ");
                dni = Console.ReadLine()?.Trim() ?? "";

                if (dni.Length == 8 && long.TryParse(dni, out _))
                {
                    break;
                }
                Console.WriteLine("Error: El DNI debe contener exactamente 8 dígitos numéricos. Intente nuevamente.");
            }

            string nombre = "";
            while (true)
            {
                Console.Write("Ingrese Nombres y Apellidos: ");
                nombre = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrEmpty(nombre))
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
                tipoDocumento = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrEmpty(tipoDocumento))
                {
                    break;
                }
                Console.WriteLine("Error: El tipo de documento no puede estar vacío. Intente nuevamente.");
            }

            string asunto = "";
            while (true)
            {
                Console.Write("Ingrese el Asunto o Motivo: ");
                asunto = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrEmpty(asunto))
                {
                    break;
                }
                Console.WriteLine("Error: El asunto no puede estar vacío. Intente nuevamente.");
            }

            char confirmar = ' ';
            while (true)
            {
                Console.Write("\n¿Desea confirmar el registro de este documento? (S/N): ");
                string inputConf = Console.ReadLine()?.Trim().ToUpper() ?? "";

                if (inputConf == "S" || inputConf == "N")
                {
                    confirmar = inputConf[0];
                    break;
                }
                Console.WriteLine("Error: Ingrese 'S' para sí o 'N' para no.");
            }

            if (confirmar == 'S')
            {
                Random random = new Random();
                string nroExpediente = $"EXP-{random.Next(1000, 9999)}";
                DateTime fechaRegistro = DateTime.Now;

                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("      EXPEDIENTE GENERADO CON ÉXITO     ");
                Console.WriteLine("========================================");
                Console.WriteLine($"Nro. Expediente : {nroExpediente}");
                Console.WriteLine($"Fecha y Hora    : {fechaRegistro}");
                Console.WriteLine($"Solicitante     : {nombre} (DNI: {dni})");
                Console.WriteLine($"Documento       : {tipoDocumento}");
                Console.WriteLine($"Asunto          : {asunto}");
                Console.WriteLine($"Estado Inicial  : Registrado");
                Console.WriteLine("========================================");
            }
            else
            {
                Console.WriteLine("\nRegistro cancelado por el usuario.");
            }
        }
    }
}