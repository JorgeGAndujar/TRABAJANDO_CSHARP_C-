using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

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
    }
}
