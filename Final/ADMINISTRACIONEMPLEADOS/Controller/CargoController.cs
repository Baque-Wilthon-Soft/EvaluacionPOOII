using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADMINISTRACIONEMPLEADOS.Model;

namespace ADMINISTRACIONEMPLEADOS.Controller
{
    public class CargoController
    {
        private readonly conexion cn = new conexion();

        public List<Cargo> ObtenerCargos()
        {
            List<Cargo> lista = new List<Cargo>();
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "SELECT * FROM Cargo";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Cargo
                    {
                        ID = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Descripcion = reader.GetString(2)
                    });
                }
            }
            return lista;
        }

        public void AgregarCargo(Cargo cargo)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "INSERT INTO Cargo (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", cargo.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", cargo.Descripcion);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarCargo(Cargo cargo)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "UPDATE Cargo SET Nombre=@Nombre, Descripcion=@Descripcion WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", cargo.ID);
                cmd.Parameters.AddWithValue("@Nombre", cargo.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", cargo.Descripcion);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarCargo(int id)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "DELETE FROM Cargo WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
