using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacturacionElectronica.Model;
using FacturacionElectronica.Modelo;

namespace FacturacionElectronica.Controllers
{
    public class ProductoController
    {
        private readonly conexion cn = new conexion();

        public void AgregarProducto(Producto producto)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "INSERT INTO Productos (Nombre, Precio, Stock) VALUES (@Nombre, @Precio, @Stock)";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (var conexion = cn.obtenerConexion())
            {
                string query = "SELECT * FROM Productos";
                using (var comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            productos.Add(new Producto
                            {
                                ID = (int)lector["ID"],
                                Nombre = lector["Nombre"].ToString(),
                                Precio = (decimal)lector["Precio"],
                                Stock = (int)lector["Stock"]
                            });
                        }
                    }
                }
            }
            return productos;
        }
        public Producto ObtenerProductoPorID(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "SELECT * FROM Productos WHERE ID = @ID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", id);
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            return new Producto
                            {
                                ID = (int)lector["ID"],
                                Nombre = lector["Nombre"].ToString(),
                                Precio = (decimal)lector["Precio"],
                                Stock = (int)lector["Stock"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void ActualizarProducto(Producto producto)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "UPDATE Productos SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock WHERE ID = @ID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", producto.ID);
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EliminarProducto(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "DELETE FROM Productos WHERE ID = @ID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", id);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Producto> BuscarProductos(string nombre)
        {
            List<Producto> productos = new List<Producto>();
            using (var conexion = cn.obtenerConexion())
            {
                string query = "SELECT * FROM Productos WHERE Nombre LIKE @Nombre + '%'";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            productos.Add(new Producto
                            {
                                ID = (int)lector["ID"],
                                Nombre = lector["Nombre"].ToString(),
                                Precio = (decimal)lector["Precio"],
                                Stock = (int)lector["Stock"]
                            });
                        }
                    }
                }
            }
            return productos;
        }
    }
}
