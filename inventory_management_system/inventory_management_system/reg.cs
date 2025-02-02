using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;


namespace inventory_management_system
{
    public partial class reg : Form
    {
        dbclass dbcon = new dbclass();
        public reg()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnadmin_Click(object sender, EventArgs e)
        {
          
            if (txtfname.Text == string.Empty || txtlname.Text == string.Empty || txtadd.Text == string.Empty || txtgender.Text == string.Empty || txtphone.Text == string.Empty || txtemail.Text == string.Empty || txtuser.Text == string.Empty || txtpass.Text == string.Empty )
            {
                MessageBox.Show("plese fill your details properly! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("adminreg", dbcon.getcon());
                cmd.Parameters.AddWithValue("@firstname", txtfname.Text.Trim());
                cmd.Parameters.AddWithValue("@lastname", txtlname.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtadd.Text.Trim());
                cmd.Parameters.AddWithValue("@gender", txtgender.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtphone.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
                cmd.Parameters.AddWithValue("@username", txtuser.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtpass.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                dbcon.getcon().Open();
                cmd.ExecuteNonQuery();
                dbcon.getcon().Close();
                MessageBox.Show("registration successfull as Admin ! ", "successful registration", MessageBoxButtons.OK);
            }
        }

        private void btnemployee_Click(object sender, EventArgs e)
        {
            if (txtfname.Text == string.Empty || txtlname.Text == string.Empty || txtadd.Text == string.Empty || txtgender.Text == string.Empty || txtphone.Text == string.Empty || txtemail.Text == string.Empty || txtuser.Text == string.Empty || txtpass.Text == string.Empty)
            {
                MessageBox.Show("plese fill your details properly! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("employeereg", dbcon.getcon());
                cmd.Parameters.AddWithValue("@firstname", txtfname.Text.Trim());
                cmd.Parameters.AddWithValue("@lastname", txtlname.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtadd.Text.Trim());
                cmd.Parameters.AddWithValue("@gender", txtgender.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtphone.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
                cmd.Parameters.AddWithValue("@username", txtuser.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtpass.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                dbcon.getcon().Open();
                cmd.ExecuteNonQuery();
                dbcon.getcon().Close();
                MessageBox.Show("registration successfull as Employee! ", "successful registration", MessageBoxButtons.OK);
            }
        }

        private void reg_to_login_Click(object sender, EventArgs e)
        {
            this.Hide();
            login fm = new login();
            fm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            main fm = new main();
            fm.Show();
        }
    }
}
