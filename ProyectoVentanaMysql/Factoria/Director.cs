using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.Factoria
{
    public class Director
    { 
        //PROPIEDADES
        public int IdDirector { get; set; }

        public Director(int idDirector) // Id Manual
        {
            IdDirector = idDirector;
        }
    }
}
