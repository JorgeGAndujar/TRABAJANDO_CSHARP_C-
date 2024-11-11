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

namespace ProyectoVentanaMysql.CuentaBancaria
{
    /// <summary>
    /// Lógica de interacción para SacarDineroCuentaBancariaWindow.xaml
    /// </summary>
    public partial class SacarDineroCuentaBancariaWindow : Window
    {
        public SacarDineroCuentaBancariaWindow()
        {
            InitializeComponent();
        }

        private void BtnSacar_Click(object sender, RoutedEventArgs e)
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
                    //double dinero = Double.Parse(TxtDinero.Text);
                    bool esDouble = double.TryParse(TxtDinero.Text, out double dinero);  // Usamos TryParse directamente con TxtSaldo.Text
                    if (!esDouble)
                    {
                        MessageBox.Show($"El Dinero {TxtDinero.Text} es incorrecto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Actualizamos el saldo con el tipo '-' (retiro)
                    cuenta.ActualizarSaldo(numeroInt, dinero, '-');

                    // Después de realizar el retiro, obtenemos la cuenta actualizada
                    cuenta = CuentaBancaria.BuscarCuentaBancaria(numeroInt);  // Volver a buscar la cuenta para obtener los cambios

                    // Ahora actualizamos el DataGrid con la cuenta actualizada
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
            TxtIdCuentaBancaria.Clear();
            TxtDinero.Clear();
        }


    }
}
