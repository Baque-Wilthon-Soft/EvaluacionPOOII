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

namespace Evaluación.Vistas.Libros
{
    public partial class CULibros : UserControl
    {
        public CULibros()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmLibros nuevoLibro = new frmLibros();
            nuevoLibro.Text = "Nuevo Libro";
            nuevoLibro.ShowDialog();
            CargarGrilla(1);
        }

        private void CULibros_Load(object sender, EventArgs e)
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

            var logicaLibros = new cls_libros();
            if (modo == 1)
            {
                dataGridView1.DataSource = logicaLibros.Todos();
            }
            else
            {
                dataGridView1.DataSource = logicaLibros.Buscar(txtBuscar.Text.Trim());
            }

            
            if (dataGridView1.Columns.Contains("LibroID"))
            {
                dataGridView1.Columns["LibroID"].Visible = true;
                dataGridView1.Columns["LibroID"].HeaderText = "ID Libro";
            }

            dataGridView1.Columns["Titulo"].HeaderText = "Título";
            dataGridView1.Columns["Autor"].HeaderText = "Autor";
            dataGridView1.Columns["Genero"].HeaderText = "Género";
            dataGridView1.Columns["AnioPublicacion"].HeaderText = "Año de Publicación";
            dataGridView1.Columns["Disponible"].HeaderText = "Disponible";

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
                var libroID = fila.Cells["LibroID"].Value;

                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Editar")
                {
                    EditarLibro((int)libroID);
                }
                else if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Eliminar")
                {
                    EliminarLibro((int)libroID);
                }
            }
        }

        private void EditarLibro(int id)
        {
           

            var logicaLibros = new cls_libros();
            var libro = logicaLibros.ObtenerPorId(id);

            if (libro != null)
            {
                frmLibros editarLibro = new frmLibros(libro);
                editarLibro.ShowDialog();
                CargarGrilla(1);
            }
            else
            {
                MessageBox.Show("No se encontró el libro con el ID especificado.");
            }
        }

        private void EliminarLibro(int id)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este libro?", "Eliminar Libro", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                var logicaLibros = new cls_libros();
                if (logicaLibros.Eliminar(id))
                {
                    MessageBox.Show("Libro eliminado con éxito.");
                    CargarGrilla(1);
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar el libro.");
                }
            }
        }
    }
}
