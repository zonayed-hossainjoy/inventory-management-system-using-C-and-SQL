using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace inventory_management_system
{
    class dbclass
    {
        private SqlConnection con=new SqlConnection(@"Data Source=ZONAYED\SQLEXPRESS;Initial Catalog=IMS;Integrated Security=True;TrustServerCertificate=True");
        public SqlConnection getcon()
        { return con; }
        public void opencon()
        {
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
        }
        public void closecon()
        {
            if (con.State == ConnectionState.Open)
            { con.Close(); }
        }
    }
}
