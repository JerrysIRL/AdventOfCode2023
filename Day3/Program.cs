using System;
using System.Collections.Generic;
using System.IO;

namespace Day3
{
    internal class Program
    {
        static readonly string[] rawInput = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day3\Input.txt");
        static readonly List<char> symbols = new List<char>() { '*', '/', '&', '@', '%', '+', '=', '#', '$','-'};
        
        public static void Main(string[] args)
        {
            List<PartNumber> numbersList = new List<PartNumber>();
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
                numbersList.Add(partNumber);
            }


            int sum = 0;
            numbersList.ForEach(s =>
            {
                s.Valid = IsValidPartNumber(s);
                if (s.Valid)
                {
                    sum += Int32.Parse(s.Number);
                }
            });
            Console.WriteLine(sum);

            

            bool IsValidPartNumber(PartNumber partNumber)
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