using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Evaluación.Modelo;

namespace Evaluación.Controlador
{
    internal class cls_usuarios
    {
        private readonly conexion cn = new conexion();

        public List<dto_usuarios> Buscar(string texto)
        {
            var listaUsuarios = new List<dto_usuarios>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"SELECT * FROM Usuarios WHERE Nombre LIKE '%{texto}%' OR Direccion LIKE '%{texto}%' OR Telefono LIKE '%{texto}%' OR Email LIKE '%{texto}%'";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var usuario = new dto_usuarios
                            {
                                UsuarioID = (int)lector["UsuarioID"],
                                Nombre = (string)lector["Nombre"],
                                Direccion = lector["Direccion"] as string,
                                Telefono = lector["Telefono"] as string,
                                Email = lector["Email"] as string
                            };
                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }
            return listaUsuarios;
        }

        public string Insertar(dto_usuarios usuario)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "INSERT INTO Usuarios (Nombre, Direccion, Telefono, Email) VALUES (" +
                                $"'{usuario.Nombre}', '{usuario.Direccion}', '{usuario.Telefono}', '{usuario.Email}')";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return "ok";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
        }

        public List<dto_usuarios> Todos()
        {
            var listaUsuarios = new List<dto_usuarios>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT * FROM Usuarios";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var usuario = new dto_usuarios
                            {
                                UsuarioID = (int)lector["UsuarioID"],
                                Nombre = lector["Nombre"]?.ToString(),
                                Direccion = lector["Direccion"]?.ToString(),
                                Telefono = lector["Telefono"]?.ToString(),
                                Email = lector["Email"]?.ToString()
                                
                            };
                            listaUsuarios.Add(usuario);
                        }
                    }
                }
            }
            return listaUsuarios;
        }

        public string Actualizar(dto_usuarios usuario)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "UPDATE Usuarios SET " +
                                $"Nombre = '{usuario.Nombre}', " +
                                $"Direccion = '{usuario.Direccion}', " +
                                $"Telefono = '{usuario.Telefono}', " +
                                $"Email = '{usuario.Email}' " +
                                $"WHERE UsuarioID = {usuario.UsuarioID}";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return "ok";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"DELETE FROM Usuarios WHERE UsuarioID = {id}";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public dto_usuarios ObtenerPorId(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"SELECT * FROM Usuarios WHERE UsuarioID = {id}";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return new dto_usuarios
                            {
                                UsuarioID = (int)lector["UsuarioID"],
                                Nombre = lector["Nombre"].ToString(),
                                Direccion = lector["Direccion"] as string,
                                Telefono = lector["Telefono"] as string,
                                Email = lector["Email"] as string
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
