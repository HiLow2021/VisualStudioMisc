using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal class Vector
    {
        public static readonly Vector North = new Vector(0, -1);
        public static readonly Vector East = new Vector(1, 0);
        public static readonly Vector South = new Vector(0, 1);
        public static readonly Vector West = new Vector(-1, 0);

        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
