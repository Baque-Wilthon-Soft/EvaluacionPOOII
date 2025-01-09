using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Evaluación.Modelo;
using Evaluación.Controlador;
namespace Evaluación.Vistas.Usuarios
{
    public partial class frmUsuarios : Form
    {
        private cls_usuarios controlUsuarios = new cls_usuarios();
        private dto_usuarios usuarioActual;
        public frmUsuarios(dto_usuarios usuario = null)
        {
            InitializeComponent();
            usuarioActual = usuario;

            if (usuarioActual != null)
            {
                lblFrm.Text = "Editar";
                txtNombre.Text = usuarioActual.Nombre ?? string.Empty;
                txtDireccion.Text = usuarioActual.Direccion ?? string.Empty;
                txtTelefono.Text = usuarioActual.Telefono ?? string.Empty;
                txtEmail.Text = usuarioActual.Email ?? string.Empty;
            }
            else
            {
                lblFrm.Text = "Nuevo";
            }
            txtTelefono.KeyPress += TxtTelefono_KeyPress;
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            var nuevoUsuario = new dto_usuarios
            {
                Nombre = txtNombre.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text
            };

            string resultado;
            if (usuarioActual == null)
            {
                resultado = controlUsuarios.Insertar(nuevoUsuario);
            }
            else
            {
                nuevoUsuario.UsuarioID = usuarioActual.UsuarioID;
                resultado = controlUsuarios.Actualizar(nuevoUsuario);
            }

            if (resultado == "ok")
            {
                MessageBox.Show("Usuario guardado con éxito.");
                this.Close();
            }
            else
            {
                MessageBox.Show($"Error: {resultado}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
