class CineUES
{
    static void Main(string[] args)
    {
        //Declaracion de la matriz inicial
        char[,] Asientos = //Para los char se utiliza 'comillas simples' no se puede con "comillas dobles"
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
        do
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("!Bienvenido a CineUES¡");
            Console.WriteLine("----------------------");
            Console.WriteLine("¿Que desea hacer?");
            Console.WriteLine("1.Mostrar Mapa de la sala. 2.Reservar asiento. 3.Comprar e imprimir ticket. 4.Cancelar reserva. 5.Mostrar estadísticas de la función. 6.Salir.");
            if (int.TryParse(Console.ReadLine(), out Quehacer)) //Con tryparse nos aeguramos que unicamnete ingrese numeros enteros
            {

                switch (Quehacer) //Realizamos acciones segun que se ha escogido
                {
                    case 1:
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Sala CineUES");
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
}