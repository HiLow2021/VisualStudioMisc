using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal class Coordinate : IEquatable<Coordinate>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public bool Equals(Coordinate Other)
        {
            if (Other == null)
            {
                return false;
            }
            else
            {
                return X == Other.X && Y == Other.Y;
            }
        }

        public override int GetHashCode()
        {
            return new { X, Y }.GetHashCode();
        }
    }
}
