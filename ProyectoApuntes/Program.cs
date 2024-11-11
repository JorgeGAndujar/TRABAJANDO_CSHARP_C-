namespace ProyectoApuntes
{
    public class Program
    {
        static void Main2(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    public class Principal
    {
        static void Main(string[] args)
        {
            //VALIDAR LA EDAD DE UNA PERSONA [0-150]
            int edad;
            bool correcto;
            do
            {
                Console.Write("Ingresar edad? ");
                edad = int.Parse(Console.ReadLine());
                correcto = edad > 0 && edad <= 150; // es falso??, si es falso TERMINA
                if (!correcto)
                {
                    Console.WriteLine("edad no válida");
                }
            }
            while (!correcto);//VERDADERO REPITE,FALSO TERMINA
            {
                Console.WriteLine("EDAD CORRECTA: " + edad);
            }
            

        }
    }
}
