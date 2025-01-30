using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacturacionElectronica.View;
using FacturacionElectronica.View.ClientesView;
using FacturacionElectronica.View.FacturaView;
using FacturacionElectronica.View.ProductoView;

namespace FacturacionElectronica
{
    public partial class Menú : Form
    {
        public Menú()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            CUCliente cliente = new CUCliente();
            PanelGeneral.Controls.Clear();
            cliente.Dock = DockStyle.Fill;
            PanelGeneral.Controls.Add(cliente);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            CUProducto producto = new CUProducto();
            PanelGeneral.Controls.Clear();
            producto.Dock = DockStyle.Fill;
            PanelGeneral.Controls.Add(producto);
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            CUFactura factura = new CUFactura();
            PanelGeneral.Controls.Clear();
            factura.Dock = DockStyle.Fill;
            PanelGeneral.Controls.Add(factura);
        }

        private void btnDetalleFacturas_Click(object sender, EventArgs e)
        {

            FacturacionElectronica.View.DetalleFacturaView.CUReporte reporte = new FacturacionElectronica.View.DetalleFacturaView.CUReporte();
            PanelGeneral.Controls.Clear();
            reporte.Dock = DockStyle.Fill;
            PanelGeneral.Controls.Add(reporte);
        }
    }
}
