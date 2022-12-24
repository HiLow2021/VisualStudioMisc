using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;

namespace ControlMemberWrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            string[] Items = listView1.SelectedItems[0].Text.Split('.');
            string NameSpace = listView1.SelectedItems[0].SubItems[2].Text;
            string ClassName = Items[0];
            string MemberName = Items[1];
            string MemberType = listView1.SelectedItems[0].SubItems[1].Text;

            DisplayCode(NameSpace, ClassName, MemberName, MemberType);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                (sender as TextBox).SelectAll();

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            UpdateListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DisplayCode(string NameSpace, string ClassName, string MemberName, string MemberType)
        {
            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
            Type t = asms.SelectMany(x => x.GetTypes()).Where(x => x.Namespace == NameSpace && x.Name == ClassName).First();

            string Text = string.Empty;

            if (MemberType == "Property" || MemberType == "Event")
            {
                Text += GetAttributeValue<BrowsableAttribute>(t, MemberName);
                Text += GetAttributeValue<DescriptionAttribute>(t, MemberName);
                Text += GetAttributeValue<CategoryAttribute>(t, MemberName);
                Text += GetAttributeValue<DefaultValueAttribute>(t, MemberName);
            }

            textBox2.Text = Text;
        }

        private string GetAttributeValue<T>(Type ClassType, string MemberName)
            where T : Attribute
        {
            string Text = string.Empty;

            Func<object, string> WriteLine = (s) =>
            {
                return s + Environment.NewLine;
            };

            Action<Type> a = null;
            a = (ct) =>
            {
                MemberInfo[] m = ct.GetMember(MemberName);

                if (0 < m.Length)
                {
                    T[] ts = (T[])m[0].GetCustomAttributes(typeof(T), false);

                    if (0 < ts.Length)
                    {
                        if (typeof(T) == typeof(BrowsableAttribute))
                        {
                            Text += WriteLine((ts[0] as BrowsableAttribute).Browsable.ToString());
                        }
                        else if (typeof(T) == typeof(DescriptionAttribute))
                        {
                            Text += WriteLine((ts[0] as DescriptionAttribute).Description);
                        }
                        else if (typeof(T) == typeof(CategoryAttribute))
                        {
                            Text += WriteLine((ts[0] as CategoryAttribute).Category);
                        }
                        else if (typeof(T) == typeof(DefaultValueAttribute))
                        {
                            Text += WriteLine((ts[0] as DefaultValueAttribute).Value);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    else
                    {
                        a(ct.BaseType);
                    }
                }
                else
                {
                    if (typeof(T) == typeof(BrowsableAttribute))
                    {
                        Text += WriteLine("True");
                    }
                    else if (typeof(T) == typeof(DescriptionAttribute) || typeof(T) == typeof(CategoryAttribute) || typeof(T) == typeof(DefaultValueAttribute))
                    {
                        Text += WriteLine("\"No " + typeof(T).Name + "\"");
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
            };

            a(ClassType);

            return Text;
        }

        private void UpdateListView()
        {
            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
            Type[] ts = asms.SelectMany(x => x.GetTypes()).Where(x => x.IsClass && IsControl(x)).ToArray();

            foreach (Type t in ts)
            {
                MemberInfo[] ms = t.GetMembers();

                listView1.Items.AddRange(ms.Where(x => Regex.IsMatch(t.Name + "." + x.Name, textBox1.Text, RegexOptions.IgnoreCase))
                                           .Select(x => new ListViewItem(new string[] { t.Name + "." + x.Name, x.MemberType.ToString(), t.Namespace }))
                                           .ToArray());
            }
        }

        private bool IsControl(Type t)
        {
            bool IsControlFlag;

            if (t == typeof(BackgroundWorker))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(BindingNavigator))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(BindingSource))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(Button))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(Chart))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(CheckBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(CheckedListBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ColorDialog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ComboBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ContextMenuStrip))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(DataGridView))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(DataSet))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(DateTimePicker))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(DirectoryEntry))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(DirectorySearcher))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(DomainUpDown))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ElementHost))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ErrorProvider))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(EventLog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(FileSystemWatcher))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(FlowLayoutPanel))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(FolderBrowserDialog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(FontDialog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(GroupBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(HelpProvider))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(HScrollBar))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ImageList))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(Label))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(LinkLabel))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ListBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ListView))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(MaskedTextBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(MenuStrip))
            {
                IsControlFlag = true;
            }
            //else if (t == typeof(MessageQueue))
            //{
            //    IsControlFlag = true;
            //}
            else if (t == typeof(MonthCalendar))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(NotifyIcon))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(NumericUpDown))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(OpenFileDialog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(PageSetupDialog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(Panel))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(PerformanceCounter))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(PictureBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(PrintDialog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(PrintDocument))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(PrintPreviewControl))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(PrintPreviewDialog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(Process))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ProgressBar))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(PropertyGrid))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(RadioButton))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(RichTextBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(SaveFileDialog))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(SerialPort))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ServiceController))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(SplitContainer))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(Splitter))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(StatusStrip))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(TabControl))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(TableLayoutPanel))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(TextBox))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(Timer))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ToolStrip))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ToolStripContainer))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(ToolTip))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(TrackBar))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(TreeView))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(VScrollBar))
            {
                IsControlFlag = true;
            }
            else if (t == typeof(WebBrowser))
            {
                IsControlFlag = true;
            }
            else
            {
                IsControlFlag = false;
            }

            return IsControlFlag;
        }
    }
}
