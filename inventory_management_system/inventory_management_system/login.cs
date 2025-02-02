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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace inventory_management_system
{
    public partial class login : Form
    {
        dbclass dbcon=new dbclass();
        public static string loginname, logintype;
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbrole.SelectedIndex = 0;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbrole.SelectedIndex > 0)
                {
                    if(cmbrole.SelectedIndex>0 && txtusername.Text!=string.Empty && txtpassword.Text!=string.Empty)
                    {
                        if (cmbrole.Text == "Admin") ;
                        {
                            SqlCommand cmd = new SqlCommand("select top 1 username,password from admintbl where username=@username and password=@password", dbcon.getcon());
                            cmd.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                            cmd.Parameters.AddWithValue("@password", txtpassword.Text.Trim());
                            dbcon.opencon();
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            dataAdapter.Fill(dt);
                            if(dt.Rows.Count > 0)
                            {
                                loginname=txtusername.Text;
                                logintype=cmbrole.Text;
                                MessageBox.Show("welcome back Admin! "  , "login successfull", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.Hide();
                                home fm = new home();
                                fm.Show();
                            }
                            else
                            {
                                MessageBox.Show("plese fill your username or password properly! ", "invalid username or password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        if (cmbrole.Text == "Employee") ;
                        {
                            SqlCommand cmd = new SqlCommand("select top 1 username,password from employeetbl where username=@username and password=@password", dbcon.getcon());
                            cmd.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                            cmd.Parameters.AddWithValue("@password", txtpassword.Text.Trim());
                            dbcon.opencon();
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            dataAdapter.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                loginname = txtusername.Text;
                                logintype = cmbrole.Text;
                                MessageBox.Show("welcome back employee! ", "login successfull", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.Hide();
                                home fm = new home();
                                fm.Show();
                            }
                            else
                            {
                                MessageBox.Show("plese fill your username or password properly! ", "invalid username or password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }

                    else if (txtusername.Text == string.Empty || txtpassword.Text == string.Empty)
                    {
                        MessageBox.Show("plese fill your username or password properly! ", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbrole.SelectedIndex = 0;
                        txtusername.Clear();
                        txtpassword.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("plese select your role", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbrole.SelectedIndex = 0;
                    txtusername.Clear();
                    txtpassword.Clear();
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnclear_Click(object sender, EventArgs e)
        {
            cmbrole.SelectedIndex = 0;
            txtusername.Clear();
            txtpassword.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            main fm = new main();
            fm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            reg fm = new reg();
            fm.Show();
        }
    }
}
