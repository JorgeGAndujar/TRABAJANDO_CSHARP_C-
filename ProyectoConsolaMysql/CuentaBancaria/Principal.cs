using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoConsolaMysql.CuentaBancaria
{
    public class Principal
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Cls();
                Console.WriteLine("MENU");
                Console.WriteLine("----");
                Console.WriteLine("[1] Crear Cuenta Bancaria");
                Console.WriteLine("[2] Buscar Cuenta Bancaria");
                Console.WriteLine("[3] Ingresar Dinero a la Cuenta Bancaria");
                Console.WriteLine("[4] Retirar Dinero de la Cuenta Bancaria");
                Console.WriteLine("[5] Salir");

                Console.Write("Ingresar opción ? ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": Cls(); CrearCuentaBancaria(); Pause(); break;
                    case "2": Cls(); BuscarCuentaBancaria(); Pause(); break;
                    case "3": Cls(); IngrearDineroCuentaBancaria(); Pause(); break;
                    case "4": Cls(); SacarDineroCuentaBancaria(); Pause(); break;
                    case "5": Environment.Exit(0); break;
                }
            }
        }

        public static void CrearCuentaBancaria()
        {
            Console.WriteLine("[1] Crear Cuenta Bancaria");
            Console.WriteLine("-------------------------");
            try
            {
                Console.WriteLine("Ingresar Titular? ");
                string titular = Console.ReadLine();
                Console.WriteLine("Ingresar Saldo? ");
                double saldo = Convert.ToDouble(Console.ReadLine());
                CuentaBancaria cuenta = new CuentaBancaria(titular, saldo);
                cuenta.CrearCuentaBancaria();

            } catch (Exception ex) 
            {
                //Console.WriteLine("ERROR: " + ex.ToString()); // Me muestra toda la excepción
                Console.WriteLine("ERROR: " + "Entrada Incorrecta");
            }
        }
        public static void BuscarCuentaBancaria()
        {
            Console.WriteLine("[2] Buscar Cuenta Bancaria");
            Console.WriteLine("--------------------------");
            try
            {
                Console.WriteLine("Ingresar id de la Cuenta Bancaria ? ");
                int id = Convert.ToInt32(Console.ReadLine());
                CuentaBancaria cuenta = CuentaBancaria.BuscarCuentaBancaria(id);
                if (cuenta != null)
                {
                    cuenta.MostrarInformacion();
                }
                else
                {
                    Console.WriteLine($"Cuenta {id} no exixte");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + "Entrada Incorrecta");
            }
        }
        public static void IngrearDineroCuentaBancaria()
        {
            Console.WriteLine("[3] Ingresar Dinero a la Cuenta Bancaria");
            Console.WriteLine("----------------------------------------");
            try
            {
                Console.WriteLine("Ingresar Id de la Cuenta Bancaria");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ingresar Dinero a la Cuenta Bancaria");
                double dinero = Convert.ToDouble(Console.ReadLine());
                CuentaBancaria cuenta = new CuentaBancaria(id, dinero);
                cuenta.ActualizarSaldo(id, dinero, '+');
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + "Entrada Incorrecta");
            }
        }
        public static void SacarDineroCuentaBancaria()
        {
            Console.WriteLine("[4] Retirar Dinero de la Cuenta Bancaria");
            Console.WriteLine("----------------------------------------");
            try
            {
                Console.WriteLine("Ingresar Id de la Cuenta Bancaria");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Sacar Dinero de la Cuenta Bancaria");
                double dinero = Convert.ToDouble(Console.ReadLine());

                CuentaBancaria cuenta = CuentaBancaria.BuscarCuentaBancaria(id);
                if(cuenta != null)
                {
                    double saldo = cuenta.Saldo;
                    if(dinero <= saldo)
                    {
                        CuentaBancaria cuenta1 = new CuentaBancaria(id, dinero);
                        cuenta1.ActualizarSaldo(id, dinero, '-');
                    }
                    else
                    {
                        Console.WriteLine($"WARNING: Saldo {saldo} insuficiente para retirar");
                    }
                }
                else
                {
                    Console.WriteLine($"ERROR: Cuenta Bancaria {id} No EXISTE");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + "Entrada Incorrecta");
            }
        }
        public static void Cls()
        {
            Console.Clear();
        }
        public static void Pause()
        {
            Console.Write("Presione una tecla para continuar...");
            Console.ReadLine();
        }
    }
}
