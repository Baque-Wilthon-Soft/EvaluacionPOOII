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

namespace FacturacionElectronica.View.FacturaView
{
    public partial class CUFactura : UserControl
    {
        private FacturaController facturaController = new FacturaController();

        public CUFactura()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            CargarFacturas();
            dgvFactura.CellClick += dgvFactura_CellClick;
        }
        private void ConfigurarDataGridView()
        {
            dgvFactura.AutoGenerateColumns = false;
            dgvFactura.Columns.Clear();

            dgvFactura.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ID", DataPropertyName = "ID", HeaderText = "ID", Width = 50 });
            dgvFactura.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Fecha", DataPropertyName = "Fecha", HeaderText = "Fecha", Width = 120 });
            dgvFactura.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreCliente", DataPropertyName = "NombreCliente", HeaderText = "Cliente", Width = 150 });
            dgvFactura.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Cedula", DataPropertyName = "Cedula", HeaderText = "Cédula", Width = 120 });
            dgvFactura.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Total", DataPropertyName = "Total", HeaderText = "Total", Width = 100 });

            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
            {
                Name = "Editar",
                HeaderText = "Editar",
                Text = "✏️",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            dgvFactura.Columns.Add(btnEditar);

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Eliminar",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            dgvFactura.Columns.Add(btnEliminar);
        }

        private void CargarFacturas()
        {
            var facturas = facturaController.ObtenerTodasLasFacturas();
            if (facturas != null)
            {
                dgvFactura.DataSource = null;
                dgvFactura.DataSource = facturas;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                CargarFacturas();
            }
            else
            {
                dgvFactura.DataSource = facturaController.ObtenerFacturasPorNombreCliente(nombre);
            }
        }

        private void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            FrmFactura frm = new FrmFactura();
            frm.ShowDialog();
            CargarFacturas();
        }

        private void dgvFactura_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dgvFactura.Columns[e.ColumnIndex].Name;
                int idFactura = Convert.ToInt32(dgvFactura.Rows[e.RowIndex].Cells["ID"].Value);

                if (columnName == "Editar")
                {
                    FrmFactura frm = new FrmFactura(idFactura); 
                    frm.ShowDialog();
                    CargarFacturas();
                }
                else if (columnName == "Eliminar")
                {
                    DialogResult result = MessageBox.Show("¿Seguro que desea eliminar esta factura?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        facturaController.EliminarFactura(idFactura); 
                        CargarFacturas();
                    }
                }
            }
            
        }
    }
}
