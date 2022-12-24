using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGraphics
{
    public partial class Form1 : Form
    {
        public class Sprite
        {
            public int px { get; set; }
            public int py { get; set; }
            public int vx { get; set; }
            public int vy { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public Color color { get; set; }

            public Sprite() { }
        }
        
        List<Sprite> sprite = new List<Sprite>();
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void snapshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "BMP形式|*.bmp|JPG形式|*.jpg;*.jpeg|PNG形式|*.png|GIF形式|*.gif";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string extension = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                Bitmap saveImg = new Bitmap(pictureBox1.Width, pictureBox1.Height);

                pictureBox1.DrawToBitmap(saveImg, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

                switch (extension)
                {
                    case ".bmp":
                        saveImg.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case ".jpg":
                    case ".jpeg":
                        saveImg.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case ".png":
                        saveImg.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;

                    case ".gif":
                        saveImg.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void colorMosaicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!colorMosaicToolStripMenuItem.Checked)
            {
                colorMosaicToolStripMenuItem.Checked = true;
                colorBallToolStripMenuItem.Checked = false;
            }

            timer1.Interval = 100;
            sprite.Clear();

            for (int y = 0; y < pictureBox1.Height; y += 30)
            {
                for (int x = 0; x < pictureBox1.Width; x += 30)
                {
                    Sprite ss = new Sprite();
                    ss.px = x;
                    ss.py = y;
                    ss.vx = 0;
                    ss.vy = 0;
                    ss.width = 30;
                    ss.height = 30;
                    ss.color = Color.FromArgb(255, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

                    sprite.Add(ss);
                }
            }

            pictureBox1.Refresh();
        }

        private void colorBallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!colorBallToolStripMenuItem.Checked)
            {
                colorMosaicToolStripMenuItem.Checked = false;
                colorBallToolStripMenuItem.Checked = true;
            }

            timer1.Interval = 15;
            sprite.Clear();

            int num = (pictureBox1.Width >= pictureBox1.Height) ? pictureBox1.Width : pictureBox1.Height;

            for (int i = 0; i < num; i++)
            {
                Sprite ss = new Sprite();
                ss.px = rnd.Next(0, pictureBox1.Width);
                ss.py = rnd.Next(0, pictureBox1.Height);
                ss.vx = rnd.Next(-5, 5);
                ss.vy = rnd.Next(-5, 5);
                ss.width = 30;
                ss.height = 30;
                ss.color = Color.FromArgb(255, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

                sprite.Add(ss);
            }

            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (colorMosaicToolStripMenuItem.Checked)
            {
                for (int i = 0; i < sprite.Count; i++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(sprite[i].color), sprite[i].px, sprite[i].py, sprite[i].width, sprite[i].height);
                }
            }

            if (colorBallToolStripMenuItem.Checked)
            {
                for (int i = 0; i < sprite.Count; i++)
                {
                    e.Graphics.FillEllipse(new SolidBrush(sprite[i].color), sprite[i].px, sprite[i].py, sprite[i].width, sprite[i].height);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (colorMosaicToolStripMenuItem.Checked)
            {
                for (int i = 0; i < sprite.Count; i++)
                {
                    sprite[i].color = Color.FromArgb(255, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                }
            }

            if (colorBallToolStripMenuItem.Checked)
            {
                for (int i = 0; i < sprite.Count; i++)
                {
                    sprite[i].px += sprite[i].vx;
                    sprite[i].py += sprite[i].vy;

                    if (sprite[i].px <= 0 || sprite[i].px >= pictureBox1.Width - sprite[i].width)
                    {
                        sprite[i].vx *= -1;
                    }
                    if (sprite[i].py <= 0 || sprite[i].py >= pictureBox1.Height - sprite[i].height)
                    {
                        sprite[i].vy *= -1;
                    }
                }
            }

            pictureBox1.Refresh();
        }
    }
}
