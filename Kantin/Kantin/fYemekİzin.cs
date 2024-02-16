using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kantin
{
    public partial class fYemekİzin : Form
    {
        public static string portismi, banthizi;
        string[] ports = SerialPort.GetPortNames();
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=kantin.accdb");
        OleDbCommand kmt = new OleDbCommand();

        public fYemekİzin()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc != "")
            {
                guna2TextBox3.Text = sonuc;
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (bag.State == ConnectionState.Closed)
            {
                bag.Open();
                OleDbCommand veriekle = new OleDbCommand("insert into yemekhane (okul_no, isim, sinif, kart_no) values (@okul_no,@isim,@sinif,@kart_no)", bag);
                veriekle.Parameters.AddWithValue("@okul_no", guna2TextBox2.Text);
                veriekle.Parameters.AddWithValue("@isim", guna2TextBox1.Text);
                veriekle.Parameters.AddWithValue("@sinif", guna2TextBox4.Text);
                veriekle.Parameters.AddWithValue("@kart_no", guna2TextBox3.Text);
                veriekle.ExecuteNonQuery();
                guna2MessageDialog1.Show("Başarıyla Eklendi","Mesafe Koleji");
                bag.Close();
            }
            else
            {
                MessageBox.Show("Veri Ekleme Başarısız !");
            }

        }

        private void fYemekİzin_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                }
                comboBox2.Items.Add("9600");
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;


                timer1.Start();
                portismi = comboBox1.Text;
                banthizi = comboBox2.Text;

                try
                {
                    serialPort1.PortName = portismi;
                    serialPort1.BaudRate = Convert.ToInt32(banthizi);

                    serialPort1.Open();

                }
                catch
                {
                    serialPort1.Close();
                    serialPort1.Open();
                    MessageBox.Show("Bağlantı zaten açık");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Kart okuyucu takılı değill!!!");
            }

        }
    }
}
