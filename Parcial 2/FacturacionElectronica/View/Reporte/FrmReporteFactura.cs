using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace FacturacionElectronica.View.Reporte
{
    public partial class FrmReporteFactura : Form
    {
        private int clienteID;

        public FrmReporteFactura(int clienteID)
        {
            InitializeComponent();
            this.clienteID = clienteID;
        }

        private void CargarReporte()
        {
            using (var conexion = new SqlConnection("Server=(local);database=FacturacionElectronica;uid=sa;pwd=123"))
            {
                string query = @"
                SELECT c.Nombre + ' ' + c.Apellido AS ClienteNombre, 
                       f.ID AS FacturaID, f.Fecha, f.Total, 
                       p.Nombre AS Producto, df.Cantidad, df.Subtotal
                FROM Facturas f
                INNER JOIN Clientes c ON f.ClienteID = c.ID
                INNER JOIN DetallesFactura df ON f.ID = df.FacturaID
                INNER JOIN Productos p ON df.ProductoID = p.ID
                WHERE c.ID = @ClienteID";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                adapter.SelectCommand.Parameters.AddWithValue("@ClienteID", clienteID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FacturasDataSet", dt));
                reportViewer1.RefreshReport();
            }
        }

        private void FrmReporteFactura_Load(object sender, EventArgs e)
        {
            CargarReporte();
            //this.reportViewer1.RefreshReport();
        }
    }
}
