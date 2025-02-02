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
    
    public partial class employee : Form
    {
        private dbclass dbcon = new dbclass();
        public employee()
        {
            InitializeComponent();
            BindEmployees();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            BindEmployees();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtfname.Text) || string.IsNullOrWhiteSpace(txtlname.Text) || string.IsNullOrWhiteSpace(txtadd.Text) ||
                string.IsNullOrWhiteSpace(txtgender.Text) || string.IsNullOrWhiteSpace(txtphone.Text) ||
                string.IsNullOrWhiteSpace(txtemail.Text) || string.IsNullOrWhiteSpace(txtuser.Text) ||
                string.IsNullOrWhiteSpace(txtpass.Text))
            {
                MessageBox.Show("Please fill all fields properly!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("employeeinsert", dbcon.getcon())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@firstname", txtfname.Text.Trim());
            cmd.Parameters.AddWithValue("@lastname", txtlname.Text.Trim());
            cmd.Parameters.AddWithValue("@address", txtadd.Text.Trim());
            cmd.Parameters.AddWithValue("@gender", txtgender.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", txtphone.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
            cmd.Parameters.AddWithValue("@username", txtuser.Text.Trim());
            cmd.Parameters.AddWithValue("@password", txtpass.Text.Trim());

            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindEmployees();
                ClearFields();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtfname.Text) || string.IsNullOrWhiteSpace(txtlname.Text) || string.IsNullOrWhiteSpace(txtadd.Text) ||
                string.IsNullOrWhiteSpace(txtgender.Text) || string.IsNullOrWhiteSpace(txtphone.Text) ||
                string.IsNullOrWhiteSpace(txtemail.Text) || string.IsNullOrWhiteSpace(txtuser.Text) ||
                string.IsNullOrWhiteSpace(txtpass.Text))
            {
                MessageBox.Show("Please fill all fields properly!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("employeeupdate", dbcon.getcon())
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@firstname", txtfname.Text.Trim());
            cmd.Parameters.AddWithValue("@lastname", txtlname.Text.Trim());
            cmd.Parameters.AddWithValue("@address", txtadd.Text.Trim());
            cmd.Parameters.AddWithValue("@gender", txtgender.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", txtphone.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
            cmd.Parameters.AddWithValue("@username", txtuser.Text.Trim());
            cmd.Parameters.AddWithValue("@password", txtpass.Text.Trim());

            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindEmployees();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Employee update failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtuser.Text))
            {
                MessageBox.Show("Please enter a Username to delete!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand("employeedelete", dbcon.getcon())
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@username", txtuser.Text.Trim());

            dbcon.opencon();
            int i = cmd.ExecuteNonQuery();
            dbcon.closecon();

            if (i > 0)
            {
                MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindEmployees();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Employee deletion failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindEmployees()
        {
            SqlCommand cmd = new SqlCommand("SELECT firstname AS FirstName, lastname AS LastName, address AS Address, gender AS Gender, phone AS Phone, email AS Email, username AS Username FROM employeetbl", dbcon.getcon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void ClearFields()
        {
            txtfname.Clear();
            txtlname.Clear();
            txtadd.Clear();
            txtgender.Clear();
            txtphone.Clear();
            txtemail.Clear();
            txtuser.Clear();
            txtpass.Clear();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            home fm = new home();
            fm.Show();
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            inventory fm = new inventory();
            fm.Show();
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
