using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using ProyectoVentanaMysql.Factoria;

namespace ProyectoVentanaMysql.Examen
{
    public class Metodos
    {
        // Método para obtener productos para el ComboBox
        public static List<string> ObtenerEmpresasComboBox()
        {
            List<string> empresas_l = new List<string>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT DISTINCT empresa FROM Cliente";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            empresas_l.Add(""); // Agregar opción vacía

                            while (reader.Read())
                            {
                                if (reader["empresa"] != DBNull.Value)
                                {
                                    string empresa = reader["empresa"].ToString();
                                    empresas_l.Add(empresa);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Error al obtener productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Depuración: Verifica los productos obtenidos
            Console.WriteLine($"Productos obtenidos: {string.Join(", ", empresas_l)}");
            return empresas_l;
        }
        // Método para obtener la lista de productos según el idFabrica
        public static List<Pedido> ObtenerListaPedido(string empresa)
        {
            List<Pedido> pedidos_l = new List<Pedido>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"SELECT * FROM Pedido p
                                         JOIN Cliente c ON p.idCliente = c.idCliente
                                         WHERE c.empresa = @empresa;";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@empresa", empresa);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idPedido = Convert.ToInt32(reader["idPedido"]?.ToString());
                                int idVendedor = Convert.ToInt32(reader["idVendedor"]?.ToString());
                                int idCliente = Convert.ToInt32(reader["idCliente"]?.ToString());
                                string? idFabrica = reader["idFabrica"]?.ToString();
                                string? idProducto = reader["idProducto"]?.ToString();
                                DateTime fechaPedido = Convert.ToDateTime(reader["fecha_pedido"]?.ToString());
                                int cantidad = Convert.ToInt32(reader["cantidad"]?.ToString());
                                double importe = Convert.ToDouble(reader["importe"]?.ToString());
                                Pedido pedido = new Pedido(idPedido, idVendedor, idCliente, idFabrica, idProducto, (fechaPedido.ToString()), cantidad, importe);
                                pedidos_l.Add(pedido);

                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Error al obtener la lista de productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Depuración: Verifica la cantidad de productos obtenidos
            Console.WriteLine($"Cantidad de productos obtenidos: {pedidos_l.Count}");
            return pedidos_l;
        }

        // PARA CONSULTAR AÑOS
        // Método para obtener productos para el ComboBox
        public static List<int> ObtenerAniosComboBox()
        {
            List<int> anios_l = new List<int>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT DISTINCT YEAR(fecha_pedido) AS ANIO FROM Pedido";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            anios_l.Add(0); // Agregar opción vacía

                            while (reader.Read())
                            {
                                if (reader["ANIO"] != DBNull.Value)
                                {
                                    int anios = Convert.ToInt32(reader["ANIO"].ToString());
                                    anios_l.Add(anios);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Error al obtener productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Depuración: Verifica los productos obtenidos
            Console.WriteLine($"Productos obtenidos: {string.Join(", ", anios_l)}");
            return anios_l;
        }
        public static List<Pedido> ObtenerListaPedidoPorAnio(int anio)
        {
            List<Pedido> pedidos_l = new List<Pedido>();

            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"SELECT * FROM Pedido 
                                         WHERE YEAR(fecha_pedido) = @fecha_p;";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@fecha_p", anio);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idPedido = Convert.ToInt32(reader["idPedido"]?.ToString());
                                int idVendedor = Convert.ToInt32(reader["idVendedor"]?.ToString());
                                int idCliente = Convert.ToInt32(reader["idCliente"]?.ToString());
                                string? idFabrica = reader["idFabrica"]?.ToString();
                                string? idProducto = reader["idProducto"]?.ToString();
                                DateTime fechaPedido = Convert.ToDateTime(reader["fecha_pedido"]?.ToString());
                                int cantidad = Convert.ToInt32(reader["cantidad"]?.ToString());
                                double importe = Convert.ToDouble(reader["importe"]?.ToString());
                                Pedido pedido = new Pedido(idPedido, idVendedor, idCliente, idFabrica, idProducto, (fechaPedido.ToString()), cantidad, importe);
                                pedidos_l.Add(pedido);
                                

                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Error al obtener la lista de productos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Depuración: Verifica la cantidad de productos obtenidos
            Console.WriteLine($"Cantidad de productos obtenidos: {pedidos_l.Count}");
            return pedidos_l;
        }

    }

}

