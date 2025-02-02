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
using System.Xml.Linq;

namespace inventory_management_system
{
    public partial class seller : Form
    {
        private dbclass dbcon = new dbclass();

        public seller()
        {
            InitializeComponent();
            BindSellers();
        }
        private void inventory_Load(object sender, EventArgs e)
        {
            BindSellers();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtid.Text) || string.IsNullOrWhiteSpace(txtname.Text) || string.IsNullOrWhiteSpace(txtphone.Text) || string.IsNullOrWhiteSpace(txtage.Text) || string.IsNullOrWhiteSpace(txtadd.Text))
            {
                MessageBox.Show("Please fill all fields properly!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("sellerupdate", dbcon.getcon())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@sellerid", txtid.Text.Trim());
            cmd.Parameters.AddWithValue("@sellername", txtname.Text.Trim());
            cmd.Parameters.AddWithValue("@sellerphone", txtphone.Text.Trim());
            cmd.Parameters.AddWithValue("@sellerage", txtage.Text.Trim());
            cmd.Parameters.AddWithValue("@selleraddress", txtadd.Text.Trim());
            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Seller updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindSellers();
                txtid.Clear();
                txtname.Clear();
                txtphone.Clear();
                txtage.Clear();
                txtadd.Clear();
            }
            else
            {
                MessageBox.Show("Seller update failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtid.Text))
            {
                MessageBox.Show("Please fill Seller ID properly!", "Invalid Seller ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtid.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtname.Text))
            {
                MessageBox.Show("Please fill Seller Name properly!", "Invalid Seller Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtname.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtphone.Text))
            {
                MessageBox.Show("Please fill Seller Phone properly!", "Invalid Seller Phone", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtphone.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtage.Text))
            {
                MessageBox.Show("Please fill Seller Age properly!", "Invalid Seller Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtage.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtadd.Text))
            {
                MessageBox.Show("Please fill Seller Address properly!", "Invalid Seller Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtadd.Focus();
                return;
            }

            SqlCommand cmd = new SqlCommand("SELECT sellername FROM sellertbl WHERE sellername = @sellername", dbcon.getcon());
            cmd.Parameters.AddWithValue("@sellername", txtid.Text.Trim());
            dbcon.opencon();
            object result = cmd.ExecuteScalar();
            dbcon.closecon();

            if (result != null)
            {
                MessageBox.Show("Seller name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtid.Clear();
                txtname.Clear();
                txtphone.Clear();
                txtage.Clear();
                txtadd.Clear();
            }
            else
            {
                cmd = new SqlCommand("sellerinsert", dbcon.getcon())
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@sellerid", txtid.Text.Trim());
                cmd.Parameters.AddWithValue("@sellername", txtname.Text.Trim());
                cmd.Parameters.AddWithValue("@sellerphone", txtphone.Text.Trim());
                cmd.Parameters.AddWithValue("@sellerage", txtage.Text.Trim());
                cmd.Parameters.AddWithValue("@selleraddress", txtadd.Text.Trim());
                dbcon.opencon();
                int i = cmd.ExecuteNonQuery();
                dbcon.closecon();

                if (i > 0)
                {
                    MessageBox.Show("Seller added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindSellers();
                    txtid.Clear();
                    txtname.Clear();
                    txtphone.Clear();
                    txtage.Clear();
                    txtadd.Clear();
                }
            }

        }

        private void BindSellers()
        {
            SqlCommand cmd = new SqlCommand("SELECT sellerid AS SellerID, sellername AS SellerName, sellerphone AS Phone, sellerage AS Age, selleraddress AS Address FROM sellertbl", dbcon.getcon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtid.Text))
            {
                MessageBox.Show("Please enter Seller ID to delete!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("sellerdelete", dbcon.getcon())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@sellerid", txtid.Text.Trim());
            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Seller deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindSellers();
                txtid.Clear();
                txtname.Clear();
                txtphone.Clear();
                txtage.Clear();
                txtadd.Clear();
            }
            else
            {
                MessageBox.Show("Seller deletion failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            home fm = new home();
            fm.Show();
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            inventory fcat = new inventory();
            fcat.Show();
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            sales fm = new sales();
            fm.Show();
        }

        private void toolStripMenuItem5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            supplier fm = new supplier();
            fm.Show();
        }

        private void toolStripMenuItem6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            seller fm = new seller();
            fm.Show();
        }

        private void toolStripMenuItem7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            employee fm = new employee();
            fm.Show();
        }

        private void toolStripMenuItem8_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            about fm = new about();
            fm.Show();
        }

        private void toolStripMenuItem9_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            this.Hide();
            main fm = new main();
            fm.Show();
        }
    }
}
