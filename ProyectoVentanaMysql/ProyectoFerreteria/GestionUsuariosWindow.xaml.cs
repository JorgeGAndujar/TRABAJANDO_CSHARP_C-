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
    /// Lógica de interacción para GestionUsuariosWindow.xaml
    /// </summary>
    public partial class GestionUsuariosWindow : Window
    {
        string idUsuario;
        string contraseñaOriginal;
        public GestionUsuariosWindow()
        {
            InitializeComponent();
            CargarDatosDataGrid();
        }
        private void CargarDatosDataGrid()
        {
            List<Usuario> usuarios_l = MetodosCrud.ObtenerListaUsuarios();
            UsuariosDataGrid.ItemsSource = usuarios_l;   
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombreUsuario = TxtNombreUsuario.Text;
            string contrasena = TxtContrasena.Text;
            string rol = TxtRol.Text;
            if (nombreUsuario.Length > 0 && contrasena.Length > 0 && rol.Length > 0)
            {
                if (rol == "Administrador" || rol == "Cajero " || rol == "Almacén")
                {
                    Usuario usuario = new Usuario
                    (
                        "0",
                        TxtNombreUsuario.Text,
                        TxtContrasena.Text,
                        TxtRol.Text
                    );
                    MetodosCrud.AgregarUsuario(usuario);
                    CargarDatosDataGrid();
                    BtnNuevo_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Rol Incorrecto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe llenar TODOS LOS CAMPOS", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {

            string nombreUsuario = TxtNombreUsuario.Text.Trim();
            string contrasena = TxtContrasena.Text.Trim();
            string rol = TxtRol.Text.Trim();//TRIM LIMPIA LOS ESPACIOS EN BLANCO AL PRINCIPIO Y AL FINAL
            MetodosCrud.ActualizarUsuario(new Usuario(idUsuario,nombreUsuario,contrasena,rol));

        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TxtContrasena.Clear();
            TxtNombreUsuario.Clear();
            TxtRol.Clear();
            
        }

        private void UsuariosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(UsuariosDataGrid.SelectedItem is Usuario usuarioSeleccionado)
            {
                idUsuario = usuarioSeleccionado.IdUsuario;
                TxtNombreUsuario.Text = usuarioSeleccionado.NombreUsuario;
                TxtContrasena.Text = usuarioSeleccionado.Contrasena;
                TxtRol.Text = usuarioSeleccionado.Rol;
                
            }
        }
    }

}
