using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacturacionElectronica.Controllers;
using FacturacionElectronica.Model;

namespace FacturacionElectronica.View
{
    public partial class FrmClientes : Form
    {
        ClienteController clienteController = new ClienteController();
        private int clienteID = 0;

        public FrmClientes()
        {
            InitializeComponent();
            txtCedula.KeyPress += TxtSoloNumeros_KeyPress;
            txtTelefono.KeyPress += TxtSoloNumeros_KeyPress;
        }
        public FrmClientes(int idCliente)
        {
            InitializeComponent();
            clienteID = idCliente;
            lblTitulo.Text = "Editar Cliente";
            CargarCliente();
        }
        private void TxtSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void CargarCliente()
        {
            Cliente cliente = clienteController.ObtenerClientePorID(clienteID);
            if (cliente != null)
            {
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtCedula.Text = cliente.Cedula;
                txtDireccion.Text = cliente.Direccion;
                txtTelefono.Text = cliente.Telefono;
                txtEmail.Text = cliente.Email;
                btnAgregar.Text = "Actualizar";
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtCedula.Text))
            {
                MessageBox.Show("Los campos Nombre, Apellido y Cédula son obligatorios.");
                return;
            }

            Cliente cliente = new Cliente()
            {
                ID = clienteID,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Cedula = txtCedula.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text
            };

            if (clienteID == 0)
            {
                string resultado = clienteController.AgregarCliente(cliente);
                MessageBox.Show(resultado);
            }
            else
            {
               
                string resultado = clienteController.ActualizarCliente(cliente);
                MessageBox.Show(resultado);
            }

            this.Close(); 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtCedula.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
        }
    }
}
