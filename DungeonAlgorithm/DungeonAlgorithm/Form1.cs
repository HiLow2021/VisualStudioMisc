using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonAlgorithm
{
    public partial class Form1 : Form
    {
        enum ColorNum
        {
            Red = 100,
            Orange,
            Yellow,
            Green,
            Blue,
            Purple,
            Black,
            White
        }
        
        const int SENTINEL = -2;        // 外壁(番兵)
        const int WALL = -1;            // 内壁
        const int ROAD = 0;             // 道
        const int ROOM = 1;             // 部屋
        const int ROOM_DOOR = 2;        // 部屋の扉
        const int MARK = 10;            // 目印
        float cellWidth;                  // 一マスの横幅
        float cellHeight;                 // 一マスの縦幅

        Floor f = new Floor(100, 100);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            cellWidth = (float)pictureBox1.Width / f.Size.GetLength(1);
            cellHeight = (float)pictureBox1.Height / f.Size.GetLength(0);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (f != null)
            {
                for (int y = 0; y < f.Size.GetLength(0); y++)
                {
                    for (int x = 0; x < f.Size.GetLength(1); x++)
                    {
                        if (f.Size[y, x] == SENTINEL)
                        {
                            e.Graphics.FillRectangle(Brushes.Black, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == WALL)
                        {
                            e.Graphics.FillRectangle(Brushes.Brown, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == ROAD)
                        {
                            e.Graphics.FillRectangle(Brushes.White, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == ROOM)
                        {
                            e.Graphics.FillRectangle(Brushes.LimeGreen, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == ROOM_DOOR)
                        {
                            e.Graphics.FillRectangle(Brushes.Yellow, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == MARK)
                        {
                            e.Graphics.FillRectangle(Brushes.Red, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == (int)ColorNum.Red)
                        {
                            e.Graphics.FillRectangle(Brushes.Red, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == (int)ColorNum.Orange)
                        {
                            e.Graphics.FillRectangle(Brushes.Orange, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == (int)ColorNum.Yellow)
                        {
                            e.Graphics.FillRectangle(Brushes.Yellow, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == (int)ColorNum.Green)
                        {
                            e.Graphics.FillRectangle(Brushes.Green, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == (int)ColorNum.Blue)
                        {
                            e.Graphics.FillRectangle(Brushes.Blue, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == (int)ColorNum.Purple)
                        {
                            e.Graphics.FillRectangle(Brushes.Purple, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == (int)ColorNum.Black)
                        {
                            e.Graphics.FillRectangle(Brushes.Black, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (f.Size[y, x] == (int)ColorNum.White)
                        {
                            e.Graphics.FillRectangle(Brushes.White, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f = new Floor(100, 100);

            cellWidth = (float)pictureBox1.Width / f.Size.GetLength(1);
            cellHeight = (float)pictureBox1.Height / f.Size.GetLength(0);
            
            RandomFloor();
            
            pictureBox1.Refresh();
        }

        public void RandomFloor()
        {
            Random rnd = new Random();
            int ID = 0;
            int type = 1;
            bool flag = rnd.Next() % 2 == 0;

            f.SplitDivision(0, type, flag);
            f.SplitDivision(0, type, !flag);
            f.SplitDivision(2, type, !flag);

            for (int i = 0; i < 20; i++)
            {
                ID = rnd.Next(0, f.DivList.Count + 1);
                flag = !flag;

                f.SplitDivision(ID, type, flag);
            }

            f.CreateRooms();

            while (!f.CheckRoomAccess())
            {
                f.ConnectRooms(rnd.Next(0, f.DivList.Count + 1), rnd.Next(0, f.DivList.Count + 1));
            }

            f.FillFloor(WALL);
            f.BorderFloor(SENTINEL);

            f.FillDivision(0, (int)ColorNum.Red);
            f.FillDivision(1, (int)ColorNum.Orange);
            f.FillDivision(2, (int)ColorNum.Blue);
            f.FillDivision(3, (int)ColorNum.Purple);

            f.FillRooms(ROOM);
            f.FillAllRoads(ROAD);
        }

        public class Floor
        {
            public class Division
            {
                public class Room
                {
                    public int Left { get; set; }
                    public int Top { get; set; }
                    public int Width { get; set; }
                    public int Height { get; set; }

                    public int Right
                    {
                        get { return Left + Width; }
                    }

                    public int Bottom
                    {
                        get { return Top + Height; }
                    }

                    public int Measure
                    {
                        get { return Width * Height; }
                    }

                    public int ClusterID = 0;

                    public Room()
                    {
                        Left = 0;
                        Top = 0;
                        Width = 0;
                        Height = 0;
                    }

                    public Room(int Left, int Top, int Width, int Height)
                    {
                        this.Left = Left;
                        this.Top = Top;
                        this.Width = Width;
                        this.Height = Height;
                    }
                }

                public class Road
                {
                    public int Left { get; set; }
                    public int Top { get; set; }
                    public int Width { get; set; }
                    public int Height { get; set; }

                    public int Right
                    {
                        get { return Left + Width; }
                    }

                    public int Bottom
                    {
                        get { return Top + Height; }
                    }

                    public int Measure
                    {
                        get { return Width * Height; }
                    }

                    public Road()
                    {
                        Left = 0;
                        Top = 0;
                        Width = 0;
                        Height = 0;
                    }

                    public Road(int Left, int Top, int Width, int Height)
                    {
                        this.Left = Left;
                        this.Top = Top;
                        this.Width = Width;
                        this.Height = Height;
                    }
                }

                public int Left { get; set; }
                public int Top { get; set; }
                public int Width { get; set; }
                public int Height { get; set; }

                public int Right
                {
                    get { return Left + Width; }
                }

                public int Bottom
                {
                    get { return Top + Height; }
                }

                public int Measure
                {
                    get { return Width * Height; }
                }

                public int MaxRoomSize = 30;
                public int MinRoomSize = 8;
                public int RoomMergin = 3;
                public Floor Parent = null;
                public Room InnerRoom = null;
                public Road TopRoad = null;
                public Road RightRoad = null;
                public Road BottomRoad = null;
                public Road LeftRoad = null;

                Random rnd = null;

                public Division(Floor Parent)
                {
                    Left = 0;
                    Top = 0;
                    Width = 0;
                    Height = 0;
                    this.Parent = Parent;
                    rnd = new Random(Environment.TickCount + this.Parent.DivList.Count);
                }

                public Division(Floor Parent, int Left, int Top, int Width, int Height)
                {
                    this.Left = Left;
                    this.Top = Top;
                    this.Width = Width;
                    this.Height = Height;
                    this.Parent = Parent;
                    rnd = new Random(Environment.TickCount + this.Parent.DivList.Count);
                }

                public void CreateRoad(int Direction, int Left, int Top, int Width, int Height)
                {
                    switch (Direction)
                    {
                        case (0):
                            TopRoad = new Road(Left, Top, Width, Height);
                            break;

                        case (1):
                            RightRoad = new Road(Left, Top, Width, Height);
                            break;

                        case (2):
                            BottomRoad = new Road(Left, Top, Width, Height);
                            break;

                        case (3):
                            LeftRoad = new Road(Left, Top, Width, Height);
                            break;
                    }
                }

                public void CreateRoom()
                {
                    int MaxWidth = Width - RoomMergin * 2;
                    int MaxHeight = Height - RoomMergin * 2;

                    if (MaxWidth < MinRoomSize || MaxHeight < MinRoomSize)
                    {
                        return;
                    }

                    int RoomLeft;
                    int RoomTop;
                    int RoomWidth;
                    int RoomHeight;
                    int LeftPadding, TopPadding;

                    RoomWidth = rnd.Next(MinRoomSize, MaxWidth + 1);
                    RoomHeight = rnd.Next(MinRoomSize, MaxHeight + 1);

                    RoomWidth = (RoomWidth > MaxRoomSize) ? MaxRoomSize : RoomWidth;
                    RoomHeight = (RoomHeight > MaxRoomSize) ? MaxRoomSize : RoomHeight;

                    LeftPadding = MaxWidth - RoomWidth;
                    TopPadding = MaxHeight - RoomHeight;

                    RoomLeft = Left + rnd.Next(0, LeftPadding + 1) + RoomMergin;
                    RoomTop = Top + rnd.Next(0, TopPadding + 1) + RoomMergin;

                    InnerRoom = new Room(RoomLeft, RoomTop, RoomWidth, RoomHeight);
                    InnerRoom.ClusterID = Parent.RoomCount++;
                }
                
                public void FillRoad(int Direction, int Chip)
                {
                    int Left = 0;
                    int Top = 0;
                    int Right = 0;
                    int Bottom = 0;

                    switch (Direction)
                    {
                        case (0):
                            if (TopRoad != null)
                            {
                                Left = TopRoad.Left;
                                Top = TopRoad.Top;
                                Right = TopRoad.Right;
                                Bottom = TopRoad.Bottom;
                            }
                            break;

                        case (1):
                            if (RightRoad != null)
                            {
                                Left = RightRoad.Left;
                                Top = RightRoad.Top;
                                Right = RightRoad.Right;
                                Bottom = RightRoad.Bottom;
                            }
                            break;

                        case (2):
                            if (BottomRoad != null)
                            {
                                Left = BottomRoad.Left;
                                Top = BottomRoad.Top;
                                Right = BottomRoad.Right;
                                Bottom = BottomRoad.Bottom;
                            }
                            break;

                        case (3):
                            if (LeftRoad != null)
                            {
                                Left = LeftRoad.Left;
                                Top = LeftRoad.Top;
                                Right = LeftRoad.Right;
                                Bottom = LeftRoad.Bottom;
                            }
                            break;
                    }

                    for (int y = Top; y < Bottom; y++)
                    {
                        for (int x = Left; x < Right; x++)
                        {
                            Parent.Size[y, x] = Chip;
                        }
                    }
                }

                public void FillRoom(int Chip)
                {
                    if (InnerRoom != null)
                    {
                        for (int y = InnerRoom.Top; y < InnerRoom.Bottom; y++)
                        {
                            for (int x = InnerRoom.Left; x < InnerRoom.Right; x++)
                            {
                                Parent.Size[y, x] = Chip;
                            }
                        }
                    }
                }
            }

            public string Name = null;
            public int[,] Size = null;
            public int MinDivisionSize = 15;
            public int MaxDivisionCount;
            public int MinDivisionCount;
            public int RoomCount = 0;
            public List<Division> DivList = null;
            public List<Division.Road> ConnectionRoadList = null;

            Random rnd = null;

            public Floor()
            {
                Name = string.Empty;
                Size = new int[0, 0];
                DivList = new List<Division>();
                ConnectionRoadList = new List<Division.Road>();
                
                DivList.Add(new Division(this));
                rnd = new Random();
            }

            public Floor(int Width, int Height)
            {
                Name = string.Empty;
                Size = new int[Height, Width];
                DivList = new List<Division>();
                ConnectionRoadList = new List<Division.Road>();
                
                DivList.Add(new Division(this, 0, 0, Width, Height));
                rnd = new Random();
            }

            public Floor(string Name, int Width, int Height)
            {
                this.Name = Name;
                Size = new int[Height, Width];
                DivList = new List<Division>();
                ConnectionRoadList = new List<Division.Road>();
                
                DivList.Add(new Division(this, 0, 0, Width, Height));
                rnd = new Random();
            }

            private bool CheckDivisionID(int ID)
            {
                return (0 <= ID && ID < DivList.Count);
            }

            public void CreateRoom(int ID)
            {
                if (CheckDivisionID(ID))
                {
                    DivList[ID].CreateRoom();
                }
            }

            public void CreateRooms()
            {
                foreach (Division Div in DivList)
                {
                    Div.CreateRoom();
                }
            }

            private bool CanConnectRooms(int FromID, int ToID)
            {
                if (!CheckDivisionID(FromID) || !CheckDivisionID(ToID) || DivList[FromID].InnerRoom == null || DivList[ToID].InnerRoom == null)
                {
                    return false;
                }
                if (DivList[FromID].Left > DivList[ToID].Right || DivList[FromID].Right < DivList[ToID].Left)
                {
                    return false;
                }
                if (DivList[FromID].Top > DivList[ToID].Bottom || DivList[FromID].Bottom < DivList[ToID].Top)
                {
                    return false;
                }
                if (DivList[FromID].InnerRoom.ClusterID == DivList[ToID].InnerRoom.ClusterID)
                {
                    return false;
                }

                return true;
            }

            public void ConnectRooms(int FromID, int ToID)
            {
                if (!CanConnectRooms(FromID, ToID))
                {
                    return;
                }

                if (DivList[FromID].Left == DivList[ToID].Right || DivList[FromID].Right == DivList[ToID].Left)
                {
                    int x;
                    int y1;
                    int y2;
                    int w1;
                    int w2;

                    y1 = rnd.Next(DivList[FromID].InnerRoom.Top, DivList[FromID].InnerRoom.Bottom);
                    y2 = rnd.Next(DivList[ToID].InnerRoom.Top, DivList[ToID].InnerRoom.Bottom);

                    if (DivList[FromID].Left > DivList[ToID].Left)
                    {
                        x = DivList[FromID].Left;
                        w1 = DivList[FromID].InnerRoom.Left - x;
                        w2 = x - DivList[ToID].InnerRoom.Right;

                        if (DivList[FromID].LeftRoad != null) { y1 = DivList[FromID].LeftRoad.Top; }
                        if (DivList[ToID].RightRoad != null) { y2 = DivList[ToID].RightRoad.Top; }

                        DivList[FromID].CreateRoad(3, x, y1, w1, 1);
                        DivList[ToID].CreateRoad(1, DivList[ToID].InnerRoom.Right, y2, w2 + 1, 1);
                    }
                    else
                    {
                        x = DivList[FromID].Right;
                        w1 = x - DivList[FromID].InnerRoom.Right;
                        w2 = DivList[ToID].InnerRoom.Left - x;

                        if (DivList[FromID].RightRoad != null) { y1 = DivList[FromID].RightRoad.Top; }
                        if (DivList[ToID].LeftRoad != null) { y2 = DivList[ToID].LeftRoad.Top; }

                        DivList[FromID].CreateRoad(1, DivList[FromID].InnerRoom.Right, y1, w1 + 1, 1);
                        DivList[ToID].CreateRoad(3, x, y2, w2, 1);
                    }

                    if (y1 > y2) { ConnectionRoadList.Add(new Division.Road(x, y2, 1, y1 - y2)); }
                    else { ConnectionRoadList.Add(new Division.Road(x, y1, 1, y2 - y1)); }
                }

                else if (DivList[FromID].Top == DivList[ToID].Bottom || DivList[FromID].Bottom == DivList[ToID].Top)
                {
                    int x1;
                    int x2;
                    int y;
                    int h1;
                    int h2;

                    x1 = rnd.Next(DivList[FromID].InnerRoom.Left, DivList[FromID].InnerRoom.Right);
                    x2 = rnd.Next(DivList[ToID].InnerRoom.Left, DivList[ToID].InnerRoom.Right);

                    if (DivList[FromID].Top > DivList[ToID].Top)
                    {
                        y = DivList[FromID].Top;
                        h1 = DivList[FromID].InnerRoom.Top - y;
                        h2 = y - DivList[ToID].InnerRoom.Bottom;

                        if (DivList[FromID].TopRoad != null) { x1 = DivList[FromID].TopRoad.Left; }
                        if (DivList[ToID].BottomRoad != null) { x2 = DivList[ToID].BottomRoad.Left; }

                        DivList[FromID].CreateRoad(0, x1, y, 1, h1);
                        DivList[ToID].CreateRoad(2, x2, DivList[ToID].InnerRoom.Bottom, 1, h2 + 1);
                    }
                    else
                    {
                        y = DivList[FromID].Bottom;
                        h1 = y - DivList[FromID].InnerRoom.Bottom;
                        h2 = DivList[ToID].InnerRoom.Top - y;

                        if (DivList[FromID].BottomRoad != null) { x1 = DivList[FromID].BottomRoad.Left; }
                        if (DivList[ToID].TopRoad != null) { x2 = DivList[ToID].TopRoad.Left; }

                        DivList[FromID].CreateRoad(2, x1, DivList[FromID].InnerRoom.Bottom, 1, h1 + 1);
                        DivList[ToID].CreateRoad(0, x2, y, 1, h2);
                    }

                    if (x1 > x2) { ConnectionRoadList.Add(new Division.Road(x2, y, x1 - x2, 1)); }
                    else { ConnectionRoadList.Add(new Division.Road(x1, y, x2 - x1, 1)); }
                }

                CrusterRooms(FromID, ToID);
            }

            private void CrusterRooms(int FromID, int ToID)
            {
                int NewCrusterID;
                int OldCrusterID;

                if (DivList[FromID].InnerRoom.ClusterID > DivList[ToID].InnerRoom.ClusterID)
                {
                    NewCrusterID = DivList[ToID].InnerRoom.ClusterID;
                    OldCrusterID = DivList[FromID].InnerRoom.ClusterID;
                }
                else
                {
                    NewCrusterID = DivList[FromID].InnerRoom.ClusterID;
                    OldCrusterID = DivList[ToID].InnerRoom.ClusterID;
                }

                foreach (Division Div in DivList)
                {
                    if (Div.InnerRoom != null && Div.InnerRoom.ClusterID == OldCrusterID)
                    {
                        Div.InnerRoom.ClusterID = NewCrusterID;
                    }
                }
            }

            public bool CheckRoomAccess()
            {
                foreach (Division Div in DivList)
                {
                    if (Div.InnerRoom != null && Div.InnerRoom.ClusterID != 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            private bool CanSplitDivision(int ID, bool VerticalFlag)
            {
                if (!CheckDivisionID(ID))
                {
                    return false;
                }
                if (VerticalFlag == true && DivList[ID].Width / 2 < MinDivisionSize)
                {
                    return false;
                }
                if (VerticalFlag == false && DivList[ID].Height / 2 < MinDivisionSize)
                {
                    return false;
                }

                return true;
            }

            public void SplitDivision(int ID, bool VerticalFlag)
            {
                if (!CanSplitDivision(ID, VerticalFlag))
                {
                    return;
                }

                int DivisionPoint = 0;

                if (VerticalFlag)
                {
                    DivisionPoint = DivList[ID].Width / 2;

                    Division Div = new Division(this);
                    Div.Left = DivList[ID].Left + DivisionPoint;
                    Div.Top = DivList[ID].Top;
                    Div.Width = DivList[ID].Width - DivisionPoint;
                    Div.Height = DivList[ID].Height;
                    DivList.Insert(ID + 1, Div);

                    DivList[ID].Width = DivisionPoint;
                }
                else
                {
                    DivisionPoint = DivList[ID].Height / 2;

                    Division Div = new Division(this);
                    Div.Left = DivList[ID].Left;
                    Div.Top = DivList[ID].Top + DivisionPoint;
                    Div.Width = DivList[ID].Width;
                    Div.Height = DivList[ID].Height - DivisionPoint;
                    DivList.Insert(ID + 1, Div);

                    DivList[ID].Height = DivisionPoint;
                }
            }

            public void SplitDivision(int ID, int Type, bool VerticalFlag)
            {
                if (!CanSplitDivision(ID, VerticalFlag) || Type < 0 || 1 < Type)
                {
                    return;
                }

                int DivisionPoint = 0;

                if (VerticalFlag)
                {
                    if (Type == 0) { DivisionPoint = DivList[ID].Width / 2; }
                    else if(Type == 1) { DivisionPoint = rnd.Next(MinDivisionSize, DivList[ID].Width - MinDivisionSize + 1); }

                    Division Div = new Division(this);
                    Div.Left = DivList[ID].Left + DivisionPoint;
                    Div.Top = DivList[ID].Top;
                    Div.Width = DivList[ID].Width - DivisionPoint;
                    Div.Height = DivList[ID].Height;
                    DivList.Insert(ID + 1, Div);

                    DivList[ID].Width = DivisionPoint;
                }
                else
                {
                    if (Type == 0) { DivisionPoint = DivList[ID].Height / 2; }
                    else if (Type == 1) { DivisionPoint = rnd.Next(MinDivisionSize, DivList[ID].Height - MinDivisionSize + 1); }

                    Division Div = new Division(this);
                    Div.Left = DivList[ID].Left;
                    Div.Top = DivList[ID].Top + DivisionPoint;
                    Div.Width = DivList[ID].Width;
                    Div.Height = DivList[ID].Height - DivisionPoint;
                    DivList.Insert(ID + 1, Div);

                    DivList[ID].Height = DivisionPoint;
                }
            }

            public void BorderFloor(int Chip)
            {
                for (int i = 0; i < Size.GetLength(0); i++)
                {
                    Size[i, 0] = Chip;
                    Size[i, Size.GetLength(1) - 1] = Chip;
                }
                for (int i = 0; i < Size.GetLength(1); i++)
                {
                    Size[0, i] = Chip;
                    Size[Size.GetLength(0) - 1, i] = Chip;
                }
            }

            public void FillFloor(int Chip)
            {
                for (int y = 0; y < Size.GetLength(0); y++)
                {
                    for (int x = 0; x < Size.GetLength(1); x++)
                    {
                        Size[y, x] = Chip;
                    }
                }
            }

            public void FillDivision(int ID, int Chip)
            {
                if (CheckDivisionID(ID))
                {
                    Division Div = DivList[ID];

                    for (int y = Div.Top; y < Div.Bottom; y++)
                    {
                        for (int x = Div.Left; x < Div.Right; x++)
                        {
                            Size[y, x] = Chip;
                        }
                    }
                }
            }

            public void FillDivisions(int Chip)
            {
                foreach (Division Div in DivList)
                {
                    for (int y = Div.Top; y < Div.Bottom; y++)
                    {
                        for (int x = Div.Left; x < Div.Right; x++)
                        {
                            Size[y, x] = Chip;
                        }
                    }
                }
            }

            public void FillRoom(int ID, int Chip)
            {
                if (CheckDivisionID(ID))
                {
                    DivList[ID].FillRoom(Chip);
                }
            }

            public void FillRooms(int Chip)
            {
                foreach (Division Div in DivList)
                {
                    Div.FillRoom(Chip);
                }
            }

            public void FillDivisionRoad(int ID, int Chip)
            {
                if (CheckDivisionID(ID))
                {
                    DivList[ID].FillRoad(0, Chip);
                    DivList[ID].FillRoad(1, Chip);
                    DivList[ID].FillRoad(2, Chip);
                    DivList[ID].FillRoad(3, Chip);
                }
            }

            public void FillDivisionRoads(int Chip)
            {
                foreach (Division Div in DivList)
                {
                    Div.FillRoad(0, Chip);
                    Div.FillRoad(1, Chip);
                    Div.FillRoad(2, Chip);
                    Div.FillRoad(3, Chip);
                }
            }

            public void FillConnectionRoads(int Chip)
            {
                foreach (Division.Road ConnectionRoad in ConnectionRoadList)
                {
                    for (int y = ConnectionRoad.Top; y < ConnectionRoad.Bottom; y++)
                    {
                        for (int x = ConnectionRoad.Left; x < ConnectionRoad.Right; x++)
                        {
                            Size[y, x] = Chip;
                        }
                    }
                }
            }

            public void FillAllRoads(int Chip)
            {
                FillDivisionRoads(Chip);
                FillConnectionRoads(Chip);
            }

            private void FillHLine(int Left, int Right, int y, int Chip)
            {
                if (Left > Right)
                {
                    int tmp = Left;
                    Left = Right;
                    Right = tmp;
                }

                for (int i = Left; i < Right; i++)
                {
                    Size[y, i] = Chip;
                }
            }

            private void FillVLine(int Top, int Bottom, int x, int Chip)
            {
                if (Top > Bottom)
                {
                    int tmp = Top;
                    Top = Bottom;
                    Bottom = tmp;
                }

                for (int i = Top; i < Bottom; i++)
                {
                    Size[i, x] = Chip;
                }
            }

            public void ShowDivisionInfo(int ID)
            {
                if (!CheckDivisionID(ID))
                {
                    return;
                }

                MessageBox.Show("Division" + ID.ToString() + Environment.NewLine +
                    "Left " + DivList[ID].Left.ToString() + Environment.NewLine +
                    "Top " + DivList[ID].Top.ToString() + Environment.NewLine +
                    "Right " + DivList[ID].Right.ToString() + Environment.NewLine +
                    "Bottom " + DivList[ID].Bottom.ToString() + Environment.NewLine +
                    "Width " + DivList[ID].Width.ToString() + Environment.NewLine +
                    "Height " + DivList[ID].Height.ToString());

                if (DivList[ID].InnerRoom == null)
                {
                    return;
                }
                
                MessageBox.Show("InnerRoom " + ID.ToString() + Environment.NewLine +
                    "Left " + DivList[ID].InnerRoom.Left.ToString() + Environment.NewLine +
                    "Top " + DivList[ID].InnerRoom.Top.ToString() + Environment.NewLine +
                    "Right " + DivList[ID].InnerRoom.Right.ToString() + Environment.NewLine +
                    "Bottom " + DivList[ID].InnerRoom.Bottom.ToString() + Environment.NewLine +
                    "Width " + DivList[ID].InnerRoom.Width.ToString() + Environment.NewLine +
                    "Height " + DivList[ID].InnerRoom.Height.ToString());
            }

            public void ShowDivisionInfoConsole(int ID)
            {
                if (!CheckDivisionID(ID))
                {
                    return;
                }

                Console.Write("Division" + ID.ToString() + Environment.NewLine +
                    "Left " + DivList[ID].Left.ToString() + Environment.NewLine +
                    "Top " + DivList[ID].Top.ToString() + Environment.NewLine +
                    "Right " + DivList[ID].Right.ToString() + Environment.NewLine +
                    "Bottom " + DivList[ID].Bottom.ToString() + Environment.NewLine +
                    "Width " + DivList[ID].Width.ToString() + Environment.NewLine +
                    "Height " + DivList[ID].Height.ToString());

                if (DivList[ID].InnerRoom == null)
                {
                    return;
                }

                Console.Write("InnerRoom " + ID.ToString() + Environment.NewLine +
                    "Left " + DivList[ID].InnerRoom.Left.ToString() + Environment.NewLine +
                    "Top " + DivList[ID].InnerRoom.Top.ToString() + Environment.NewLine +
                    "Right " + DivList[ID].InnerRoom.Right.ToString() + Environment.NewLine +
                    "Bottom " + DivList[ID].InnerRoom.Bottom.ToString() + Environment.NewLine +
                    "Width " + DivList[ID].InnerRoom.Width.ToString() + Environment.NewLine +
                    "Height " + DivList[ID].InnerRoom.Height.ToString());
            }
        }
    }
}
