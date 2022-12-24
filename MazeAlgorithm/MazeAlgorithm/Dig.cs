using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Extensions;
using My.Randomizer;

namespace Maze
{
    public class Dig : MazeProvider
    {
        public bool IsPerfectlyRecursive { get; set; } = false;

        public Dig(bool IsPerfectlyRecursive)
        {
            this.IsPerfectlyRecursive = IsPerfectlyRecursive;
        }

        public override void Create(Layer Layer)
        {
            HashSet<Coordinate> RemainingPoints = new HashSet<Coordinate>();

            Initialize(Layer, RemainingPoints);
            Start(Layer, RemainingPoints);
            SetEntrance(Layer, out Coordinate e1, out Coordinate e2);
            SetAnswerRoute(Layer, e1, e2);
        }

        private void Start(Layer Layer, HashSet<Coordinate> RemainingPoints)
        {
            Stack<Coordinate> RestartPoints = new Stack<Coordinate>();

            RestartPoints.Push(RemainingPoints.First());

            do
            {
                var rp = RestartPoints.Pop();

                try
                {
                    RecursiveDig(Layer, rp, RemainingPoints, RestartPoints);
                }
                catch (StackOverflowException)
                {
                    IsPerfectlyRecursive = false;
                }
            }
            while (RemainingPoints.Count != 0);
        }

        private void RecursiveDig(Layer Layer, Coordinate c, HashSet<Coordinate> RemainingPoints, Stack<Coordinate> RestartPoints)
        {
            Layer.Set(c.X, c.Y, BlockType.Road);
            RemainingPoints.Remove(c);
            RestartPoints.Push(c);

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
                if (Layer.Get(c.X + (v.X * 2), c.Y + (v.Y * 2)) == BlockType.Wall)
                {
                    Layer.Set(c.X + v.X, c.Y + v.Y, BlockType.Road);
                    RecursiveDig(Layer, new Coordinate(c.X + (v.X * 2), c.Y + (v.Y * 2)), RemainingPoints, RestartPoints);

                    if (!IsPerfectlyRecursive)
                    {
                        return;
                    }
                }
            }

            RestartPoints.Pop();
        }

        private void Initialize(Layer Layer, HashSet<Coordinate> RemainingPoints)
        {
            Layer.FillValues(BlockType.Wall);
            Layer.BorderValues(BlockType.Sentinel);
            RemainingPoints.Clear();

            List<Coordinate> temp = new List<Coordinate>();

            for (int y = 1; y < Layer.Height - 1; y += 2)
            {
                for (int x = 1; x < Layer.Width - 1; x += 2)
                {
                    temp.Add(new Coordinate(x, y));
                }
            }

            temp.Shuffle(_rnd);
            RemainingPoints.UnionWith(temp);
        }
    }
}
