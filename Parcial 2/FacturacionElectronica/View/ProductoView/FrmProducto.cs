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
using FacturacionElectronica.Model;

namespace FacturacionElectronica.View.ProductoView
{
    public partial class FrmProducto : Form
    {
        ProductoController productoController = new ProductoController();
        private int? idProducto;
        public FrmProducto()
        {
            InitializeComponent();
            txtPrecio.KeyPress += TxtPrecio_KeyPress;
            txtStock.KeyPress += TxtStock_KeyPress;
        }
        public FrmProducto(int idProducto) : this()
        {
            this.idProducto = idProducto;
            lblTitulo.Text = "Editar Producto";
            CargarProducto();
        }
        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }
        private void TxtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void CargarProducto()
        {
            Producto producto = productoController.ObtenerProductoPorID(idProducto.Value);
            if (producto != null)
            {
                txtNombre.Text = producto.Nombre;
                txtPrecio.Text = producto.Precio.ToString();
                txtStock.Text = producto.Stock.ToString();
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Producto producto = new Producto
            {
                Nombre = txtNombre.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                Stock = int.Parse(txtStock.Text)
            };

            if (idProducto.HasValue)
            {
                
                producto.ID = idProducto.Value;
                productoController.ActualizarProducto(producto);
            }
            else
            {
                productoController.AgregarProducto(producto);
            }

            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }
    }
}
