using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADMINISTRACIONEMPLEADOS.Model
{
    public class Empleado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int CargoID { get; set; }
        public int DepartamentoID { get; set; }
        public decimal Salario { get; set; }
    }
}
