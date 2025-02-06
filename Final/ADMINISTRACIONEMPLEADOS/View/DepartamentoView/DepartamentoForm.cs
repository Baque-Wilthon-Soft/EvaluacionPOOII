using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADMINISTRACIONEMPLEADOS.Controller;
using ADMINISTRACIONEMPLEADOS.Model;

namespace ADMINISTRACIONEMPLEADOS.View.DepartamentoForm
{
    public partial class DepartamentoForm : Form
    {
        private DepartamentoController departamentoController = new DepartamentoController();
        private int departamentoSeleccionadoID = -1;
        public DepartamentoForm()
        {
            InitializeComponent();
            CargarDepartamentos();
            LimpiarCampos();
        }
        private void CargarDepartamentos()
        {
            dgvDepartamentos.DataSource = departamentoController.ObtenerDepartamentos();
        }
        private void dgvDepartamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                departamentoSeleccionadoID = Convert.ToInt32(dgvDepartamentos.Rows[e.RowIndex].Cells[0].Value);
                txtNombre.Text = dgvDepartamentos.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtUbicacion.Text = dgvDepartamentos.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                Departamento nuevoDepartamento = new Departamento
                {
                    Nombre = txtNombre.Text,
                    Ubicacion = txtUbicacion.Text
                };

                departamentoController.AgregarDepartamento(nuevoDepartamento);
                LimpiarCampos();
                CargarDepartamentos();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (departamentoSeleccionadoID != -1 && ValidarCampos())
            {
                Departamento departamentoEditado = new Departamento
                {
                    ID = departamentoSeleccionadoID,
                    Nombre = txtNombre.Text,
                    Ubicacion = txtUbicacion.Text
                };

                departamentoController.EditarDepartamento(departamentoEditado);
                LimpiarCampos();
                CargarDepartamentos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (departamentoSeleccionadoID != -1)
            {
                departamentoController.EliminarDepartamento(departamentoSeleccionadoID);
                LimpiarCampos();
                CargarDepartamentos();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtUbicacion.Clear();
            departamentoSeleccionadoID = -1;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtUbicacion.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
