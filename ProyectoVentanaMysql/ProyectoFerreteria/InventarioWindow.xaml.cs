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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    /// <summary>
    /// Lógica de interacción para InventarioWindow.xaml
    /// </summary>
    public partial class InventarioWindow : Window
    {
        string idProducto;
        public InventarioWindow()
        {
            InitializeComponent();
            CargarDatosDataGrid();
        }
        private void CargarDatosDataGrid()
        {
            List<Productos> productos_l = MetodosCrud.ObtenerListaProductos();
            ProductosDataGrid.ItemsSource = productos_l;
        }

        private void ProductosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si el elemento seleccionado es del tipo 'Productos'
            if (ProductosDataGrid.SelectedItem is Productos productoSeleccionado)
            {
                // Asigna el ID del producto al TextBox
                TxtIdProducto.Text = productoSeleccionado.IdProducto.ToString();
                idProducto = productoSeleccionado.IdProducto;
            }
        }

        private void BtnDisminuirStock_Click(object sender, RoutedEventArgs e)
        {
            int cantidad = Convert.ToInt32(SpnCantidad.Value);
            int stock = MetodosCrud.ObtenerStock(int.Parse(idProducto));
            if (cantidad <= stock)
            {
                //ACTUALIZAR
                MetodosCrud.ActualizarStock(int.Parse(idProducto), cantidad, '-');
                CargarDatosDataGrid();
            }
            else
            {
                MessageBox.Show($"Cantidad {cantidad} debe ser menor o igual al STOCK", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnAumentarStock_Click(object sender, RoutedEventArgs e)
        {
            int cantidad = Convert.ToInt32(SpnCantidad.Value);
            int stock = MetodosCrud.ObtenerStock(int.Parse(idProducto));
            //ACTUALIZAR
            MetodosCrud.ActualizarStock(int.Parse(idProducto), cantidad, '+');
            CargarDatosDataGrid();
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            CargarDatosDataGrid();
        }
    }
}
