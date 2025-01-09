using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Evaluación.Modelo;

namespace Evaluación.Controlador
{
    public class cls_libros
    {
        private readonly conexion cn = new conexion();

        public List<dto_prestamos> Buscar(string texto)
        {
            var listaPrestamos = new List<dto_prestamos>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena =
                    "SELECT p.PrestamoID, u.Nombre AS Usuario, l.Titulo AS Libro, p.FechaPrestamo, p.FechaDevolucion " +
                    "FROM Prestamos p " +
                    "INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID " +
                    "INNER JOIN Libros l ON p.LibroID = l.LibroID " +
                    $"WHERE u.Nombre LIKE '%{texto}%' OR l.Titulo LIKE '%{texto}%'";

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
        public string Insertar(dto_libros libro)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "INSERT INTO Libros (Titulo, Autor, Genero, AnioPublicacion, Disponible) VALUES (" +
                                $"'{libro.Titulo}', '{libro.Autor}', '{libro.Genero}', {libro.AnioPublicacion}, {(libro.Disponible ? 1 : 0)})";
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
        public List<dto_libros> Todos()
        {
            var listaLibros = new List<dto_libros>();
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "SELECT * FROM Libros";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            var libro = new dto_libros
                            {
                                LibroID = (int)lector["LibroID"],
                                Titulo = lector["Titulo"].ToString(),
                                Autor = lector["Autor"].ToString(),
                                Genero = lector["Genero"].ToString(),
                                AnioPublicacion = lector["AnioPublicacion"] as int?,
                                Disponible = (bool)lector["Disponible"]
                            };
                            listaLibros.Add(libro);
                        }
                    }
                }
            }
            return listaLibros;
        }

        public string Actualizar(dto_libros libro)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = "UPDATE Libros SET " +
                                $"Titulo = '{libro.Titulo}', " +
                                $"Autor = '{libro.Autor}', " +
                                $"Genero = '{libro.Genero}', " +
                                $"AnioPublicacion = {libro.AnioPublicacion}, " +
                                $"Disponible = {(libro.Disponible ? 1 : 0)} " +
                                $"WHERE LibroID = {libro.LibroID}";
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

        // Eliminar libro
        public bool Eliminar(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"DELETE FROM Libros WHERE LibroID = {id}";
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

        public dto_libros ObtenerPorId(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string cadena = $"SELECT * FROM Libros WHERE LibroID = {id}";
                using (var comando = new SqlCommand(cadena, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return new dto_libros
                            {
                                LibroID = (int)lector["LibroID"],
                                Titulo = lector["Titulo"].ToString(),
                                Autor = lector["Autor"].ToString(),
                                Genero = lector["Genero"].ToString(),
                                AnioPublicacion = lector["AnioPublicacion"] as int?,
                                Disponible = (bool)lector["Disponible"]
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
