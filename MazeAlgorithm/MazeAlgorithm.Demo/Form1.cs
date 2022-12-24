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
using Maze;

namespace MazeAlgorithm.Demo
{
    public partial class Form1 : Form
    {
        MazeProvider _Provider = new PullBar();
        Layer _Layer = new Layer(101, 61);
        CancellationTokenSource _cts = null;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_cts == null)
            {
                try
                {
                    _cts = new CancellationTokenSource();

                    await Task.Run(() => _Provider.Create(_Layer));

                    pictureBox1.Refresh();
                }
                finally
                {
                    _cts.Dispose();
                    _cts = null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = (sender as ComboBox).SelectedIndex;

            if (Index == 0)
            {
                _Provider = new PullBar();
            }
            else if (Index == 1)
            {
                _Provider = new Dig(true);
            }
            else if (Index == 2)
            {
                _Provider = new Dig(false);
            }
            else if (Index == 3)
            {
                _Provider = new ExtendWall();
            }
            else if (Index == 4)
            {
                _Provider = new Cluster();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox PB = (PictureBox)sender;
            float CellWidth = (float)PB.Width / _Layer.Width;
            float CellHeight = (float)PB.Height / _Layer.Height;

            for (int y = 0; y < _Layer.Height; y++)
            {
                for (int x = 0; x < _Layer.Width; x++)
                {
                    if (_Layer.Get(x, y) == BlockType.Sentinel)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    }
                    else if (_Layer.Get(x, y) == BlockType.Wall)
                    {
                        e.Graphics.FillRectangle(Brushes.Brown, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    }
                    else if (_Layer.Get(x, y) == BlockType.Road)
                    {
                        e.Graphics.FillRectangle(Brushes.White, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    }
                    else if (_Layer.Get(x, y) == BlockType.Mark)
                    {
                        e.Graphics.FillRectangle(Brushes.GreenYellow, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    }
                }
            }
        }
    }
}
