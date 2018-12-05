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

namespace MonkeyScanningSimulator
{
    public partial class Form1 : Form
    {
        Thread thread; int stopFlag = 0;

        public Form1()
        {
            InitializeComponent();

            readPast();
            mySetup();
            TopMost = true;
        }

        public void mySetup()
        {
            comboBox3.Enabled = true;
            comboBox2.Enabled = true;
            comboBox1.Enabled = true;

            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;
            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;
            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;

            comboBox3.Items.Add(1);
            comboBox3.Items.Add(3);
            comboBox3.Items.Add(5);

            comboBox1.Items.Add(10);
            comboBox1.Items.Add(50);
            comboBox1.Items.Add(100);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public void readPast()
        {
            try
            {
                using (var tsw = new StreamReader(Environment.CurrentDirectory + @"\save.txt"))
                {
                    string line;
                    while ((line = tsw.ReadLine()) != null)
                    {
                        comboBox2.Items.Add(line);
                    }
                }
            }
            catch(Exception ex)
            {
                return;
            }
        }

        public void typing(string text, string times, string speed)
        {
            int zoom = 0;
            int time = 0;

            try
            {
                time = int.Parse(times);
                zoom = int.Parse((1000 * double.Parse(speed)).ToString());
            }
            catch
            {
                MessageBox.Show("You not good typing",
                                    "Error: could not parse number you entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        
            if (zoom <= 0)
                zoom = 5000;

            for (int i = 0; i < time; i++)
            {
                if (stopFlag == 1)
                    break;

                foreach (char a in text)
                    SendKeys.SendWait("" + a);

                SendKeys.SendWait("{ENTER}");

                Thread.Sleep(zoom);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Focus();

            if(thread !=null && thread.IsAlive)
            {
                stopFlag = 1;
                thread.Abort();
                return;
            }

            string param1 = comboBox2.Text;
            string param2 = comboBox1.Text;
            string param3 = comboBox3.Text;

            stopFlag = 0;
            thread = new Thread(() => typing(param1, param2, param3));
            thread.Start();
            
            using (var tsw = new StreamWriter(Environment.CurrentDirectory + @"\save.txt"))
            {
                tsw.WriteLine(comboBox2.Text);
            }
        }

        private void Form1_Closing()
        {

        }
    }
}
