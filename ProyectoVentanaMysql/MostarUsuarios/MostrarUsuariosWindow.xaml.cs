using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace ProyectoVentanaMysql.MostarUsuarios
{
    /// <summary>
    /// Lógica de interacción para MostrarUsuariosWindow.xaml
    /// </summary>
    public partial class MostrarUsuariosWindow : Window
    {
        public MostrarUsuariosWindow()
        {
            InitializeComponent();
        }

        private void BtnRecargar_Click(object sender, RoutedEventArgs e)
        {
            CargarUsuariosDataGrid();
        }

        private void BtnLimpiarTabla_Click(object sender, RoutedEventArgs e)
        {
            dgUsuarios.ItemsSource = null;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public static MySqlConnection ObtenerConexion()
        {
            string conexionUrl = "Server=localhost;Database=ferreteria;Uid=root;Pwd=12345678;Port=3307";
            MySqlConnection conexion = new MySqlConnection(conexionUrl);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (MySqlException ex)
            {
                return null;
            }
        }
        public void CargarUsuariosDataGrid()
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Usuario";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        List<dynamic> usuarios_l = new List<dynamic>();

                        while (reader.Read())
                        {
                            usuarios_l.Add(new
                            {
                                id_usuario = reader["id_usuario"],
                                nombre = reader["nombre_usuario"],
                                clave = reader["contrasena"],
                                rol = reader["rol"]
                            });

                        }
                        dgUsuarios.ItemsSource = usuarios_l;

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("CONEXIÓN", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
