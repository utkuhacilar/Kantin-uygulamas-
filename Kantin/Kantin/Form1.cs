using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kantin
{
    public partial class Form1 : Form
    {
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=kantin.accdb");
        OleDbCommand kmt = new OleDbCommand();


        public static string portismi, banthizi;
        string[] ports=SerialPort.GetPortNames();

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kid;
            timer1.Start();
            portismi = comboBox1.Text;
            banthizi = comboBox2.Text;

            try
            {
                serialPort1.PortName = portismi;
                serialPort1.BaudRate = Convert.ToInt32(banthizi);

                serialPort1.Open();
                label1.Text = "Bağlandı";
                label1.ForeColor = Color.Green;

            }
            catch 
            {
                serialPort1.Close();
                serialPort1.Open();
                MessageBox.Show("Bağlantı zaten açık");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            if (serialPort1.IsOpen==true)
            {
                serialPort1.Close();
                label1.Text = "Bağlantı Yok";
                label1.ForeColor = Color.Red;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();

            }

        }
        private void temizle()
        {
            lOkul.Text = "";
            lisim.Text ="" ;
            lsinif.Text ="" ;
            lkart.Text = "";
            lbakiye.Text = "";

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc!="")
            {
                lkart.Text = sonuc;
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "SELECT * FROM kantin WHERE kart_no='"+sonuc+"'";

                OleDbDataReader oku = kmt.ExecuteReader();
                if (oku.Read())
                {
                    DateTime bugun = DateTime.Now;

                    lOkul.Text = oku["okul_no"].ToString();
                    lisim.Text = oku["isim"].ToString();
                    lsinif.Text = oku["sinif"].ToString();
                    lkart.Text = oku["kart_no"].ToString();
                    lbakiye.Text = oku["bakiye"].ToString();
                    label5.Text=bugun.ToShortDateString();
                    label3.Text=bugun.ToLongTimeString() ;
                    bag.Close();

                    bag.Open();
                    kmt.CommandText = "INSERT INTO zaman(isim,tarih,saat,bakiye)VALUES ('" + lisim.Text + "','" + label5.Text + "','" + label3.Text + "','"+lbakiye.Text+"')";
                    kmt.ExecuteReader();
                    bag.Close();
                }
                else
                {
                    temizle();

                    MessageBox.Show("Böyle bir öğrenci yoktur.");
                }

                bag.Close();
            }
            
        }
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F5)
            {
                Form2 f = new Form2();
                f.Show();
            }
            if (e.KeyCode == Keys.F12)
            {
                Form3 f = new Form3();
                f.Show();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update ogrenci set isim='" + lisim.Text + "',sinif='" + lsinif.Text + "',kart_no='" + lkart.Text + "',bakiye='" + lbakiye.Text + "' where okul_no=" + lOkul.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();






        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                //Textboxları okuyup sayi1 ve sayi2 değişkenlerine aktarma
                double sayi1 = Convert.ToDouble(lbakiye.Text);
                double sayi2 = Convert.ToDouble(textBox1.Text);

                //sayıları toplayıp label nesnelerine ekliyoruz.
                textBox2.Text = Convert.ToString(sayi1 + sayi2);

            }
            catch (Exception)
            {
                MessageBox.Show("Para ekleme hatalı");
               
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
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
                    label1.Text = "Bağlandı";
                    label1.ForeColor = Color.Green;

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
