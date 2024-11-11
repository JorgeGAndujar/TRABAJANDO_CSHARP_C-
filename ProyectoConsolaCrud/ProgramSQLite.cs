using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Messaging;

namespace ProyectoConsolaCrud
{
    internal class ProgramSQLite
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
        public static void crearTabla()
        {
            SQLiteConnection conexion = ObtenerConexion();
            if (conexion != null)
            {
                Console.WriteLine("OK: CONEXION");
                try
                {
                    string query = @"
                     CREATE TABLE IF NOT EXISTS Producto (
                       id_producto       INTEGER PRIMARY KEY AUTOINCREMENT,
                       nombre            TEXT    NOT NULL,
                       descripcion       TEXT    NOT NULL,
                       precio            REAL    NOT NULL,
                       stock             INTEGER NOT NULL,
                       categoria         TEXT    NOT NULL
                     )";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("OK: CREATE TABLE");
                }
                catch
                {
                    Console.WriteLine("ERROR: CREATE TABLE");
                }
            }
            else
            {
                Console.WriteLine("ERROR: CONEXION");
            }
            conexion.Close();

        }

        public static SQLiteConnection ObtenerConexion()
        {
            //string ruta_relativa = "data/ferreteria.sqlite";
            string ruta_absoluta = @"Data Source=C:/TRABAJANDO_CSHARP_C#/ProyectoConsolaCrud/bin/Debug/data/ferreteria.sqlite;Version=3";
            string conexionPath = ruta_absoluta;
            SQLiteConnection conexion = new SQLiteConnection(conexionPath);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (SQLiteException ex)
            {
                return null;
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
            Console.WriteLine("Lista de IDs disponibles(No Usados)");
            Console.WriteLine(string.Join(", ", ObtenerIdDisponibles().ToArray()));

            Console.WriteLine("Desea Ingresar id automático(1)/manual(2)");
            string opcion = Console.ReadLine();
            if (opcion == "1")
            {
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
            if (opcion == "2")
            {
                Console.WriteLine("Ingrese id del producto a Insertar? ");
                string idProducto = Console.ReadLine();
                string patron = "[0-9]+";
                bool correcto = Regex.IsMatch(idProducto, patron);
                if (correcto)
                {
                    Select2(int.Parse(idProducto));
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
                    InsertManual(int.Parse(idProducto), nombre, descripcion, precio, stock, categoria);
                    Select2(int.Parse(idProducto));
                }
                else
                {
                    Console.WriteLine("ID no válido");
                }

            }
        }
        public static void opcion4()
        {
            Console.WriteLine("ACTUALIZAR UN PRODUCTO");
            Console.WriteLine("----------------------");
            Console.WriteLine("Ingrese id del producto a actualizar? ");
            string idProducto = Console.ReadLine();
            string patron = "[0-9]+";
            bool correcto = Regex.IsMatch(idProducto, patron);
            if (correcto)
            {
                if (existeProducto(int.Parse(idProducto)))
                {
                    Select2(int.Parse(idProducto));
                    Console.WriteLine("Ingrese el Nombre nuevo? ");
                    string nombre = Console.ReadLine();
                    Console.WriteLine("Ingrese el Descripcion nuevo? ");
                    string descripcion = Console.ReadLine();
                    Console.WriteLine("Ingrese el Precio nuevo? ");
                    double precio = double.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese el Stock nuevo? ");
                    int stock = int.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese el Categoría nueva? ");
                    string categoria = Console.ReadLine();

                    Console.WriteLine("Confirmar ACTUALIZACIÓN S/N");
                    string confirmacion = Console.ReadLine();
                    if (confirmacion.ToUpper() == "S")
                    {
                        Update(int.Parse(idProducto), nombre, descripcion, precio, stock, categoria);
                        Console.WriteLine("OK: Producto Actualizado");
                    }
                    else
                    {
                        Console.WriteLine("SE CANCELÓ LA ACTUALIZACION");
                    }
                }
                else
                {
                    Console.WriteLine($"ID {idProducto} no existe");
                }
            }
            else
            {
                Console.WriteLine($"ID {idProducto}no válido, debe ingresar un entero");
            }
        }
        public static void opcion5()
        {
            Console.WriteLine("ELIMINAR UN PRODUCTO POR SU ID");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Ingrese el Id Producto a Eliminar? ");
            string idProducto = Console.ReadLine();
            string patron = "[0-9]+";
            bool correcto = Regex.IsMatch(idProducto, patron);
            if (correcto)
            {
                if (existeProducto(int.Parse(idProducto)))
                {
                    Select2(int.Parse(idProducto));

                    Console.WriteLine("Confirmar ELIMINACIÓN S/N");
                    string confirmacion = Console.ReadLine();
                    if (confirmacion.ToUpper() == "S")
                    {
                        Delete(int.Parse(idProducto));
                        Console.WriteLine("OK: Producto Eliminado");
                    }
                    else
                    {
                        Console.WriteLine("SE CANCELÓ LA ELIMINACIÓN");
                    }
                }
                else
                {
                    Console.WriteLine($"ID {idProducto} no existe");
                }
            }
            else
            {
                Console.WriteLine($"ID {idProducto}no válido, debe ingresar un entero");
            }
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
        //CRUD = CREATE(INSERT) READ(SELECT) UPDATE DELETE

        public static void Select1()
        {
            using (SQLiteConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                        SQLiteDataReader reader = cmd.ExecuteReader();
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
                        reader.Close();
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY  SELECT {ex.Message}");
                    }

                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();

            }
            
        }
        public static ArrayList ObtenerIdDisponibles()
        {
            HashSet<int> listaIdsUsados = new HashSet<int>();
            ArrayList listaIdsTodos = new ArrayList();
            ArrayList listaIdsNoUsados = new ArrayList();
            using (SQLiteConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query1 = "SELECT id_producto FROM Producto";
                        string query2 = "SELECT id_producto FROM Producto ORDER BY id_producto DESC LIMIT 1";

                        SQLiteCommand cmdUsados = new SQLiteCommand(query1, conexion);
                        SQLiteDataReader reader1 = cmdUsados.ExecuteReader();
                        while (reader1.Read())
                        {
                            int id_producto = int.Parse(reader1["id_producto"].ToString());
                            listaIdsUsados.Add(id_producto);
                        }
                        reader1.Close();
                        SQLiteCommand cmdTodos = new SQLiteCommand(query2, conexion);
                        SQLiteDataReader reader2 = cmdTodos.ExecuteReader();
                        while (reader2.Read())
                        {
                            int id_producto = int.Parse(reader2["id_producto"].ToString());
                            for (int i = 1; i <= id_producto; i++)
                            {
                                listaIdsTodos.Add(i);
                            }
                        }
                        reader2.Close();
                        for (int i = 0; i < listaIdsTodos.Count; i++)
                        {
                            if (!listaIdsUsados.Contains((int)listaIdsTodos[i]))
                            {
                                listaIdsNoUsados.Add((int)listaIdsTodos[i]);
                            }
                        }

                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY  SELECT {ex.Message}");
                    }
                    

                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();

            }
            return listaIdsNoUsados;

        }
        public static void Select2(int id_producto_buscar)
        {
            using (SQLiteConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto WHERE id_producto = @id_producto_p";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", id_producto_buscar);
                        SQLiteDataReader reader = cmd.ExecuteReader();
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
                        reader.Close();
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY SELECT {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();

            }
        }
        public static void Select3()
        {
            using (SQLiteConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                        SQLiteDataReader reader = cmd.ExecuteReader();
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
                        reader.Close();
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY  SELECT {ex.Message}");
                    }

                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();

            }
        }
        public static bool existeProducto(int id_producto_buscar)
        {
            bool existe = false;
            using (SQLiteConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto WHERE id_producto = @id_producto_p";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", id_producto_buscar);
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            existe = true;
                        }
                        reader.Close();
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY SELECT {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();
            }
            return existe;
        }
        public static void Insert(string nombre, string descripcion, double precio, int stock, string categoria)
        {
            using (SQLiteConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO Producto (nombre, descripcion, precio, stock, categoria)
                                       VALUES(@nombre_p, @descripcion_p, @precio_p, @stock_p, @categoria_p)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_p", nombre);
                        cmd.Parameters.AddWithValue("@descripcion_p", descripcion);
                        cmd.Parameters.AddWithValue("@precio_p", precio);
                        cmd.Parameters.AddWithValue("@stock_p", stock);
                        cmd.Parameters.AddWithValue("@categoria_p", categoria);
                        //SQLiteDataReader reader = cmd.ExecuteReader();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("OK: QUERY INSERT");
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY INSERT {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();

            }
        }
        public static void InsertManual(int idProducto, string nombre, string descripcion, double precio, int stock, string categoria)
        {
            using (SQLiteConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO Producto (id_producto, nombre, descripcion, precio, stock, categoria)
                                       VALUES(@id_producto_p, @nombre_p, @descripcion_p, @precio_p, @stock_p, @categoria_p)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", idProducto);
                        cmd.Parameters.AddWithValue("@nombre_p", nombre);
                        cmd.Parameters.AddWithValue("@descripcion_p", descripcion);
                        cmd.Parameters.AddWithValue("@precio_p", precio);
                        cmd.Parameters.AddWithValue("@stock_p", stock);
                        cmd.Parameters.AddWithValue("@categoria_p", categoria);
                        //SQLiteDataReader reader = cmd.ExecuteReader();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("OK: QUERY INSERT MANUEL");
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY INSERT MANUAL {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();

            }
        }
        public static void Update(int id_producto_buscar, string nombre_nuevo, string descripcion_nuevo, double precio_nuevo, int stock_nuevo, string categoria_nuevo)
        {
            using (SQLiteConnection conexion = ObtenerConexion())
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
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
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
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY UPDATE {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();
            }
        }
        public static void Delete(int id_producto_buscar)
        {
            using (SQLiteConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"DELETE FROM Producto 
                                       WHERE id_producto = @id_producto_p";
                        SQLiteCommand cmd = new SQLiteCommand(query, conexion);
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
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY DELETE {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXIÓN");
                }
                conexion.Close();
            }
        }






    }
}
