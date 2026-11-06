
using System;

namespace Mesa_de_partes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;

            do
            {
                MostrarMenu();

                int opcion = LeerOpcion();

                continuar = ProcesarOpcion(opcion);

                if (continuar)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.WriteLine();
                }

            } while (continuar);
        }
        static void MostrarMenu()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("       SISTEMA DE MESA DE PARTES");
            Console.WriteLine(" MUNICIPALIDAD DE SAN JUAN DE LURIGANCHO");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Registrar nuevo documento");
            Console.WriteLine("2. Digitalizar y validar documento");
            Console.WriteLine("3. Clasificar y derivar expediente");
            Console.WriteLine("4. Consultar expediente");
            Console.WriteLine("5. Mostrar expedientes registrados");
            Console.WriteLine("6. Ordenar expedientes por codigo");
            Console.WriteLine("7. Salir");
            Console.WriteLine("======================================");
        }

        //  validar la opcion ingresada
        static int LeerOpcion()
        {
            int opcion;

            Console.Write("Seleccione una opcion: ");
            string entrada = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(entrada) ||
                !int.TryParse(entrada, out opcion))
            {
                Console.WriteLine("Error: ingrese un numero del 1 al 7.");
                return 0;
            }

            if (opcion < 1 || opcion > 7)
            {
                Console.WriteLine("Error: seleccione una opcion entre 1 y 7.");
                return 0;
            }

            return opcion;
        }

        //  opciones del menu
        static bool ProcesarOpcion(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    RegistrarDocumento();
                    break;

                case 2:
                    DigitalizarDocumento();
                    break;

                case 3:
                    ClasificarExpediente();
                    break;

                case 4:
                    ConsultarExpediente();
                    break;

                case 5:
                    MostrarExpedientes();
                    break;

                case 6:
                    OrdenarExpedientes();
                    break;

                case 7:
                    if (ConfirmarSalida())
                    {
                        Console.WriteLine("Saliendo del sistema...");
                        return false;
                    }
                    break;

                    case 0:
                 break;

                default:
                    Console.WriteLine("Opcion no valida. Intente nuevamente.");
                    break;
            }

            return true;
        }

        // metodos de navegación
        static void RegistrarDocumento()
        {
            Console.WriteLine("Modulo de registro de documentos.");
        }

        static void DigitalizarDocumento()
        {
            Console.WriteLine("Modulo de digitalizacion y validacion.");
        }

        static void ClasificarExpediente()
        {
            Console.WriteLine("Modulo de clasificacion y derivacion.");
        }

        static void ConsultarExpediente()
        {
            Console.WriteLine("Modulo de consulta de expedientes.");
        }

        static void MostrarExpedientes()
        {
            Console.WriteLine("Modulo de expedientes registrados.");
        }

        static void OrdenarExpedientes()
        {
            Console.WriteLine("Modulo de ordenamiento de expedientes.");
        }

        //  la salida
        static bool ConfirmarSalida()
        {
            string confirma;

            do
            {
                Console.Write("¿Confirma que desea salir? (S/N): ");
                confirma = (Console.ReadLine() ?? "").Trim();

                if (confirma.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (confirma.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Salida cancelada.");
                    return false;
                }

                Console.WriteLine("Ingrese S para salir o N para continuar.");

            } while (true);
        }
    }
}

