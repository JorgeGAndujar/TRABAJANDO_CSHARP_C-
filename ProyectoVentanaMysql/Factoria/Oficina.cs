using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.Factoria
{
    public class Oficina
    {
        public int IdOficina { get; set; }
        public int IdDirector {  get; set; }
        public string Ciudad {  get; set; }
        public string Region { get; set; }
        public double Objetivo {  get; set; }
        public double Ventas { get; set; }

        public Oficina(int idOficina, int idDirector, string ciudad, string region, double objetivo, double ventas) 
        {
            IdOficina = idOficina;
            IdDirector = idDirector;
            Ciudad = ciudad;
            Region = region;
            Objetivo = objetivo;
            Ventas = ventas;
        }
    }
}
