using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using My.Extensions;
using My.Network;

namespace SimpleChatClient
{
    public partial class Form1 : Form
    {
        private SimpleTcpClient tcpClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcpClient?.Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void connectServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new Form2())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    tcpClient = new SimpleTcpClient(IPAddress.Loopback, form.Port);
                    tcpClient.Connected += (sender2, e2) => ShowMessage(e2.Address + "に接続しました。");
                    tcpClient.Disconnected += (sender2, e2) => ShowMessage(e2.Address + "の接続を切りました。");
                    tcpClient.Received += (sender2, e2) => ShowMessage(e2.Address + "からデータを受信しました。");
                    tcpClient?.Start();
                }
            }
        }

        private void disconnectServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcpClient?.Stop();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var data = Encoding.UTF8.GetBytes(textBox2.Text);

            await tcpClient?.SendAsync(data);

            textBox2.Text = string.Empty;
        }

        private void ShowMessage(string message)
        {
            textBox1.InvokeIfRequired(() => textBox1.AppendText(message + Environment.NewLine));
        }
    }
}
