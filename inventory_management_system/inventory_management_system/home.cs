using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory_management_system
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void home_Load(object sender, EventArgs e)
        {
            if(login.loginname!=null)
            {
                toolStripStatusLabel2.Text= login.loginname;
            }
            if (login.logintype != null && login.logintype =="Employee")
            {
                toolStripMenuItem3.Enabled = false;
                toolStripMenuItem4.Enabled = false;

            }
        }

    

      

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            home fm = new home();
            fm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
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

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
