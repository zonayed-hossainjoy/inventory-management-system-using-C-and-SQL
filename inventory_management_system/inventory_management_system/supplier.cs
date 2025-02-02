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
    public partial class supplier : Form
    {
        private dbclass dbcon = new dbclass();
        public supplier()
        {
            InitializeComponent();
            BindSuppliers();
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            BindSuppliers();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtid.Text) || string.IsNullOrWhiteSpace(txtname.Text) || string.IsNullOrWhiteSpace(txtphone.Text) ||
                string.IsNullOrWhiteSpace(txtage.Text) || string.IsNullOrWhiteSpace(txtadd.Text) || string.IsNullOrWhiteSpace(txtcompany.Text) ||
                string.IsNullOrWhiteSpace(txtSuppliedGoods.Text))
            {
                MessageBox.Show("Please fill all fields properly!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("supplierinsert", dbcon.getcon())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@supplierid", txtid.Text.Trim());
            cmd.Parameters.AddWithValue("@suppliername", txtname.Text.Trim());
            cmd.Parameters.AddWithValue("@supplierphone", txtphone.Text.Trim());
            cmd.Parameters.AddWithValue("@supplierage", txtage.Text.Trim());
            cmd.Parameters.AddWithValue("@supplieraddress", txtadd.Text.Trim());
            cmd.Parameters.AddWithValue("@company", txtcompany.Text.Trim());
            cmd.Parameters.AddWithValue("@suppliedgoods", txtSuppliedGoods.Text.Trim());

            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindSuppliers();
                ClearFields();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtid.Text))
            {
                MessageBox.Show("Please enter Supplier ID to update!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("supplierupdate", dbcon.getcon())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@supplierid", txtid.Text.Trim());
            cmd.Parameters.AddWithValue("@suppliername", txtname.Text.Trim());
            cmd.Parameters.AddWithValue("@supplierphone", txtphone.Text.Trim());
            cmd.Parameters.AddWithValue("@supplierage", txtage.Text.Trim());
            cmd.Parameters.AddWithValue("@supplieraddress", txtadd.Text.Trim());
            cmd.Parameters.AddWithValue("@company", txtcompany.Text.Trim());
            cmd.Parameters.AddWithValue("@suppliedgoods", txtSuppliedGoods.Text.Trim());

            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindSuppliers();
                ClearFields();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtid.Text))
            {
                MessageBox.Show("Please enter Supplier ID to delete!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("supplierdelete", dbcon.getcon())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@supplierid", txtid.Text.Trim());

            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindSuppliers();
                ClearFields();
            }
        }

        private void BindSuppliers()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM suppliertbl", dbcon.getcon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void ClearFields()
        {
            txtid.Clear();
            txtname.Clear();
            txtphone.Clear();
            txtage.Clear();
            txtadd.Clear();
            txtcompany.Clear();
            txtSuppliedGoods.Clear();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            home fcat = new home();
            fcat.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Hide();
            inventory fm = new inventory();
            fm.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Hide();
            sales fm = new sales();
            fm.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.Hide();
            supplier fm = new supplier();
            fm.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.Hide();
            seller fm = new seller();
            fm.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.Hide();
            employee fm = new employee();
            fm.Show();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            this.Hide();
            about fm = new about();
            fm.Show();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
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

        private void supplier_Load_1(object sender, EventArgs e)
        {

        }
    }
}
