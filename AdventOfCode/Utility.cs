﻿using System;
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

        public List<Space> Neighbours { get; set; } = new List<Space>();
    }

    public class Row
    {
        public List<string> Left { get; set; } = new List<string>();
        public List<string> Right { get; set; } = new List<string>();
    }

    public class Map
    {
        public long mapId { get; set; } = 0;
        public long DestinationRangeStart { get; set; }

        public long SourceRangeStart { get; set; }

        public long RangeLength { get; set; }
    }

    public class AlmanacConvert
    {
        public long source { get; set; }
        public long dest { get; set; }
    }
}
