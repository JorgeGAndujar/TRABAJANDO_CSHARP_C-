using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.Factoria
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdVendedor { get; set; }
        public int IdCliente { get; set; }
        public string IdFabrica { get; set; }
        public string IdProducto { get; set; }
        public string FechaPedido { get; set; }
        public int Cantidad { get; set; }
        public double Importe { get; set; }

        public Pedido (int idPedido, int idVendedor, int idCliente, string idFabrica, string idProducto, string fechaPedido, int cantidad, double importe)
        {
            IdPedido = idPedido;
            IdVendedor = idVendedor;
            IdCliente = idCliente;
            IdFabrica = idFabrica;
            IdProducto = idProducto;
            FechaPedido = fechaPedido;
            Cantidad = cantidad;
            Importe = importe;
        }
    }
}
