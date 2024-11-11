using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;

namespace ProyectoConsolaMysql.CuentaBancaria
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
                            cmd.Parameters.AddWithValue("@titular_p", this.Titular);
                            cmd.Parameters.AddWithValue("@saldo_p", this.Saldo);
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                Console.WriteLine("OK INSERT: Se Creó la Cuenta Bancária");
                            }
                            else
                            {
                                Console.WriteLine("WARNING: No se pudo crear la Cuenta Bancária");
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: Query Insert {ex.Message}");

                    }
                }
                else
                {
                    Console.WriteLine("ERROR: Conexión");
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
                        Console.WriteLine($"ERROR: Query Select {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: Conexión");
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
                        string query = @"UPDATE CuentaBancaria
                                         SET saldo = saldo + @dinero_p 
                                         WHERE id = @id_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_p", id);
                        if (tipo == '+')
                            cmd.Parameters.AddWithValue("@dinero_p", dinero);
                        else
                            cmd.Parameters.AddWithValue("@dinero_p", (-1) * dinero);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            Console.WriteLine("OK UPDATE: Se Actualizó el salario de la Cuenta Bancária");
                        else
                            Console.WriteLine("WARNING: No existe la Cuenta Bancária para Actualizar");
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: Query Update {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: Conexión");
                }

            }
        }



    }
}
