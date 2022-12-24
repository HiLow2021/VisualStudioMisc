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

namespace SimpleChatServer
{
    public partial class Form1 : Form
    {
        private SimpleTcpServer tcpServer;

        public Form1()
        {
            InitializeComponent();

            Load += (sender, e) =>
            {
                tcpServer = new SimpleTcpServer(IPAddress.Any, 10000);
                tcpServer.Start();
                tcpServer.Connected += (sender2, e2) => ShowMessage(e2.Address + "が接続しました。");
                tcpServer.Disconnected += (sender2, e2) => ShowMessage(e2.Address + "が接続を切りました。");
                tcpServer.Received += (sender2, e2) =>
                {
                    var data = Encoding.UTF8.GetString(e2.Data).TrimEnd('\0');

                    ShowMessage($"{e2.Address}から{data}を受信しました。");
                };
            };
            FormClosing += (sender, e) =>
            {
                tcpServer?.Stop();
                //tcpServer?.Dispose();
            };
        }

        private void ShowMessage(string message)
        {
            textBox1.InvokeIfRequired(() => textBox1.AppendText(message + Environment.NewLine));
        }
    }
}
