using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceSearcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var index = comboBox1.SelectedIndex;
            var deviceInfos = GetDeviceInfos(index);

            foreach (var item in deviceInfos)
            {
                ShowMessage($"{nameof(item.PNPClass)} : {item.PNPClass}");
                ShowMessage($"{nameof(item.DeviceID)} : {item.DeviceID}");
                ShowMessage($"{nameof(item.PnpDeviceID)} : {item.PnpDeviceID}");
                ShowMessage($"{nameof(item.Description)} : {item.Description}");
                ShowMessage($"{nameof(item.Caption)} : {item.Caption}", 2);
            }

            ShowMessage($"総数 : {deviceInfos.Count}", 3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private IList<DeviceInfo> GetDeviceInfos(int index)
        {
            switch (index)
            {
                case 0:
                    return DeviceSearcher.GetDeviceInfos();

                case 1:
                    return DeviceSearcher.GetUSBDevices();

                case 2:
                    return DeviceSearcher.GetWebCameras();

                case 3:
                    return DeviceSearcher.GetSerialPorts();

                default:
                    throw new NotSupportedException();
            }
        }

        private void ShowMessage(string message, int newLineCount = 1)
        {
            var newLines = string.Join(string.Empty, Enumerable.Repeat(Environment.NewLine, newLineCount));

            textBox1.AppendText(message + newLines);
        }
    }
}
