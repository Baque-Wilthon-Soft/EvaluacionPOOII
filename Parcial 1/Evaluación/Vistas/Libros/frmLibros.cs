using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Evaluación.Controlador;
using Evaluación.Modelo;

namespace Evaluación.Vistas.Libros
{
    public partial class frmLibros : Form
    {
        private cls_libros controlLibros = new cls_libros();
        private dto_libros libroActual;
        public frmLibros(dto_libros libro = null)
        {
            InitializeComponent();
            libroActual = libro;
            if (libroActual != null)
            {
                txtTitulo.Text = libroActual.Titulo;
                txtAutor.Text = libroActual.Autor;
                txtGenero.Text = libroActual.Genero; 
                numericUpDown1.Value = libroActual.AnioPublicacion ?? 0;
                checkBox1.Checked = libroActual.Disponible;
                lblFrm.Text = $"ID: {libroActual.LibroID}, Título: {libroActual.Titulo}, Autor: {libroActual.Autor}";
            }
            else
            {
                lblFrm.Text = "Creando nuevo libro";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtAutor.Text))
            {
                MessageBox.Show("El título y el autor son obligatorios.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGenero.Text))
            {
                MessageBox.Show("El género es obligatorio.");
                return;
            }

            var nuevoLibro = new dto_libros
            {
                Titulo = txtTitulo.Text,
                Autor = txtAutor.Text,
                Genero = txtGenero.Text,
                AnioPublicacion = (int)numericUpDown1.Value,
                Disponible = checkBox1.Checked
            };

            string resultado;
            if (libroActual == null)
            {
                resultado = controlLibros.Insertar(nuevoLibro);
            }
            else
            {
                nuevoLibro.LibroID = libroActual.LibroID;
                resultado = controlLibros.Actualizar(nuevoLibro);
            }

            if (resultado == "ok")
            {
                MessageBox.Show("Libro guardado con éxito.");
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
