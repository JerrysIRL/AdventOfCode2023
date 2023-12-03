using System;
using System.Collections.Generic;
using System.IO;

namespace Day3
{
    internal class Program
    {
        static string[] rawInput = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day3\Input.txt");
        static readonly List<char> symbols = new List<char>() { '*', '/', '&', '@', '%', '+', '=', '#', '$', '-' };

        public static void Main(string[] args)
        {
            List<PartNumber> NumbersList = new List<PartNumber>();
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
                    }
                    else
                    {
                        if (partNumber.Number == "")
                        {
                            continue;
                        }

                        NumbersList.Add(partNumber);
                        partNumber = new PartNumber("");
                    }
                }
            }


            int sum = 0;
            NumbersList.ForEach(s =>
            {
                s.Valid = IsValidPartNumber(s);
                if (s.Valid)
                {
                    sum += Int32.Parse(s.Number);
                    Console.WriteLine(s.Number);
                }
            });
            Console.WriteLine(sum);

            // string subStr = line.Substring(x, 3);
            // string resultString = Regex.Match(subStr, @"\d+").Value;
            // int partNumber = Int32.Parse(resultString);
            // Console.WriteLine(partNumber);
            /*bool IsValidPartNumber(PartNumber partNumber)
            {
                for (int i = 0; i < partNumber.Number.ToString().Length; i++)
                {
                    for (int y = -1; y < 1; y++)
                    {
                        if (partNumber.Coordinates.Item1 + y < 0 || partNumber.Coordinates.Item1 + y >= rawInput.Length) { continue; }

                        for (int x = -1; x < 1; x++)
                        {

                            if (partNumber.Coordinates.Item2 + x + i < 0 || partNumber.Coordinates.Item2 + x + i >= rawInput[partNumber.Coordinates.Item1 + y].Length)
                            {
                                continue;
                            }

                            var c = GetCharFromInput(partNumber.Coordinates.Item1 + y, partNumber.Coordinates.Item2 + x + i);
                            foreach (var symbol in symbols)
                            {
                                if (c == symbol)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                return false;
            }
        }*/

            bool IsValidPartNumber(PartNumber partNumber)
            {
                foreach (var coord in partNumber.CoordinatesList)
                {
                    int y = coord.Item1;
                    int x = coord.Item2;

                    // Check in an extended 3x3 grid around the current cell (including diagonals)
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

                            char c = GetCharFromInput(newY, newX);

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


        public class PartNumber
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
        }

        static char GetCharFromInput(int y, int x)
        {
            return rawInput[y][x];
        }
    }
}