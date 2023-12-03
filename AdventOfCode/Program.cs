using System.Text.RegularExpressions;

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode;




////////////////////////////////////////////////// DAY 3 /////////////////////////////////////////
///
var fileLines = System.IO.File.ReadAllLines("input-3.txt");

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
foreach (Space space in listOfSpaces)
{
    foreach (Part part in listOfParts.Where(k=>k.Y == space.Y || k.Y == space.Y - 1 || k.Y == space.Y + 1))
    {

                if (part.X == space.X.First() - 1 || part.X == space.X.Last() + 1 || space.X.Contains(part.X))
                {
                    sum += space.Number;
                }
    }
}

Console.WriteLine("Day 1 part 1: Sum of part numbers is " + sum);

/////////////////////////////////////////////////  DAY 2  ///////////////////////////////////////
fileLines = System.IO.File.ReadAllLines("input-2.txt");

Regex rxNonDigits = new Regex(@"[^\d]+");
int gameId = 1;
int red = 12;
int green = 13;
int blue = 14;

List<int> possibleGames = new List<int>();
List<int> powerSums     = new List<int>();

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
                head = position-1;
            }
        }
        return listOfNumbers;
    }
}