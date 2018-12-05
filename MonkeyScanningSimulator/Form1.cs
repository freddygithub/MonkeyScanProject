using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonkeyScanningSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            comboBox1.Items.Add(10);
            comboBox1.Items.Add(50);
            comboBox1.Items.Add(100);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public void typing(string text, string times)
        {
            int time = int.Parse(times);

            for (int i = 0; i < time; i++)
                foreach(char a in text)
                    SendKeys.Send("" + a);
            
            SendKeys.Send("{ENTER}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            typing(textBox1.Text, comboBox1.Text);
        }
    }
}
