using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Randomizer;

namespace Maze
{
    public abstract class MazeProvider
    {
        protected XorShift _rnd = new XorShift();

        public abstract void Create(Layer Layer);

        internal virtual bool SetAnswerRoute(Layer Layer, Coordinate Entrance1, Coordinate Entrance2)
        {
            Layer.Set(Entrance1.X, Entrance1.Y, BlockType.Mark);

            if (Entrance1.Equals(Entrance2)) { return true; }

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
                if (Layer.Get(Entrance1.X + v.X, Entrance1.Y + v.Y) == BlockType.Road)
                {
                    if (SetAnswerRoute(Layer, new Coordinate(Entrance1.X + v.X, Entrance1.Y + v.Y), Entrance2))
                    {
                        return true;
                    }
                    else
                    {
                        Layer.Set(Entrance1.X + v.X, Entrance1.Y + v.Y, BlockType.Road);
                    }
                }
            }

            return false;
        }

        internal virtual void SetEntrance(Layer Layer, out Coordinate Entrance1, out Coordinate Entrance2)
        {
            var Coordinates = new Coordinate[]
            {
                new Coordinate(_rnd.Next(1, Layer.Width - 1), 0),
                new Coordinate(Layer.Width - 1, _rnd.Next(1, Layer.Height - 1)),
                new Coordinate(_rnd.Next(1, Layer.Width - 1), Layer.Height - 1),
                new Coordinate(0 , _rnd.Next(1, Layer.Height - 1))
            };

            Coordinates.Shuffle(_rnd);
            Entrance1 = DoOddExceptBorder(Coordinates[0]);
            Entrance2 = DoOddExceptBorder(Coordinates[1]);
            Layer.Set(Entrance1.X, Entrance1.Y, BlockType.Road);
            Layer.Set(Entrance2.X, Entrance2.Y, BlockType.Road);

            Coordinate DoOddExceptBorder(Coordinate c)
            {
                if (c.X != 0 && c.X != Layer.Width - 1 && c.X % 2 == 0) { c.X--; }
                if (c.Y != 0 && c.Y != Layer.Height - 1 && c.Y % 2 == 0) { c.Y--; }

                return c;
            }
        }
    }
}
