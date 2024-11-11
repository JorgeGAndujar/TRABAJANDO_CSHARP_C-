using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleObjetos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ejemplo1();
            Ejemplo2();
            Ejemplo3();
        }

        //EJEMPLO 1 ---> SIN CONSTRUCTOR
        public static void Ejemplo1()
        {
            List<Alumno1> alumnos_l = new List<Alumno1>
            {
                new Alumno1 { Nombre = "Luis", Edad = 41 },
                new Alumno1 { Nombre = "Marta", Edad = 40 },
            };

            foreach (Alumno1 alumno in alumnos_l)
            {
                Console.WriteLine(alumno.Nombre);
                Console.WriteLine(alumno.Edad);
            }
        }
        //EJEMPLO 2 ---> CON CONSTRUCTOR
        public static void Ejemplo2()
        {
            List<Alumno2> alumnos_l = new List<Alumno2>
            {
                new Alumno2("Pablo", 33),
                new Alumno2("Rafa",33),
            };

            foreach (Alumno2 alumno in alumnos_l)
            {
                Console.WriteLine(alumno.Nombre);
                Console.WriteLine(alumno.Edad);
            }
        }
        //EJEMPLO 3 ---> OBJETO ANÓNIMO
        public static void Ejemplo3()
        {
            List<dynamic> alumnos_l = new List<dynamic> // (DYNAMIC) PARA QUE EL PROGRAMA DECIDA Q LISTA ES
            {
                new { Nombre = "Adela", Edad = 98 },
                new { Nombre = "Paloma", Edad = 64 },
            };

            foreach (var alumno in alumnos_l) // (VAR) LO USAS SI NO QUIERES DEFINIR EL TIPO, AGUANTA TODO TIPO
            {
                Console.WriteLine(alumno.Nombre);
                Console.WriteLine(alumno.Edad);
            }
        }
    }
    // sin CONSTRUCTOR
    public class Alumno1
    {
        public string Nombre {  get; set; }
        public int Edad { get; set; }
    }
    // con CONSTRUCTOR
    public class Alumno2
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public Alumno2(string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }

    }
}
