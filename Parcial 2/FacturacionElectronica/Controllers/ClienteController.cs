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
    public class ClienteController
    {
        private readonly conexion cn = new conexion();

        public string AgregarCliente(Cliente cliente)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "INSERT INTO Clientes (Nombre, Apellido, Cedula, Direccion, Telefono, Email) VALUES (@Nombre, @Apellido, @Cedula, @Direccion, @Telefono, @Email)";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    comando.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                    comando.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@Email", cliente.Email);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    return "Cliente agregado correctamente";
                }
            }
        }

        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (var conexion = cn.obtenerConexion())
            {
                string query = "SELECT * FROM Clientes";
                using (var comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            clientes.Add(new Cliente
                            {
                                ID = (int)lector["ID"],
                                Nombre = lector["Nombre"].ToString(),
                                Apellido = lector["Apellido"].ToString(),
                                Cedula = lector["Cedula"].ToString(),
                                Direccion = lector["Direccion"].ToString(),
                                Telefono = lector["Telefono"].ToString(),
                                Email = lector["Email"].ToString()
                            });
                        }
                    }
                }
            }
            return clientes;
        }
        public List<Cliente> ObtenerClientesPorNombre(string nombre)
        {
            List<Cliente> clientes = new List<Cliente>();
            using (var conexion = cn.obtenerConexion())
            {
                string query = "SELECT ID, Nombre, Apellido, Cedula FROM Clientes WHERE Nombre LIKE @Nombre + '%'";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    conexion.Open();
                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            clientes.Add(new Cliente
                            {
                                ID = (int)lector["ID"],
                                Nombre = lector["Nombre"].ToString(),
                                Apellido = lector["Apellido"].ToString(),
                                Cedula = lector["Cedula"].ToString()
                            });
                        }
                    }
                }
            }
            return clientes;
        }

        public void EliminarCliente(int id)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "DELETE FROM Clientes WHERE ID = @ID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", id);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
        public Cliente ObtenerClientePorID(int id)
        {
            Cliente cliente = null;
            using (var conexion = cn.obtenerConexion())
            {
                string query = "SELECT ID, Nombre, Apellido, Cedula FROM Clientes WHERE ID = @ID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", id);
                    conexion.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                ID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Cedula = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return cliente;
            
        }

        public string ActualizarCliente(Cliente cliente)
        {
            using (var conexion = cn.obtenerConexion())
            {
                string query = "UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Cedula = @Cedula, Direccion = @Direccion, Telefono = @Telefono, Email = @Email WHERE ID = @ID";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@ID", cliente.ID);
                    comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    comando.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    comando.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                    comando.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@Email", cliente.Email);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return "Cliente actualizado correctamente";
        }
    }
}
