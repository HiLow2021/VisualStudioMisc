using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRChatAPITest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

            comboBox1.SelectedIndexChanged += (sender, e) =>
            {
                var enabled = comboBox1.SelectedIndex == 0;

                textBox1.Enabled = enabled;
                textBox2.Enabled = enabled;
            };
            button1.Click += async (sender, e) =>
            {
                var worldId = textBox4.Text;
                var result = string.Empty;

                if (comboBox1.SelectedIndex == 0)
                {
                    var username = textBox1.Text;
                    var password = textBox2.Text;
                    var wrapper = new ApiWrapper(username, password);
                    result = await wrapper.GetWorldAsync(worldId);
                }
                else
                {
                    var wrapper = new ApiNetWrapper();
                    result = await wrapper.GetWorldAsync(worldId);
                }

                textBox3.AppendText(result + Environment.NewLine + Environment.NewLine);
            };
        }
    }
}
