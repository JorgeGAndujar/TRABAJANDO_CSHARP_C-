using MySql.Data.MySqlClient;
using System.Windows;

namespace ProyectoVentanaMysql
{
    public partial class MainWindow : Window
    {
        // Declaramos la cadena de conexión aquí
        string conexionUrl = "Server=localhost;Database=ferreteria;Uid=root;Pwd=12345678;Port=3307";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idProducto = int.Parse(txtIdProducto.Text);

                try
                {
                    // Usamos la cadena de conexión declarada arriba
                    MySqlConnection conexion = new MySqlConnection(conexionUrl);
                    conexion.Open();
                    Console.WriteLine("OK: CONEXION");

                    string query = "SELECT nombre FROM Producto WHERE id_producto = @idProducto";
                    MySqlCommand comando = new MySqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@idProducto", idProducto);
                    MySqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombreProducto.Text = "" + reader["nombre"];
                    }else
                    {
                        MessageBox.Show("NO EXISTE ESE ID", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Captura errores específicos de la conexión
                    MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("NO INGRESASTES UN ENTERO", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdProducto.Clear();
            txtNombreProducto.Clear();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
