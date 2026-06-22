using System.Diagnostics.CodeAnalysis;
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
            Console.WriteLine("1.Mostrar Sala de la sala.");
            Console.WriteLine("2. Reservar asiento");
            Console.WriteLine("3. Comprara e imprimir ticket");
            Console.WriteLine("4. Cancelar reserva");
            Console.WriteLine("5. Mostrar estadisticas de la función");
            Console.WriteLine("6. Salir");


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
                        MostrarMapa(sala);
                        reservarAsiento(ref sala);
                        Console.WriteLine("Presione enter para continuar");
                        Console.ReadKey();
                        break;

                    case 3: //Aqui ira la fucion de comprar e imprimir el tickect
                        ComprarTicket(ref sala, ref acumulador, ref contador); //Se llama la funcion comprar ticket y las variables que se le referencian
                        break;

                    case 4: //Aqui ira la funcion de cancelar la reserva
                        MostrarMapa(sala);
                        cancelarReserva(ref sala);
                        Console.WriteLine("Presione enter para continuar");
                        Console.ReadKey();
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

    //la funcion para reservar el asiento
    //recibe la matriz por referencia (ref) para poder modificarla directamente
    static void reservarAsiento(ref char[,] sala)
    {
        Console.WriteLine("Reservar Asiento");

        // bandera que servirá para controlar si los datos ingresados son válidos
        // si paso algun error lo va a cambiar a false
        bool datosValidos = true;

        //solicita la fila
        Console.Write("Ingrese la fila por favor (A-F)");
        //lee un caracter para convertirlo en mayuscula, no importa si escribe a o A
        char filaIngresada = char.ToUpper(Console.ReadKey().KeyChar); //Readkey espera que el usuario presione una tecla y keychar toma el resultado
        Console.WriteLine();

        //validariamos la fila pero solo se puede con letras de la A a la F
        if (filaIngresada < 'A' || filaIngresada > 'F')
        {
            Console.WriteLine("Esa fila no es valida");

            //la bandera pasa a falso porque hubo un error
            datosValidos = false;
        }

        int columnaIngresada = 0;//en esta variable guardo la columna que selecciono

        //aqui se solicita la columna solamente si la fila fue válida        
        {
            Console.Write("Ingrese la columna porfavor (1-8)");

            //esta para convertir lo de entrada a entero
            if (!int.TryParse(Console.ReadLine(), out columnaIngresada))
            {
                Console.WriteLine("La columna debe ser un numero");

                //si no es un numero la bandera va pasar a false
                datosValidos = false;
            }

            // este es para verificar que el numero este entre 1 y 8
            else if (columnaIngresada < 1 || columnaIngresada > 8)
            {
                Console.WriteLine("Esa columna no es valida");

                //si esta fuera del rango es invalido
                datosValidos = false;
            }
            // si algun dato no era correcto se termina y regresa al menu principal
            if (!datosValidos)
            {
                return;
            }

            //se le sumara 1 porque la fila 0 es la que tiene los encabezados
            int fila = filaIngresada - 'A' + 1;

            // la columna coincide directamente con los índices de la matriz,
            // porque la columna 0 contiene las letras de las filas.
            int columna = columnaIngresada;

            //verificar si el asiento esta libre osea los que esten en L
            if (sala[fila, columna] != 'L')
            {
                Console.WriteLine("Este asiento ya esta ocupado");

                //terminaria el metodo sin modificar la matriz
                return;
            }

            //pero si el asiento esta libre va a cambiar el estado
            sala[fila, columna] = 'R';
            Console.WriteLine($"Asiento {filaIngresada} {columnaIngresada} fueron reservados correctamente");
        }
    }

    //esta es la funcio para quitar la reserva del asiento
    //recibe la matriz por referencia para porder modificarla
    static void cancelarReserva(ref char[,] sala)
    {
        Console.WriteLine("Cancelar la reserva");

        //para poder controlar si los datos que ingreso son validos
        bool datosValidos = true;

        Console.WriteLine("Ingrese la fila del asiento reservado (A-F): ");

        //ahora aqui convierte la letra ingresada a mayuscula 
        char filaIngresada = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        //ahora toca verificar que la fila este entre A y F
        if (filaIngresada < 'A' || filaIngresada > 'F' )
        {
            Console.WriteLine("Esa fila no es valida");
            datosValidos = false;
        }

        //esta es la variable que guarda la columna
        int columnaIngresada = 0;

        //solo solicita la columna si fue valida 
        if (datosValidos)
        {
            Console.WriteLine("Ingres la columna del asiento por favor (1-8): ");

            //valida que el numero sea entero
            if (!int.TryParse(Console.ReadLine(), out columnaIngresada))
            {
                Console.WriteLine("La columna deber ser un numero!!");
                datosValidos = false;
            }

            //verificar que este dentro del rango
            else if (columnaIngresada < 1 || columnaIngresada > 8)
            {
                Console.WriteLine("Esa columna no es valida");
                datosValidos = false;
            }
        }

        //en el caso que haiga un error va terminar el metodo
        if (!datosValidos)
        {
            return;
        }

        //conversion de la letra a indice de la fila
        int fila = filaIngresada - 'A' + 1;

        //la columna coincide con el indice de la matriz
        int columna = columnaIngresada;

        //verificar que el asiento este reservado
        if (sala[fila, columna] != 'R')
        {
            Console.WriteLine("Este asiento no esta reservado");
            return;
        }

        //cancelar la reserva "R" y volver a ponerla libre "L"
        sala[fila,columna] = 'L';
        Console.WriteLine($"La reserva del asiento {filaIngresada} {columnaIngresada} fueron cancelados"); 
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

