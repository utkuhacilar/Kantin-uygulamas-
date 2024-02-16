using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kantin
{
    public partial class Form3 : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;

        public Form3()
        {
            InitializeComponent();
        }
        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=kantin.accdb");
            da = new OleDbDataAdapter("SElect *from zaman", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "zaman");
            dataGridView1.DataSource = ds.Tables["zaman"];
            con.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            griddoldur();

        }
        DataTable table;


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
