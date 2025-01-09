using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Evaluación.Modelo;
 
namespace Evaluación.Controlador
{
    public class cls_prestamos
    {
        private readonly conexion cn = new conexion();

        public List<dynamic> Buscar(string texto)
        {
            var listaPrestamos = new List<dynamic>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = @"
            SELECT p.PrestamoID, u.Nombre AS Usuario, l.Titulo AS Libro, 
                   p.FechaPrestamo, p.FechaDevolucion
            FROM Prestamos p
            INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID
            INNER JOIN Libros l ON p.LibroID = l.LibroID
            WHERE u.Nombre LIKE @Texto OR l.Titulo LIKE @Texto";

                using (var comando = new SqlCommand(cadena, conexion))
                {
                    comando.Parameters.AddWithValue("@Texto", $"%{texto}%");
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listaPrestamos.Add(new
                            {
                                PrestamoID = (int)lector["PrestamoID"],
                                Usuario = (string)lector["Usuario"],
                                Libro = (string)lector["Libro"],
                                FechaPrestamo = (DateTime)lector["FechaPrestamo"],
                                FechaDevolucion = lector["FechaDevolucion"] as DateTime?
                            });
                        }
                    }
                }
            }
            return listaPrestamos;
            
        }

        public string Insertar(dto_prestamos prestamo)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "INSERT INTO Prestamos (UsuarioID, LibroID, FechaPrestamo, FechaDevolucion) VALUES (" +
                                $"{prestamo.UsuarioID}, {prestamo.LibroID}, '{prestamo.FechaPrestamo:yyyy-MM-dd}', " +
                                $"{(prestamo.FechaDevolucion.HasValue ? $"'{prestamo.FechaDevolucion.Value:yyyy-MM-dd}'" : "NULL")})";
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

        public List<dto_prestamos> Todos()
        {
            var listaPrestamos = new List<dto_prestamos>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT PrestamoID, UsuarioID, LibroID, FechaPrestamo, FechaDevolucion FROM Prestamos";
                using (var comando = new SqlCommand(cadena, conexion))
                {   
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var prestamo = new dto_prestamos
                            {
                                PrestamoID = (int)lector["PrestamoID"],
                                UsuarioID = (int)lector["UsuarioID"],
                                LibroID = (int)lector["LibroID"],
                                FechaPrestamo = (DateTime)lector["FechaPrestamo"],
                                FechaDevolucion = lector["FechaDevolucion"] as DateTime?
                            };
                            listaPrestamos.Add(prestamo);
                        }
                    }
                }
            }
            return listaPrestamos;
        }
        public dto_prestamos Uno(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"SELECT * FROM Prestamos WHERE PrestamoID = {id}";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            var prestamo = new dto_prestamos
                            {
                                PrestamoID = (int)lector["PrestamoID"],
                                UsuarioID = (int)lector["UsuarioID"],
                                LibroID = (int)lector["LibroID"],
                                FechaPrestamo = (DateTime)lector["FechaPrestamo"],
                                FechaDevolucion = lector["FechaDevolucion"] as DateTime?
                            };
                            return prestamo;
                        }
                        return null;
                    }
                }
            }
        }
        public string Actualizar(dto_prestamos prestamo)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "UPDATE Prestamos SET " +
                                $"UsuarioID = {prestamo.UsuarioID}, " +
                                $"LibroID = {prestamo.LibroID}, " +
                                $"FechaPrestamo = '{prestamo.FechaPrestamo:yyyy-MM-dd}', " +
                                $"FechaDevolucion = {(prestamo.FechaDevolucion.HasValue ? $"'{prestamo.FechaDevolucion.Value:yyyy-MM-dd}'" : "NULL")} " +
                                $"WHERE PrestamoID = {prestamo.PrestamoID}";
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
                string cadena = $"DELETE FROM Prestamos WHERE PrestamoID = {id}";
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

        public List<dto_prestamos> BuscarPorUsuario(int usuarioID)
        {
            var listaPrestamos = new List<dto_prestamos>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"SELECT * FROM Prestamos WHERE UsuarioID = {usuarioID}";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var prestamo = new dto_prestamos
                            {
                                PrestamoID = (int)lector["PrestamoID"],
                                UsuarioID = (int)lector["UsuarioID"],
                                LibroID = (int)lector["LibroID"],
                                FechaPrestamo = (DateTime)lector["FechaPrestamo"],
                                FechaDevolucion = lector["FechaDevolucion"] as DateTime?
                            };
                            listaPrestamos.Add(prestamo);
                        }
                    }
                }
            }
            return listaPrestamos;
        }

        
    }
}
