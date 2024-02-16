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
    public partial class fUrunEkleme : Form
    {
        public fUrunEkleme()
        {
            InitializeComponent();
        }

        private void fUrunEkleme_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=kantin.accdb");

        private void button1_Click(object sender, EventArgs e)
        {
            if (bag.State == ConnectionState.Closed)
            {
                bag.Open();
                OleDbCommand veriekle = new OleDbCommand("insert into urunler (Barkod, Adı, Acıklama, AlisFiyat, SatisFiyat, Miktar) values (@okul_no,@isim,@sinif,@kart_no,@bakiye,@miktar)", bag);
                veriekle.Parameters.AddWithValue("@okul_no", tBarkod.Text);
                veriekle.Parameters.AddWithValue("@isim", tAdı.Text);
                veriekle.Parameters.AddWithValue("@sinif", tAcıklama.Text);
                veriekle.Parameters.AddWithValue("@kart_no", tAlis.Text);
                veriekle.Parameters.AddWithValue("@bakiye", tSatis.Text);
                veriekle.Parameters.AddWithValue("@miktar", tMiktar.Text);
                veriekle.ExecuteNonQuery();
                bag.Close();
            }
            else
            {
                MessageBox.Show("Veri Ekleme Başarısız !");
            }

        }
    }
}
