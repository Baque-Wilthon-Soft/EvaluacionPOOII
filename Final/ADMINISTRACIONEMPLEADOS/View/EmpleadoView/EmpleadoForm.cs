using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADMINISTRACIONEMPLEADOS.Controller;
using ADMINISTRACIONEMPLEADOS.Model;

namespace ADMINISTRACIONEMPLEADOS.View.EmpleadoView
{
    public partial class EmpleadoForm : Form
    {
        private EmpleadoController empleadoController = new EmpleadoController();
        private CargoController cargoController = new CargoController();
        private DepartamentoController departamentoController = new DepartamentoController();
        private int empleadoSeleccionadoID = -1;
        public EmpleadoForm()
        {
            InitializeComponent();
            CargarEmpleados();
            CargarCombos();
            LimpiarCampos();
        }
        private void CargarEmpleados()
        {
            dgvEmpleados.DataSource = empleadoController.ObtenerEmpleados();
        }

        private void CargarCombos()
        {
            cmbCargo.DataSource = cargoController.ObtenerCargos();
            cmbCargo.DisplayMember = "Nombre";
            cmbCargo.ValueMember = "ID";

            cmbDepartamento.DataSource = departamentoController.ObtenerDepartamentos();
            cmbDepartamento.DisplayMember = "Nombre";
            cmbDepartamento.ValueMember = "ID";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                string salarioTexto = txtSalario.Text.Replace(".", "");
                decimal salario = Convert.ToDecimal(salarioTexto, CultureInfo.GetCultureInfo("es-ES"));

                Empleado nuevoEmpleado = new Empleado
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    CargoID = Convert.ToInt32(cmbCargo.SelectedValue),
                    DepartamentoID = Convert.ToInt32(cmbDepartamento.SelectedValue),
                    Salario = salario 
                };

                empleadoController.AgregarEmpleado(nuevoEmpleado);
                LimpiarCampos();
                CargarEmpleados();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (empleadoSeleccionadoID != -1 && ValidarCampos())
            {
                string salarioTexto = txtSalario.Text.Replace(".", "");

                Empleado empleadoEditado = new Empleado
                {
                    ID = empleadoSeleccionadoID,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    CargoID = Convert.ToInt32(cmbCargo.SelectedValue),
                    DepartamentoID = Convert.ToInt32(cmbDepartamento.SelectedValue),
                    Salario = Convert.ToDecimal(salarioTexto, CultureInfo.GetCultureInfo("es-ES"))
                };

                empleadoController.EditarEmpleado(empleadoEditado);
                LimpiarCampos();
                CargarEmpleados();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (empleadoSeleccionadoID != -1)
            {
                empleadoController.EliminarEmpleado(empleadoSeleccionadoID);
                LimpiarCampos();
                CargarEmpleados();
            }
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                empleadoSeleccionadoID = Convert.ToInt32(dgvEmpleados.Rows[e.RowIndex].Cells["ID"].Value);
                txtNombre.Text = dgvEmpleados.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtApellido.Text = dgvEmpleados.Rows[e.RowIndex].Cells["Apellido"].Value.ToString();
                cmbCargo.SelectedValue = dgvEmpleados.Rows[e.RowIndex].Cells["CargoID"].Value;
                cmbDepartamento.SelectedValue = dgvEmpleados.Rows[e.RowIndex].Cells["DepartamentoID"].Value;
                decimal salario = Convert.ToDecimal(dgvEmpleados.Rows[e.RowIndex].Cells["Salario"].Value);
                txtSalario.Text = salario.ToString("N2", new CultureInfo("es-ES")).Replace(".", "");
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            cmbCargo.SelectedIndex = -1;
            cmbDepartamento.SelectedIndex = -1;
            txtSalario.Clear();
            empleadoSeleccionadoID = -1;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtSalario.Text) ||
                cmbCargo.SelectedIndex == -1 ||
                cmbDepartamento.SelectedIndex == -1)
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return false;
            }

            if (!ValidarSalario(txtSalario.Text))
            {
                MessageBox.Show("El salario debe ser un número positivo con hasta 18 dígitos y máximo 2 decimales.");
                return false;
            }

            return true;


        }

        private bool ValidarSalario(string salarioTexto)
        {
            salarioTexto = salarioTexto.Replace(".", "");

            if (!decimal.TryParse(salarioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-ES"), out decimal salario))
                return false;

            if (salario < 0) return false;

            string[] partes = salarioTexto.Split(',');
            int enteros = partes[0].Length;
            int decimales = partes.Length > 1 ? partes[1].Length : 0;

            return enteros <= 18 && decimales <= 2;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void txtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == ',' && txtSalario.Text.Contains(","))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
