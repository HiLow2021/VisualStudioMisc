using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertTextTemplate
{
    public partial class Form1 : Form
    {
        enum Encoding
        {
            Shift_JIS = 932,
            EUC_JP = 51932,
            US_ASCII = 20127,
            UTF_16 = 1200,
            UTF_8 = 65001
        }

        string LoadFileName;
        string SaveFileName;
        int ReadEncodingID = (int)Encoding.Shift_JIS;
        int WriteEncodingID = (int)Encoding.Shift_JIS;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadFileName = textBox1.Text;
            SaveFileName = Path.GetDirectoryName(LoadFileName) +
                @"\" + Path.GetFileNameWithoutExtension(LoadFileName) +
                "_out" + Path.GetExtension(LoadFileName);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;

            if (index == 0)
            {
                ReadEncodingID = (int)Encoding.Shift_JIS;
            }
            else if (index == 1)
            {
                ReadEncodingID = (int)Encoding.EUC_JP;
            }
            else if (index == 2)
            {
                ReadEncodingID = (int)Encoding.US_ASCII;
            }
            else if (index == 3)
            {
                ReadEncodingID = (int)Encoding.UTF_16;
            }
            else
            {
                ReadEncodingID = (int)Encoding.UTF_8;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox2.SelectedIndex;

            if (index == 0)
            {
                WriteEncodingID = (int)Encoding.Shift_JIS;
            }
            else if (index == 1)
            {
                WriteEncodingID = (int)Encoding.EUC_JP;
            }
            else if (index == 2)
            {
                WriteEncodingID = (int)Encoding.US_ASCII;
            }
            else if (index == 3)
            {
                WriteEncodingID = (int)Encoding.UTF_16;
            }
            else
            {
                WriteEncodingID = (int)Encoding.UTF_8;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(LoadFileName))
            {
                return;
            }

            string txt;

            txt = File.ReadAllText(LoadFileName, System.Text.Encoding.GetEncoding(ReadEncodingID));

            // ここに行いたい処理を記述する。

            File.WriteAllText(SaveFileName, txt, System.Text.Encoding.GetEncoding(WriteEncodingID));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
