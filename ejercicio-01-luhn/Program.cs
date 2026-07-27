using System;

int tarjetasValidas = 0;
int tarjetasInvalidas = 0;

int totalVisa = 0;
int totalMaster = 0;
int totalAmex = 0;
int totalDiscover = 0;
int totalDesconocidas = 0;

int opcion;

do
{
    Console.Clear();
    Console.WriteLine("=================================");
    Console.WriteLine("     VALIDADOR DE TARJETAS");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Validar una tarjeta");
    Console.WriteLine("2. Validar desde archivo");
    Console.WriteLine("3. Generar número válido");
    Console.WriteLine("4. Estadísticas");
    Console.WriteLine("5. Salir");
    Console.Write("Seleccione una opción: ");

    try
    {
        opcion = Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        opcion = 0;
    }

    switch (opcion)
    {
        case 1:

            Console.Clear();
            Console.Write("Ingrese el número de la tarjeta: ");
            string numero = Console.ReadLine();

            string marca = IdentificarMarca(numero);
            bool valida = ValidarTarjeta(numero);

            Console.WriteLine();
            Console.WriteLine($"Número : {numero}");
            Console.WriteLine($"Marca  : {marca}");

            if (valida)
            {
                Console.WriteLine("Estado : ✅ VÁLIDA");
                tarjetasValidas++;

                if (marca == "Visa")
                    totalVisa++;
                else if (marca == "Mastercard")
                    totalMaster++;
                else if (marca == "American Express")
                    totalAmex++;
                else if (marca == "Discover")
                    totalDiscover++;
                else
                    totalDesconocidas++;
            }
            else
            {
                Console.WriteLine("Estado : ❌ INVÁLIDA");
                tarjetasInvalidas++;
            }

            Console.WriteLine();
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();

            break;

        case 2:

            Console.Clear();
            Console.Write("Ruta del archivo: ");
            string ruta = Console.ReadLine();

            ValidarDesdeArchivo(ruta);

            Console.WriteLine();
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();

            break;

        case 3:

            Console.Clear();

            string nueva = GenerarNumeroValido();

            Console.WriteLine("Número generado:");
            Console.WriteLine(nueva);
            Console.WriteLine($"Marca: {IdentificarMarca(nueva)}");

            Console.WriteLine();
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();

            break;

        case 4:

            Console.Clear();

            Console.WriteLine("========== ESTADÍSTICAS ==========");
            Console.WriteLine($"Tarjetas válidas   : {tarjetasValidas}");
            Console.WriteLine($"Tarjetas inválidas : {tarjetasInvalidas}");
            Console.WriteLine();
            Console.WriteLine($"Visa               : {totalVisa}");
            Console.WriteLine($"Mastercard         : {totalMaster}");
            Console.WriteLine($"American Express   : {totalAmex}");
            Console.WriteLine($"Discover           : {totalDiscover}");
            Console.WriteLine($"Desconocidas       : {totalDesconocidas}");

            Console.WriteLine();
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();

            break;

        case 5:

            Console.WriteLine("Gracias por utilizar el programa.");
            break;

        default:

            Console.WriteLine("Opción inválida.");
            Console.ReadLine();
            break;
    }

} while (opcion != 5);



// Función que valida una tarjeta con el algoritmo Luhn
bool ValidarTarjeta(string numero)
{
    if (string.IsNullOrWhiteSpace(numero))
        return false;

    foreach (char c in numero)
    {
        if (!char.IsDigit(c))
            return false;
    }

    int suma = 0;
    bool duplicar = false;

    // Ciclo que recorre la tarjeta de derecha a izquierda
    for (int i = numero.Length - 1; i >= 0; i--)
    {
        int digito = (int)char.GetNumericValue(numero[i]);

        if (duplicar)
        {
            digito *= 2;

            if (digito > 9)
                digito -= 9;
        }

        suma += digito;

        duplicar = !duplicar;
    }

    return suma % 10 == 0;
}


string IdentificarMarca(string numero)
{
    if (string.IsNullOrWhiteSpace(numero))
        return "Desconocida";

    
    if (numero.StartsWith("4") &&
        (numero.Length == 13 || numero.Length == 16))
    {
        return "Visa";
    }

    if (numero.Length == 16)
    {
        int prefijo2 = Convert.ToInt32(numero.Substring(0, 2));

        if (prefijo2 >= 51 && prefijo2 <= 55)
            return "Mastercard";
    }

    if (numero.Length == 15)
    {
        if (numero.StartsWith("34") || numero.StartsWith("37"))
            return "American Express";
    }

    if (numero.Length >= 16 && numero.Length <= 19)
    {
        if (numero.StartsWith("6011"))
            return "Discover";

        if (numero.StartsWith("65"))
            return "Discover";

        int prefijo3 = Convert.ToInt32(numero.Substring(0, 3));

        if (prefijo3 >= 644 && prefijo3 <= 649)
            return "Discover";

        int prefijo6 = Convert.ToInt32(numero.Substring(0, 6));

        if (prefijo6 >= 622126 && prefijo6 <= 622925)
            return "Discover";
    }

    return "Desconocida";
}



// Lee un archivo y valida todas las tarjetas
void ValidarDesdeArchivo(string ruta)
{
    try
    {
        string[] lineas = File.ReadAllLines(ruta);

        int validas = 0;
        int invalidas = 0;

        Console.WriteLine();

        foreach (string tarjeta in lineas)
        {
            string numero = tarjeta.Trim();

            if (numero == "")
                continue;

            bool estado = ValidarTarjeta(numero);
            string marca = IdentificarMarca(numero);

            Console.Write(numero);
            Console.Write(" - ");
            Console.Write(marca);
            Console.Write(" - ");

            if (estado)
            {
                Console.WriteLine("VÁLIDA");
                validas++;

                tarjetasValidas++;

                if (marca == "Visa")
                    totalVisa++;
                else if (marca == "Mastercard")
                    totalMaster++;
                else if (marca == "American Express")
                    totalAmex++;
                else if (marca == "Discover")
                    totalDiscover++;
                else
                    totalDesconocidas++;
            }
            else
            {
                Console.WriteLine("INVÁLIDA");
                invalidas++;
                tarjetasInvalidas++;
            }
        }

        Console.WriteLine();
        Console.WriteLine("========= RESUMEN =========");
        Console.WriteLine($"Válidas   : {validas}");
        Console.WriteLine($"Inválidas : {invalidas}");
    }
    catch (Exception ex)
    {
        Console.WriteLine();
        Console.WriteLine("Error al leer el archivo.");
        Console.WriteLine(ex.Message);
    }
}

string GenerarNumeroValido()
{
    Random random = new Random();

    // Prefijos disponibles
    string[] prefijos =
    {
        "4",      // Visa
        "51",     // Mastercard
        "52",     // Mastercard
        "53",     // Mastercard
        "54",     // Mastercard
        "55",     // Mastercard
        "34",     // American Express
        "37",     // American Express  
        "6011",   // Discover
        "65"      // Discover
    };

    int indice = random.Next(prefijos.Length);

    string prefijo = prefijos[indice];

    int longitud;

    if (prefijo == "34" || prefijo == "37")
        longitud = 15;
    else
        longitud = 16;

    string numero = prefijo;

    // Se generan todos los dígitos excepto el último
    while (numero.Length < longitud - 1)
    {
        numero += random.Next(10);
    }

    // Se calcula el último dígito para que pase Luhn
    int digito = CalcularDigito(numero);

    numero += digito;

    return numero;
}



// Calcula el último dígito usando Luhn
int CalcularDigito(string numero)
{
    for (int i = 0; i <= 9; i++)
    {
        string prueba = numero + i;

        if (ValidarTarjeta(prueba))
            return i;
    }

    return 0;
}