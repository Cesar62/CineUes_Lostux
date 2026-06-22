using System.Security.Cryptography.X509Certificates;

class CineUES
{
    static void Main(string[] args)
    {
        //Declaracion de la matriz inicial 
        char[,] sala = //Para los char se utiliza 'comillas simples' no se puede con "comillas dobles"
        {
            {'-','1', '2', '3', '4', '5', '6', '7', '8'}, //Añado una fila y una columna mas para las marcas de filas con letras y las columnas con numeros
            {'A','L', 'L', 'L', 'L', 'L', 'L', 'L', 'L'},
            {'B','L', 'L', 'L', 'L', 'L', 'L', 'L', 'L'},
            {'C','L', 'L', 'L', 'L', 'L', 'L', 'L', 'L'},
            {'D','L', 'L', 'L', 'L', 'L', 'L', 'L', 'L'},
            {'E','L', 'L', 'L', 'L', 'L', 'L', 'L', 'L'},
            {'F','L', 'L', 'L', 'L', 'L', 'L', 'L', 'L'}
        };

        //Variables
        bool Menu = true; //variable para el bucle de menu
        int Quehacer;  //Variable que guardara la opcion a elegir por el usuario
        double acumulador = 0.00;
        int contador = 0;
        do
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("!Bienvenido a CineUES¡");
            Console.WriteLine("----------------------");
            Console.WriteLine("¿Que desea hacer?");
            Console.WriteLine("1.Mostrar Sala de la sala. 2.Reservar asiento. 3.Comprar e imprimir ticket. 4.Cancelar reserva. 5.Mostrar estadísticas de la función. 6.Salir.");
            if (int.TryParse(Console.ReadLine(), out Quehacer)) //Con tryparse nos aeguramos que unicamnete ingrese numeros enteros
            {

                switch (Quehacer) //Realizamos acciones segun que se ha escogido
                {
                    case 1: //Muestra los asientos del cine
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Sala CineUES");
                        Console.WriteLine("----------------------");
                        MostrarMapa(sala);
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Presione una tecla para continuar");
                        Console.ReadKey();
                        break;

                    case 2: //Aqui ira la funcion de reservar asiento
                        break;

                    case 3: //Aqui ira la fucion de comprar e imprimir el tickect
                            ComprarTicket(ref sala, ref acumulador, ref contador); //Se llama la funcion comprar ticket y las variables que se le referencian
                        break;

                    case 4: //Aqui ira la funcion de cancelar la reserva
                        break;

                    case 5: // Aqui se mostraran las estadisiticas de la la funcion
                        break;

                    case 6:
                        Menu = false;
                        break;

                    default: //Esto por si ingresa un valor que no esta en el menu
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Ha ingresado una opción invalida");
                        Console.WriteLine("Presione una tecla para continuar");
                        Console.ReadKey();
                        break;
                }

            }
            else //Si ingresa un valor invalido se muestra un mensaje
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Ha ingresado una opción invalida");
                Console.WriteLine("Presione una tecla para continuar");
                Console.ReadKey();
            }
        } while (Menu);
    }

    static void MostrarMapa(char[,] Sala) //Funcion que mostrara los asientos
    {
        for (int fila = 0; fila < Sala.GetLength(0); fila++) //Creamos el primer bucle que ira mostrando las filas
        {
            for (int columna = 0; columna < Sala.GetLength(1); columna++) //Este bucle ira imprimiendo los valores tomando la fila y la columna
            {
                Console.Write(Sala[fila, columna] + " ");
            }

            //Cuando termine de recorrer las columnas pone un espacio para separar y repite hasta que se termine las filas
            Console.WriteLine("");
        }
    }

    //constantes
    const double PRECIO_BASE = 5.00;
    const double IVA = 0.13;
    const double RECARGO_VIP = 2.50;
    const double DESCUENTO_ESTUDIANTE = 0.10;

    static void ComprarTicket(ref char[,] sala, ref double acumulador, ref int contador)
    {
        Console.Write("Ingrese fila (A-F): ");
        char filaChar = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        Console.Write("Ingrese columna (1-8): ");
        if (!int.TryParse(Console.ReadLine(), out int columna) || columna < 1 || columna > 8)
        {
            Console.WriteLine("Columna inválida.");
            return;
        }

        int filaIdx = filaChar - 'A';

        if (filaIdx < 0 || filaIdx > 5)
        {
            Console.WriteLine("Fila inválida.");
            return;
        }

        if (sala[filaIdx, columna - 1] != 'R')
        {
            Console.WriteLine("El asiento no está reservado.");
            return;
        }

        Console.Write("Categoría (1=Normal, 2=Estudiante, 3=VIP): ");
        if (!int.TryParse(Console.ReadLine(), out int categoria) || categoria < 1 || categoria > 3)
        {
            Console.WriteLine("Categoría inválida.");
            return;
        }

        Console.Write("Día (1=Lunes...7=Domingo): ");
        if (!int.TryParse(Console.ReadLine(), out int dia) || dia < 1 || dia > 7)
        {
            Console.WriteLine("Día inválido.");
            return;
        }

        double precio = CalcularPrecio(filaChar, categoria, dia);
        sala[filaIdx, columna - 1] = 'V';
        acumulador += precio;
        contador++;

        ImprimirTicket(filaChar, columna, categoria, dia, precio);
    }

    static double CalcularPrecio(char fila, int categoria, int dia)
    {
        double precio = PRECIO_BASE;

        if (fila == 'C' || fila == 'D')
            precio += RECARGO_VIP;

        if (categoria == 2)
            precio -= precio * DESCUENTO_ESTUDIANTE;

        if (dia == 6 || dia == 7)
            precio -= precio * 0.05;

        precio += precio * IVA;
        return Math.Round(precio, 2);
    }

    static void ImprimirTicket(char fila, int columna, int categoria, int dia, double precio)
    {
        string[] cats = { "", "Normal", "Estudiante", "VIP" };
        string[] dias = { "", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

        Console.WriteLine("\n=============================");
        Console.WriteLine("        TICKET DE CINE       ");
        Console.WriteLine("=============================");
        Console.WriteLine($"  Asiento : Fila {fila}, Columna {columna}");
        Console.WriteLine($"  Categoría: {cats[categoria]}");
        Console.WriteLine($"  Día      : {dias[dia]}");
        Console.WriteLine($"  Total    : ${precio:F2}");
        Console.WriteLine("=============================\n");
    }

}

