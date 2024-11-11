using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoConsolaCrud
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MENU APLICACIÓN";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetWindowSize(150, 20);
            Console.SetBufferSize(150, 300);

            while (true)
            {
                Cls();
                Console.WriteLine("[1] Buscar un producto por su ID");
                Console.WriteLine("[2] Mostrar todos los Productos");
                Console.WriteLine("[3] Insertar un Producto");
                Console.WriteLine("[4] Actualizar un Producto");
                Console.WriteLine("[5] Eliminar un Producto por su ID");
                Console.WriteLine("[6] Salir");

                Console.Write("Ingrese opción? ");
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1": Cls(); opcion1(); Pause(); break;
                    case "2": Cls(); opcion2(); Pause(); break;
                    case "3": Cls(); opcion3(); Pause(); break;
                    case "4": Cls(); opcion4(); Pause(); break;
                    case "5": Cls(); opcion5(); Pause(); break;
                    case "6": Cls(); Environment.Exit(0); break;
                }
                            
            }
        }
        public static void opcion1()
        {
            Console.WriteLine("BUSCAR UN PRODUCTO POR SU ID");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Ingrese id del producto a buscar? ");
            string idProducto = Console.ReadLine();
            string patron = "[0-9]+";
            bool correcto = Regex.IsMatch(idProducto, patron);
            if (correcto)
            {
                Select2(int.Parse(idProducto));
            }
            else
            {
                Console.WriteLine("ID no válido");            
            }
        }
        public static void opcion2()
        {
            Console.WriteLine("MOSTRAR TODOS LOS PRODUCTOS");
            Console.WriteLine("---------------------------");
            Select3();
        }
        public static void opcion3()
        {
            Console.WriteLine("INSERTAR UN PRODUCTO");
            Console.WriteLine("--------------------");
            Console.WriteLine("Ingrese el Nombre? ");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el Descripcion? ");
            string descripcion = Console.ReadLine();
            Console.WriteLine("Ingrese el Precio? ");
            double precio = double.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el Stock? ");
            int stock = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el Categoría? ");
            string categoria = Console.ReadLine();
            Insert(nombre, descripcion, precio, stock, categoria);
        }
        public static void opcion4()
        {
            Console.WriteLine("ACTUALIZAR UN PRODUCTO");
            Console.WriteLine("----------------------");
            Console.WriteLine("Ingrese el Id Producto a Actualizar? ");
            int idProducto = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el Nombre? ");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el Descripcion? ");
            string descripcion = Console.ReadLine();
            Console.WriteLine("Ingrese el Precio? ");
            double precio = double.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el Stock? ");
            int stock = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el Categoría? ");
            string categoria = Console.ReadLine();
            Update(idProducto,nombre, descripcion, precio, stock, categoria);

        }
        public static void opcion5()
        {
            Console.WriteLine("ELIMINAR UN PRODUCTO POR SU ID");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Ingrese el Id Producto a Eliminar? ");
            int idProducto = int.Parse(Console.ReadLine());
            Delete(idProducto);
        }
            
        public static void Cls()
        {
            Console.Clear();
        }
        public static void Pause()
        {
            Console.Write("Presione una tecla para continuar...");
            Console.Read();
        }
        public static MySqlConnection ObtenerConexion()
        {
            string conexionUrl = "Server=localhost;Database=ferreteria;Uid=root;Pwd=12345678;Port=3307";
            MySqlConnection conexion = new MySqlConnection(conexionUrl);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (MySqlException ex)
            {
                return null;
            }
        }
        //CRUD = CREATE(INSERT) READ(SELECT) UPDATE DELETE

        public static void Select1()
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int id_producto = int.Parse(reader["id_producto"].ToString());
                            string nombre = reader["nombre"].ToString();
                            string descripcion = reader["descripcion"].ToString();
                            double precio = double.Parse(reader["precio"].ToString());
                            int stock = int.Parse(reader["stock"].ToString());
                            string categoria = reader["categoria"].ToString();

                            Console.WriteLine("Id Producto: " + id_producto);
                            Console.WriteLine("Nombre     : " + nombre);
                            Console.WriteLine("Descripción: " + descripcion);
                            Console.WriteLine("Precio     : " + precio);
                            Console.WriteLine("Stock      : " + stock);
                            Console.WriteLine("Categoría  : " + categoria);
                            Console.WriteLine();

                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY  SELECT {ex.Message}");
                    }

                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }

            }
        }
        public static void Select2(int id_producto_buscar)
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", id_producto_buscar);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int id_producto = int.Parse(reader["id_producto"].ToString());
                            string nombre = reader["nombre"].ToString();
                            string descripcion = reader["descripcion"].ToString();
                            double precio = double.Parse(reader["precio"].ToString());
                            int stock = int.Parse(reader["stock"].ToString());
                            string categoria = reader["categoria"].ToString();

                            Console.WriteLine("Id Producto: " + id_producto);
                            Console.WriteLine("Nombre     : " + nombre);
                            Console.WriteLine("Descripción: " + descripcion);
                            Console.WriteLine("Precio     : " + precio);
                            Console.WriteLine("Stock      : " + stock);
                            Console.WriteLine("Categoría  : " + categoria);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"ID {id_producto_buscar} NO EXISTE");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY SELECT {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }

            }
        }
        public static void Select3()
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int id_producto = int.Parse(reader["id_producto"].ToString());
                            string nombre = reader["nombre"].ToString();
                            string descripcion = reader["descripcion"].ToString();
                            double precio = double.Parse(reader["precio"].ToString());
                            int stock = int.Parse(reader["stock"].ToString());
                            string categoria = reader["categoria"].ToString();

                            Console.WriteLine("{0,5} {1,-20} {2,-60} {3,10} {4,10} {5,-20}", id_producto,
                                                                                             nombre,
                                                                                             descripcion,
                                                                                             precio,
                                                                                             stock,
                                                                                             categoria
                                                                                             );

                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY  SELECT {ex.Message}");
                    }

                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }

            }
        }
        public static void Insert(string nombre, string descripcion, double precio, int stock, string categoria)
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO Producto (nombre, descripcion, precio, stock, categoria)
                                       VALUES(@nombre_p, @descripcion_p, @precio_p, @stock_p, @categoria_p)";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_p", nombre);
                        cmd.Parameters.AddWithValue("@descripcion_p", descripcion);
                        cmd.Parameters.AddWithValue("@precio_p", precio);
                        cmd.Parameters.AddWithValue("@stock_p", stock);
                        cmd.Parameters.AddWithValue("@categoria_p", categoria);
                        //MySqlDataReader reader = cmd.ExecuteReader();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("OK: QUERY INSERT");
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY INSERT {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }

            }
        }
        public static void Update(int id_producto_buscar, string nombre_nuevo, string descripcion_nuevo, double precio_nuevo, int stock_nuevo, string categoria_nuevo)
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"UPDATE Producto 
                                       SET nombre = @nombre_p,
                                           descripcion = @descripcion_p,
                                           precio = @precio_p,
                                           stock = @stock_p,
                                           categoria = @categoria_p
                                       WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", id_producto_buscar);
                        cmd.Parameters.AddWithValue("@nombre_p", nombre_nuevo);
                        cmd.Parameters.AddWithValue("@descripcion_p", descripcion_nuevo);
                        cmd.Parameters.AddWithValue("@precio_p", precio_nuevo);
                        cmd.Parameters.AddWithValue("@stock_p", stock_nuevo);
                        cmd.Parameters.AddWithValue("@categoria_p", categoria_nuevo);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("OK: QUERY UPDATE");
                        }
                        else
                        {
                            Console.WriteLine("ERROR: NO EXISTE REGISTRO PARA ACTUALIZAR");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY UPDATE {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
            }
        }
        public static void Delete(int id_producto_buscar)
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"DELETE FROM Producto 
                                       WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", id_producto_buscar);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("OK: QUERY DELETE");
                        }
                        else
                        {
                            Console.WriteLine("ERROR: NO EXISTE REGISTRO PARA ELIMINAR");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY DELETE {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
            }
        }
    }
}
