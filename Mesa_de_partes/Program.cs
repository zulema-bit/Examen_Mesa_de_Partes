using Mesa_de_partes;

var datos = new AlmacenExpedientes(Path.Combine(AppContext.BaseDirectory, "expedientes.json"));
while (true)
{
    Console.WriteLine("\n=== SISTEMA DE MESA DE PARTES ===");
    Console.WriteLine("1. Registrar documento");
    Console.WriteLine("2. Digitalizar y validar documento");
    Console.WriteLine("3. Clasificar y derivar expediente");
    Console.WriteLine("4. Consultar expediente");
    Console.WriteLine("5. Mostrar expedientes");
    Console.WriteLine("6. Ordenar por codigo");
    Console.WriteLine("7. Salir");
    Console.Write("Seleccione una opcion: ");
    if (!int.TryParse(Console.ReadLine(), out var opcion) || opcion < 1 || opcion > 7)
    {
        Console.WriteLine("Opcion no valida. Ingrese un numero del 1 al 7.");
        continue;
    }
    switch (opcion)
    {
        case 1: Modulos.Registrar(datos); break;
        case 2: Modulos.Digitalizar(datos); break;
        case 3: Modulos.Clasificar(datos); break;
        case 4: Modulos.Consultar(datos); break;
        case 5: Modulos.Mostrar(datos.Expedientes); break;
        case 6: Modulos.Ordenar(datos); break;
        case 7:
            Console.Write("Confirma que desea salir? (S/N): ");
            if (string.Equals(Console.ReadLine()?.Trim(), "S", StringComparison.OrdinalIgnoreCase)) return;
            break;
    }
}
