using Bakery_Software.Reportes;
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

namespace Bakery_Software
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Green;
            radioButton2.ForeColor = Color.Red;

            cmb_items.Items.Clear();
            cmb_items.Items.Add("Postres Item 1");
            cmb_items.Items.Add("Postres Item 2");
            cmb_items.Items.Add("Postres Item 3");
            
          
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Red;
            radioButton2.ForeColor = Color.Green;

            cmb_items.Items.Clear();
            cmb_items.Items.Add("Panes Item 1");
            cmb_items.Items.Add("Panes Item 2");
            cmb_items.Items.Add("Panes Item 3");
            
        }

        private void cmb_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_items.SelectedItem.ToString() == "Postres Item 1")
            { txt_price.Text = "50"; }
            else if (cmb_items.SelectedItem.ToString() == "Postres Item 2")
            { txt_price.Text = "100"; }
            else if (cmb_items.SelectedItem.ToString() == "Postres Item 3")
            { txt_price.Text = "150"; }
            else if (cmb_items.SelectedItem.ToString() == "Panes Item 1")
            { txt_price.Text = "200"; }
            else if (cmb_items.SelectedItem.ToString() == "Panes Item 2")
            { txt_price.Text = "250"; }
            else if (cmb_items.SelectedItem.ToString() == "Postres Item 3")
            { txt_price.Text = "300"; }
            else
            { txt_price.Text = "0"; }


            txt_total.Text = "";
            txt_qty.Text = "";
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            if (txt_qty.Text.Length>0)
            {
                txt_total.Text = (Convert.ToInt16(txt_qty.Text) * Convert.ToInt16(txt_price.Text)).ToString();
            }
            }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] arr = new string[4];
            arr[0] = cmb_items.SelectedItem.ToString();
            arr[1] = txt_price.Text;
            arr[2] = txt_qty.Text;
            arr[3] = txt_total.Text;

            ListViewItem lvi = new ListViewItem(arr);           
            listView1.Items.Add(lvi);


            txt_sub.Text = (Convert.ToInt16(txt_sub.Text) + Convert.ToInt16(txt_total.Text)).ToString();

        }

        private void button5_Click(object sender, EventArgs e)        {
          
            if (listView1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView1.Items.Count ; i++)
                {
                    if (listView1.Items[i].Selected)
                    {
                        txt_sub.Text = (Convert.ToInt16(txt_sub.Text) - Convert.ToInt16(listView1.Items[i].SubItems[3].Text)).ToString();
                        listView1.Items[i].Remove();
                    }
                }
            }
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            if (txt_discount.Text.Length > 0)
            {
                txt_net.Text = (Convert.ToInt16(txt_sub.Text) - Convert.ToInt16(txt_discount.Text)).ToString();
            }
        }

        private void txt_paid_TextChanged(object sender, EventArgs e)
        {
            if (txt_discount.Text.Length > 0)
            {
                txt_balance.Text = (Convert.ToInt16(txt_net.Text) - Convert.ToInt16(txt_paid.Text)).ToString();
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {

                try
                {
        string ConnectionString = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=AB_Inventory_DB; Data Source=localhost";

                    SqlConnection connection = new SqlConnection(ConnectionString);
                    SqlCommand command = connection.CreateCommand();

            connection.Open();

         command.CommandText = "Insert into Test_Invoice_Master (InvoiceDate, Sub_Total, Discount, Net_Amount, Paid_Amount) values " +
                       " ( getdate() , " + txt_sub.Text + " ," + txt_discount.Text + " , " + txt_net.Text + ", " + txt_paid.Text + ")  select scope_identity() ";

                    string InvoiceID = command.ExecuteScalar().ToString();

                    foreach (ListViewItem ListItem in listView1.Items)
                    {

                    command.CommandText = "Insert into Test_Invoice_Detail (MasterID, ItemName, ItemPrice, ItemQtty, ItemTotal ) values   " +
               " ('" + InvoiceID + "', '" + ListItem.SubItems[0].Text + "', '" + ListItem.SubItems[1].Text + "', '" + ListItem.SubItems[2].Text + "' , " + ListItem.SubItems[3].Text + ")";
                   
                        command.ExecuteNonQuery();

                    }
                    connection.Close();
                    MessageBox.Show("Venta registrada, con recibo # " + InvoiceID);
                }
                catch (Exception ex)
                {               
                    MessageBox.Show("Venta no registrada\n"+ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Debe agregar un elemento a la lista");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Reportes.Print().Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
