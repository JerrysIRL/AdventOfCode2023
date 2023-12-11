using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    internal class Program
    {
        private static string[] input = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day11\Input.txt");

        public static void Main(string[] args)
        {
            List<Galaxy> galaxyList = new List<Galaxy>();
            List<Line> linesList = new List<Line>();
            List<Column> columnsList = new List<Column>();


            for (int y = 0; y < input.Length; y++)
            {
                Line line = new Line() { Y = y };
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        Galaxy galaxy = new Galaxy() { Coordinates = (y, x) };
                        galaxyList.Add(galaxy);
                        line.IsEmpty = false;
                        // while (columnsList.Count <= x)
                        // {
                        //     columnsList.Add(new Column() { X = x, IsEmpty = true });
                        // }
                        //
                        // columnsList[x].IsEmpty = false;
                    }
                }

                linesList.Add(line);
            }

            List<Column> emptyColumns = new List<Column>(); //columnsList.Where(c => c.IsEmpty).ToList();
            var emptyLines = linesList.Where(l => l.IsEmpty).ToList();

            bool IsEmptyColumn(int x)
            {
                for (int y = 0; y < linesList.Count; y++)
                {
                    if (input[y][x] != '.')
                        return false;
                }

                return true;
            }

            for (int x = 0; x < input[0].Length; x++)
            {
                if (IsEmptyColumn(x))
                {
                    emptyColumns.Add(new Column() { X = x, IsEmpty = true });
                }
            }
            
            HashSet<GalaxyPair> galaxyPairs = new HashSet<GalaxyPair>();
            for (int i = 0; i < galaxyList.Count; i++)
            {
                for (int j = i + 1; j < galaxyList.Count; j++)
                {
                    var galaxyPair = new GalaxyPair()
                    {
                        One = galaxyList[i],
                        Two = galaxyList[j]
                    };
                    galaxyPair.SetMinMax();
                    long intersection = 0;

                    foreach (var line in emptyLines)
                    {
                        if (galaxyPair.MinY <= line.Y && galaxyPair.MaxY >= line.Y)
                        {
                            intersection += 1000000 -1;
                        }
                    }

                    foreach (var column in emptyColumns)
                    {
                        if (galaxyPair.MinX <= column.X && galaxyPair.MaxX >= column.X)
                        {
                            intersection +=1000000 -1;
                        }
                    }

                    galaxyPair.Distance = CalculateDistance(galaxyPair.One.Coordinates, galaxyPair.Two.Coordinates) + intersection;

                    galaxyPairs.Add(galaxyPair);
                }
            }
            
            
            var sum = galaxyPairs.Sum(p => p.Distance);
            Console.WriteLine(sum);
        }


        public class Line
        {
            public int Y { get; set; }
            public bool IsEmpty { get; set; } = true;
        }

        public class Column
        {
            public int X { get; set; }
            public bool IsEmpty { get; set; } = true;
        }

        public class Galaxy
        {
            public (int y, int x) Coordinates { get; set; }
        }

        public class GalaxyPair
        {
            public Galaxy One { get; set; }
            public Galaxy Two { get; set; }
            public int MinX { get; set; }
            public int MinY { get; set; }
            public int MaxX { get; set; }
            public int MaxY { get; set; }
            public long Distance { get; set; }

            public void SetMinMax()
            {
                MinX = Math.Min(One.Coordinates.x, Two.Coordinates.x);
                MinY = Math.Min(One.Coordinates.y, Two.Coordinates.y);
                MaxX = Math.Max(One.Coordinates.x, Two.Coordinates.x);
                MaxY = Math.Max(One.Coordinates.y, Two.Coordinates.y);
            }
        }


        static long CalculateDistance((int y, int x) coordinates1, (int y, int x) coordinates2)
        {
            return Math.Abs(coordinates1.x - coordinates2.x) + Math.Abs(coordinates1.y - coordinates2.y);
        }
    }
}