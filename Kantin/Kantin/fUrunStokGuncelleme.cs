using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kantin
{
    public partial class fUrunStokGuncelleme : Form
    {
        public fUrunStokGuncelleme()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=kantin.accdb");

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "update urunler set Barkod=@pIsim, Miktar=@pNot";
            komut.Parameters.AddWithValue("@pIsim", textBox1.Text);
            komut.Parameters.AddWithValue("@pNot", textBox4.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Textboxları okuyup sayi1 ve sayi2 değişkenlerine aktarma
                double sayi1 = Convert.ToDouble(textBox2.Text);
                double sayi2 = Convert.ToDouble(textBox3.Text);

                //sayıları toplayıp label nesnelerine ekliyoruz.
                textBox4.Text = Convert.ToString(sayi1 + sayi2);

            }
            catch (Exception)
            {
                MessageBox.Show("Para ekleme hatalı");

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from urunler where Barkod  like '" + textBox1.Text + "'", baglanti);
            OleDbDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox3.Text = read["Miktar"].ToString();
            }
            baglanti.Close();

        }
    }
}
