using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVentanaMysql.Examen
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Empresa { get; set; }
        public int IdVendedor { get; set; }
        public double LimiteCredito { get; set; }

        public Cliente(int idCliente, string empresa, int idVendedor, double limiteCredito)
        {
            IdCliente = idCliente;
            Empresa = empresa;
            IdVendedor = idVendedor;
            LimiteCredito = limiteCredito;
        }
    }
}
