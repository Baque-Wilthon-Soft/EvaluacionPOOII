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
    public class DetalleFacturaController
    {
        private readonly conexion cn = new conexion();

        public void AgregarDetalle(DetalleFactura detalle)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "INSERT INTO DetallesFactura (FacturaID, ProductoID, Cantidad, Subtotal) VALUES (@FacturaID, @ProductoID, @Cantidad, @Subtotal)";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@FacturaID", detalle.FacturaID);
                    comando.Parameters.AddWithValue("@ProductoID", detalle.ProductoID);
                    comando.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    comando.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
        public List<DetalleFactura> ObtenerDetallesPorFactura(int idFactura)
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();

            using (var conexion = cn.obtenerConexion())
            {
                string query = "SELECT ProductoID, Cantidad, Subtotal FROM DetallesFactura WHERE FacturaID = @FacturaID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@FacturaID", idFactura);
                    conexion.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            detalles.Add(new DetalleFactura
                            {
                                ProductoID = reader.GetInt32(0),
                                Cantidad = reader.GetInt32(1),
                                Subtotal = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }
            return detalles;
        }
    
    public bool EliminarDetallesPorFactura(int facturaID)
        {
            bool exito = false;
            try
            {
                using (var conexion = cn.obtenerConexion())
                {
                    string query = "DELETE FROM DetallesFactura WHERE FacturaID = @FacturaID";
                    using (var comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@FacturaID", facturaID);
                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();
                        exito = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar los detalles de la factura: " + ex.Message);
            }
            return exito;
        }
    }
}
