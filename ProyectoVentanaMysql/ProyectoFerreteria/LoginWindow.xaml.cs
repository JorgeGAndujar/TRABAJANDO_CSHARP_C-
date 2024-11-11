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

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLoguearse_Click(object sender, RoutedEventArgs e)
        {
            string nombreUsuario = TxtNombreUsuario.Text.Trim();
            string contrasenaIngresada = TxtContrasena.Password.Trim();

            Usuario usuario = MetodosCrud.BuscarUsuario(nombreUsuario);
            if (BCrypt.Net.BCrypt.Verify(contrasenaIngresada, usuario.Contrasena))
            {
                if(usuario.Rol == "Administrador")
                {
                    //LEVANTAR LA VENTANA MENU ADMINISTRADOR
                    MessageBox.Show($"BIENVENIDO {nombreUsuario} rol ADMINISTRADOR", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    MenuAdministradorWindow ventana = new MenuAdministradorWindow(this);
                    ventana.Show();
                    LimpiarCajitas();
                    this.Hide();    
                }
                if (usuario.Rol == "Almacén")
                {
                    //LEVANTAR LA VENTANA MENU ALMACÉN
                    MessageBox.Show($"BIENVENIDO {nombreUsuario} rol ALMACÉN", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    MenuAlmacenWindow ventana = new MenuAlmacenWindow(this);
                    ventana.Show();
                    LimpiarCajitas();
                    this.Hide();

                }
                if (usuario.Rol == "Cajero")
                {
                    //LEVANTAR LA VENTANA MENU CAJERO
                    MessageBox.Show($"BIENVENIDO {nombreUsuario} rol CAJERO", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    MenuCajeroWindow ventana = new MenuCajeroWindow(this);
                    ventana.Show();
                    LimpiarCajitas();

                }
            }
            else
            {
                MessageBox.Show("Contraseña no Válida", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        public void LimpiarCajitas()
        {
            TxtNombreUsuario.Clear();
            TxtContrasena.Clear();
        }
    }
}
