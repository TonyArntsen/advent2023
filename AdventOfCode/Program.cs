using System.Text.RegularExpressions;
using System.Linq;
using AdventOfCode;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Diagnostics;



//////////////////////////////////////////////// DAY 6 ///////////////////////////////////////////////


List<long> races = new List<long>();
List<long> destinations = new List<long>();
var fileLines = System.IO.File.ReadAllLines("input-6.txt");

    List<string> raceDuration = fileLines[0].Split(':').Last().Split(' ').Where(x=>x!=string.Empty).ToList();
    List<string> raceRecord = fileLines[1].Split(':').Last().Split(' ').Where(x => x != string.Empty).ToList();

raceDuration.ForEach(x => races.Add(int.Parse(x.Replace(" ", string.Empty))));
raceRecord.ForEach(x => destinations.Add(long.Parse(x.Replace(" ", string.Empty))));


List<int> results = new List<int>();
long numberOfWays = 0;

    for (int j = 0; j < races[0]; j++)
    {
        long result = j * (races[0] - j);

        if (result > destinations[0])
        {
            numberOfWays++;
        }
    }


Console.WriteLine("Day 6 part 2: Number of ways to win: " + numberOfWays);

/////////////////////////////////////////////////// DAY 5 ///////////////////////////////////////////

day5();
static void day5()
{
    Stopwatch stopwatch = new Stopwatch();

    stopwatch.Start();
    var fileLines = System.IO.File.ReadAllLines("input-5.txt");

    List<string> seeds = fileLines.First().Split(':').Last().TrimStart().Split(' ').ToList();
    List<string> seedPairs = new List<string>();
    object lockObj = new object();

    for (int i = 0; i < seeds.Count; i += 2)
    {
        seedPairs.Add(seeds[i] + " " + seeds[i + 1]);
    }

    HashSet<Map> maps = new HashSet<Map>();
    bool startAdding = false;
    int mapId = -1;
    foreach (string l in fileLines)
    {
        if (string.IsNullOrEmpty(l))
        {
            startAdding = false;
        }

        if (startAdding)
        {
            string[] brokenString = l.Split(' ');
            Map map = new Map()
            {
                DestinationRangeStart = Int64.Parse(brokenString[0]),
                SourceRangeStart = Int64.Parse(brokenString[1]),
                RangeLength = Int64.Parse(brokenString[2]),
                mapId = mapId
            };

            maps.Add(map);
        }

        if (l.Contains("map:"))
        {
            startAdding = true;
            mapId++;
        }
    }

    List<long> seedLocations = new List<long>();

    Parallel.ForEach(seedPairs, (seed) =>
    {
        long rangeFrom = Int64.Parse(seed.Split(' ').First());
        long rangeTo = Int64.Parse(seed.Split(' ').Last());
        long seedId = 0;
        IEnumerable<Map> tempMap;

        for (long r = rangeFrom; r < rangeFrom + rangeTo; r++)
        {
            seedId = r;

            for (int mapNumber = 0; mapNumber <= 6; mapNumber++)
            {
                tempMap = maps.Where(x => x.mapId == mapNumber && seedId >= x.SourceRangeStart && seedId < x.SourceRangeStart + x.RangeLength);
                if (!tempMap.Any())
                {
                    continue;
                }

                else
                {
                    seedId = tempMap.First().DestinationRangeStart + (seedId - tempMap.First().SourceRangeStart);
                }

            }
            lock (lockObj)
            {
                seedLocations.Add(seedId);
            }
        }
    });

    Console.WriteLine("Day 5: Nearest location is " + seedLocations.Min());
    stopwatch.Stop();

}







////////////////////////////////////////////////// DAY 4 ////////////////////////////////////////////

int line = 1;
int totalSumOfCards = 0;
fileLines = System.IO.File.ReadAllLines("input-4.txt");
int fish = 0;
int iterator = 0;
int wonCards = 0;
List<Row> rows = new List<Row>();
List<Row> newRows = new List<Row>();


foreach (string singleLine in fileLines)
{
    int cardWorth = 0;
    string washedString = singleLine.Replace("  ", " ");

    string removedPrefix = washedString.Split(':')[1];

    string dealerCardsString = removedPrefix.Split('|').First().TrimStart().TrimEnd();
    string yourCardsString = removedPrefix.Split('|').Last().TrimStart().TrimEnd();

    List<string> dealerCards = dealerCardsString.Split(' ').ToList();
    List<string> yourCards = yourCardsString.Split(' ').ToList();

    Row row = new Row();
    row.Left = dealerCards;
    row.Right = yourCards;
    rows.Add(row);

    foreach (string card in dealerCards)
    {

        if (yourCards.Contains(card))
        {
            if (cardWorth == 0)
            {
                cardWorth = 1;
                continue;
            }
            cardWorth *= 2;
        }
    }
    totalSumOfCards += cardWorth;
    line++;
}

int k = 0;
while (k < rows.Count())
{
    var hashSet = new HashSet<string>(rows[k].Left);
    int count = rows[k].Right.Count(x => hashSet.Contains(x));
    int index = rows.FindIndex(x => x.Equals(rows[k]));
    for (int j = index + 1; j < count + index + 1; j++)
    {
        if (count + index < rows.Count())
        {
            rows.Add(rows[j]);
        }
    }
    k++;
}

Console.WriteLine("Day 4 part 1: " + totalSumOfCards);
Console.WriteLine("Day 4 part 2: " + rows.Count());









////////////////////////////////////////////////// DAY 3 ////////////////////////////////////////////
fileLines = System.IO.File.ReadAllLines("input-3.txt");

List<Space> listOfSpaces = new List<Space>();
List<Part> listOfParts = new List<Part>();
int y = 0;

foreach (string singleLine in fileLines)
{
    string number = "";
    List<Space> spaceTemp = new List<Space>();
    List<int> xSpaces = new List<int>();
    for (int x = 0; x < singleLine.Length; x++)
    {
        if (char.IsNumber(singleLine[x]))
        {
            number += singleLine[x];
            xSpaces.Add(x);
        }


        if (!char.IsNumber(singleLine[x]) && singleLine[x] != '.')
        {
            listOfParts.Add(new Part() { Symbol = singleLine[x], X = x, Y = y });
        }

        if (!char.IsNumber(singleLine[x]) || x == singleLine.Length - 1)
        {
            if (number.Length > 0)
            {
                List<int> xcords = new List<int>();
                xcords = xcords.Concat(xSpaces).ToList();
                spaceTemp.Add(new Space() { X = xcords, Y = y, Number = int.Parse(number) });
                listOfSpaces = listOfSpaces.Concat(spaceTemp).ToList();
                spaceTemp.Clear();
                xSpaces.Clear();
            }
            number = "";
            continue;
        }
    }
    y++;
}

int sum = 0;
int gearRatioSum = 0;
foreach (Space space in listOfSpaces)
{
    foreach (Part part in listOfParts.Where(k => k.Y == space.Y || k.Y == space.Y - 1 || k.Y == space.Y + 1))
    {
        if (part.X == space.X.First() - 1 || part.X == space.X.Last() + 1 || space.X.Contains(part.X))
        {

            if (!part.Neighbours.Contains(space))
            {
                part.Neighbours.Add(space);
            }
            sum += space.Number;
        }
    }
}


foreach (Part part in listOfParts)
{
    int num = 1;
    if (part.Neighbours.Count > 1)
    {
        foreach (Space space in part.Neighbours)
        {
            num = num * space.Number;
        }
        gearRatioSum += num;
    }
}

Console.WriteLine("Day 3 part 1: Sum of part numbers is " + sum);
Console.WriteLine("Day 3 part 2: Sum of gear ratios is " + gearRatioSum);











/////////////////////////////////////////////////  DAY 2  ///////////////////////////////////////
fileLines = System.IO.File.ReadAllLines("input-2.txt");

Regex rxNonDigits = new Regex(@"[^\d]+");
int gameId = 1;
int red = 12;
int green = 13;
int blue = 14;

List<int> possibleGames = new List<int>();
List<int> powerSums = new List<int>();

foreach (string singleLine in fileLines)
{
    string removedPrefix = singleLine.Split(':')[1];
    string[] games = removedPrefix.Split(";");
    bool currentGameIsPossible = true;

    List<int> highestRed = new List<int>();
    List<int> highestGreen = new List<int>();
    List<int> highestBlue = new List<int>();

    foreach (string game in games)
    {
        int redPicked = 0;
        int greenPicked = 0;
        int bluePicked = 0;

        string[] colorNumbers = game.Split(",");

        foreach (string colorNumber in colorNumbers)
        {
            int numberOnly = int.Parse(rxNonDigits.Replace(colorNumber, ""));
            if (colorNumber.Contains("red"))
            {
                redPicked += numberOnly;
            }

            if (colorNumber.Contains("green"))
            {
                greenPicked += numberOnly;
            }

            if (colorNumber.Contains("blue"))
            {
                bluePicked += numberOnly;
            }
        }

        if (redPicked > red || greenPicked > green || bluePicked > blue)
        {
            currentGameIsPossible = false;
        }

        highestRed.Add(redPicked);
        highestGreen.Add(greenPicked);
        highestBlue.Add(bluePicked);
    }
    powerSums.Add(highestRed.Max() * highestGreen.Max() * highestBlue.Max());

    if (currentGameIsPossible)
    {
        possibleGames.Add(gameId);
    }
    gameId++;
}

Console.WriteLine("Day 2 part 1: " + possibleGames.Sum(gameId => gameId) + " games are possible");
Console.WriteLine("Day 2 part 2: " + powerSums.Sum(x => x) + " sum of the powers");










/////////////////////////////////////////////////  DAY 1  ///////////////////////////////////////

int totalSum = 0;
fileLines = System.IO.File.ReadAllLines("input-1.txt");
foreach (string singleLine in fileLines)
{
    List<int> numberList = ScanForNumbers.GetNumbers(singleLine);

    string digit = numberList.First().ToString();
    digit += numberList.Last().ToString();
    totalSum += int.Parse(digit);
}

Console.WriteLine("Day 1 part 2: " + totalSum.ToString() + " calibration numbers");


public static class ScanForNumbers
{
    public static List<int> GetNumbers(string text)
    {
        Dictionary<string, int> numbers = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        List<int> listOfNumbers = new List<int>();
        int head = 0;
        for (int position = 0; position <= text.Length; position++)
        {
            string partOfstring = text.Substring(head, position - head);

            if (partOfstring.ToCharArray().ToList().Any())
            {
                char rightMost = partOfstring[partOfstring.Length - 1];
                if (char.IsNumber(rightMost))
                {
                    listOfNumbers.Add((int)char.GetNumericValue(rightMost));
                    head = position;

                }
            }

            if (numbers.Any(x => partOfstring.Contains(x.Key)))
            {
                int result = numbers.Where(x => partOfstring.Contains(x.Key)).First().Value;
                listOfNumbers.Add(result);
                head = position - 1;
            }
        }
        return listOfNumbers;
    }
}
