using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADMINISTRACIONEMPLEADOS.View.CargoView;
using ADMINISTRACIONEMPLEADOS.View.DepartamentoForm;
using ADMINISTRACIONEMPLEADOS.View.EmpleadoView;

namespace ADMINISTRACIONEMPLEADOS
{
    public partial class MenúPrincipal : Form
    {
        public MenúPrincipal()
        {
            InitializeComponent();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            EmpleadoForm form = new EmpleadoForm();
            form.ShowDialog();
        }

        private void btnCargos_Click(object sender, EventArgs e)
        {
            CargoForm form = new CargoForm();
            form.ShowDialog();
        }

        private void Departamentos_Click(object sender, EventArgs e)
        {
            DepartamentoForm form = new DepartamentoForm();
            form.ShowDialog();
        }
    }
}
