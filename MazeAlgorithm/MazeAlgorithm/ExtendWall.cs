using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Extensions;
using My.Randomizer;

namespace Maze
{
    public class ExtendWall : MazeProvider
    {
        public ExtendWall() { }

        public override void Create(Layer Layer)
        {
            Queue<Coordinate> StartPoints = new Queue<Coordinate>();

            Initialize(Layer, StartPoints);
            Start(Layer, StartPoints);
            SetEntrance(Layer, out Coordinate e1, out Coordinate e2);
            SetAnswerRoute(Layer, e1, e2);
        }

        private void Start(Layer Layer, Queue<Coordinate> StartPoints)
        {
            do
            {
                var sp = StartPoints.Dequeue();
                var CurrentRoutes = new List<Coordinate>();

                try
                {
                    if (Layer.Get(sp.X, sp.Y) != BlockType.Sentinel)
                    {
                        RecursiveExtendWall(Layer, sp, CurrentRoutes);

                        if (CurrentRoutes.Count == 0)
                        {
                            StartPoints.Enqueue(sp);
                        }
                    }
                }
                catch (StackOverflowException)
                {
                    CurrentRoutes.ForEach(r => Layer.Set(r.X, r.Y, BlockType.Road));
                    StartPoints.Enqueue(sp);
                }
            }
            while (StartPoints.Count != 0);
        }

        private void RecursiveExtendWall(Layer Layer, Coordinate c, List<Coordinate> CurrentRoutes)
        {
            Layer.Set(c.X, c.Y, BlockType.Wall);
            CurrentRoutes.Add(c);

            var Vectors = new Vector[]
            {
                Vector.North,
                Vector.East,
                Vector.South,
                Vector.West
            };

            Vectors.Shuffle(_rnd);

            foreach (var v in Vectors)
            {
                if (Layer.Get(c.X + (v.X * 2), c.Y + (v.Y * 2)) != BlockType.Wall)
                {
                    Layer.Set(c.X + v.X, c.Y + v.Y, BlockType.Wall);
                    CurrentRoutes.Add(new Coordinate(c.X + v.X, c.Y + v.Y));

                    if (Layer.Get(c.X + (v.X * 2), c.Y + (v.Y * 2)) == BlockType.Road)
                    {
                        RecursiveExtendWall(Layer, new Coordinate(c.X + (v.X * 2), c.Y + (v.Y * 2)), CurrentRoutes);
                    }
                    else
                    {
                        CurrentRoutes.ForEach(r => Layer.Set(r.X, r.Y, BlockType.Sentinel));
                    }

                    return;
                }
            }

            CurrentRoutes.ForEach(r => Layer.Set(r.X, r.Y, BlockType.Road));
            CurrentRoutes.Clear();
        }

        private void Initialize(Layer Layer, Queue<Coordinate> StartPoints)
        {
            Layer.FillValues(BlockType.Road);
            Layer.BorderValues(BlockType.Sentinel);
            StartPoints.Clear();

            List<Coordinate> temp = new List<Coordinate>();

            for (int y = 1; y < Layer.Height / 2; y++)
            {
                for (int x = 1; x < Layer.Width / 2; x++)
                {
                    temp.Add(new Coordinate(x * 2, y * 2));
                }
            }

            temp.Shuffle(_rnd);

            foreach (var Item in temp)
            {
                StartPoints.Enqueue(Item);
            }
        }
    }
}
