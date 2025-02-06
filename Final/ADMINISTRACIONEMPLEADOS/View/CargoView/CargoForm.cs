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

namespace ADMINISTRACIONEMPLEADOS.View.CargoView
{
    public partial class CargoForm : Form
    {
        private CargoController cargoController = new CargoController();
        private int cargoSeleccionadoID = -1;
        public CargoForm()
        {
            InitializeComponent();
            CargarCargos();
            LimpiarCampos();
        }
        private void CargarCargos()
        {
            dgvCargos.DataSource = cargoController.ObtenerCargos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                Cargo nuevoCargo = new Cargo
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text
                };

                cargoController.AgregarCargo(nuevoCargo);
                LimpiarCampos();
                CargarCargos();
            }
        }
        private void dgvCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                cargoSeleccionadoID = Convert.ToInt32(dgvCargos.Rows[e.RowIndex].Cells[0].Value);
                txtNombre.Text = dgvCargos.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDescripcion.Text = dgvCargos.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cargoSeleccionadoID != -1 && ValidarCampos())
            {
                Cargo cargoEditado = new Cargo
                {
                    ID = cargoSeleccionadoID,
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text
                };

                cargoController.EditarCargo(cargoEditado);
                LimpiarCampos();
                CargarCargos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cargoSeleccionadoID != -1)
            {
                cargoController.EliminarCargo(cargoSeleccionadoID);
                LimpiarCampos();
                CargarCargos();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            cargoSeleccionadoID = -1;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
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
