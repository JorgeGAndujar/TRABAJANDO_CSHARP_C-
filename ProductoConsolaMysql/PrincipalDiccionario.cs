using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductoConsolaMysql
{
    internal class PrincipalDiccionario
    {
        static void Main(string[] args)
        {
            Ejemplo3();
        }

        public static void Ejemplo1()
        {
            //CADA ELEMENTO DEL DICCIONARIO TIENEN DOS VALORES CLAVE Y VALOR
            Dictionary<string, string> paises_d = new Dictionary<string, string>();

            paises_d.Add("España", "Madrid");
            paises_d.Add("Francia", "París");

            foreach (var paises in paises_d)
            {
                Console.WriteLine(paises);//RETORNA CLAVE Y VALOR
                Console.WriteLine(paises.Key);// RETORNA SOLO LA CLAVE
                Console.WriteLine(paises.Value);// RETORNA LO QUE GUARDA LA CLAVE(VALOR)
            }

        }
        public static void Ejemplo2()
        {
            //CADA ELEMENTO DEL DICCIONARIO TIENEN DOS VALORES CLAVE Y VALOR
            Dictionary<string, string> paises_d = new Dictionary<string, string>
            {
                { "España", "Madrid" },
                { "Francia", "París" }
            };
            foreach (var paises in paises_d)
            {
                Console.WriteLine(paises);
            }            
        }
        public static void Ejemplo3()
        {
            //CADA ELEMENTO DEL DICCIONARIO TIENEN DOS VALORES CLAVE Y VALOR
            Dictionary<int, Paises> objetos_d = new Dictionary<int, Paises>
            {
                { 1, new Paises {Pais="España", Capital="Madrid" } },
                { 2, new Paises {Pais="Francia", Capital="París" } }
            };
            foreach (var paises in objetos_d)
            {
                Console.WriteLine(paises.Key);
                Console.WriteLine(paises.Value.Pais);
                Console.WriteLine(paises.Value.Capital);
            }
        }
    }
    public class Paises
    {
        public string Pais { get; set; }
        public string Capital { get; set; }
    }
}
