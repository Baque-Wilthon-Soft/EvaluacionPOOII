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
using FacturacionElectronica.View.ProductoView;

namespace FacturacionElectronica.View.ProductoView
{
    public partial class CUProducto : UserControl
    {
        ProductoController productoController = new ProductoController();
        public CUProducto()
        {
            InitializeComponent();
            CargarProductos();
            ConfigurarDataGridView();
        }
        private void CargarProductos()
        {
            dgvProductos.DataSource = productoController.ObtenerProductos();
        }

        private void ConfigurarDataGridView()
        {
            if (dgvProductos.Columns["Editar"] == null)
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                btnEditar.Name = "Editar";
                btnEditar.HeaderText = "Editar";
                btnEditar.Text = "✎";
                btnEditar.UseColumnTextForButtonValue = true;
                dgvProductos.Columns.Add(btnEditar);
            }

            if (dgvProductos.Columns["Eliminar"] == null)
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.Name = "Eliminar";
                btnEliminar.HeaderText = "Eliminar";
                btnEliminar.Text = "🗑";
                btnEliminar.UseColumnTextForButtonValue = true;
                dgvProductos.Columns.Add(btnEliminar);
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            dgvProductos.DataSource = productoController.BuscarProductos(nombre);
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            FrmProducto frm = new FrmProducto();
            frm.ShowDialog();
            CargarProductos();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idProducto = (int)dgvProductos.Rows[e.RowIndex].Cells["ID"].Value;

                if (dgvProductos.Columns[e.ColumnIndex].Name == "Editar")
                {
                    FrmProducto frm = new FrmProducto(idProducto); 
                    frm.ShowDialog();
                    CargarProductos(); 
                }

                if (dgvProductos.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    DialogResult result = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        productoController.EliminarProducto(idProducto);
                        CargarProductos();
                    }
                }
            }
        }
    }
}
