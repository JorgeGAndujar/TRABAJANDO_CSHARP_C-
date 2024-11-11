using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ProyectoVentanaMysql.ProyectoFerreteria;

namespace ProyectoVentanaMysql.CuentaBancaria
{
    public class CuentaBancaria
    {
        //PROPIEDADES
        public int Id { get; set; }
        public string Titular { get; set; }
        public double Saldo { get; set; }

        public CuentaBancaria(int id, string titular, double saldo) // Id Manual
        {
            Id = id;
            Titular = titular;
            Saldo = saldo;
        }
        public CuentaBancaria(string titular, double saldo) // Id Automático
        {
            Titular = titular;
            Saldo = saldo;
        }
        public CuentaBancaria(int id, double saldo) // Buscar id y saldo
        {
            Id = id;
            Saldo = saldo;
        }
        //OTRO MÉTODO Para mostrar información de cuenta
        public void MostrarInformacion()
        {
            Console.WriteLine("Titular: " + Titular);
            Console.WriteLine("Saldo  : " + Saldo);
        }
        
        //MéTODOS DE COMPROBACIÓN
        public static bool EsDouble(string valor)
        {
            //Intenta convertir el valor a double
            return double.TryParse(valor, out _);
        }
        public static bool ValidarNumeroConComa(string numero)
        {
            string patron = @"^\d+(,\d+)?$";
            return Regex.IsMatch(numero, patron);
        }

        //MÉTODOS CRUD
        public void CrearCuentaBancaria()
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO CuentaBancaria(titular, saldo)
                                         VALUES(@titular_p, @saldo_p)";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@titular_p",this.Titular);
                            cmd.Parameters.AddWithValue("@saldo_p", this.Saldo);
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Cuenta Bancaria Insertada Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show($"NO EXISTE LA CUENTA", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Insert {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public static CuentaBancaria BuscarCuentaBancaria(int id)
        {
            CuentaBancaria cuenta = null;
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM CuentaBancaria WHERE id = @id_p";

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id_p", id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int id1 = Convert.ToInt32(reader["id"].ToString());
                                    string titular = reader["titular"].ToString();
                                    double salario = Convert.ToDouble(reader["saldo"].ToString());

                                    cuenta = new CuentaBancaria(id, titular, salario);
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return cuenta;
        }
        public void ActualizarSaldo(int id, double dinero, char tipo)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        // Consulta para obtener el saldo actual de la cuenta
                        string querySaldo = "SELECT saldo FROM CuentaBancaria WHERE id = @id_p";
                        MySqlCommand cmdSaldo = new MySqlCommand(querySaldo, conexion);
                        cmdSaldo.Parameters.AddWithValue("@id_p", id);

                        double saldoActual = 0;
                        using (MySqlDataReader reader = cmdSaldo.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                saldoActual = Convert.ToDouble(reader["saldo"]);
                            }
                        }

                        // Si el tipo es retiro, validamos si hay suficiente saldo
                        if (tipo == '-' && saldoActual < dinero)
                        {
                            MessageBox.Show("No hay suficiente saldo para realizar la operación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;  // Salimos del método si el saldo no es suficiente
                        }

                        // Si el saldo es suficiente o es un ingreso, continuamos con la actualización
                        string queryUpdate = @"UPDATE CuentaBancaria
                                       SET saldo = saldo + @dinero_p 
                                       WHERE id = @id_p";
                        MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, conexion);
                        cmdUpdate.Parameters.AddWithValue("@id_p", id);
                        if (tipo == '+')
                            cmdUpdate.Parameters.AddWithValue("@dinero_p", dinero);
                        else
                            cmdUpdate.Parameters.AddWithValue("@dinero_p", (-1) * dinero);

                        int rowsAffected = cmdUpdate.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Cuenta Bancaria Actualizada Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                            MessageBox.Show("NO EXISTE CUENTA BANCARIA", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Update {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }





    }
}
