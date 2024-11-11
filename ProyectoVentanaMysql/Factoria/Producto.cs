using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.Factoria
{
    public class Producto
    {
        public string IdFabrica {  get; set; }
        public string IdProducto {  get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }

        public Producto(string idFabrica, string idProducto, string descripcion, double precio, int existencia)
        {
            IdFabrica = idFabrica;
            IdProducto = idProducto;
            Descripcion = descripcion;
            Precio = precio;
            Existencia = existencia;
        }
    }
}
