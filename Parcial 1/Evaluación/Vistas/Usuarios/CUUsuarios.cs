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
using Evaluación.Vistas;

namespace Evaluación.Vistas.Usuarios
{
    public partial class CUUsuarios : UserControl
    {
        public CUUsuarios()
        {
            InitializeComponent();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmUsuarios nuevoUsuario = new frmUsuarios();
            nuevoUsuario.Text = "Nuevo Usuario";
            nuevoUsuario.ShowDialog();
            CargarGrilla(1);
        }

        private void CUUsuarios_Load(object sender, EventArgs e)
        {
            CargarGrilla(1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla(2);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            CargarGrilla(2);
        }

        private void CargarGrilla(int modo)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            var logicaUsuarios = new cls_usuarios();
            if (modo == 1)
            {
                dataGridView1.DataSource = logicaUsuarios.Todos();
            }
            else
            {
                dataGridView1.DataSource = logicaUsuarios.Buscar(txtBuscar.Text.Trim());
            }

            if (dataGridView1.Columns.Contains("UsuarioID"))
            {
                dataGridView1.Columns["UsuarioID"].Visible = true; 
                dataGridView1.Columns["UsuarioID"].HeaderText = "ID Usuario";
            }

            dataGridView1.Columns["Nombre"].HeaderText = "Nombre";
            dataGridView1.Columns["Direccion"].HeaderText = "Dirección";
            dataGridView1.Columns["Telefono"].HeaderText = "Teléfono";
            dataGridView1.Columns["Email"].HeaderText = "Email";

            var btnEditar = new DataGridViewButtonColumn
            {
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true
            };
            var btnEliminar = new DataGridViewButtonColumn
            {
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(btnEditar);
            dataGridView1.Columns.Add(btnEliminar);
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var fila = dataGridView1.Rows[e.RowIndex];
                var usuarioID = fila.Cells["UsuarioID"].Value;

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Editar")
                {
                    EditarUsuario((int)usuarioID);
                }
                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Eliminar")
                {
                    EliminarUsuario((int)usuarioID);
                }
            }
        }

        private void EditarUsuario(int id)
        {
            var logicaUsuarios = new cls_usuarios();
            var usuario = logicaUsuarios.ObtenerPorId(id); 

            if (usuario != null)
            {
                frmUsuarios editarUsuario = new frmUsuarios(usuario);
                editarUsuario.Text = "Editar Usuario";
                editarUsuario.ShowDialog();
                CargarGrilla(1);
            }
            else
            {
                MessageBox.Show("No se encontró el usuario con el ID especificado.");
            }
        }
        
        private void EliminarUsuario(int id)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este usuario?", "Eliminar Usuario", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                var logicaUsuarios = new cls_usuarios();
                if (logicaUsuarios.Eliminar(id))
                {
                    MessageBox.Show("Usuario eliminado con éxito.");
                    CargarGrilla(1);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar el usuario.");
                }
            }
        }
    }
}
