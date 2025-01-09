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

namespace Evaluación.Vistas.Prestamos
{
    public partial class CUPrestamos : UserControl
    {
        public CUPrestamos()
        {
            InitializeComponent();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmPrestamos nuevoPrestamo = new frmPrestamos();
            nuevoPrestamo.Text = "Nuevo Préstamo";
            nuevoPrestamo.ShowDialog();
            CargarGrilla(1);
        }

        private void CUPrestamos_Load(object sender, EventArgs e)
        {
            CargarGrilla(1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            CargarGrilla(2);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            CargarGrilla(2);
        }

        private void CargarGrilla(int modo)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            var logicaPrestamos = new cls_prestamos();

            var columnaIndex = new DataGridViewTextBoxColumn
            {
                HeaderText = "N.-",
                ReadOnly = true
            };
            dataGridView1.Columns.Add(columnaIndex);

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

            if (modo == 1 || string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                dataGridView1.DataSource = logicaPrestamos.Todos();
            }
            else
            {
                dataGridView1.DataSource = logicaPrestamos.BuscarPorUsuario(Convert.ToInt32(txtBuscar.Text.Trim()));
            }

            if (dataGridView1.Columns["PrestamoID"] != null)
            {
                dataGridView1.Columns["PrestamoID"].HeaderText = "ID Préstamo";
                dataGridView1.Columns["PrestamoID"].Visible = true; 
            }

            if (dataGridView1.Columns["UsuarioID"] != null)
            {
                dataGridView1.Columns["UsuarioID"].HeaderText = "ID Usuario";
            }

            if (dataGridView1.Columns["LibroID"] != null)
            {
                dataGridView1.Columns["LibroID"].HeaderText = "ID Libro";
            }

            if (dataGridView1.Columns["FechaPrestamo"] != null)
            {
                dataGridView1.Columns["FechaPrestamo"].HeaderText = "Fecha Préstamo";
            }

            if (dataGridView1.Columns["FechaDevolucion"] != null)
            {
                dataGridView1.Columns["FechaDevolucion"].HeaderText = "Fecha Devolución";
            }

            
            dataGridView1.Columns.Add(btnEditar);
            dataGridView1.Columns.Add(btnEliminar);
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var fila = dataGridView1.Rows[e.RowIndex];
                var prestamoID = fila.Cells["PrestamoID"].Value;

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Editar")
                {
                    EditarPrestamo((int)prestamoID);
                }
                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Eliminar")
                {
                    EliminarPrestamo((int)prestamoID);
                }
            }
        }

        private void EditarPrestamo(int id)
        {
            var logicaPrestamos = new cls_prestamos();
            var prestamo = logicaPrestamos.Uno(id);

            if (prestamo != null)
            {
                frmPrestamos editarPrestamo = new frmPrestamos(prestamo);
                editarPrestamo.ShowDialog();
                CargarGrilla(1);
            }
            else
            {
                MessageBox.Show("No se encontró el préstamo con el ID proporcionado.");
            }
        }

        private void EliminarPrestamo(int id)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este préstamo?", "Eliminar Préstamo", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                var logicaPrestamos = new cls_prestamos();
                if (logicaPrestamos.Eliminar(id))
                {
                    MessageBox.Show("Préstamo eliminado con éxito.");
                    CargarGrilla(1);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar el préstamo.");
                }
            }
        }
    }
}
