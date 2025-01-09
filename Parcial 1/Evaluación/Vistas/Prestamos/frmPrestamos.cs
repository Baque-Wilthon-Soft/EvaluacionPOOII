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

namespace Evaluación.Vistas.Prestamos
{
    public partial class frmPrestamos : Form
    {
        private cls_prestamos controlPrestamos = new cls_prestamos();
        private dto_prestamos prestamoActual;

        public frmPrestamos(dto_prestamos prestamo = null)
        {
            InitializeComponent();
            prestamoActual = prestamo ?? new dto_prestamos();
            prestamoActual = prestamo;
            CargarCombos();

            if (prestamoActual != null && prestamoActual.PrestamoID > 0)
            {
                cmbUsuario.SelectedValue = prestamoActual.UsuarioID;
                cmbLibro.SelectedValue = prestamoActual.LibroID;
                dateTimePicker1.Value = prestamoActual.FechaPrestamo;
                dateTimePicker2.Value = prestamoActual.FechaDevolucion ?? DateTime.Now;

                lblFrm.Text = $"Editando Préstamo: ID {prestamoActual.PrestamoID}, UsuarioID {prestamoActual.UsuarioID}, LibroID {prestamoActual.LibroID}";
            }
            else
            {
                lblFrm.Text = "Creando un nuevo préstamo.";
            }
        }
        
        private void CargarCombos()
        {
            var controlUsuarios = new cls_usuarios();
            var controlLibros = new cls_libros();

            cmbUsuario.DataSource = controlUsuarios.Todos();
            cmbUsuario.DisplayMember = "Nombre";
            cmbUsuario.ValueMember = "UsuarioID";

            cmbLibro.DataSource = controlLibros.Todos();
            cmbLibro.DisplayMember = "Titulo";
            cmbLibro.ValueMember = "LibroID";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedValue == null || cmbLibro.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un usuario y un libro.");
                return;
            }

            var controlLibros = new cls_libros();
            int libroID = (int)cmbLibro.SelectedValue;
            var libro = controlLibros.ObtenerPorId(libroID);

            if (libro == null)
            {
                MessageBox.Show("El libro seleccionado no existe.");
                return;
            }

            if (!libro.Disponible)
            {
                MessageBox.Show("El libro seleccionado no está disponible para préstamo.");
                return;
            }

            var nuevoPrestamo = new dto_prestamos
            {
                UsuarioID = (int)cmbUsuario.SelectedValue,
                LibroID = libroID,
                FechaPrestamo = dateTimePicker1.Value,
                FechaDevolucion = dateTimePicker2.Value
            };

            string resultado;
            if (prestamoActual == null)
            {
                resultado = controlPrestamos.Insertar(nuevoPrestamo);
            }
            else
            {
                nuevoPrestamo.PrestamoID = prestamoActual.PrestamoID;
                resultado = controlPrestamos.Actualizar(nuevoPrestamo);
            }

            if (resultado == "ok")
            {
                MessageBox.Show("Préstamo guardado con éxito.");
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
