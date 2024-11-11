using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace ProyectoVentanaMysql.Factoria
{
    public class Metodos
    {

            // Método para obtener productos para el ComboBox
            public static List<string> ObtenerProductosComboBox()
            {
                List<string> productos_l = new List<string>();

                using (MySqlConnection conexion = Conexion.ObtenerConexion())
                {
                    if (conexion != null)
                    {
                        try
                        {
                            string query = "SELECT DISTINCT idFabrica FROM Producto";
                            MySqlCommand cmd = new MySqlCommand(query, conexion);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                productos_l.Add(""); // Agregar opción vacía

                                while (reader.Read())
                                {
                                    if (reader["idFabrica"] != DBNull.Value)
                                    {
                                        string idFabrica = reader["idFabrica"].ToString();
                                        productos_l.Add(idFabrica);
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
                Console.WriteLine($"Productos obtenidos: {string.Join(", ", productos_l)}");
                return productos_l;
            }

            // Método para obtener la lista de productos según el idFabrica
            public static List<Producto> ObtenerListaProductos(string idFabrica)
            {
                List<Producto> productos_l = new List<Producto>();

                using (MySqlConnection conexion = Conexion.ObtenerConexion())
                {
                    if (conexion != null)
                    {
                        try
                        {
                            string query = "SELECT * FROM Producto WHERE idFabrica = @idFabrica";
                            MySqlCommand cmd = new MySqlCommand(query, conexion);
                            cmd.Parameters.AddWithValue("@idFabrica", idFabrica);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string idProducto = reader["idProducto"]?.ToString();
                                    string descripcion = reader["descripcion"]?.ToString();

                                    if (double.TryParse(reader["precio"]?.ToString(), out double precio) &&
                                        int.TryParse(reader["existencia"]?.ToString(), out int existencia))
                                    {
                                        Producto producto = new Producto(idFabrica, idProducto, descripcion, precio, existencia);
                                        productos_l.Add(producto);
                                    }
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
                Console.WriteLine($"Cantidad de productos obtenidos: {productos_l.Count}");
                return productos_l;
            }
        }

    }
