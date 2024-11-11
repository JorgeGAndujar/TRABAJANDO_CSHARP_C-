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
using ProyectoVentanaMysql.ProyectoFerreteria;

namespace ProyectoVentanaMysql.ProyectoConsultaHospital
{
    /// <summary>
    /// Lógica de interacción para OpcionesWindow.xaml
    /// </summary>
    public partial class OpcionesWindow : Window
    {
        public DataGrid ConsultasDataGrid;
        public OpcionesWindow(DataGrid dg)
        {
            InitializeComponent();
            CargarComboBox();
            try
            {
                CargarComboBoxPartos();
            }
            catch (Exception ex) {
                MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            ConsultasDataGrid = dg;
            
        }
        private void CargarComboBox()
        {
            int year = Convert.ToInt32(YearComboBox.SelectedItem);
            YearComboBox.ItemsSource = Metodos.ObtenerListaYear();
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {    
                int year = Convert.ToInt32(YearComboBox.SelectedItem.ToString());
                
                ConsultasDataGrid.ItemsSource = Metodos.ObtenerListaConsultaPorYear(year);           
        }
        private void CargarComboBoxPartos()
        {
            PartosComboBox.ItemsSource = Metodos.ObtenerListaTiposParto();
        }
        private void PartosComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? partos = PartosComboBox.SelectedItem.ToString();
            ConsultasDataGrid.ItemsSource = Metodos.ObtenerListaConsultaPorPartos(partos);
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string? partos = PartosComboBox.SelectedItem.ToString();
            int year = Convert.ToInt32(YearComboBox.SelectedItem.ToString());
            ConsultasDataGrid.ItemsSource = Metodos.ObtenerListaConsultaPorPartosYear(partos,year);
        }
    }
}
