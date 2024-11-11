using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysqlx.Cursor;

namespace ProyectoVentanaMysql.Factoria
{
    public class Vendedor
    {
        public int IdVendedor {  get; set; }
        public string Nombre { get; set; }
        public int Edad {  get; set; }
        public int IdOficina { get; set; }
        public string Titulo { get; set; }
        public string Contrato { get; set; }
        public double Cuota { get; set; }
        public double Ventas { get; set; }
        public int IdVendedorJefe {  get; set; }
        public Vendedor(int idVendedor, string nombre, int edad, int idOficina, string titulo, string contrato, double cuota, double ventas, int idVendedorJefe)
        {
            IdVendedor = idVendedor;
            Nombre = nombre;
            Edad = edad;
            IdOficina = idOficina;
            Titulo = titulo;
            Contrato = contrato;
            Cuota = cuota;
            Ventas = ventas;
            IdVendedorJefe = idVendedorJefe;
        }
    }
}
