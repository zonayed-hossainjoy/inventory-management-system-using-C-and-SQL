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

namespace inventory_management_system
{
    public partial class sales : Form
    {
        dbclass dbcon = new dbclass();
        public sales()
        {
            InitializeComponent();
            bindproducts();
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            updateQuantity(true);
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            updateQuantity(false);
        }

        private void bindproducts()
        {
            SqlCommand cmd = new SqlCommand("SELECT productid AS ProductID, productname AS ProductName, productdesc AS Description, productquantity AS Quantity FROM inventorytbl", dbcon.getcon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void updateQuantity(bool isIncrease)
        {
            if (txtProductId.Text == string.Empty || txtQuantity.Text == string.Empty)
            {
                MessageBox.Show("Please enter Product ID and Quantity!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int productId, quantity;
            if (!int.TryParse(txtProductId.Text.Trim(), out productId) || !int.TryParse(txtQuantity.Text.Trim(), out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter valid numbers for Product ID and Quantity!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand checkCmd = new SqlCommand("SELECT productquantity FROM inventorytbl WHERE productid = @productid", dbcon.getcon());
            checkCmd.Parameters.AddWithValue("@productid", productId);
            dbcon.opencon();
            object result = checkCmd.ExecuteScalar();
            dbcon.closecon();

            if (result == null)
            {
                MessageBox.Show("Product ID not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int currentQuantity = Convert.ToInt32(result);
            int newQuantity = isIncrease ? currentQuantity + quantity : currentQuantity - quantity;

            if (!isIncrease && newQuantity < 0)
            {
                MessageBox.Show("Quantity cannot be negative!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand cmd = new SqlCommand("UPDATE inventorytbl SET productquantity = @quantity WHERE productid = @productid", dbcon.getcon());
            cmd.Parameters.AddWithValue("@productid", productId);
            cmd.Parameters.AddWithValue("@quantity", newQuantity);

            dbcon.opencon();
            int rowsAffected = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Quantity updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bindproducts();
                txtProductId.Clear();
                txtQuantity.Clear();
            }
            else
            {
                MessageBox.Show("Failed to update quantity!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            home fm = new home();
            fm.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            inventory fm = new inventory();
            fm.Show();
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
            this.Hide();
            main fm = new main();
            fm.Show();
        }
    }
}
