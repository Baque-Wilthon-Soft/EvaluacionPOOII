
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADMINISTRACIONEMPLEADOS.Model;

namespace ADMINISTRACIONEMPLEADOS.Controller
{
    public class EmpleadoController
    {
        private readonly conexion cn = new conexion();

        
        public List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "SELECT * FROM Empleado";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Empleado
                    {
                        ID = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        CargoID = reader.GetInt32(3),
                        DepartamentoID = reader.GetInt32(4),
                        Salario = reader.GetDecimal(5)
                    });
                }
            }
            return lista;
        }

        public void AgregarEmpleado(Empleado empleado)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "INSERT INTO Empleado (Nombre, Apellido, CargoID, DepartamentoID, Salario) VALUES (@Nombre, @Apellido, @CargoID, @DepartamentoID, @Salario)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                cmd.Parameters.AddWithValue("@CargoID", empleado.CargoID);
                cmd.Parameters.AddWithValue("@DepartamentoID", empleado.DepartamentoID);
                cmd.Parameters.AddWithValue("@Salario", empleado.Salario);
                cmd.ExecuteNonQuery();
            }
        }

        public void EditarEmpleado(Empleado empleado)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "UPDATE Empleado SET Nombre=@Nombre, Apellido=@Apellido, CargoID=@CargoID, DepartamentoID=@DepartamentoID, Salario=@Salario WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", empleado.ID);
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                cmd.Parameters.AddWithValue("@CargoID", empleado.CargoID);
                cmd.Parameters.AddWithValue("@DepartamentoID", empleado.DepartamentoID);
                cmd.Parameters.AddWithValue("@Salario", empleado.Salario);
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarEmpleado(int id)
        {
            using (SqlConnection con = cn.obtenerConexion())
            {
                con.Open();
                string query = "DELETE FROM Empleado WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
