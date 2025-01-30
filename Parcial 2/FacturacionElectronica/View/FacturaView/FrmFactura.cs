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
using FacturacionElectronica.View.Reporte;

namespace FacturacionElectronica.View.FacturaView
{
    public partial class FrmFactura : Form
    {
        FacturaController facturaController = new FacturaController();
        DetalleFacturaController detalleController = new DetalleFacturaController();
        ClienteController clienteController = new ClienteController();
        List<DetalleFactura> detalles = new List<DetalleFactura>();
        ProductoController productoController = new ProductoController();
        private int? facturaID = null;
        public FrmFactura()
        {
            InitializeComponent();
        }
        public FrmFactura(int idFactura)
        {
            InitializeComponent();
            CargarClientes();
            CargarProductos();
            CargarFactura(idFactura);
            this.facturaID = idFactura;
            lblTitulo.Text = "Editar Factura";
        }
        private void CargarFactura(int idFactura)
        {

            Factura factura = facturaController.ObtenerFacturaPorId(idFactura);
            if (factura != null)
            {
                dtpFecha.Value = factura.Fecha;
                lblTotal.Text = factura.Total.ToString("C2");

                Cliente cliente = clienteController.ObtenerClientePorID(factura.ClienteID);
                if (cliente != null)
                {
                    cbClientes.SelectedValue = cliente.ID;

                    Console.WriteLine($"Cliente seleccionado: {cliente.Nombre} (ID: {cliente.ID})");
                }

                List<DetalleFactura> detalles = detalleController.ObtenerDetallesPorFactura(idFactura);
                dgvDetalleFactura.DataSource = detalles;
            }

        }
       
        private void FrmFactura_Load(object sender, EventArgs e)
        {
            if (facturaID == null)
            {
                CargarClientes();
            }
            CargarProductos();
        }
        private void CargarClientes()
        {
            cbClientes.DataSource = new ClienteController().ObtenerClientes();
            cbClientes.DisplayMember = "Nombre";
            cbClientes.ValueMember = "ID";
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = new ProductoController().ObtenerProductos();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            Producto producto = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            int cantidad = (int)numCantidad.Value;

            if (cantidad <= 0 || cantidad > producto.Stock)
            {
                MessageBox.Show("Cantidad inválida o stock insuficiente.");
                return;
            }

            var detalleExistente = detalles.FirstOrDefault(d => d.ProductoID == producto.ID);

            if (detalleExistente != null)
            {
                detalleExistente.Cantidad += cantidad;
                detalleExistente.Subtotal = detalleExistente.Cantidad * producto.Precio;
            }
            else
            {
                DetalleFactura detalle = new DetalleFactura()
                {
                    ProductoID = producto.ID,
                    Cantidad = cantidad,
                    Subtotal = cantidad * producto.Precio
                };

                detalles.Add(detalle);
            }

            producto.Stock -= cantidad;
            productoController.ActualizarProducto(producto);

            dgvDetalleFactura.DataSource = null;
            dgvDetalleFactura.DataSource = detalles;

            CargarProductos();
            ActualizarTotal();

            if (producto.Stock == 0)
            {
                MessageBox.Show($"El producto '{producto.Nombre}' ya no tiene stock disponible.");
            }
            
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvDetalleFactura.SelectedRows.Count > 0)
            { 
                var detalle = detalles[dgvDetalleFactura.SelectedRows[0].Index];
                Producto producto = productoController.ObtenerProductoPorID(detalle.ProductoID);

                
                producto.Stock += detalle.Cantidad;
                productoController.ActualizarProducto(producto);

              
                detalles.Remove(detalle);
                dgvDetalleFactura.DataSource = null;
                dgvDetalleFactura.DataSource = detalles;

                
                CargarProductos();
                ActualizarTotal();
            }
        }

        private void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            if (cbClientes.SelectedItem == null || detalles.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente y agregue productos.");
                return;
            }

            Factura factura = new Factura()
            {
                ClienteID = (int)cbClientes.SelectedValue,
                Fecha = dtpFecha.Value,
                Total = detalles.Sum(d => d.Subtotal)
            };

            if (facturaID == null)
            {
                facturaID = facturaController.AgregarFactura(factura);
            }
            else
            {
                factura.ID = facturaID.Value;
                facturaController.ActualizarFactura(factura);

                var detallesAnteriores = detalleController.ObtenerDetallesPorFactura(facturaID.Value);
                foreach (var detalle in detallesAnteriores)
                {
                    Producto producto = productoController.ObtenerProductoPorID(detalle.ProductoID);
                    if (producto != null)
                    {
                        producto.Stock += detalle.Cantidad;
                        productoController.ActualizarProducto(producto);
                    }
                }

                detalleController.EliminarDetallesPorFactura(facturaID.Value);
            }

            foreach (var detalle in detalles)
            {
                detalle.FacturaID = facturaID.Value;
                detalleController.AgregarDetalle(detalle);

                Producto producto = productoController.ObtenerProductoPorID(detalle.ProductoID);
                if (producto != null)
                {
                    producto.Stock -= detalle.Cantidad;
                    productoController.ActualizarProducto(producto);
                }
            }

            MessageBox.Show("Factura guardada correctamente.");

            detalles.Clear();
            dgvDetalleFactura.DataSource = null;
            lblTotal.Text = "Total: 0.00";

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productoController.ObtenerProductos();
            
        }

        private void btnReporteFactura_Click(object sender, EventArgs e)
        {
            if (cbClientes.SelectedItem != null)
            {
                int clienteID = Convert.ToInt32(cbClientes.SelectedValue);
                FrmReporteFactura reporte = new FrmReporteFactura(clienteID);
                reporte.ShowDialog();
            }
        }
        private void ActualizarTotal()
        {
            lblTotal.Text = "Total: " + detalles.Sum(d => d.Subtotal).ToString("C2");
        }
    }
}
