using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiczbyPierwsze
{
    public partial class Form1 : Form
    {
        //Program odczytujący plik tekstowy z liczbami całkowitymi
        //i wyszukujący liczby pierwsze.
        //Znalezione liczby mają być zapisane do pliku wynikowego.
        //Plik źródłowy ma być najpierw wygenerowany losowo i
        //zawierać przykładowo 1 miliard liczb.
        //Program należy napisać w wersji wielowątkowej.
        private int ID;
        private Thread t;
        public string sciezka = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                ThreadedWorker(i);
            }
        }

        public void ThreadedWorker(int ID)
        {
            this.ID = ID;
            t = new Thread(new ThreadStart(funkcjaGenerujPlik));
            t.Start();
        }

        private void funkcjaGenerujPlik()
        {
            Random rnd = new Random();
            string tekst = "";
            for (int i = 0; i < 10000; i++)
            {
                int liczba = rnd.Next(0, 10000);
                tekst += liczba + "\r\n";
                this.Invoke((MethodInvoker)(() => listBox1.Items.Add(liczba)));
            }
            //saveFileDialog1.Filter = "Text Files | *.txt";
            //saveFileDialog1.ShowDialog();
            //sciezka = saveFileDialog1.FileName;
            //File.WriteAllText(sciezka, tekst);
            //MessageBox.Show("Wygenerowano plik " + saveFileDialog1.FileName);
            //tbLiczbaWierszy.Text = string.Empty;
        }

        static bool pierwsza(long a)
        {
            for (long j = 2; j <= (a / 2); j++)
                if (a % j == 0)
                    return false;
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tekst = "";
            foreach (var row in listBox1.Items)
            {
                if (pierwsza(int.Parse(row.ToString())))
                {
                    listBox2.Items.Add(row.ToString());
                    tekst += row.ToString() + "\r\n";
                }
            }
            saveFileDialog2.Filter = "Text Files | *.txt";
            saveFileDialog2.ShowDialog();
            sciezka = saveFileDialog2.FileName;
            File.WriteAllText(sciezka, tekst);
            MessageBox.Show("Wygenerowano plik z liczbami Pierwszymi: " + sciezka);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            czysc();
        }

        private void czysc()
        {
            tbLiczbaWierszy.Text = string.Empty;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files | *.txt";
            openFileDialog1.ShowDialog();
            sciezka = openFileDialog1.FileName;

            int counter = 0;
            string line;
            StreamReader file = new StreamReader(sciezka);
            while ((line = file.ReadLine()) != null)
            {
                listBox1.Items.Add(line);
            }
            file.Close();
        }
    }
}
