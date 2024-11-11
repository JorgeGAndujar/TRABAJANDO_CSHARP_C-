using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VentanaWPF_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSumar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double n1 = double.Parse(TxtN1.Text);//los numeros pueden ir con , (2,333) pero no con 2.3
                double n2 = double.Parse(TxtN2.Text);

                Sumar metodo = new Sumar();// no es un método de la clase public static; asi que tienes que hacer un objeto

                double r = metodo.Suma(n1, n2);

                TxtResultado.Text = r.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Entrada Incorrecta");
            }
        }
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TxtN1.Clear();
            TxtN2.Clear();
            TxtResultado.Clear();
            RbDividir.IsChecked = false;
            RbMultiplicar.IsChecked = false;
            CboAritmetica.Items.Clear();
        }
        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void RbDividir_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                double n1 = double.Parse(TxtN1.Text);//los numeros pueden ir con , (2,333) pero no con 2.3
                double n2 = double.Parse(TxtN2.Text);

                double r = Operaciones.Dividir(n1, n2);
                TxtResultado.Text = r.ToString();
            }catch (Exception ex)
            {
                MessageBox.Show("Entrada incorrecta");
            }
        }
        private void RbMultiplicar_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                double n1 = double.Parse(TxtN1.Text);//los numeros pueden ir con , (2,333) pero no con 2.3
                double n2 = double.Parse(TxtN2.Text);

                double r = Operaciones.Multiplicar(n1, n2);
                TxtResultado.Text = r.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Entrada incorrecta");
            }
        }
        private void CboAritmetica_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string opcion = ((ComboBoxItem)CboAritmetica.SelectedItem)?.Content.ToString();

            if(opcion == "Sumar")
            {
                try
                {
                    double n1 = double.Parse(TxtN1.Text);//los numeros pueden ir con , (2,333) pero no con 2.3
                    double n2 = double.Parse(TxtN2.Text);

                    Sumar metodo = new Sumar();// no es un método de la clase public static; asi que tienes que hacer un objeto

                    double r = metodo.Suma(n1, n2);

                    TxtResultado.Text = r.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Entrada Incorrecta");
                }
            }
            if (opcion == "Multiplicar")
            {
                try
                {
                    double n1 = double.Parse(TxtN1.Text);//los numeros pueden ir con , (2,333) pero no con 2.3
                    double n2 = double.Parse(TxtN2.Text);

                    double r = Operaciones.Multiplicar(n1, n2);
                    TxtResultado.Text = r.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Entrada incorrecta");
                }
            }
            if (opcion == "Dividir")
            {
                try
                {
                    double n1 = double.Parse(TxtN1.Text);//los numeros pueden ir con , (2,333) pero no con 2.3
                    double n2 = double.Parse(TxtN2.Text);

                    double r = Operaciones.Dividir(n1, n2);
                    TxtResultado.Text = r.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Entrada incorrecta");
                }
            }
            if (opcion == "Restar")
            {
                try
                {
                    double n1 = double.Parse(TxtN1.Text);//los numeros pueden ir con , (2,333) pero no con 2.3
                    double n2 = double.Parse(TxtN2.Text);

                    double r = Operaciones.Restar(n1, n2);
                    TxtResultado.Text = r.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Entrada incorrecta");
                }
            }

        }
    }
}