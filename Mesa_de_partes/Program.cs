
using System;
using System.IO;

namespace Mesa_de_partes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var datos = new AlmacenExpedientes(
                Path.Combine(AppContext.BaseDirectory, "expedientes.json"));

            bool continuar = true;

            do
            {
                MostrarMenu();

                int opcion = LeerOpcion();

                continuar = ProcesarOpcion(opcion, datos);

                if (continuar)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione cualquier tecla para continuar...");
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

        static int LeerOpcion()
        {
            Console.Write("Seleccione una opcion: ");

            string entrada = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(entrada) ||
                !int.TryParse(entrada, out int opcion))
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

        static bool ProcesarOpcion(int opcion, AlmacenExpedientes datos)
        {
            switch (opcion)
            {
                case 1:
                    Modulos.Registrar(datos);
                    break;

                case 2:
                    Modulos.Digitalizar(datos);
                    break;

                case 3:
                    Modulos.Clasificar(datos);
                    break;

                case 4:
                    Modulos.Consultar(datos);
                    break;

                case 5:
                    Modulos.Mostrar(datos.Expedientes);
                    break;

                case 6:
                    Modulos.Ordenar(datos);
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
            }

            return true;
        }

        static bool ConfirmarSalida()
        {
            do
            {
                Console.Write("¿Confirma que desea salir? (S/N): ");

                string confirma = (Console.ReadLine() ?? "").Trim();

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
