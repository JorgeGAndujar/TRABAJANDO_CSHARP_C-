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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ProyectoVentanaMysql.ProyectoConsultaHospital
{
    /// <summary>
    /// Lógica de interacción para ConsultasWindow.xaml
    /// </summary>
    public partial class ConsultasWindow : Window
    {
        public ConsultasWindow()
        {
            InitializeComponent();
            CargarComboBox();

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            ConsultasDataGrid.ItemsSource = null;
            CmbMedicos.SelectedIndex = -1;

        }
        private void CargarComboBox()
        {
            CmbMedicos.ItemsSource = Metodos.ObtenerMedicosComboBox();
        }

        private void CmbMedicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nombreMedico = CmbMedicos.SelectedItem.ToString();
            ConsultasDataGrid.ItemsSource = Metodos.ObtenerListaConsultaMedicos(nombreMedico);
        }
    }
}
