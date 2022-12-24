using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Extensions;
using My.Randomizer;

namespace Maze
{
    public class PullBar : MazeProvider
    {
        public PullBar() { }

        public override void Create(Layer Layer)
        {
            Initialize(Layer);
            Start(Layer);
            SetEntrance(Layer, out Coordinate e1, out Coordinate e2);
            SetAnswerRoute(Layer, e1, e2);
        }

        private void Start(Layer Layer)
        {
            for (int y = 2; y < Layer.Height - 1; y += 2)
            {
                for (int x = 2; x < Layer.Width - 1; x += 2)
                {
                    PutWall(Layer, x, y);
                }
            }
        }

        private void PutWall(Layer Layer, int x, int y)
        {
            Layer.Set(x, y, BlockType.Wall);

            List<Vector> Vectors = new List<Vector>()
            {
                Vector.East,
                Vector.South,
                Vector.West
            };

            if (y == 2)
            {
                Vectors.Add(Vector.North);
            }

            Vectors.Shuffle(_rnd);

            foreach (var v in Vectors)
            {
                if (Layer.Get(x + v.X, y + v.Y) != BlockType.Wall)
                {
                    Layer.Set(x + v.X, y + v.Y, BlockType.Wall);
                    break;
                }
            }
        }

        private void Initialize(Layer Layer)
        {
            Layer.FillValues(BlockType.Road);
            Layer.BorderValues(BlockType.Sentinel);
        }
    }
}
