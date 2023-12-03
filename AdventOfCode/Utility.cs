using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Space
    {
        public int Number { get; set; }

        public List<int> X { get; set; } = new List<int>();

        public int Y { get; set; }
    }

    public class Part
    {
        public char Symbol { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
