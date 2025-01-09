using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Evaluación.Vistas;
using Evaluación.Vistas.Usuarios;
using Evaluación.Vistas.Libros;
using Evaluación.Vistas.Prestamos;

namespace Evaluación
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            CUUsuarios cuusuarios = new CUUsuarios();
            panelgeneral.Controls.Clear();
            cuusuarios.Dock = DockStyle.Fill;
            panelgeneral.Controls.Add(cuusuarios);
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            CULibros culibros = new CULibros();
            panelgeneral.Controls.Clear();
            culibros.Dock = DockStyle.Fill;
            panelgeneral.Controls.Add(culibros);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CUPrestamos cuprestamos = new CUPrestamos();
            panelgeneral.Controls.Clear();
            cuprestamos.Dock = DockStyle.Fill;
            panelgeneral.Controls.Add(cuprestamos);
        }
    }
}
