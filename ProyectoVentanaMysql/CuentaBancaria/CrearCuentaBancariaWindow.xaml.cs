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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoVentanaMysql.CuentaBancaria
{
    /// <summary>
    /// Lógica de interacción para CrearCuentaBancariaWindow.xaml
    /// </summary>
    public partial class CrearCuentaBancariaWindow : Window
    {
        public CrearCuentaBancariaWindow()
        {
            InitializeComponent();
        }

        private void BtnCrear_Click(object sender, RoutedEventArgs e)
        {
            string titular = TxtTitular.Text;
            bool esDouble = double.TryParse(TxtSaldo.Text, out double saldo);  // Usamos TryParse directamente con TxtSaldo.Text

            if (titular.Length > 0 && esDouble && saldo >= 0)  // Validamos que el saldo sea un número y positivo
            {
                // Creamos la cuenta bancaria
                CuentaBancaria cuenta = new CuentaBancaria(titular, saldo);
                cuenta.CrearCuentaBancaria();
                


                // Llamamos al método para limpiar o realizar alguna acción adicional
                BtnNuevo_Click(null, null);
            }
            else
            {
                MessageBox.Show($"DATOS INCORRECTOS", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TxtSaldo.Clear();
            TxtTitular.Clear();
        }
    }
}
