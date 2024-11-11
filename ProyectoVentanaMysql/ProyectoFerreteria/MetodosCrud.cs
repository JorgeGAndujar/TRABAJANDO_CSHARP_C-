using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    public class MetodosCrud
    {
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
        public static void ActualizarUsuario(Usuario usuario)
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
                        cmd.Parameters.AddWithValue("@contrasena_p", BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena));
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
        public static void EliminarUsuario(Usuario usuario)
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
                        cmd.Parameters.AddWithValue("@id_usuario_p", usuario.IdUsuario);
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

    }
}
