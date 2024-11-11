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
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoVentanaMysql.CuentaBancaria
{
    /// <summary>
    /// Lógica de interacción para BuscarCuentaBancariaWindow.xaml
    /// </summary>
    public partial class BuscarCuentaBancariaWindow : Window
    {
        public BuscarCuentaBancariaWindow()
        {
            InitializeComponent();
        }
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtIdCuentaBancaria.Text;
            bool esInt = int.TryParse(id, out int numeroInt);  // Usamos int.TryParse para validar que el ID es un número entero

            if (esInt)
            {
                // Llamamos a BuscarCuentaBancaria que devuelve un solo objeto CuentaBancaria
                CuentaBancaria cuenta = CuentaBancaria.BuscarCuentaBancaria(numeroInt);

                if (cuenta != null)
                {
                    // Si se encontró la cuenta, mostramos el resultado en el DataGrid
                    List<CuentaBancaria> cuenta_l = new List<CuentaBancaria> { cuenta };  // Convertimos la cuenta en una lista
                    CuentaBancariaDataGrid.ItemsSource = cuenta_l;  // Mostramos la lista (con solo una cuenta) en el DataGrid
                }
                else
                {
                    MessageBox.Show("No se encontró la cuenta", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("El ID es Incorrecto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            // Desvincula el ItemsSource o asigna una lista vacía
            TxtIdCuentaBancaria.Clear();
            CuentaBancariaDataGrid.ItemsSource = null;
        }
    }
}
