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
using ProyectoVentanaMysql.Factoria;
using ProyectoVentanaMysql.ProyectoConsultaHospital;
using ProyectoVentanaMysql.ProyectoFerreteria;

namespace ProyectoVentanaMysql.Examen
{
    /// <summary>
    /// Lógica de interacción para ClientesWindow.xaml
    /// </summary>
    public partial class ClientesWindow : Window
    {
        public AniosWindow anios;
        public static DataGrid dg;
        public ClientesWindow()
        {
            InitializeComponent();
            CargarComboBox();
            dg = PedidosDataGrid;

            AniosWindow ventana = new AniosWindow(dg, TxtImporteTotal);
            ventana.Show();
        }
        public void CargarComboBox()
        {
            List<string> empresas_l = Metodos.ObtenerEmpresasComboBox();
            CmbEmpresa.ItemsSource = empresas_l; // Asigna los datos al ComboBox
        }

        private void CmbEmpresa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbEmpresa.SelectedItem != null)
            {
                string empresa = CmbEmpresa.SelectedItem.ToString();
                CargarDataGrid(empresa); // Cargar el DataGrid las empresas correspondientes
            }
        }
        public void CargarDataGrid(string empresa)
        {
            List<Pedido> pedidos_l = Metodos.ObtenerListaPedido(empresa);
            PedidosDataGrid.ItemsSource = pedidos_l; // Asigna los datos al DataGrid
            CalcularTotal();
        }
        public void CalcularTotal()
        {
            double total = 0;

            // Obtener los elementos del DataGrid (ItemsSource)
            var items = PedidosDataGrid.ItemsSource as IEnumerable<Pedido>;

            if (items != null)
            {
                // Iterar sobre cada ítem y sumar los importes
                foreach (var item in items)
                {
                    total += item.Importe;  // Asegúrate de que Importe sea un número
                }
            }

            // Actualizar el TextBlock con el total
            TxtImporteTotal.Text = $"Total: {total:C2}";  // Formato de moneda con 2 decimales
        }
    }
}
