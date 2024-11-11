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
using Mysqlx.Cursor;

namespace ProyectoVentanaMysql.ProyectoConsultaHospital
{
    /// <summary>
    /// Lógica de interacción para DatosHospitalWindow.xaml
    /// </summary>
    public partial class DatosHospitalWindow : Window
    {
        public DatosHospitalWindow()
        {
            InitializeComponent();
            CargarDataGrid();

        }

        public void CargarDataGrid()
        {
            
            ConsultasDataGrid.ItemsSource = Metodos.ObtenerListaConsulta();
        }
    }
    public class Medico
    {

    }
}
