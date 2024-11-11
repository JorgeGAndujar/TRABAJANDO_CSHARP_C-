using System;
class Principal2
{
    // Comentario de una linea
        /*
        Comentario de varias líneas
        */
    static void Main()
    {
        Console.Clear();
       //DEFINIR TIPOS DE VARIABLES
        double numero1, numero2, suma;
       //ENTRADA
        Console.Write("Ingresar numero 1?");
        numero1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Ingresar numero 2?");
        numero2 = Convert.ToDouble(Console.ReadLine());
       //PROCESO
        suma = sumar(numero1,numero2);
       //SALIDA
        Console.WriteLine("Suma: " + Math.Round(suma,2));

        Console.ReadLine();    
    }
       
    // CREAR EL MÉTODO SUMAR
    static double sumar(double n1, double n2){
        return n1 + n2;
    }    
}
