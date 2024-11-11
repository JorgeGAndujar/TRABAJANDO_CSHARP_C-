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
    /// Lógica de interacción para VentaWindow.xaml
    /// </summary>
    public partial class VentaWindow : Window
    {
        Dictionary<string, Productos> productosdisponibles_ld;
        List<Venta> carrito_lo = new List<Venta>();
        public VentaWindow()
        {
            InitializeComponent();
            productosdisponibles_ld = MetodosCrud.ObtenerDiccionarioProductosDisponible();
            CargarDatosComboBox();
        }
        private void CargarDatosComboBox()
        {

            CmbProductos.ItemsSource = productosdisponibles_ld.Keys;
        }

        private void BtnAgregarCarrito_Click(object sender, RoutedEventArgs e)
        {
            if (CmbProductos.SelectedItem == null)
            {
                MessageBox.Show("Por favor, Debe seleccionar un producto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int cantidad = cantidad = Convert.ToInt32(SpnCantidad.Value.ToString());
            if (cantidad == 0)
            {
                MessageBox.Show("Por favor, Debe seleccionar una cantidad mayor a 0", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string clave = CmbProductos.SelectedItem.ToString();

            Productos producto = productosdisponibles_ld[clave];

            if(cantidad > producto.Stock)
            {
                MessageBox.Show("Cantidad excede al stock disponible", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
             
            string idProducto = producto.IdProducto;
            string nombreProducto = producto.Nombre;
            double precio = producto.Precio;

            Venta venta = new Venta()
            {
                IdProducto = Convert.ToInt32(idProducto),
                NombreProducto = nombreProducto,
                Cantidad = cantidad,
                PrecioUnitario = precio
            };

            carrito_lo.Add(venta);
            double total = 0;
            foreach (Venta v in carrito_lo)
            {
                total = total + v.Total;
            }
            lblTotal.Content = $"Total: {total:N2}" + " €";

            //ACTUALIZAR EL DATAGRID CON EL CARRITO_LO
            CarritoDataGrid.ItemsSource = null;//limpiar
            CarritoDataGrid.ItemsSource = carrito_lo;//llamar

            CmbProductos.SelectedIndex = -1;
            SpnCantidad.Text = "0";
        }

        private void BtnEliminarSeleccion_Click(object sender, RoutedEventArgs e)
        {
            if (CarritoDataGrid.SelectedItem is Venta productosSeleccionado)
            {
                MessageBoxResult resultado = MessageBox.Show(
                "Confirme si desea eliminar el producto del carrito",
                "Opciones",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show("Has seleccionado Sí.");
                        carrito_lo.Remove(productosSeleccionado);
                        //ACTUALIZAR EL DATAGRID CON EL CARRITO_LO
                        CarritoDataGrid.ItemsSource = null;//limpiar
                        CarritoDataGrid.ItemsSource = carrito_lo;//llamar
                        double total = 0;
                        foreach (Venta v in carrito_lo)
                        {
                            total = total + v.Total;
                        }
                        lblTotal.Content = $"Total: {total:N2}" + " €";
                        MessageBox.Show("Producto eliminado del carrito", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                        
                    case MessageBoxResult.No:
                        MessageBox.Show("Has seleccionado No.");
                        break;
                }
                

            }
            else
            {
                MessageBox.Show("Debe seleccionar un PRODUCTO", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnLimpiarCarrito_Click(object sender, RoutedEventArgs e)
        {
            CmbProductos.SelectedIndex = -1;
            carrito_lo.Clear(); 
            CarritoDataGrid.ItemsSource = null;
            SpnCantidad.Text = "0";
            lblTotal.Content = "Total: 0.00 €";
        }

        private void BtnRealizarVenta_Click(object sender, RoutedEventArgs e)
        {
            MetodosCrud.RealizarVenta(carrito_lo);
            BtnLimpiarCarrito_Click(null,null);
            productosdisponibles_ld = MetodosCrud.ObtenerDiccionarioProductosDisponible();
            CargarDatosComboBox();
        }

        private void CarritoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CarritoDataGrid.SelectedItem is Venta productoSeleccionado)
            {
                string idProducto = productoSeleccionado.IdProducto.ToString();
                string? nombre = productoSeleccionado.NombreProducto;
                string cantidad = productoSeleccionado.Cantidad.ToString();
                string precioUnitario = productoSeleccionado.PrecioUnitario.ToString();
                string total = productoSeleccionado.Total.ToString();               
            }
        }

        private void CmbProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
