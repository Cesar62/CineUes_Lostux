using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
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
        double acumulador = 0.00; //variable que lleva las ganacias
        int contador = 0; //Varieble que lleva los tickest que se han vendido
        do
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("!Bienvenido a CineUES¡");
            Console.WriteLine("----------------------");
            Console.WriteLine("¿Que desea hacer?");
            Console.WriteLine("1.Mostrar Sala de la sala.");
            Console.WriteLine("2. Reservar asiento");
            Console.WriteLine("3. Comprar e imprimir ticket");
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
                        MostrarEstadisticas(sala, contador, acumulador);
                        Console.WriteLine("Presione enter para continuar");
                        Console.ReadKey();
                        break;

                    case 6: // Este es el mensaje de despedida y el resumen final
                        Console.WriteLine("\n------ RESUMEN FINAL ------");
                        Console.WriteLine($"Total de boletos vendidos: {contador}");
                        Console.WriteLine($"Recaudación total        : ${acumulador:F2}");
                        if (contador > 0)
                        {
                            Console.WriteLine($"Promedio por boleto      : ${acumulador / contador:F2}");
                        }
                        Console.WriteLine("\n¡Gracias por usar CineUES!");
                        Console.WriteLine("-------------------------\n");
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
        for (int fila = 0; fila < Sala.GetLength(0); fila++) //Creamos el primer bucle que ira mostrando las filas en este bucle se va repitiendo hasta que pasa por todas las filas en este caso 7 ya que una es la fila que muestra los numeros de las columnas
        {
            for (int columna = 0; columna < Sala.GetLength(1); columna++) //Este bucle ira imprimiendo los valores tomando la fila y la columna Se entra va repitiendo el bucle por cada columna
            {
                Console.Write(Sala[fila, columna] + " ");
            }

            //Cuando termine de recorrer las columnas pone un espacio para separar y repite hasta que se termine las filas
            Console.WriteLine("");
        }
    }

    //Funcion para reservar los asientos
    //Se usa ref para actualizar la matriz original
    static void reservarAsiento(ref char[,] sala)
    {
        Console.WriteLine("Reservar Asiento");

        // bandera para ver si los datos son correctos
        //si pasa un error se pone en false
        bool datosValidos = true;

        //Se pide la fila
        Console.Write("Ingrese la fila por favor (A-F)");
        //con ToUpper se convierte la letra en mayuscula
        char filaIngresada = char.ToUpper(Console.ReadKey().KeyChar); //Readkey espera que el usuario presione una tecla y keychar toma el resultado no espera a que el usuario de enter
        Console.WriteLine();

        //Se verifica si la letra ingresada esta desde la A a la F
        if (filaIngresada < 'A' || filaIngresada > 'F')
        {
            Console.WriteLine("Esa fila no es valida");

            //la bandera pasa a falso porque hubo un error
            datosValidos = false;
        }

        int columnaIngresada = 0;//en esta variable guardo la columna que selecciono


        if (datosValidos) //Asi se evita pedir columna si ya se equivoco de fila
        {
            //Aqui se pide que ingrese la columna
            Console.Write("Ingrese la columna porfavor (1-8)");

            //Con tryparse se verifica que lo que se ingrese es un entero
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

        }
        // si algun dato no era correcto se termina y regresa al menu principal
        if (!datosValidos)
        {
            return;
        }

        //en C# y en varios lenguajes basados en C los char son codigo ASCCI
        //Entonces la A en ascci es 65
        //Enonces fila ingresada toma el valor de la letra en ascii por ejemplo la b es 66
        //Por lo que 66 -65 = 1 pero como la b esta en la fila 2 contando desde 0 en la matriz
        //se le suma 1 para compensar
        int fila = filaIngresada - 'A' + 1;

        //no hay problema en poner coumna 1 porque en este caso si empiezan desde ahi por lo que se usa la misma directamente
        int columna = columnaIngresada;

        //verificar si el asiento esta libre osea los que esten en L
        if (sala[fila, columna] != 'L')  //Se revisa en la matriz ingresando los datos de la fila y columna
        {
            Console.WriteLine("Este asiento ya esta ocupado");

            //Si esta ocupado se sale de la funcion y no se continua
            return;
        }

        //pero si el asiento esta libre va a cambiar el estado
        sala[fila, columna] = 'R';
        Console.WriteLine($"Asiento {filaIngresada} {columnaIngresada} fueron reservados correctamente");

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
        //Por lo mismo de que estan en codigo asccii
        if (filaIngresada < 'A' || filaIngresada > 'F')
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

        //en C# y en varios lenguajes basados en C los char son codigo ASCCI
        //Entonces la A en ascci es 65
        //Enonces fila ingresada toma el valor de la letra en ascii por ejemplo la b es 66
        //Por lo que 66 -65 = 1 pero como la b esta en la fila 2 contando desde 0 en la matriz
        //se le suma 1 para compensar
        int fila = filaIngresada - 'A' + 1;

        //La columna es la misma que se va  a usar asi que no hay problemas
        int columna = columnaIngresada;

        //verificar que el asiento este reservado
        //Solo tomamos el valor de la fila y la columna que obtuvimos y se inserta en la matriz para encontrarla
        //Luego se verifica que no este reservado para comprarlo
        if (sala[fila, columna] != 'R')
        {
            Console.WriteLine("Este asiento no esta reservado");
            return;
        }

        //cancelar la reserva "R" y volver a ponerla libre "L"
        sala[fila, columna] = 'L';
        Console.WriteLine($"La reserva del asiento {filaIngresada} {columnaIngresada} fueron cancelados");
    }

    //constantes Para compra e imprimir ticket
    const double PRECIO_BASE = 4.00;
    const double IVA = 0.13;
    const double RECARGO_VIP = 2.00;
    const double DESCUENTO_NINO = 0.50;
    const double DESCUENTO_TERCERA = 0.30;
    const double DESCUENTO_MIERCOLES = 0.20;

    // Método: ComprarTicket
    // Solicita asiento, categoría y día al usuario,
    // calcula el precio, marca el asiento como vendido,
    // actualiza acumulador y contador, e imprime el ticket.

    static void ComprarTicket(ref char[,] sala, ref double acumulador, ref int contador)
    {

        // Mostrar el mapa antes de pedir el asiento
        MostrarMapa(sala);

        // Solicitar y validar la FILA 
        Console.Write("Ingrese fila (A-F): ");
        char filaChar = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        // Verificar que la fila esté en el rango A-F
        if (filaChar < 'A' || filaChar > 'F')
        {
            Console.WriteLine("Fila inválida. Solo se aceptan filas A-F.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        // Solicitar y validar la COLUMNA 
        Console.Write("Ingrese columna (1-8): ");
        if (!int.TryParse(Console.ReadLine(), out int columna) || columna < 1 || columna > 8)
        {
            Console.WriteLine("Columna inválida. Solo se aceptan columnas del 1 al 8.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        // Calcular índices reales en la matriz
        // +1 en fila porque la fila 0 tiene los encabezados numéricos
        // columna se usa directo porque la columna 0 tiene las letras de fila
        int filaIdx = filaChar - 'A' + 1;
        int colIdx = columna;

        // Verificar disponibilidad del asiento 
        // Se puede comprar si está Libre (L) o Reservado (R)
        // Solo se rechaza si ya fue Vendido (V)
        if (sala[filaIdx, colIdx] == 'V')
        {
            Console.WriteLine($"El asiento {filaChar}{columna} ya fue vendido. Elija otro.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        // Solicitar y validar la CATEGORÍA 
        // Arreglo auxiliar con los nombres de las categorías (requerido por rúbrica)
        string[] nombresCategorias = { "", "General", "Niño", "Tercera edad" };

        Console.WriteLine("Categoría del cliente:");
        Console.WriteLine("  1 = General. Precio completo");
        Console.WriteLine("  2 = Niño. 50% de descuento");
        Console.WriteLine("  3 = Tercera edad. 30% de descuento");
        Console.Write("Seleccione: ");
        if (!int.TryParse(Console.ReadLine(), out int categoria) || categoria < 1 || categoria > 3)
        {
            Console.WriteLine("Categoría inválida.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        // Solicitar y validar el DÍA 
        // Arreglo auxiliar con los nombres de los días
        string[] nombresDias = { "", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

        Console.WriteLine("Día de la función:");
        Console.WriteLine("  1=Lunes  2=Martes  3=Miércoles. 20% descuento  4=Jueves");
        Console.WriteLine("  5=Viernes  6=Sábado  7=Domingo");
        Console.Write("Seleccione: ");
        if (!int.TryParse(Console.ReadLine(), out int dia) || dia < 1 || dia > 7)
        {
            Console.WriteLine("Día inválido.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        // Calcular el precio final llamando a CalcularPrecio 
        double precio = CalcularPrecio(filaChar, categoria, dia);

        // Marcar el asiento como VENDIDO en la matriz 
        sala[filaIdx, colIdx] = 'V';

        //  Actualizar variables globales compartidas 
        acumulador += precio;  // Sumar al total recaudado
        contador++;            // Incrementar conteo de boletos

        // Imprimir el ticket formateado 
        ImprimirTicket(filaChar, columna, categoria, dia, precio, nombresCategorias, nombresDias);

        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

    //CalcularPrecio
    // Calcula el precio final aplicando recargo VIP,
    // descuento por categoría, descuento de miércoles e IVA.
    static double CalcularPrecio(char fila, int categoria, int dia)
    {
        double precio = PRECIO_BASE; // Partir del precio base

        // Aplicar recargo si el asiento es de fila C o D (zona central/VIP)
        if (fila == 'C' || fila == 'D')
            precio += RECARGO_VIP;

        // Aplicar descuento según categoría con if-else if (como pide la rúbrica)
        if (categoria == 2)
            precio -= precio * DESCUENTO_NINO;       // Niño: 50% de descuento
        else if (categoria == 3)
            precio -= precio * DESCUENTO_TERCERA;    // Tercera edad: 30% de descuento
                                                     // General (categoria == 1): sin descuento

        // Aplicar descuento adicional si es miércoles (día 3)
        if (dia == 3)
            precio -= precio * DESCUENTO_MIERCOLES;  // 20% adicional

        // Sumar IVA al precio resultante
        precio += precio * IVA;

        return Math.Round(precio, 2); // Redondear a 2 decimales
    }

    //ImprimirTicket
    // Muestra el comprobante de compra con desglose de precio.
    static void ImprimirTicket(char fila, int columna, int categoria, int dia, double precioFinal, string[] categorias, string[] dias)
    {
        // Calcular valores para el desglose
        double precioSinIVA = Math.Round(precioFinal / (1 + IVA), 2);
        double montoIVA = Math.Round(precioFinal - precioSinIVA, 2);

        Console.WriteLine("================================");
        Console.WriteLine("         TICKET CINEUES         ");
        Console.WriteLine("================================");
        Console.WriteLine($"  Asiento   : Fila {fila}, Columna {columna}");
        Console.Write($"  Categoría : {categorias[categoria]}");
        if(categoria == 2) //Agregando mensaje del descuento que tiene
        {
            Console.WriteLine(" Descuento del 50%");
        }else if(categoria == 3)
        {
            Console.WriteLine(" Descuento del 30%");
        }
        else
        {
            Console.WriteLine("");
        }
        Console.Write($"  Día       : {dias[dia]}");
        if(dia == 3){ Console.WriteLine(" !!!Descuento del 20%!!!");}else{Console.WriteLine("");} //mensaje si hay descuento por dia
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"  Subtotal  : ${precioSinIVA:F2}");
        Console.WriteLine($"  IVA (13%) : ${montoIVA:F2}");
        Console.WriteLine($"  TOTAL     : ${precioFinal:F2}");
        Console.WriteLine("================================");
    }
    static void MostrarEstadisticas(char[,] sala, int vendidos, double recaudacion)
    {
        Console.WriteLine("\n----- ESTADISTICAS DE LA SALA -----");

        // manejo de la division por 0 
        if (vendidos == 0)  //mensaje por si no se han vendido boletos
        {
            Console.WriteLine("No se ha vendido ningun boleto");
            Console.WriteLine("Promedio por boleto: $0.00");
        }
        else
        {
            double promedio = recaudacion / vendidos; //para sacar promedio de ganacia de los boletos vendidos se divide la recaudacion entre los boletas
            Console.WriteLine($"Boletos vendidos   : {vendidos}");
            Console.WriteLine($"Recaudación total  : ${recaudacion:F2}");
            Console.WriteLine($"Promedio por boleto: ${promedio:F2}");
        }

        // contador de asientos
        int libres = 0, reservados = 0, comprados = 0;


        //Bucle que va pasando toda la matriz para verificar si el asiento esta vendido, comprado o vacio
        for (int i = 1; i < sala.GetLength(0); i++) //Se repite por cada fila
        {
            for (int j = 1; j < sala.GetLength(1); j++) //Se repite por cada columna
            {
                if (sala[i, j] == 'L') //se hace la suma segun la letra que contien el indice
                {
                    libres++;
                }
                else if (sala[i, j] == 'R')
                {
                    reservados++;
                }
                else if (sala[i, j] == 'V')
                {
                    comprados++;
                }
            }
        }

        //Se imprime resumen del dia
        Console.WriteLine($"Asientos libres    : {libres}");
        Console.WriteLine($"Asientos reservados: {reservados}");
        Console.WriteLine($"Asientos comprados : {comprados}");
        Console.WriteLine($"Total de asientos  : {libres + reservados + comprados}");
        Console.WriteLine("------------------------\n");
    }
}

