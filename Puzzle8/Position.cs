using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle8
{
    internal struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int x, int y)
        {
            X=x; Y=y;
        }
        public override string ToString()
        {
            return "X = "+this.X+ "\tY = " + this.Y;
        }
    }
}
