using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADMINISTRACIONEMPLEADOS.Model;

namespace ADMINISTRACIONEMPLEADOS.Controller
{
    public class DepartamentoController
    {
        private readonly conexion cn = new conexion();

        public List<Departamento> ObtenerDepartamentos()
        {
            List<Departamento> lista = new List<Departamento>();
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "SELECT * FROM Departamento";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Departamento
                    {
                        ID = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Ubicacion = reader.GetString(2)
                    });
                }
            }
            return lista;
        }

        public void AgregarDepartamento(Departamento departamento)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "INSERT INTO Departamento (Nombre, Ubicacion) VALUES (@Nombre, @Ubicacion)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", departamento.Nombre);
                cmd.Parameters.AddWithValue("@Ubicacion", departamento.Ubicacion);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarDepartamento(Departamento departamento)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "UPDATE Departamento SET Nombre=@Nombre, Ubicacion=@Ubicacion WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", departamento.ID);
                cmd.Parameters.AddWithValue("@Nombre", departamento.Nombre);
                cmd.Parameters.AddWithValue("@Ubicacion", departamento.Ubicacion);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarDepartamento(int id)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "DELETE FROM Departamento WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
