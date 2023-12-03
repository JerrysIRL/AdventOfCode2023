using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    internal class Program
    {
        static readonly string[] rawInput = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day3\Input.txt");
        static readonly List<char> symbols = new List<char>() { '*', '/', '&', '@', '%', '+', '=', '#', '$', '-' };

        public static void Main(string[] args)
        {
            List<PartNumber> numbersList = new List<PartNumber>();
            PartOne(numbersList);

            int sumGearRatios = 0;

            foreach (var gearCoord in GetGearsCoordinates())
            {
                int gearRatio = CalculateGearRatio(gearCoord.Item1, gearCoord.Item2);
                sumGearRatios += gearRatio;
            }

            Console.WriteLine(sumGearRatios);

            List<(int, int)> GetGearsCoordinates()
            {
                List<(int, int)> gears = new List<(int, int)>();

                for (int y = 0; y < rawInput.Length; y++)
                {
                    var line = rawInput[y];
                    for (int x = 0; x < line.Length; x++)
                    {
                        char c = line[x];
                        if (c == '*')
                        {
                            int adjacentPartNumbers = GetAdjacentPartNumbers(y, x).Count;
                            if (adjacentPartNumbers == 2)
                            {
                                gears.Add((y, x));
                            }
                        }
                    }
                }

                return gears;
            }

            int CalculateGearRatio(int y, int x)
            {
                var adjacentPartNumbers = GetAdjacentPartNumbers(y, x);
                if (adjacentPartNumbers.Count == 2)
                {
                    int ratio = Int32.Parse(adjacentPartNumbers[0].Number) * Int32.Parse(adjacentPartNumbers[1].Number);
                    return ratio;
                }
                return 0;
            }

            List<PartNumber> GetAdjacentPartNumbers(int y, int x)
            {
                HashSet<PartNumber> connectingPartNumbers = new HashSet<PartNumber>();

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int newY = y + i;
                        int newX = x + j;

                        // Skip the current cell
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        // Skip invalid coordinates
                        if (newY < 0 || newY >= rawInput.Length || newX < 0 || newX >= rawInput[newY].Length)
                        {
                            continue;
                        }

                        char c = GetCharAtPosition(newY, newX);

                        if (char.IsDigit(c))
                        {
                            connectingPartNumbers.Add(numbersList.Find(num => num.CoordinatesList.Contains((newY, newX))));
                        }
                    }
                }

                return connectingPartNumbers.ToList();
            }
            
            
            int sum = 0;
            numbersList.ForEach(s =>
            {
                s.Valid = s.IsValidPartNumber(s);
                if (s.Valid)
                {
                    sum += Int32.Parse(s.Number);
                }
            });
            Console.WriteLine(sum);
        }

        private static void PartOne(List<PartNumber> numbersList)
        {
            for (int y = 0; y < rawInput.Length; y++)
            {
                var line = rawInput[y];
                PartNumber partNumber = new PartNumber("");
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    if (char.IsDigit(c))
                    {
                        partNumber.AddCoordinates((y, x));
                        partNumber.Number += c;
                        if (x + 1 < line.Length && !char.IsDigit(line[x + 1]))
                        {
                            numbersList.Add(partNumber);
                            partNumber = new PartNumber("");
                        }
                    }
                }

                if (partNumber.Number != "")
                {
                    numbersList.Add(partNumber);
                }
            }
        }

        private class PartNumber
        {
            public string Number { get; set; }
            public List<(int, int)> CoordinatesList { get; set; }
            public bool Valid { get; set; }

            public PartNumber(string number)
            {
                Number = number;
                CoordinatesList = new List<(int, int)>();
            }

            public void AddCoordinates((int, int) coordinates)
            {
                CoordinatesList.Add(coordinates);
            }


            public bool IsValidPartNumber(PartNumber partNumber)
            {
                foreach (var coord in partNumber.CoordinatesList)
                {
                    int y = coord.Item1;
                    int x = coord.Item2;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int newY = y + i;
                            int newX = x + j;

                            if (i == 0 && j == 0)
                            {
                                continue;
                            }

                            if (newY < 0 || newY >= rawInput.Length || newX < 0 || newX >= rawInput[newY].Length)
                            {
                                continue;
                            }

                            char c = GetCharAtPosition(newY, newX);
                            if (symbols.Contains(c))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
        }

        static char GetCharAtPosition(int y, int x)
        {
            return rawInput[y][x];
        }
    }
}