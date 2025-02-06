using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMINISTRACIONEMPLEADOS.Model
{
    public class conexion
    {
        private readonly string varconexion =
            "Server=(local);database=GestionEmpleados;uid=sa;pwd=123";

        public SqlConnection obtenerConexion()
        {
            return new SqlConnection(varconexion);
        }

    }
}
