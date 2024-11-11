using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ProyectoVentanaMysql.ProyectoFerreteria;

namespace ProyectoVentanaMysql.ProyectoConsultaHospital
{
    internal class Metodos
    {
        public static List<Consulta> ObtenerListaConsulta()
        {
            List<Consulta> consultas_l = new List<Consulta>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Consulta";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string? numeroConsulta = reader["numeroConsulta"].ToString();
                            DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                            string? nombreMedico = reader["nombreMedico"].ToString();
                            string? deinpr = reader["deinpr"].ToString();
                            string? procedencia = reader["procedencia"].ToString();
                            Consulta consulta = new Consulta(numeroConsulta, fecha.Date.ToString("dd/MM/yyyy"), nombreMedico, deinpr, procedencia);
                            consultas_l.Add(consulta);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return consultas_l;

            }
        }
        public static List<string> ObtenerMedicosComboBox()
        {
            List<string> medicos_l = new List<string>();

            // Obtención de la conexión
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT DISTINCT nombreMedico FROM Consulta ORDER BY nombreMedico ASC";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);

                        // Ejecución del comando y lectura de resultados
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            medicos_l.Add("");
                            while (reader.Read())
                            {
                                // Verifica si el valor de 'nombreMedico' no es DBNull
                                if (reader["nombreMedico"] != DBNull.Value)
                                {
                                    string nombreMedico = reader["nombreMedico"].ToString();
                                    medicos_l.Add(nombreMedico);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return medicos_l;
        }

        public static List<Consulta> ObtenerListaConsultaMedicos(string NombreMedico)
        {
            List<Consulta> consultas_l = new List<Consulta>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Consulta WHERE nombreMedico = @nombreMedico_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombreMedico_p", NombreMedico);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string? numeroConsulta = reader["numeroConsulta"].ToString();
                            DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                            string? deinpr = reader["deinpr"].ToString();
                            string? procedencia = reader["procedencia"].ToString();
                            Consulta consulta = new Consulta(numeroConsulta, fecha.Date.ToString("dd/MM/yyyy"), NombreMedico, deinpr, procedencia);
                            consultas_l.Add(consulta);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return consultas_l;

            }
        }
        //Para OPCIONES
        public static List<int> ObtenerListaYear()
        {
            List<int> anios_li = new List<int>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT DISTINCT YEAR(fecha) as year1 FROM Consulta";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int year = reader.GetInt32("year1");
                                anios_li.Add(year);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return anios_li;
           }
        }
        public static List<Consulta> ObtenerListaConsultaPorYear(int year)
        {
            List<Consulta> consultas_l = new List<Consulta>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Consulta WHERE YEAR(fecha) = @fecha_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@fecha_p", year);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string? numeroConsulta = reader["numeroConsulta"].ToString();
                            DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                            string? nombreMedico = reader["nombreMedico"].ToString();
                            string? deinpr = reader["deinpr"].ToString();
                            string? procedencia = reader["procedencia"].ToString();
                            Consulta consulta = new Consulta(numeroConsulta, fecha.Date.ToString(), nombreMedico, deinpr, procedencia);
                            consultas_l.Add(consulta);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return consultas_l;

            }
        }
        public static List<string> ObtenerListaTiposParto()
        {
            List<string> partos_lo = new List<string>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT DISTINCT deinpr FROM Consulta";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string? partos = reader["deinpr"].ToString();
                                partos_lo.Add(partos);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return partos_lo;
            }
        }
        public static List<Consulta> ObtenerListaConsultaPorPartos(string parto)
        {
            List<Consulta> consultas_l = new List<Consulta>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Consulta WHERE deinpr = @deinpr_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@deinpr_p", parto);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string? numeroConsulta = reader["numeroConsulta"].ToString();
                            DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                            string? nombreMedico = reader["nombreMedico"].ToString();
                            string? deinpr = reader["deinpr"].ToString();
                            string? procedencia = reader["procedencia"].ToString();
                            Consulta consulta = new Consulta(numeroConsulta, fecha.Date.ToString(), nombreMedico, deinpr, procedencia);
                            consultas_l.Add(consulta);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return consultas_l;

            }
        }
        public static List<Consulta> ObtenerListaConsultaPorPartosYear(string parto, int year)
        {
            List<Consulta> consultas_l = new List<Consulta>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Consulta WHERE deinpr = @deinpr_p AND YEAR(fecha) = @fecha_p ";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@deinpr_p", parto);
                        cmd.Parameters.AddWithValue("@fecha_p", year);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string? numeroConsulta = reader["numeroConsulta"].ToString();
                            DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                            string? nombreMedico = reader["nombreMedico"].ToString();
                            string? deinpr = reader["deinpr"].ToString();
                            string? procedencia = reader["procedencia"].ToString();
                            Consulta consulta = new Consulta(numeroConsulta, fecha.Date.ToString(), nombreMedico, deinpr, procedencia);
                            consultas_l.Add(consulta);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return consultas_l;

            }
        }
    }
}
