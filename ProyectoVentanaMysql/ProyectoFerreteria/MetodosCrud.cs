using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using ProyectoVentanaMysql.GestionUsuarios;

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    public class MetodosCrud
    {
        //PARA USUARIO
        public static List<Usuario> ObtenerListaUsuarios()
        {
            List<Usuario> usuarios_l = new List<Usuario>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Usuario";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string? idUsuario = reader["id_usuario"].ToString();
                            string? nombreUsuario = reader["nombre_usuario"].ToString();
                            string? contrasena = reader["contrasena"].ToString();
                            string? rol = reader["rol"].ToString();
                            Usuario usuario = new Usuario(idUsuario, nombreUsuario, contrasena, rol);
                            usuarios_l.Add(usuario);
                        }
                        reader.Close();
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
                return usuarios_l;

            }
        }
        public static void AgregarUsuario(Usuario usuario)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO Usuario(nombre_usuario,contrasena,rol)
                                         VALUES(@nombre_usuario_p, @contrasena_p, @rol_p)";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_usuario_p",usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@contrasena_p", BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena));
                        cmd.Parameters.AddWithValue("@rol_p", usuario.Rol);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Usuario Agregado Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
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
        public static void ActualizarUsuario(Usuario usuario , int x)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"UPDATE Usuario 
                                         SET nombre_usuario = @nombre_usuario_p, 
                                             contrasena = @contrasena_p,
                                             rol = @rol_p 
                                         WHERE id_usuario = @id_usuario_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_usuario_p", usuario.IdUsuario);
                        cmd.Parameters.AddWithValue("@nombre_usuario_p", usuario.NombreUsuario);
                        if (x == 0)
                        {
                            cmd.Parameters.AddWithValue("@contrasena_p", usuario.Contrasena);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@contrasena_p", BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena));
                        }
                        cmd.Parameters.AddWithValue("@rol_p", usuario.Rol);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Usuario Editado Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("EL USUARIO(registro no encontrado) NO SE PUDO ACTUALIZAR", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        public static void EliminarUsuario(int id_usuario_eliminar)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"DELETE FROM Usuario                                   
                                         WHERE id_usuario = @id_usuario_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_usuario_p", id_usuario_eliminar);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Usuario Eliminado Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("EL USUARIO(registro no encontrado) NO SE PUDO ELIMINAR", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Delete {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public static Usuario BuscarUsuario(string nombre_usuario_buscar)
        {
            string? contrasena = "";
            string? rol = "";
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT contrasena,rol FROM Usuario WHERE nombre_usuario = @nombre_usuario_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_usuario_p", nombre_usuario_buscar);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            contrasena = reader["contrasena"].ToString();
                            rol = reader["rol"].ToString();
                        }
                        else
                        {
                            MessageBox.Show($"NO EXISTE {nombre_usuario_buscar} USUARIO", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        reader.Close();

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
            return new Usuario(nombre_usuario_buscar, contrasena, rol);
        }
        //PARA PRODUCTO



        public static List<Productos> ObtenerListaProductos()
        {
            List<Productos> productos_l = new List<Productos>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        if (conexion.State != ConnectionState.Open)
                        {
                            conexion.Open();
                        }

                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string? idProducto = reader["id_producto"].ToString();
                                string? nombreProducto = reader["nombre"].ToString();
                                string? descripcion = reader["descripcion"].ToString();

                                double precio = 0;
                                if (!double.TryParse(reader["precio"].ToString(), out precio))
                                {
                                    precio = 0;
                                }

                                int stock = 0;
                                if (!int.TryParse(reader["stock"].ToString(), out stock))
                                {
                                    stock = 0;
                                }

                                string? categoria = reader["categoria"].ToString();

                                Productos productos = new Productos(idProducto, nombreProducto, descripcion, precio, stock, categoria);
                                productos_l.Add(productos);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron productos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error al conectar con la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return productos_l;
        }
        public static void AgregarProducto(Productos productos)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO Producto(nombre,descripcion,precio,stock,categoria)
                                         VALUES(@nombre_p, @descripcion_p, @precio_p, @stock_p, @categoria_p)";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_p", productos.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion_p", productos.Descripcion);
                        cmd.Parameters.AddWithValue("@precio_p", productos.Precio);
                        cmd.Parameters.AddWithValue("@stock_p", productos.Stock);
                        cmd.Parameters.AddWithValue("@categoria_p", productos.Categoria);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto Agregado Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
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
        public static void ActualizarProducto(Productos productos)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
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
                        cmd.Parameters.AddWithValue("@id_producto_p", productos.IdProducto);
                        cmd.Parameters.AddWithValue("@nombre_p", productos.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion_p", productos.Descripcion);
                        cmd.Parameters.AddWithValue("@precio_p", productos.Precio);
                        cmd.Parameters.AddWithValue("@stock_p", productos.Stock);
                        cmd.Parameters.AddWithValue("@categoria_p", productos.Categoria);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Producto Editado Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("EL PRODUCTO(registro no encontrado) NO SE PUDO ACTUALIZAR", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
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
        public static void EliminarProducto(string id_producto_eliminar)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"DELETE FROM Producto                                   
                                         WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", id_producto_eliminar);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Producto Eliminado Correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("EL PRODUCTO(registro no encontrado) NO SE PUDO ELIMINAR", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Delete {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

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
        //PARA INVENTARIO
        public static int ObtenerStock(int idProductoBuscar)
        {
            int stock = 0;
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT stock FROM Producto WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("id_producto_p", idProductoBuscar);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            stock = Convert.ToInt32(reader["stock"]);
                        }
                        reader.Close();
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
                return stock;

            }
        }
        public static void ActualizarStock(int idProducto, int cantidad, char tipo)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"UPDATE Producto 
                                         SET stock = stock + @stock_p
                                         WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", idProducto);
                        if(tipo == '+')
                        {
                            cmd.Parameters.AddWithValue("@stock_p", cantidad);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@stock_p", (-1)*cantidad);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Producto Stock editado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                            MessageBox.Show("EL PRODUCTO(registro no encontrado) NO SE PUDO AC<TUALIZAR", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        //PARA VENTA
        //MÉTODOS PARA EL CARRITO
        public static Dictionary<string, Productos> ObtenerDiccionarioProductosDisponible()
        {
            Dictionary<string, Productos> productosdisponibles_ld = new Dictionary<string, Productos>();
            MySqlConnection conexion = Conexion.ObtenerConexion();
            if (conexion != null)
            {
                try
                {
                    string query = "SELECT * FROM Producto WHERE stock > 0";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idProducto = Convert.ToInt32(reader["id_producto"]);
                            string nombre = reader.GetString("nombre");
                            double precio = reader.GetDouble("precio");
                            int stock = Convert.ToInt32(reader["stock"]);

                            string clave = $"{idProducto} - {nombre}";
                            productosdisponibles_ld[clave] = new Productos(idProducto + "", nombre, precio, stock);

                        }
                    }
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return productosdisponibles_ld;
        }


        public static void RealizarVenta(List<Venta> carrito_lo)
        {
            MySqlConnection conexion = Conexion.ObtenerConexion();
            if (conexion != null)
            {
                try
                {
                    double total = 0;
                    foreach (Venta venta in carrito_lo)
                    {
                        total += venta.Total;
                    }
                    string query = "INSERT INTO Venta(fecha, total) VALUES (@fecha_p, @total_p)";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@fecha_p", DateTime.Now);
                    cmd.Parameters.AddWithValue("@total_p", total);
                    cmd.ExecuteNonQuery();

                    int id_venta = (int)cmd.LastInsertedId;

                    string query2 = "INSERT INTO DetalleVentas (id_venta, id_producto,cantidad, subtotal)" +
                                    "VALUES (@id_venta_p, @id_producto_p, @cantidad_p, @subtotal_p)";
                    foreach(Venta venta in carrito_lo)
                    {
                        MySqlCommand cmd2 = new MySqlCommand(query2, conexion);
                        cmd2.Parameters.AddWithValue("id_venta_p", id_venta);
                        cmd2.Parameters.AddWithValue("id_producto_p", venta.IdProducto);
                        cmd2.Parameters.AddWithValue("cantidad_p", venta.Cantidad);
                        cmd2.Parameters.AddWithValue("subtotal_p", venta.Total);
                        cmd2.ExecuteNonQuery();

                        //ACTUALIZAR STOCK DEL PRODUCTO
                        ActualizarStock(venta.IdProducto, venta.Cantidad, '-');
                    }

                    MessageBox.Show("Venta realizada correctamente", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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

        //PARA IDS A BUSCAR VENTAS
        public static List<int> ObtenerListaIdsVenta()
        {
            List<int> idsventa_li = new List<int>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT id_venta FROM Venta";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);
                                idsventa_li.Add(idVenta);
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
                return idsventa_li;

            }
        }
        public static dynamic ObtenerVentaPorId(int idVenta)
        {
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                dynamic? objeto = null;
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Venta WHERE id_venta = @id_venta_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_venta_p", idVenta);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                double total = Convert.ToDouble(reader["total"]);
                                objeto = new
                                {
                                    IdVenta = idVenta,
                                    FechaHoraVenta = fecha,
                                    TotalVenta = total,
                                };
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
                return objeto;

            }
        }
        public static List<Venta> ObtenerListaProductosVendidos(int idVenta)
        {
            List<Venta> productosvendidos_lo = new List<Venta>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        // Consulta que hace el JOIN entre DetalleVentas y Producto
                        string query = @"
                    SELECT dv.id_producto, dv.cantidad, dv.subtotal, p.nombre, p.precio
                    FROM DetalleVentas dv
                    INNER JOIN Producto p ON dv.id_producto = p.id_producto
                    WHERE dv.id_venta = @id_venta_p";

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_venta_p", idVenta);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int idProducto = Convert.ToInt32(reader["id_producto"]);
                                    int cantidad = Convert.ToInt32(reader["cantidad"]);
                                    double subtotal = Convert.ToDouble(reader["subtotal"]);
                                    string? nombre = reader["nombre"].ToString();
                                    double precio = Convert.ToDouble(reader["precio"]);

                                    // Crear el objeto Venta y agregarlo a la lista
                                    Venta productoVendido = new Venta
                                    {
                                        IdProducto = idProducto,
                                        NombreProducto = nombre,
                                        Cantidad = cantidad,
                                        PrecioUnitario = precio
                                    };

                                    productosvendidos_lo.Add(productoVendido);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron productos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Error en la consulta: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error al conectar con la base de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return productosvendidos_lo;
        }
        //BUSCAR VENTAS POR FECHA
        public static List<Venta2> BuscarVentasPorFecha(DateTime fechaSeleccionada)
        {
            List<Venta2> ventas_lo = new List<Venta2>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Venta WHERE DATE(fecha) = @fecha_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@fecha_p", fechaSeleccionada.Date.ToString("yyyy-MM-dd"));
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                double total = Convert.ToDouble(reader["total"]);
                                Venta2 venta = new Venta2
                                {
                                    IdVenta = idVenta,
                                    Fecha = fecha,
                                    Total = total
                                };
                                ventas_lo.Add(venta);
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
                return ventas_lo;
            }
        }
        public static List<Venta2> BuscarVentasPorFechaHora(DateTime fechaSeleccionada)
        {
            List<Venta2> ventas_lo = new List<Venta2>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Venta WHERE fecha = @fecha_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@fecha_p", fechaSeleccionada.ToString("yyyy-MM-dd HH:mm:ss"));
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                double total = Convert.ToDouble(reader["total"]);
                                Venta2 venta = new Venta2
                                {
                                    IdVenta = idVenta,
                                    Fecha = fecha,
                                    Total = total
                                };
                                ventas_lo.Add(venta);
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
                return ventas_lo;
            }
        }
        // VENTANA OPCIONES
        public static List<Venta2> BuscarVentasPorYear(int year)
        {
            List<Venta2> ventas_lo = new List<Venta2>();
            using (MySqlConnection conexion = Conexion.ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Venta WHERE YEAR(fecha) = @fecha_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@fecha_p", year);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenta = Convert.ToInt32(reader["id_venta"]);
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                double total = Convert.ToDouble(reader["total"]);
                                Venta2 venta = new Venta2
                                {
                                    IdVenta = idVenta,
                                    Fecha = fecha,
                                    Total = total
                                };
                                ventas_lo.Add(venta);
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
                return ventas_lo;
            }
        }



    }
}
