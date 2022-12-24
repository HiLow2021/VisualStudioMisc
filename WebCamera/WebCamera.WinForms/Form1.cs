using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCamera.WinForms
{
    public partial class Form1 : Form
    {
        private readonly ToolStripMenuItem[] menuItems;
        private readonly WebCamera webCamera = new WebCamera();
        private int selectedDeviceIndex;

        public Form1()
        {
            InitializeComponent();

            menuItems = new ToolStripMenuItem[]
            {
                device1ToolStripMenuItem,
                device2ToolStripMenuItem,
                device3ToolStripMenuItem,
                device4ToolStripMenuItem
            };

            Load += (sender, e) =>
            {
                OpenWebCamera();
                menuItems[selectedDeviceIndex].Checked = true;
            };
            FormClosed += (sender, e) => webCamera?.Dispose();
            exitToolStripMenuItem.Click += (sender, e) => Application.Exit();
            openDeviceToolStripMenuItem.Click += (sender, e) => OpenWebCamera();
            closeDeviceToolStripMenuItem.Click += (sender, e) => CloseWebCamera();
            device1ToolStripMenuItem.Click += (sender, e) => ChangeToolStripMenuItemChecked(sender as ToolStripMenuItem);
            device2ToolStripMenuItem.Click += (sender, e) => ChangeToolStripMenuItemChecked(sender as ToolStripMenuItem);
            device3ToolStripMenuItem.Click += (sender, e) => ChangeToolStripMenuItemChecked(sender as ToolStripMenuItem);
            device4ToolStripMenuItem.Click += (sender, e) => ChangeToolStripMenuItemChecked(sender as ToolStripMenuItem);
            webCamera.FrameChanged += (sender, e) =>
            {
                pictureBox1.Invoke(new Action(() =>
                {
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = e.Frame;
                }));
            };
        }

        private void OpenWebCamera()
        {
            webCamera.Open(selectedDeviceIndex, 800, 600, 30);

            if (webCamera.IsOpened)
            {
                openDeviceToolStripMenuItem.Checked = true;
                closeDeviceToolStripMenuItem.Checked = false;
            }
        }

        private void CloseWebCamera()
        {
            webCamera.Close();

            Task.Delay(1000).ContinueWith(x => pictureBox1.Invoke(new Action(() =>
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = null;
                pictureBox1.Refresh();
            })));

            openDeviceToolStripMenuItem.Checked = false;
            closeDeviceToolStripMenuItem.Checked = true;
        }

        private void ChangeToolStripMenuItemChecked(ToolStripMenuItem toolStripMenuItem)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Checked = false;

                if (menuItems[i] == toolStripMenuItem)
                {
                    selectedDeviceIndex = i;
                }
            }

            toolStripMenuItem.Checked = true;
        }
    }
}
