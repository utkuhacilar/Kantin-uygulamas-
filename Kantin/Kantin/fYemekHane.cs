using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kantin
{
    public partial class fYemekHane : Form
    {
        public fYemekHane()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=kantin.accdb");
        OleDbCommand kmt = new OleDbCommand();
        public static string portismi, banthizi;
        string[] ports = SerialPort.GetPortNames();

        private void timer1_Tick(object sender, EventArgs e)
        {

            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc != "")
            {
                lkart.Text = sonuc;
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "SELECT * FROM yemekhane WHERE kart_no='" + sonuc + "'";

                OleDbDataReader oku = kmt.ExecuteReader();
                if (oku.Read())
                {
                    lOkul.Text = oku["okul_no"].ToString();
                    lisim.Text = oku["isim"].ToString();
                    lsinif.Text = oku["sinif"].ToString();
                    lkart.Text = oku["kart_no"].ToString();
                    bag.Close();
                    timer2.Start();
                }
                bag.Close();
            }

        }
        private void temizle()
        {
            lOkul.Text = "";
            lisim.Text = "";
            lsinif.Text = "";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            string sonuc;
            sonuc = lkart.Text;

            if (sonuc != "")
            {
                lkart.Text = sonuc;
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "SELECT * FROM yemekhane WHERE kart_no='" + sonuc + "'";

                OleDbDataReader oku = kmt.ExecuteReader();
                if (oku.Read())
                {
                    this.BackColor = Color.Green;
                    bag.Close();

                }
                else
                {
                    if (this.BackColor != Color.PapayaWhip)
                    {
                        this.BackColor = Color.Red;
                        Console.Beep(900, 100);
                        Console.Beep(900, 100);
                        Console.Beep(900, 100);
                        Console.Beep(900, 100);
                        Console.Beep(900, 100);
                        temizle();
                        timer2.Stop();
                    }

                }

                bag.Close();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            temizle();
        }

        private void timer3_Tick_1(object sender, EventArgs e)
        {
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void timer3_Tick_2(object sender, EventArgs e)
        {

        }

        private void fYemekHane_Load(object sender, EventArgs e)
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
