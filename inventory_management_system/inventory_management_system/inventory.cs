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
    public partial class inventory : Form
    {
        dbclass dbcon = new dbclass();
        public inventory()
        {
            InitializeComponent();
            bindproducts();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void inventory_Load(object sender, EventArgs e)
        {
            bindproducts();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtid.Text == string.Empty || txtname.Text == string.Empty || txtdesc.Text == string.Empty || txtquantity.Text == string.Empty)
            {
                MessageBox.Show("Please fill all fields properly!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtquantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity!", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("SELECT productname FROM inventorytbl WHERE productname=@productname", dbcon.getcon());
            cmd.Parameters.AddWithValue("@productname", txtname.Text.Trim());
            dbcon.opencon();
            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                MessageBox.Show($"Product {txtname.Text} already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearFields();
            }
            else
            {
                cmd = new SqlCommand("productinsert", dbcon.getcon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", txtid.Text.Trim());
                cmd.Parameters.AddWithValue("@productname", txtname.Text.Trim());
                cmd.Parameters.AddWithValue("@productdesc", txtdesc.Text.Trim());
                cmd.Parameters.AddWithValue("@productquantity", quantity);

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindproducts();
                    ClearFields();
                }
            }
            dbcon.closecon();
        }

        private void bindproducts()
        {
            SqlCommand cmd = new SqlCommand("SELECT productid AS ProductID, productname AS ProductName, productdesc AS Description, productquantity AS Quantity FROM inventorytbl", dbcon.getcon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtdesc.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtquantity.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }



        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtid.Text == string.Empty || txtname.Text == string.Empty || txtdesc.Text == string.Empty || txtquantity.Text == string.Empty)
            {
                MessageBox.Show("Please fill all fields properly!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtquantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity!", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("productupdate2", dbcon.getcon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@productid", txtid.Text.Trim());
            cmd.Parameters.AddWithValue("@productname", txtname.Text.Trim());
            cmd.Parameters.AddWithValue("@productdesc", txtdesc.Text.Trim());
            cmd.Parameters.AddWithValue("@productquantity", quantity);

            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bindproducts();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Product update failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtid.Text == string.Empty)
                {
                    MessageBox.Show("Please enter a valid Product ID!", "Invalid Product ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM inventorytbl WHERE productid = @productid", dbcon.getcon());
                checkCmd.Parameters.AddWithValue("@productid", txtid.Text.Trim());

                dbcon.opencon();
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                dbcon.closecon();

                if (count == 0)
                {
                    MessageBox.Show("Product ID not found! Cannot delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                SqlCommand cmd = new SqlCommand("productdelete2", dbcon.getcon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productid", txtid.Text.Trim());

                dbcon.opencon();
                int rowsAffected = cmd.ExecuteNonQuery();
                dbcon.closecon();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindproducts();
                }
                else
                {
                    MessageBox.Show("Product deletion failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
                txtid.Clear();
                txtname.Clear();
                txtdesc.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtid.Clear();
            txtname.Clear();
            txtdesc.Clear();
            txtquantity.Clear();
        }


        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            home fm = new home();
            fm.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inventory fcat = new inventory();
            fcat.Show();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            sales fm = new sales();
            fm.Show();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            supplier fm = new supplier();
            fm.Show();
        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            seller fm = new seller();
            fm.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            employee fm = new employee();
            fm.Show();
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            about fm = new about();
            fm.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
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
