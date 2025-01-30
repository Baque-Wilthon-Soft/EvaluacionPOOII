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
    public class FacturaController
    {
        private readonly conexion cn = new conexion();

        public int AgregarFactura(Factura factura)
        {
            int facturaID = 0; 
            using (var conexion = cn.obtenerConexion())
            {
                string query = "INSERT INTO Facturas (ClienteID, Fecha, Total) OUTPUT INSERTED.ID VALUES (@ClienteID, @Fecha, @Total)";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ClienteID", factura.ClienteID);
                    comando.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    comando.Parameters.AddWithValue("@Total", factura.Total);

                    conexion.Open();
                    facturaID = (int)comando.ExecuteScalar(); 
                }
            }
            return facturaID;
        }
        public List<dynamic> ObtenerTodasLasFacturas()
        {
            List<dynamic> facturas = new List<dynamic>();
            try
            {
                using (var conexion = cn.obtenerConexion())
                {
                    string query = @"
                SELECT f.ID, f.Fecha, f.Total, c.Nombre AS NombreCliente, c.Cedula 
                FROM Facturas f
                INNER JOIN Clientes c ON f.ClienteID = c.ID";

                    using (var comando = new SqlCommand(query, conexion))
                    {
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                facturas.Add(new
                                {
                                    ID = reader.GetInt32(0),
                                    Fecha = reader.GetDateTime(1),
                                    Total = reader.GetDecimal(2),
                                    NombreCliente = reader.GetString(3),
                                    Cedula = reader.GetString(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener facturas: " + ex.Message);
            }
            return facturas;
        }

        public List<dynamic> ObtenerFacturasPorNombreCliente(string nombreCliente)
        {
            List<dynamic> facturas = new List<dynamic>();
            try
            {
                using (var conexion = cn.obtenerConexion())
                {
                    string query = @"
                SELECT f.ID, f.Fecha, f.Total, c.Nombre AS NombreCliente, c.Cedula
                FROM Facturas f
                INNER JOIN Clientes c ON f.ClienteID = c.ID
                WHERE c.Nombre LIKE @NombreCliente";

                    using (var comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@NombreCliente", "%" + nombreCliente + "%");
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                facturas.Add(new
                                {
                                    ID = reader.GetInt32(0),
                                    Fecha = reader.GetDateTime(1),
                                    Total = reader.GetDecimal(2),
                                    NombreCliente = reader.GetString(3),
                                    Cedula = reader.GetString(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar facturas por cliente: " + ex.Message);
            }
            return facturas;
        }

        public bool EliminarFactura(int idFactura)
        {
            bool exito = false;
            try
            {
                using (var conexion = cn.obtenerConexion())
                {
                    string query = "DELETE FROM Facturas WHERE ID = @ID";
                    using (var comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@ID", idFactura);
                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();
                        exito = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la factura: " + ex.Message);
            }
            return exito;
        }
        
        public Factura ObtenerFacturaPorId(int idFactura)
        {
            Factura factura = null;
            using (var conexion = cn.obtenerConexion())
            {
                string query = "SELECT ID, ClienteID, Fecha, Total FROM Facturas WHERE ID = @ID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", idFactura);
                    conexion.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            factura = new Factura
                            {
                                ID = reader.GetInt32(0),
                                ClienteID = reader.GetInt32(1),
                                Fecha = reader.GetDateTime(2),
                                Total = reader.GetDecimal(3)
                            };
                        }
                    }
                }
            }
            return factura;
        }


        public bool ActualizarFactura(Factura factura)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "UPDATE Facturas SET ClienteID = @ClienteID, Fecha = @Fecha, Total = @Total WHERE ID = @ID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ClienteID", factura.ClienteID);
                    comando.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    comando.Parameters.AddWithValue("@Total", factura.Total);
                    comando.Parameters.AddWithValue("@ID", factura.ID);

                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}
