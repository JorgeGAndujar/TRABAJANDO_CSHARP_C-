class Principal
{
    static void Main()
    {
        {
          double x = 1.72; //DENTRO DEL TECLADO (SI LO INTRODUCES CON ,) PERO EN EL PROGRAMA CON .
          int y = (int)x;
          Console.WriteLine("X: " + y);
        }

        {
          float x = 1.72f; //DENTRO DEL TECLADO (SI LO INTRODUCES CON ,) PERO EN EL PROGRAMA CON .
          int y = (int)x;
          Console.WriteLine("X: " + y);
        }

        {
          int numerador = 2;
          int denominador = 3;
          // Si el numerador y el cociente es entero el resultado siempre es entero, aunque sea con fragciones
          double cociente = numerador / (double)denominador;// con el cast indicas q sea punto flotante
          double cociente1 = Math.Round(numerador / (double)denominador,2);// redondeado
          
          Console.WriteLine("Cociente: " + cociente);
          Console.WriteLine("Cociente: " + cociente1);

            Console.ReadLine();

        }
    }
}
