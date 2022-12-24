using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestLibWrapper;

namespace DllWrapperDemo
{
    public partial class Form1 : Form
    {
        private readonly DllWrapper wrapper = new DllWrapper();

        public Form1()
        {
            InitializeComponent();

            button1.Click += (sender, e) =>
            {
                var result = wrapper.GetGrandParent("family");

                DisplayMessage(result.Type.ToString() + ": " + result.Name);
                DisplayMessage(result.Parent.Type.ToString() + ": " + result.Parent.Name);
                DisplayMessage(result.Child.Type.ToString() + ": " + result.Child.Name);

                var array = wrapper.GetStringArray(20);

                foreach (var item in array)
                {
                    DisplayMessage(item.ToString());
                }
            };
        }

        private void DisplayMessage(string message)
        {
            textBox1.AppendText(message + Environment.NewLine);
        }
    }
}
