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

namespace ProyectoVentanaMysql.Examen
{
    /// <summary>
    /// Lógica de interacción para AniosWindow.xaml
    /// </summary>
    public partial class AniosWindow : Window
    {
        public DataGrid PedidosDataGrid;
        public TextBlock TxtImporteTotal;
        public AniosWindow(DataGrid dg, TextBlock TxtImporteTotal)
        {
            InitializeComponent();
            CargarComboBox();
            // Asignamos el DataGrid recibido al campo de la clase
            PedidosDataGrid = dg;
            TxtImporteTotal = TxtImporteTotal;
        }

        private void CargarComboBox()
        {
            int year = Convert.ToInt32(YearComboBox.SelectedItem);
            YearComboBox.ItemsSource = Metodos.ObtenerAniosComboBox();
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int year = Convert.ToInt32(YearComboBox.SelectedItem.ToString());

            // Asignar los datos al DataGrid
            PedidosDataGrid.ItemsSource = Metodos.ObtenerListaPedidoPorAnio(year);

        }
       
    }
}
