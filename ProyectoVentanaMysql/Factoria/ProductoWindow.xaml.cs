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

namespace ProyectoVentanaMysql.Factoria
{
    /// <summary>
    /// Lógica de interacción para ProductoWindow.xaml
    /// </summary>
    public partial class ProductoWindow : Window
    {
        public ProductoWindow()
        {
            InitializeComponent();
            CargarComboBox();
            cmbIdFabrica.SelectedIndex = 0;
        }
        // Se eliminó la declaración estática, porque los controles de UI no pueden ser accedidos estáticamente.
        // Método para cargar datos en el ComboBox
        public void CargarComboBox()
        {
            List<string> productos_l = Metodos.ObtenerProductosComboBox();
            cmbIdFabrica.ItemsSource = productos_l; // Asigna los datos al ComboBox
        }

        // Evento para manejar cambios de selección en el ComboBox
        public void CmbIdFabrica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbIdFabrica.SelectedItem != null)
            {
                string idFabrica = cmbIdFabrica.SelectedItem.ToString();
                CargarDataGrid(idFabrica); // Cargar el DataGrid con los productos correspondientes
            }
        }

        // Método para cargar datos en el DataGrid
        public void CargarDataGrid(string idFabrica)
        {
            List<Producto> productos_l = Metodos.ObtenerListaProductos(idFabrica);
            ProductosDataGrid.ItemsSource = productos_l; // Asigna los datos al DataGrid
        }
    }

}
