using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleChatClient
{
    public partial class Form2 : Form
    {
        public string Host { get; private set; }
        public int Port { get; private set; }
        public string ClientName { get; private set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Host = textBox1.Text;
            Port = (int)numericUpDown1.Value;
            ClientName = textBox2.Text;
        }
    }
}
