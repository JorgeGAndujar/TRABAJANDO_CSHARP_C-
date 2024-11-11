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
using ProyectoVentanaMysql.GestionUsuarios;

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    /// <summary>
    /// Lógica de interacción para GestionProductosWindow.xaml
    /// </summary>
    public partial class GestionProductosWindow : Window
    {
        string idProducto;
        public GestionProductosWindow()
        {
            InitializeComponent();
            CargarDatosDataGrid();
        }
        private void CargarDatosDataGrid()
        {
            List<Productos> productos_l = MetodosCrud.ObtenerListaProductos();
            ProductosDataGrid.ItemsSource = productos_l;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtNombreProducto.Text;
            string descripcion = TxtDescripcion.Text;
            string precio = TxtPrecio.Text;
            string stock = TxtStock.Text;
            string categoria = TxtCategoria.Text;
            if (nombre.Length > 0 && descripcion.Length > 0 && precio.Length > 0 && stock.Length > 0 && categoria.Length > 0)
            {
                bool esDouble = double.TryParse(precio, out double numeroDouble);
                bool esInt = double.TryParse(stock, out double numeroInt);
                if (esDouble && esInt)
                {
                    Productos productos = new Productos
                    (
                        TxtNombreProducto.Text,
                        TxtDescripcion.Text,
                        double.Parse(TxtPrecio.Text),
                        int.Parse(TxtStock.Text),
                        TxtCategoria.Text
                    );
                    MetodosCrud.AgregarProducto(productos);
                    CargarDatosDataGrid();
                    BtnNuevo_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Los Datos de Stock o Precio son Incorrectos", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe llenar TODOS LOS CAMPOS", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if(!(TxtNombreProducto.Text.Length > 0 &&
                 TxtDescripcion.Text.Length > 0 &&
                 TxtPrecio.Text.Length > 0 &&
                 TxtStock.Text.Length > 0 &&
                 TxtCategoria.Text.Length > 0))
            {
                MessageBox.Show("Debe llenar todos los campos", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(!(MetodosCrud.EsDouble(TxtPrecio.Text.Trim()) &&
                MetodosCrud.ValidarNumeroConComa(TxtPrecio.Text.Trim())))
            {
                MessageBox.Show("Por favor, utilice la coma para el precio", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string nombre = TxtNombreProducto.Text.Trim();
                string descripcion = TxtDescripcion.Text.Trim();
                double precio = Convert.ToDouble(TxtPrecio.Text.Trim());
                int stock = Convert.ToInt32(TxtStock.Text.Trim());
                string categoria = TxtCategoria.Text.Trim();
                Productos productos = new Productos(idProducto, nombre, descripcion, precio, stock, categoria);
                MetodosCrud.ActualizarProducto(productos);
                CargarDatosDataGrid();
                BtnNuevo_Click(null, null);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Campos no cummplen con el formato(mal llenados)", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosDataGrid.SelectedItem is Productos productosSeleccionado)
            {
                MetodosCrud.EliminarProducto(productosSeleccionado.IdProducto);
                CargarDatosDataGrid();
                BtnNuevo_Click(null, null);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un PRODUCTO", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TxtNombreProducto.Clear();
            TxtDescripcion.Clear();
            TxtPrecio.Clear();
            TxtStock.Clear();
            TxtCategoria.Clear();
        }

        private void ProductosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductosDataGrid.SelectedItem is Productos productoSeleccionado)
            {
                idProducto = productoSeleccionado.IdProducto.ToString();
                TxtNombreProducto.Text = productoSeleccionado.Nombre.ToString();
                TxtDescripcion.Text = productoSeleccionado.Descripcion.ToString();
                TxtPrecio.Text = productoSeleccionado.Precio.ToString();
                TxtStock.Text = productoSeleccionado.Stock.ToString();
                TxtCategoria.Text = productoSeleccionado.Categoria.ToString();
            }
        }

        


    }
}
