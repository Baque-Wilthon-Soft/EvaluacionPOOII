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

namespace FacturacionElectronica.View.ClientesView
{
    public partial class CUCliente : UserControl
    {
        private ClienteController clienteController = new ClienteController();
        public CUCliente()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            CargarClientes();
        }
        private void ConfigurarDataGridView()
        {
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.Columns.Clear();

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", DataPropertyName = "ID", HeaderText = "ID", Width = 50 });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre", Width = 150 });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Apellido", DataPropertyName = "Apellido", HeaderText = "Apellido", Width = 150 });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Cedula", DataPropertyName = "Cedula", HeaderText = "Cédula", Width = 120 });

            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
            {
                Name = "Editar",
                HeaderText = "Editar",
                Text = "✏️",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            dgvClientes.Columns.Add(btnEditar);

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Eliminar",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            dgvClientes.Columns.Add(btnEliminar);
        }

        private void CargarClientes()
        {
            dgvClientes.DataSource = clienteController.ObtenerClientes();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                CargarClientes();
            }
            else
            {
                dgvClientes.DataSource = clienteController.ObtenerClientesPorNombre(nombre);
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            FrmClientes frm = new FrmClientes();
            frm.ShowDialog();
            CargarClientes();
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idCliente = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["ID"].Value);

                if (dgvClientes.Columns[e.ColumnIndex].Name == "Editar")
                {
                    FrmClientes frm = new FrmClientes(idCliente);
                    frm.ShowDialog();
                    CargarClientes();
                }
                else if (dgvClientes.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    DialogResult result = MessageBox.Show("¿Seguro que desea eliminar este cliente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        clienteController.EliminarCliente(idCliente);
                        CargarClientes();
                    }
                }
            }
        }
    }
}
