using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Extensions;
using My.Randomizer;

namespace Maze
{
    public class Cluster : MazeProvider
    {
        private class BreakPoint
        {
            public bool IsVertical { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public BreakPoint(int X, int Y, bool IsVertical = false)
            {
                this.IsVertical = IsVertical;
                this.X = X;
                this.Y = Y;
            }
        }

        public Cluster() { }

        public override void Create(Layer Layer)
        {
            Stack<BreakPoint> BreakPoints = new Stack<BreakPoint>();
            int[] Cluster = new int[(Layer.Height / 2) * (Layer.Width / 2)];

            Initialize(Layer, BreakPoints, Cluster);
            Start(Layer, BreakPoints, Cluster);
            SetEntrance(Layer, out Coordinate e1, out Coordinate e2);
            SetAnswerRoute(Layer, e1, e2);
        }

        private void Start(Layer Layer, Stack<BreakPoint> BreakPoints, int[] Cluster)
        {
            do
            {
                var bp = BreakPoints.Pop();
                var Index1 = (bp.Y / 2) * (Layer.Width / 2) + bp.X / 2;
                var Index2 = (bp.IsVertical) ? Index1 - Layer.Width / 2 : Index1 - 1;
                var ID1 = GetClusterID(Cluster, Index1);
                var ID2 = GetClusterID(Cluster, Index2);

                if (ID1 != ID2)
                {
                    Layer.Set(bp.X, bp.Y, BlockType.Road);

                    if (ID1 < ID2)
                    {
                        Cluster[ID2] = ID1;
                    }
                    else
                    {
                        Cluster[ID1] = ID2;
                    }
                }
            }
            while (BreakPoints.Count != 0);
        }

        private int GetClusterID(int[] Cluster, int Index)
        {
            while (Index != Cluster[Index])
            {
                Index = Cluster[Index];
            }

            return Index;
        }

        private void Initialize(Layer Layer, Stack<BreakPoint> BreakPoints, int[] Cluster)
        {
            Layer.FillValues(BlockType.Sentinel);

            for (int y = 1; y < Layer.Height - 1; y += 2)
            {
                for (int x = 1; x < Layer.Width - 1; x += 2)
                {
                    Layer.Set(x, y, BlockType.Road);
                }
            }

            BreakPoints.Clear();

            List<BreakPoint> temp = new List<BreakPoint>();

            for (int y = 1; y < Layer.Height - 1; y += 2)
            {
                for (int x = 2; x < Layer.Width - 1; x += 2)
                {
                    temp.Add(new BreakPoint(x, y));
                }
            }
            for (int y = 2; y < Layer.Height - 1; y += 2)
            {
                for (int x = 1; x < Layer.Width - 1; x += 2)
                {
                    temp.Add(new BreakPoint(x, y, true));
                }
            }

            temp.Shuffle(_rnd);

            foreach (var Item in temp)
            {
                BreakPoints.Push(Item);
            }

            for (int i = 0; i < Cluster.Length; i++)
            {
                Cluster[i] = i;
            }
        }
    }
}
