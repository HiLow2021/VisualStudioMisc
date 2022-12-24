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

namespace MazeAlgorithm.Demo.Old
{
    public partial class Form1 : Form
    {
        int[,] Maze;                    // 迷路
        const int MAZE_W_NUM = 101;     // 迷路の横マスの数(※ 必ず奇数にすること)
        const int MAZE_H_NUM = 61;      // 迷路の縦マスの数(※ 必ず奇数にすること)
        const int SENTINEL = -2;        // 外壁(番兵)
        const int WALL = -1;            // 内壁(※ x,y座標ともに奇数に置かないこと)
        const int ROAD = 0;             // 道(※ x,y座標ともに偶数に置かないこと)
        const int MARK = 1;             // 目印
        const int NORTH = 1;            // 北
        const int EAST = 2;             // 東
        const int SOUTH = 3;            // 南
        const int WEST = 4;             // 西
        int WaitTime = 10;              // 表示の時間間隔(0は瞬間表示)
        float CellWidth;                // 一マスの横幅
        float CellHeight;               // 一マスの縦幅

        CancellationTokenSource cts = null;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Maze = new int[MAZE_H_NUM, MAZE_W_NUM];
            CellWidth = (float)pictureBox1.Width / MAZE_W_NUM;
            CellHeight = (float)pictureBox1.Height / MAZE_H_NUM;

            for (int i = 0; i < MAZE_W_NUM; i++)
            {
                Maze[0, i] = SENTINEL;
                Maze[MAZE_H_NUM - 1, i] = SENTINEL;
            }
            for (int i = 0; i < MAZE_H_NUM; i++)
            {
                Maze[i, 0] = SENTINEL;
                Maze[i, MAZE_W_NUM - 1] = SENTINEL;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            CellWidth = (float)pictureBox1.Width / MAZE_W_NUM;
            CellHeight = (float)pictureBox1.Height / MAZE_H_NUM;
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) // 表示用メソッド
        {
            for (int y = 0; y < MAZE_H_NUM; y++)
            {
                for (int x = 0; x < MAZE_W_NUM; x++)
                {
                    if (Maze[y, x] == SENTINEL)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    }
                    else if (Maze[y, x] == WALL)
                    {
                        e.Graphics.FillRectangle(Brushes.Brown, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    }
                    else if (Maze[y, x] == ROAD)
                    {
                        e.Graphics.FillRectangle(Brushes.White, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    }
                    else if (Maze[y, x] == MARK)
                    {
                        e.Graphics.FillRectangle(Brushes.GreenYellow, x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                    }
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ButtonControlEnabled(false);

            for (int i = 0; i < MAZE_W_NUM; i++)
            {
                Maze[0, i] = SENTINEL;
                Maze[MAZE_H_NUM - 1, i] = SENTINEL;
            }
            for (int i = 0; i < MAZE_H_NUM; i++)
            {
                Maze[i, 0] = SENTINEL;
                Maze[i, MAZE_W_NUM - 1] = SENTINEL;
            }

            for (int y = 1; y < MAZE_H_NUM - 1; y++)
            {
                for (int x = 1; x < MAZE_W_NUM - 1; x++)
                {
                    Maze[y, x] = ROAD;
                }
            }
            for (int y = 2; y < MAZE_H_NUM - 1; y += 2)
            {
                for (int x = 2; x < MAZE_W_NUM - 1; x += 2)
                {
                    Maze[y, x] = WALL;
                }
            }

            if (cts == null)
            {
                cts = new CancellationTokenSource();
            }

            await Task.Run(() =>
            {
                PullBarMethodDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                int[] p = MakeStartGoalPointDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                DisplayAnwserRouteDemo(p[0], p[1], p[2], p[3]);
            }, cts.Token).ContinueWith((t) =>
            {
                cts.Dispose();
                cts = null;
            });

            pictureBox1.Refresh();
            ButtonControlEnabled(true);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            ButtonControlEnabled(false);

            for (int i = 0; i < MAZE_W_NUM; i++)
            {
                Maze[0, i] = SENTINEL;
                Maze[MAZE_H_NUM - 1, i] = SENTINEL;
            }
            for (int i = 0; i < MAZE_H_NUM; i++)
            {
                Maze[i, 0] = SENTINEL;
                Maze[i, MAZE_W_NUM - 1] = SENTINEL;
            }

            for (int j = 1; j < MAZE_H_NUM - 1; j++)
            {
                for (int i = 1; i < MAZE_W_NUM - 1; i++)
                {
                    Maze[j, i] = WALL;
                }
            }

            if (cts == null)
            {
                cts = new CancellationTokenSource();
            }

            await Task.Run(() =>
            {
                RecursiveDigMethodDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                int[] p = MakeStartGoalPointDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                DisplayAnwserRouteDemo(p[0], p[1], p[2], p[3]);
            }, cts.Token).ContinueWith((t) =>
            {
                cts.Dispose();
                cts = null;
            });

            pictureBox1.Refresh();
            ButtonControlEnabled(true);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            ButtonControlEnabled(false);

            for (int i = 0; i < MAZE_W_NUM; i++)
            {
                Maze[0, i] = SENTINEL;
                Maze[MAZE_H_NUM - 1, i] = SENTINEL;
            }
            for (int i = 0; i < MAZE_H_NUM; i++)
            {
                Maze[i, 0] = SENTINEL;
                Maze[i, MAZE_W_NUM - 1] = SENTINEL;
            }

            for (int j = 1; j < MAZE_H_NUM - 1; j++)
            {
                for (int i = 1; i < MAZE_W_NUM - 1; i++)
                {
                    Maze[j, i] = WALL;
                }
            }

            if (cts == null)
            {
                cts = new CancellationTokenSource();
            }

            await Task.Run(() =>
            {
                DigMethodDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                int[] p = MakeStartGoalPointDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                DisplayAnwserRouteDemo(p[0], p[1], p[2], p[3]);
            }, cts.Token).ContinueWith((t) =>
            {
                cts.Dispose();
                cts = null;
            });

            pictureBox1.Refresh();
            ButtonControlEnabled(true);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            ButtonControlEnabled(false);

            for (int i = 0; i < MAZE_W_NUM; i++)
            {
                Maze[0, i] = SENTINEL;
                Maze[MAZE_H_NUM - 1, i] = SENTINEL;
            }
            for (int i = 0; i < MAZE_H_NUM; i++)
            {
                Maze[i, 0] = SENTINEL;
                Maze[i, MAZE_W_NUM - 1] = SENTINEL;
            }

            for (int j = 1; j < MAZE_H_NUM - 1; j++)
            {
                for (int i = 1; i < MAZE_W_NUM - 1; i++)
                {
                    Maze[j, i] = ROAD;
                }
            }

            if (cts == null)
            {
                cts = new CancellationTokenSource();
            }

            await Task.Run(() =>
            {
                ExtendWallMethodDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                int[] p = MakeStartGoalPointDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                DisplayAnwserRouteDemo(p[0], p[1], p[2], p[3]);
            }, cts.Token).ContinueWith((t) =>
            {
                cts.Dispose();
                cts = null;
            });

            pictureBox1.Refresh();
            ButtonControlEnabled(true);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            ButtonControlEnabled(false);

            for (int j = 0; j < MAZE_H_NUM; j++)
            {
                for (int i = 0; i < MAZE_W_NUM; i++)
                {
                    Maze[j, i] = SENTINEL;
                }
            }

            for (int j = 1; j < MAZE_H_NUM - 1; j += 2)
            {
                for (int i = 1; i < MAZE_W_NUM - 1; i += 2)
                {
                    Maze[j, i] = ROAD;
                }
            }

            if (cts == null)
            {
                cts = new CancellationTokenSource();
            }

            await Task.Run(() =>
            {
                ClusterMethodDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                int[] p = MakeStartGoalPointDemo();

                if (cts.IsCancellationRequested)
                {
                    return;
                }

                DisplayAnwserRouteDemo(p[0], p[1], p[2], p[3]);
            }, cts.Token).ContinueWith((t) =>
            {
                cts.Dispose();
                cts = null;
            });

            pictureBox1.Refresh();
            ButtonControlEnabled(true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }

        private void ButtonControlEnabled(bool State)
        {
            if (State)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = false;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = true;
            }
        }

        //-----------以下、迷路アルゴリズムを見せるために、冗長コードを入れたデモ版-----------//

        private void PullBarMethodDemo() // 棒倒し法
        {
            int direction;
            bool Flag = false;

            for (int y = 2; y < MAZE_H_NUM - 1; y += 2)
            {
                for (int x = 2; x < MAZE_W_NUM - 1; x += 2)
                {
                    do
                    {
                        direction = rnd.Next(1, 5);

                        switch (direction)
                        {
                            case (NORTH):
                                if (Maze[y - 1, x] != WALL && y == 2)
                                {
                                    Maze[y - 1, x] = WALL;
                                    Flag = true;
                                }
                                break;

                            case (EAST):
                                if (Maze[y, x + 1] != WALL)
                                {
                                    Maze[y, x + 1] = WALL;
                                    Flag = true;
                                }
                                break;

                            case (SOUTH):
                                if (Maze[y + 1, x] != WALL)
                                {
                                    Maze[y + 1, x] = WALL;
                                    Flag = true;
                                }
                                break;

                            case (WEST):
                                if (Maze[y, x - 1] != WALL)
                                {
                                    Maze[y, x - 1] = WALL;
                                    Flag = true;
                                }
                                break;
                        }

                        if (cts.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                    while (!Flag);
                    Flag = false;

                    UpdatePictureBox();
                }
            }
        }

        private void RecursiveDigMethodDemo()  // 穴掘り法(道のばし法)(完全再帰Ver)
        {
            int x = rnd.Next(2, MAZE_W_NUM - 1);
            int y = rnd.Next(2, MAZE_H_NUM - 1);

            x = (x % 2 == 0) ? x - 1 : x;
            y = (y % 2 == 0) ? y - 1 : y;

            RecursiveDigDemo(x, y);
        }

        private void RecursiveDigDemo(int x, int y) // 穴を掘るメソッド(完全に再帰的に掘る)
        {
            int direction;          // 方向
            bool NorthFlag = false; // 北を調べたか判定
            bool EastFlag = false;  // 東を調べたか判定
            bool SouthFlag = false; // 南を調べたか判定
            bool WestFlag = false;  // 西を調べたか判定

            Maze[y, x] = ROAD; // 穴を掘って道にする

            UpdatePictureBox();

            do
            {
                direction = rnd.Next(1, 5);

                switch (direction)
                {
                    case (NORTH):
                        if (Maze[y - 1, x] != SENTINEL && (x % 2 != 0 || (y - 1) % 2 != 0))
                        {
                            if (Maze[y - 1, x] != ROAD && Maze[y - 2, x] != ROAD)
                            {
                                RecursiveDigDemo(x, y - 1);
                            }
                        }
                        NorthFlag = true;
                        break;

                    case (EAST):
                        if (Maze[y, x + 1] != SENTINEL && ((x + 1) % 2 != 0 || y % 2 != 0))
                        {
                            if (Maze[y, x + 1] != ROAD && Maze[y, x + 2] != ROAD)
                            {
                                RecursiveDigDemo(x + 1, y);
                            }
                        }
                        EastFlag = true;
                        break;

                    case (SOUTH):
                        if (Maze[y + 1, x] != SENTINEL && (x % 2 != 0 || (y + 1) % 2 != 0))
                        {
                            if (Maze[y + 1, x] != ROAD && Maze[y + 2, x] != ROAD)
                            {
                                RecursiveDigDemo(x, y + 1);
                            }
                        }
                        SouthFlag = true;
                        break;

                    case (WEST):
                        if (Maze[y, x - 1] != SENTINEL && ((x - 1) % 2 != 0 || y % 2 != 0))
                        {
                            if (Maze[y, x - 1] != ROAD && Maze[y, x - 2] != ROAD)
                            {
                                RecursiveDigDemo(x - 1, y);
                            }
                        }
                        WestFlag = true;
                        break;
                }

                if (cts.IsCancellationRequested)
                {
                    return;
                }
            }
            while (!(NorthFlag && EastFlag && SouthFlag && WestFlag));
        }

        private void DigMethodDemo() // 穴掘り法(道のばし法)(部分再帰Ver)
        {
            int x = rnd.Next(2, MAZE_W_NUM - 1);
            int y = rnd.Next(2, MAZE_H_NUM - 1);

            x = (x % 2 == 0) ? x - 1 : x;
            y = (y % 2 == 0) ? y - 1 : y;

            DigDemo(x, y);

            do
            {
                x = rnd.Next(2, MAZE_W_NUM - 1);
                y = rnd.Next(2, MAZE_H_NUM - 1);

                x = (x % 2 == 0) ? x - 1 : x;
                y = (y % 2 == 0) ? y - 1 : y;

                if (CanDigDemo(x, y))
                {
                    DigDemo(x, y);
                }

                if (cts.IsCancellationRequested)
                {
                    return;
                }
            }
            while (SearchCanDigDemo());
        }

        private bool CanDigDemo(int x, int y) // 指定された開始場所から穴が掘れるか調べるメソッド
        {
            if (Maze[y, x] == ROAD)
            {
                for (int j = y - 2; j <= y + 2; j += 2)
                {
                    for (int i = x - 2; i <= x + 2; i += 2)
                    {
                        if ((0 <= j && j <= MAZE_H_NUM - 1) && (0 <= i && i <= MAZE_W_NUM - 1))
                        {
                            if (Maze[j, i] == ROAD)
                            {
                                continue;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool SearchCanDigDemo() // 穴を掘れる開始場所が存在するか調べるメソッド
        {
            for (int y = 1; y < MAZE_H_NUM - 1; y += 2)
            {
                for (int x = 1; x < MAZE_W_NUM - 1; x += 2)
                {
                    if (Maze[y, x] == ROAD)
                    {
                        for (int j = y - 2; j <= y + 2; j += 2)
                        {
                            for (int i = x - 2; i <= x + 2; i += 2)
                            {
                                if ((0 <= j && j <= MAZE_H_NUM - 1) && (0 <= i && i <= MAZE_W_NUM - 1))
                                {
                                    if (Maze[j, i] == ROAD)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void DigDemo(int x, int y) // 穴を掘るメソッド(部分的に再帰的に掘る)
        {
            int direction;          // 方向
            bool NorthFlag = false; // 北を調べたか判定
            bool EastFlag = false;  // 東を調べたか判定
            bool SouthFlag = false; // 南を調べたか判定
            bool WestFlag = false;  // 西を調べたか判定

            Maze[y, x] = ROAD; // 穴を掘って道にする

            UpdatePictureBox();

            do
            {
                direction = rnd.Next(1, 5);

                switch (direction)
                {
                    case (NORTH):
                        if (Maze[y - 1, x] != SENTINEL && (x % 2 != 0 || (y - 1) % 2 != 0))
                        {
                            if (Maze[y - 1, x] != ROAD && Maze[y - 2, x] != ROAD)
                            {
                                DigDemo(x, y - 1);
                                return;
                            }
                        }
                        NorthFlag = true;
                        break;

                    case (EAST):
                        if (Maze[y, x + 1] != SENTINEL && ((x + 1) % 2 != 0 || y % 2 != 0))
                        {
                            if (Maze[y, x + 1] != ROAD && Maze[y, x + 2] != ROAD)
                            {
                                DigDemo(x + 1, y);
                                return;
                            }
                        }
                        EastFlag = true;
                        break;

                    case (SOUTH):
                        if (Maze[y + 1, x] != SENTINEL && (x % 2 != 0 || (y + 1) % 2 != 0))
                        {
                            if (Maze[y + 1, x] != ROAD && Maze[y + 2, x] != ROAD)
                            {
                                DigDemo(x, y + 1);
                                return;
                            }
                        }
                        SouthFlag = true;
                        break;

                    case (WEST):
                        if (Maze[y, x - 1] != SENTINEL && ((x - 1) % 2 != 0 || y % 2 != 0))
                        {
                            if (Maze[y, x - 1] != ROAD && Maze[y, x - 2] != ROAD)
                            {
                                DigDemo(x - 1, y);
                                return;
                            }
                        }
                        WestFlag = true;
                        break;
                }

                if (cts.IsCancellationRequested)
                {
                    return;
                }
            }
            while (!(NorthFlag && EastFlag && SouthFlag && WestFlag));
        }

        private void ExtendWallMethodDemo() // 壁のばし法
        {
            int x;
            int y;

            do
            {
                x = rnd.Next(2, MAZE_W_NUM - 1);
                y = rnd.Next(2, MAZE_H_NUM - 1);

                x = (x % 2 == 0) ? x : x - 1;
                y = (y % 2 == 0) ? y : y - 1;

                if (Maze[y, x] != SENTINEL)
                {
                    if (ExtendWallDemo(x, y))
                    {
                        Maze[y, x] = SENTINEL;
                    }
                    else
                    {
                        Maze[y, x] = ROAD;
                    }
                }

                if (cts.IsCancellationRequested)
                {
                    return;
                }
            }
            while (SearchCanPutWallDemo());
        }

        private bool SearchCanPutWallDemo() // 壁を置ける場所が存在するか調べるメソッド
        {
            for (int y = 2; y < MAZE_H_NUM - 1; y += 2)
            {
                for (int x = 2; x < MAZE_W_NUM - 1; x += 2)
                {
                    if (Maze[y, x] == ROAD)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ExtendWallDemo(int x, int y) // 壁をのばすメソッド
        {
            int direction;          // 方向
            bool NorthFlag = false; // 北を調べたか判定
            bool EastFlag = false;  // 東を調べたか判定
            bool SouthFlag = false; // 南を調べたか判定
            bool WestFlag = false;  // 西を調べたか判定

            Maze[y, x] = WALL; // 壁を伸ばす

            UpdatePictureBox();

            do
            {
                direction = rnd.Next(1, 5);

                switch (direction)
                {
                    case (NORTH):
                        if (Maze[y - 1, x] != SENTINEL)
                        {
                            if (x % 2 == 0 || (y - 1) % 2 == 0)
                            {
                                if (Maze[y - 1, x] != WALL && Maze[y - 2, x] != WALL)
                                {
                                    if (ExtendWallDemo(x, y - 1))
                                    {
                                        Maze[y - 1, x] = SENTINEL;
                                        return true;
                                    }
                                    else
                                    {
                                        Maze[y - 1, x] = ROAD;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        NorthFlag = true;
                        break;

                    case (EAST):
                        if (Maze[y, x + 1] != SENTINEL)
                        {
                            if ((x + 1) % 2 == 0 || y % 2 == 0)
                            {
                                if (Maze[y, x + 1] != WALL && Maze[y, x + 2] != WALL)
                                {
                                    if (ExtendWallDemo(x + 1, y))
                                    {
                                        Maze[y, x + 1] = SENTINEL;
                                        return true;
                                    }
                                    else
                                    {
                                        Maze[y, x + 1] = ROAD;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        EastFlag = true;
                        break;

                    case (SOUTH):
                        if (Maze[y + 1, x] != SENTINEL)
                        {
                            if (x % 2 == 0 || (y + 1) % 2 == 0)
                            {
                                if (Maze[y + 1, x] != WALL && Maze[y + 2, x] != WALL)
                                {
                                    if (ExtendWallDemo(x, y + 1))
                                    {
                                        Maze[y + 1, x] = SENTINEL;
                                        return true;
                                    }
                                    else
                                    {
                                        Maze[y + 1, x] = ROAD;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        SouthFlag = true;
                        break;

                    case (WEST):
                        if (Maze[y, x - 1] != SENTINEL)
                        {
                            if ((x - 1) % 2 == 0 || y % 2 == 0)
                            {
                                if (Maze[y, x - 1] != WALL && Maze[y, x - 2] != WALL)
                                {
                                    if (ExtendWallDemo(x - 1, y))
                                    {
                                        Maze[y, x - 1] = SENTINEL;
                                        return true;
                                    }
                                    else
                                    {
                                        Maze[y, x - 1] = ROAD;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        WestFlag = true;
                        break;
                }

                if (cts.IsCancellationRequested)
                {
                    return true;
                }
            }
            while (!(NorthFlag && EastFlag && SouthFlag && WestFlag));

            return false;
        }

        private void ClusterMethodDemo() // クラスタリング法
        {
            int x;
            int y;
            int[,] ClusterID = new int[MAZE_H_NUM / 2, MAZE_W_NUM / 2];

            for (int j = 0; j < ClusterID.GetLength(0); j++)
            {
                for (int i = 0; i < ClusterID.GetLength(1); i++)
                {
                    ClusterID[j, i] = j * ClusterID.GetLength(1) + i;
                }
            }

            do
            {
                x = rnd.Next(1, MAZE_W_NUM - 1);
                y = rnd.Next(1, MAZE_H_NUM - 1);

                if (CanClusterDemo(x, y, ClusterID))
                {
                    Maze[y, x] = ROAD;
                    ClusterDemo(x, y, ClusterID);

                    UpdatePictureBox();
                }

                if (cts.IsCancellationRequested)
                {
                    return;
                }
            }
            while (!EndOfClusterDemo(ClusterID));
        }

        private void ClusterDemo(int x2, int y2, int[,] ClusterID) // 接続された道をクラスタリングするメソッド
        {
            int x1 = x2 / 2;
            int y1 = y2 / 2;
            int NewClusterID = 0;
            int OldClusterID = 0;

            if (x2 % 2 == 1 && y2 % 2 == 0)
            {
                if (ClusterID[y1, x1] > ClusterID[y1 - 1, x1])
                {
                    NewClusterID = ClusterID[y1 - 1, x1];
                    OldClusterID = ClusterID[y1, x1];
                }
                else
                {
                    NewClusterID = ClusterID[y1, x1];
                    OldClusterID = ClusterID[y1 - 1, x1];
                }
            }
            else if (x2 % 2 == 0 && y2 % 2 == 1)
            {
                if (ClusterID[y1, x1] > ClusterID[y1, x1 - 1])
                {
                    NewClusterID = ClusterID[y1, x1 - 1];
                    OldClusterID = ClusterID[y1, x1];
                }
                else
                {
                    NewClusterID = ClusterID[y1, x1];
                    OldClusterID = ClusterID[y1, x1 - 1];
                }
            }

            for (int j = 0; j < ClusterID.GetLength(0); j++)
            {
                for (int i = 0; i < ClusterID.GetLength(1); i++)
                {
                    if (ClusterID[j, i] == OldClusterID)
                    {
                        ClusterID[j, i] = NewClusterID;
                    }
                }
            }
        }

        private bool CanClusterDemo(int x2, int y2, int[,] ClusterID) // 接続される道が同じクラスターでないか調べるメソッド
        {
            int x1 = x2 / 2;
            int y1 = y2 / 2;

            if (x2 % 2 == 1 && y2 % 2 == 0)
            {
                if (ClusterID[y1, x1] != ClusterID[y1 - 1, x1])
                {
                    return true;
                }
            }
            else if (x2 % 2 == 0 && y2 % 2 == 1)
            {
                if (ClusterID[y1, x1] != ClusterID[y1, x1 - 1])
                {
                    return true;
                }
            }

            return false;
        }

        private bool EndOfClusterDemo(int[,] ClusterID) // すべての道が同じクラスターか調べるメソッド
        {
            for (int y = 0; y < ClusterID.GetLength(0); y++)
            {
                for (int x = 0; x < ClusterID.GetLength(1); x++)
                {
                    if (ClusterID[y, x] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int[] MakeStartGoalPointDemo() // スタート地点とゴール地点を生成するメソッド
        {
            int direction;          // 方向
            int sx = 0;             // スタート地点x座標
            int sy = 0;             // スタート地点y座標
            int gx = 0;             // ゴール地点x座標
            int gy = 0;             // ゴール地点y座標

            direction = rnd.Next(1, 5);

            switch (direction)
            {
                case (NORTH):
                    do
                    {
                        sx = rnd.Next(1, MAZE_W_NUM - 1);
                        sy = 0;
                        gx = rnd.Next(1, MAZE_W_NUM - 1);
                        gy = MAZE_H_NUM - 1;
                    }
                    while (Maze[sy + 1, sx] != ROAD || Maze[gy - 1, gx] != ROAD);
                    break;

                case (EAST):
                    do
                    {
                        sx = MAZE_W_NUM - 1;
                        sy = rnd.Next(1, MAZE_H_NUM - 1);
                        gx = 0;
                        gy = rnd.Next(1, MAZE_H_NUM - 1);
                    }
                    while (Maze[sy, sx - 1] != ROAD || Maze[gy, gx + 1] != ROAD);
                    break;

                case (SOUTH):
                    do
                    {
                        sx = rnd.Next(1, MAZE_W_NUM - 1);
                        sy = MAZE_H_NUM - 1;
                        gx = rnd.Next(1, MAZE_W_NUM - 1);
                        gy = 0;
                    }
                    while (Maze[sy - 1, sx] != ROAD || Maze[gy + 1, gx] != ROAD);
                    break;

                case (WEST):
                    do
                    {
                        sx = 0;
                        sy = rnd.Next(1, MAZE_H_NUM - 1);
                        gx = MAZE_W_NUM - 1;
                        gy = rnd.Next(1, MAZE_H_NUM - 1);
                    }
                    while (Maze[sy, sx + 1] != ROAD || Maze[gy, gx - 1] != ROAD);
                    break;
            }
            Maze[sy, sx] = ROAD;
            Maze[gy, gx] = ROAD;

            return new int[] { sx, sy, gx, gy };
        }

        private bool DisplayAnwserRouteDemo(int sx, int sy, int gx, int gy) // 正解ルートを表示するメソッド
        {
            int direction;          // 方向
            bool NorthFlag = false; // 北を調べたか判定
            bool EastFlag = false;  // 東を調べたか判定
            bool SouthFlag = false; // 南を調べたか判定
            bool WestFlag = false;  // 西を調べたか判定

            Maze[sy, sx] = MARK; // 目印をつける

            if (sx == gx && sy == gy)
            {
                return true;
            }

            UpdatePictureBox();

            do
            {
                direction = rnd.Next(1, 5);

                switch (direction)
                {
                    case (NORTH):
                        if (!NorthFlag)
                        {
                            if (0 <= sy - 1 && Maze[sy - 1, sx] == ROAD)
                            {
                                if (DisplayAnwserRouteDemo(sx, sy - 1, gx, gy))
                                {
                                    return true;
                                }
                                else
                                {
                                    Maze[sy - 1, sx] = ROAD;
                                }
                            }
                        }
                        NorthFlag = true;
                        break;

                    case (EAST):
                        if (!EastFlag)
                        {
                            if (sx + 1 <= MAZE_W_NUM - 1 && Maze[sy, sx + 1] == ROAD)
                            {
                                if (DisplayAnwserRouteDemo(sx + 1, sy, gx, gy))
                                {
                                    return true;
                                }
                                else
                                {
                                    Maze[sy, sx + 1] = ROAD;
                                }
                            }
                        }
                        EastFlag = true;
                        break;

                    case (SOUTH):
                        if (!SouthFlag)
                        {
                            if (sy + 1 <= MAZE_H_NUM - 1 && Maze[sy + 1, sx] == ROAD)
                            {
                                if (DisplayAnwserRouteDemo(sx, sy + 1, gx, gy))
                                {
                                    return true;
                                }
                                else
                                {
                                    Maze[sy + 1, sx] = ROAD;
                                }
                            }
                        }
                        SouthFlag = true;
                        break;

                    case (WEST):
                        if (!WestFlag)
                        {
                            if (0 <= sx - 1 && Maze[sy, sx - 1] == ROAD)
                            {
                                if (DisplayAnwserRouteDemo(sx - 1, sy, gx, gy))
                                {
                                    return true;
                                }
                                else
                                {
                                    Maze[sy, sx - 1] = ROAD;
                                }
                            }
                        }
                        WestFlag = true;
                        break;
                }

                if (cts.IsCancellationRequested)
                {
                    return true;
                }
            }
            while (!(NorthFlag && EastFlag && SouthFlag && WestFlag));

            UpdatePictureBox();

            return false;
        }

        private void UpdatePictureBox() // 迷路の生成過程を見せるためにPictureBoxを更新するメソッド
        {
            if (WaitTime > 0)
            {
                if (pictureBox1.InvokeRequired)
                {
                    pictureBox1.Invoke(new Action(pictureBox1.Refresh));
                }
                else
                {
                    pictureBox1.Refresh();
                }

                Thread.Sleep(WaitTime);
            }
        }

        //-----------以下、迷路の生成過程を見せない実使用版-----------//

        private void PullBarMethod() // 棒倒し法
        {
            int direction;
            bool Flag = false;

            for (int y = 2; y < MAZE_H_NUM - 1; y += 2)
            {
                for (int x = 2; x < MAZE_W_NUM - 1; x += 2)
                {
                    do
                    {
                        direction = rnd.Next(1, 5);

                        switch (direction)
                        {
                            case (NORTH):
                                if (Maze[y - 1, x] != WALL && y == 2)
                                {
                                    Maze[y - 1, x] = WALL;
                                    Flag = true;
                                }
                                break;

                            case (EAST):
                                if (Maze[y, x + 1] != WALL)
                                {
                                    Maze[y, x + 1] = WALL;
                                    Flag = true;
                                }
                                break;

                            case (SOUTH):
                                if (Maze[y + 1, x] != WALL)
                                {
                                    Maze[y + 1, x] = WALL;
                                    Flag = true;
                                }
                                break;

                            case (WEST):
                                if (Maze[y, x - 1] != WALL)
                                {
                                    Maze[y, x - 1] = WALL;
                                    Flag = true;
                                }
                                break;
                        }
                    }
                    while (!Flag);
                    Flag = false;
                }
            }
        }

        private void RecursiveDigMethod()  // 穴掘り法(道のばし法)(完全再帰Ver)
        {
            int x = rnd.Next(2, MAZE_W_NUM - 1);
            int y = rnd.Next(2, MAZE_H_NUM - 1);

            x = (x % 2 == 0) ? x - 1 : x;
            y = (y % 2 == 0) ? y - 1 : y;

            RecursiveDig(x, y);
        }

        private void RecursiveDig(int x, int y) // 穴を掘るメソッド(完全に再帰的に掘る)
        {
            int direction;          // 方向
            bool NorthFlag = false; // 北を調べたか判定
            bool EastFlag = false;  // 東を調べたか判定
            bool SouthFlag = false; // 南を調べたか判定
            bool WestFlag = false;  // 西を調べたか判定

            Maze[y, x] = ROAD; // 穴を掘って道にする

            do
            {
                direction = rnd.Next(1, 5);

                switch (direction)
                {
                    case (NORTH):
                        if (Maze[y - 1, x] != SENTINEL && (x % 2 != 0 || (y - 1) % 2 != 0))
                        {
                            if (Maze[y - 1, x] != ROAD && Maze[y - 2, x] != ROAD)
                            {
                                RecursiveDig(x, y - 1);
                            }
                        }
                        NorthFlag = true;
                        break;

                    case (EAST):
                        if (Maze[y, x + 1] != SENTINEL && ((x + 1) % 2 != 0 || y % 2 != 0))
                        {
                            if (Maze[y, x + 1] != ROAD && Maze[y, x + 2] != ROAD)
                            {
                                RecursiveDig(x + 1, y);
                            }
                        }
                        EastFlag = true;
                        break;

                    case (SOUTH):
                        if (Maze[y + 1, x] != SENTINEL && (x % 2 != 0 || (y + 1) % 2 != 0))
                        {
                            if (Maze[y + 1, x] != ROAD && Maze[y + 2, x] != ROAD)
                            {
                                RecursiveDig(x, y + 1);
                            }
                        }
                        SouthFlag = true;
                        break;

                    case (WEST):
                        if (Maze[y, x - 1] != SENTINEL && ((x - 1) % 2 != 0 || y % 2 != 0))
                        {
                            if (Maze[y, x - 1] != ROAD && Maze[y, x - 2] != ROAD)
                            {
                                RecursiveDig(x - 1, y);
                            }
                        }
                        WestFlag = true;
                        break;
                }
            }
            while (!(NorthFlag && EastFlag && SouthFlag && WestFlag));
        }

        private void DigMethod() // 穴掘り法(道のばし法)(部分再帰Ver)
        {
            int x = rnd.Next(2, MAZE_W_NUM - 1);
            int y = rnd.Next(2, MAZE_H_NUM - 1);

            x = (x % 2 == 0) ? x - 1 : x;
            y = (y % 2 == 0) ? y - 1 : y;

            Dig(x, y);

            do
            {
                x = rnd.Next(2, MAZE_W_NUM - 1);
                y = rnd.Next(2, MAZE_H_NUM - 1);

                x = (x % 2 == 0) ? x - 1 : x;
                y = (y % 2 == 0) ? y - 1 : y;

                if (CanDig(x, y))
                {
                    Dig(x, y);
                }
            }
            while (SearchCanDig());
        }

        private bool CanDig(int x, int y) // 指定された開始場所から穴が掘れるか調べるメソッド
        {
            if (Maze[y, x] == ROAD)
            {
                for (int j = y - 2; j <= y + 2; j += 2)
                {
                    for (int i = x - 2; i <= x + 2; i += 2)
                    {
                        if ((0 <= j && j <= MAZE_H_NUM - 1) && (0 <= i && i <= MAZE_W_NUM - 1))
                        {
                            if (Maze[j, i] == ROAD)
                            {
                                continue;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool SearchCanDig() // 穴を掘れる開始場所が存在するか調べるメソッド
        {
            for (int y = 1; y < MAZE_H_NUM - 1; y += 2)
            {
                for (int x = 1; x < MAZE_W_NUM - 1; x += 2)
                {
                    if (Maze[y, x] == ROAD)
                    {
                        for (int j = y - 2; j <= y + 2; j += 2)
                        {
                            for (int i = x - 2; i <= x + 2; i += 2)
                            {
                                if ((0 <= j && j <= MAZE_H_NUM - 1) && (0 <= i && i <= MAZE_W_NUM - 1))
                                {
                                    if (Maze[j, i] == ROAD)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void Dig(int x, int y) // 穴を掘るメソッド(部分的に再帰的に掘る)
        {
            int direction;          // 方向
            bool NorthFlag = false; // 北を調べたか判定
            bool EastFlag = false;  // 東を調べたか判定
            bool SouthFlag = false; // 南を調べたか判定
            bool WestFlag = false;  // 西を調べたか判定

            Maze[y, x] = ROAD; // 穴を掘って道にする

            do
            {
                direction = rnd.Next(1, 5);

                switch (direction)
                {
                    case (NORTH):
                        if (Maze[y - 1, x] != SENTINEL && (x % 2 != 0 || (y - 1) % 2 != 0))
                        {
                            if (Maze[y - 1, x] != ROAD && Maze[y - 2, x] != ROAD)
                            {
                                Dig(x, y - 1);
                                return;
                            }
                        }
                        NorthFlag = true;
                        break;

                    case (EAST):
                        if (Maze[y, x + 1] != SENTINEL && ((x + 1) % 2 != 0 || y % 2 != 0))
                        {
                            if (Maze[y, x + 1] != ROAD && Maze[y, x + 2] != ROAD)
                            {
                                Dig(x + 1, y);
                                return;
                            }
                        }
                        EastFlag = true;
                        break;

                    case (SOUTH):
                        if (Maze[y + 1, x] != SENTINEL && (x % 2 != 0 || (y + 1) % 2 != 0))
                        {
                            if (Maze[y + 1, x] != ROAD && Maze[y + 2, x] != ROAD)
                            {
                                Dig(x, y + 1);
                                return;
                            }
                        }
                        SouthFlag = true;
                        break;

                    case (WEST):
                        if (Maze[y, x - 1] != SENTINEL && ((x - 1) % 2 != 0 || y % 2 != 0))
                        {
                            if (Maze[y, x - 1] != ROAD && Maze[y, x - 2] != ROAD)
                            {
                                Dig(x - 1, y);
                                return;
                            }
                        }
                        WestFlag = true;
                        break;
                }
            }
            while (!(NorthFlag && EastFlag && SouthFlag && WestFlag));
        }

        private void ExtendWallMethod() // 壁のばし法
        {
            int x;
            int y;

            do
            {
                x = rnd.Next(2, MAZE_W_NUM - 1);
                y = rnd.Next(2, MAZE_H_NUM - 1);

                x = (x % 2 == 0) ? x : x - 1;
                y = (y % 2 == 0) ? y : y - 1;

                if (Maze[y, x] != SENTINEL)
                {
                    if (ExtendWall(x, y))
                    {
                        Maze[y, x] = SENTINEL;
                    }
                    else
                    {
                        Maze[y, x] = ROAD;
                    }
                }
            }
            while (SearchCanPutWall());
        }

        private bool SearchCanPutWall() // 壁を置ける場所が存在するか調べるメソッド
        {
            for (int y = 2; y < MAZE_H_NUM - 1; y += 2)
            {
                for (int x = 2; x < MAZE_W_NUM - 1; x += 2)
                {
                    if (Maze[y, x] == ROAD)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ExtendWall(int x, int y) // 壁をのばすメソッド
        {
            int direction;          // 方向
            bool NorthFlag = false; // 北を調べたか判定
            bool EastFlag = false;  // 東を調べたか判定
            bool SouthFlag = false; // 南を調べたか判定
            bool WestFlag = false;  // 西を調べたか判定

            Maze[y, x] = WALL; // 壁を伸ばす

            do
            {
                direction = rnd.Next(1, 5);

                switch (direction)
                {
                    case (NORTH):
                        if (Maze[y - 1, x] != SENTINEL)
                        {
                            if (x % 2 == 0 || (y - 1) % 2 == 0)
                            {
                                if (Maze[y - 1, x] != WALL && Maze[y - 2, x] != WALL)
                                {
                                    if (ExtendWall(x, y - 1))
                                    {
                                        Maze[y - 1, x] = SENTINEL;
                                        return true;
                                    }
                                    else
                                    {
                                        Maze[y - 1, x] = ROAD;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        NorthFlag = true;
                        break;

                    case (EAST):
                        if (Maze[y, x + 1] != SENTINEL)
                        {
                            if ((x + 1) % 2 == 0 || y % 2 == 0)
                            {
                                if (Maze[y, x + 1] != WALL && Maze[y, x + 2] != WALL)
                                {
                                    if (ExtendWall(x + 1, y))
                                    {
                                        Maze[y, x + 1] = SENTINEL;
                                        return true;
                                    }
                                    else
                                    {
                                        Maze[y, x + 1] = ROAD;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        EastFlag = true;
                        break;

                    case (SOUTH):
                        if (Maze[y + 1, x] != SENTINEL)
                        {
                            if (x % 2 == 0 || (y + 1) % 2 == 0)
                            {
                                if (Maze[y + 1, x] != WALL && Maze[y + 2, x] != WALL)
                                {
                                    if (ExtendWall(x, y + 1))
                                    {
                                        Maze[y + 1, x] = SENTINEL;
                                        return true;
                                    }
                                    else
                                    {
                                        Maze[y + 1, x] = ROAD;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        SouthFlag = true;
                        break;

                    case (WEST):
                        if (Maze[y, x - 1] != SENTINEL)
                        {
                            if ((x - 1) % 2 == 0 || y % 2 == 0)
                            {
                                if (Maze[y, x - 1] != WALL && Maze[y, x - 2] != WALL)
                                {
                                    if (ExtendWall(x - 1, y))
                                    {
                                        Maze[y, x - 1] = SENTINEL;
                                        return true;
                                    }
                                    else
                                    {
                                        Maze[y, x - 1] = ROAD;
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        WestFlag = true;
                        break;
                }
            }
            while (!(NorthFlag && EastFlag && SouthFlag && WestFlag));

            return false;
        }

        private void ClusterMethod() // クラスタリング法
        {
            int x;
            int y;
            int[,] ClusterID = new int[MAZE_H_NUM / 2, MAZE_W_NUM / 2];

            for (int j = 0; j < ClusterID.GetLength(0); j++)
            {
                for (int i = 0; i < ClusterID.GetLength(1); i++)
                {
                    ClusterID[j, i] = j * ClusterID.GetLength(1) + i;
                }
            }

            do
            {
                x = rnd.Next(1, MAZE_W_NUM - 1);
                y = rnd.Next(1, MAZE_H_NUM - 1);

                if (CanCluster(x, y, ClusterID))
                {
                    Maze[y, x] = ROAD;
                    Cluster(x, y, ClusterID);
                }
            }
            while (!EndOfCluster(ClusterID));
        }

        private void Cluster(int x2, int y2, int[,] ClusterID) // 接続された道をクラスタリングするメソッド
        {
            int x1 = x2 / 2;
            int y1 = y2 / 2;
            int NewClusterID = 0;
            int OldClusterID = 0;

            if (x2 % 2 == 1 && y2 % 2 == 0)
            {
                if (ClusterID[y1, x1] > ClusterID[y1 - 1, x1])
                {
                    NewClusterID = ClusterID[y1 - 1, x1];
                    OldClusterID = ClusterID[y1, x1];
                }
                else
                {
                    NewClusterID = ClusterID[y1, x1];
                    OldClusterID = ClusterID[y1 - 1, x1];
                }
            }
            else if (x2 % 2 == 0 && y2 % 2 == 1)
            {
                if (ClusterID[y1, x1] > ClusterID[y1, x1 - 1])
                {
                    NewClusterID = ClusterID[y1, x1 - 1];
                    OldClusterID = ClusterID[y1, x1];
                }
                else
                {
                    NewClusterID = ClusterID[y1, x1];
                    OldClusterID = ClusterID[y1, x1 - 1];
                }
            }

            for (int j = 0; j < ClusterID.GetLength(0); j++)
            {
                for (int i = 0; i < ClusterID.GetLength(1); i++)
                {
                    if (ClusterID[j, i] == OldClusterID)
                    {
                        ClusterID[j, i] = NewClusterID;
                    }
                }
            }
        }

        private bool CanCluster(int x2, int y2, int[,] ClusterID) // 接続される道が同じクラスターでないか調べるメソッド
        {
            int x1 = x2 / 2;
            int y1 = y2 / 2;

            if (x2 % 2 == 1 && y2 % 2 == 0)
            {
                if (ClusterID[y1, x1] != ClusterID[y1 - 1, x1])
                {
                    return true;
                }
            }
            else if (x2 % 2 == 0 && y2 % 2 == 1)
            {
                if (ClusterID[y1, x1] != ClusterID[y1, x1 - 1])
                {
                    return true;
                }
            }

            return false;
        }

        private bool EndOfCluster(int[,] ClusterID) // すべての道が同じクラスターか調べるメソッド
        {
            for (int y = 0; y < ClusterID.GetLength(0); y++)
            {
                for (int x = 0; x < ClusterID.GetLength(1); x++)
                {
                    if (ClusterID[y, x] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int[] MakeStartGoalPoint() // スタート地点とゴール地点を生成するメソッド
        {
            int direction;          // 方向
            int sx = 0;             // スタート地点x座標
            int sy = 0;             // スタート地点y座標
            int gx = 0;             // ゴール地点x座標
            int gy = 0;             // ゴール地点y座標

            direction = rnd.Next(1, 5);

            switch (direction)
            {
                case (NORTH):
                    do
                    {
                        sx = rnd.Next(1, MAZE_W_NUM - 1);
                        sy = 0;
                        gx = rnd.Next(1, MAZE_W_NUM - 1);
                        gy = MAZE_H_NUM - 1;
                    }
                    while (Maze[sy + 1, sx] != ROAD || Maze[gy - 1, gx] != ROAD);
                    break;

                case (EAST):
                    do
                    {
                        sx = MAZE_W_NUM - 1;
                        sy = rnd.Next(1, MAZE_H_NUM - 1);
                        gx = 0;
                        gy = rnd.Next(1, MAZE_H_NUM - 1);
                    }
                    while (Maze[sy, sx - 1] != ROAD || Maze[gy, gx + 1] != ROAD);
                    break;

                case (SOUTH):
                    do
                    {
                        sx = rnd.Next(1, MAZE_W_NUM - 1);
                        sy = MAZE_H_NUM - 1;
                        gx = rnd.Next(1, MAZE_W_NUM - 1);
                        gy = 0;
                    }
                    while (Maze[sy - 1, sx] != ROAD || Maze[gy + 1, gx] != ROAD);
                    break;

                case (WEST):
                    do
                    {
                        sx = 0;
                        sy = rnd.Next(1, MAZE_H_NUM - 1);
                        gx = MAZE_W_NUM - 1;
                        gy = rnd.Next(1, MAZE_H_NUM - 1);
                    }
                    while (Maze[sy, sx + 1] != ROAD || Maze[gy, gx - 1] != ROAD);
                    break;
            }
            Maze[sy, sx] = ROAD;
            Maze[gy, gx] = ROAD;

            return new int[] { sx, sy, gx, gy };
        }

        private bool DisplayAnwserRoute(int sx, int sy, int gx, int gy) // 正解ルートを表示するメソッド
        {
            int direction;          // 方向
            bool NorthFlag = false; // 北を調べたか判定
            bool EastFlag = false;  // 東を調べたか判定
            bool SouthFlag = false; // 南を調べたか判定
            bool WestFlag = false;  // 西を調べたか判定

            Maze[sy, sx] = MARK; // 目印をつける

            if (sx == gx && sy == gy)
            {
                return true;
            }

            do
            {
                direction = rnd.Next(1, 5);

                switch (direction)
                {
                    case (NORTH):
                        if (!NorthFlag)
                        {
                            if (0 <= sy - 1 && Maze[sy - 1, sx] == ROAD)
                            {
                                if (DisplayAnwserRoute(sx, sy - 1, gx, gy))
                                {
                                    return true;
                                }
                                else
                                {
                                    Maze[sy - 1, sx] = ROAD;
                                }
                            }
                        }
                        NorthFlag = true;
                        break;

                    case (EAST):
                        if (!EastFlag)
                        {
                            if (sx + 1 <= MAZE_W_NUM - 1 && Maze[sy, sx + 1] == ROAD)
                            {
                                if (DisplayAnwserRoute(sx + 1, sy, gx, gy))
                                {
                                    return true;
                                }
                                else
                                {
                                    Maze[sy, sx + 1] = ROAD;
                                }
                            }
                        }
                        EastFlag = true;
                        break;

                    case (SOUTH):
                        if (!SouthFlag)
                        {
                            if (sy + 1 <= MAZE_H_NUM - 1 && Maze[sy + 1, sx] == ROAD)
                            {
                                if (DisplayAnwserRoute(sx, sy + 1, gx, gy))
                                {
                                    return true;
                                }
                                else
                                {
                                    Maze[sy + 1, sx] = ROAD;
                                }
                            }
                        }
                        SouthFlag = true;
                        break;

                    case (WEST):
                        if (!WestFlag)
                        {
                            if (0 <= sx - 1 && Maze[sy, sx - 1] == ROAD)
                            {
                                if (DisplayAnwserRoute(sx - 1, sy, gx, gy))
                                {
                                    return true;
                                }
                                else
                                {
                                    Maze[sy, sx - 1] = ROAD;
                                }
                            }
                        }
                        WestFlag = true;
                        break;
                }
            }
            while (!(NorthFlag && EastFlag && SouthFlag && WestFlag));

            return false;
        }
    }
}
