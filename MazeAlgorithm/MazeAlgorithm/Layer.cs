using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class Layer
    {
        private readonly BlockType[,] _Values;

        public int Width { get; }
        public int Height { get; }
        public int Measure
        {
            get { return Width * Height; }
        }

        public Layer(int Width, int Height)
        {
            if (Width < 5 || Height < 5 || Width % 2 == 0 || Height % 2 == 0)
            {
                throw new ArgumentException("縦横共に5マス以上、奇数にしてください。");
            }

            this.Width = Width;
            this.Height = Height;
            _Values = new BlockType[Height, Width];
        }

        public BlockType Get(int x, int y)
        {
            if (IsOutOfRange(x, y))
            {
                return BlockType.OutOfRange;
            }

            return _Values[y, x];
        }

        public void Set(int x, int y, BlockType Chip)
        {
            if (IsOutOfRange(x, y))
            {
                return;
            }

            _Values[y, x] = Chip;
        }

        internal bool IsOutOfRange(int x, int y)
        {
            if (x < 0 || Width <= x)
            {
                return true;
            }
            if (y < 0 || Height <= y)
            {
                return true;
            }

            return false;
        }

        internal void BorderValues(BlockType Chip)
        {
            for (int i = 0; i < Width; i++)
            {
                _Values[0, i] = Chip;
                _Values[Height - 1, i] = Chip;
            }
            for (int i = 0; i < Height; i++)
            {
                _Values[i, 0] = Chip;
                _Values[i, Width - 1] = Chip;
            }
        }

        internal void FillValues(BlockType Chip)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _Values[y, x] = Chip;
                }
            }
        }
    }
}
