using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluación.Modelo
{
    internal class conexion
    {
        private readonly string varconexion = "Server=(local);database=Biblioteca;uid=sa;pwd=123";
        public SqlConnection obtenerConexion()
        {
            return new SqlConnection(varconexion);
        }
    }
}
