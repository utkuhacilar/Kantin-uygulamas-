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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kantin
{
    public partial class fUrunFiyatGuncelleme : Form
    {
        public fUrunFiyatGuncelleme()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "update urunler set Barkod=@pIsim, SatisFiyat=@pNot";
            komut.Parameters.AddWithValue("@pIsim", textBox1.Text);
            komut.Parameters.AddWithValue("@pNot", textBox3.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=kantin.accdb");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from urunler where Barkod  like '" + textBox1.Text + "'", baglanti);
            OleDbDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["SatisFiyat"].ToString();
            }
            baglanti.Close();

        }
    }
}
