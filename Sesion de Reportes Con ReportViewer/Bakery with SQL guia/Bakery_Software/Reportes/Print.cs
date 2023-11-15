using Microsoft.Reporting.WinForms;
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

namespace Bakery_Software.Reportes
{
    public partial class Print : Form
    {
        public Print()
        {
            InitializeComponent();
        }

        private void Print_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }


        private void txtInvoce_ID_Click(object sender, EventArgs e)
        {
            string Conexion = "Integrated Security=SSPI; Persist Security Info=False;" +
                " Initial Catalog=AB_Inventory_DB; Data Source=localhost";
            SqlConnection con=new SqlConnection(Conexion);
            SqlDataAdapter da=new SqlDataAdapter("Select * from View_ALL_Bill_test where "+
                 "InvoiceID= '"+textBox1.Text+"'",con);
            DataSet d1=new DataSet();
            da.Fill(d1, "DataTable1_Invoce");

            ReportDataSource rds = new ReportDataSource("D_Report", d1.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
