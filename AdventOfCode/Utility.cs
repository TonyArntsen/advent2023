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

    public class Race
    {
        public int duration { get; set; }

        public int record { get; set; }
    }

    public class Card
    {
        public int rank { get; set; } = 0;

        public string face { get; set; }

        public string prePattern { get; set; } 

        public string pattern { get; set; }
        public int bid { get; set; }

        public string originalFace { get; set; }

        public List<int> cStrength { get; set; }

    }

    public class RankComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            int minLength = Math.Min(x.cStrength.Count, y.cStrength.Count);

            for (int i = 0; i < minLength; i++)
            {
                if (x.cStrength[i] > y.cStrength[i])
                {
                    return 1;
                }
                else if (x.cStrength[i] < y.cStrength[i])
                {
                    return -1;
                }
            }
            return x.cStrength.Count.CompareTo(y.cStrength.Count);
        }
    }
}

