﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    internal class Program
    {
        private static string[] input = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day10\Input.txt");

        public static void Main(string[] args)
        {
            List<Pipe> allPipes = new List<Pipe>();
            Queue<Pipe> visited = new Queue<Pipe>();
            Pipe start = new Pipe();

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    Pipe tempPipe = new Pipe()
                    {
                        Location = (y, x),
                        Type = Pipe.GetType(input[y][x]),
                        Symbol = input[y][x]
                    };
                    if (tempPipe.Type == PipeType.Start)
                    {
                        start = tempPipe;
                    }

                    allPipes.Add(tempPipe);
                }
            }

            start.DistanceFromS = 0;
            visited.Enqueue(start);

            HashSet<Pipe> closedNodes = new HashSet<Pipe>();

            while (visited.Any())
            {
                var current = visited.Dequeue();
                current.Visited = true;
                closedNodes.Add(current);

                var ns = GetNeighbors(current);
                foreach (var n in ns)
                {
                    if (!n.Visited)
                    {
                        visited.Enqueue(n);
                    }
                }
            }


            List<long> resultList = new List<long>();
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    var pipe = GetPipeByLocation((y, x));
                    Console.Write($"{pipe.Symbol,1}");
                }
                Console.WriteLine();
            }

            resultList.Sort();
            Console.WriteLine(resultList.Last());
            
            
            List<Pipe> GetNeighbors(Pipe currentPipe)
            {
                List<Pipe> neighbours = new List<Pipe>();

                if (currentPipe.Type == PipeType.Start)
                {
                    AddUpPipe();
                    AddDownPipe();
                    AddRightPipe();
                    AddLeftPipe();
                }
                else if (currentPipe.Type == PipeType.H)
                {
                    AddRightPipe();
                    AddLeftPipe();
                }

                else if (currentPipe.Type == PipeType.V)
                {
                    AddDownPipe();
                    AddUpPipe();
                }

                else if (currentPipe.Type == PipeType.NE)
                {
                    AddRightPipe();
                    AddUpPipe();
                }

                else if (currentPipe.Type == PipeType.NW)
                {
                    AddUpPipe();
                    AddLeftPipe();
                }

                else if (currentPipe.Type == PipeType.SW)
                {
                    AddDownPipe();
                    AddLeftPipe();
                }

                else if (currentPipe.Type == PipeType.SE)
                {
                    AddDownPipe();
                    AddRightPipe();
                }

                void AddRightPipe()
                {
                    var r = GetPipeByLocation((currentPipe.Location.y, currentPipe.Location.x + 1));
                    if (r == null || r.Visited) return;
                    if (r.Type == PipeType.H || r.Type == PipeType.NW || r.Type == PipeType.SW)
                    {
                        r.DistanceFromS = currentPipe.DistanceFromS + 1;
                        neighbours.Add(r);
                    }
                }

                void AddLeftPipe()
                {
                    var l = GetPipeByLocation((currentPipe.Location.y, currentPipe.Location.x - 1));
                    if (l == null || l.Visited) return;
                    if (l.Type == PipeType.H || l.Type == PipeType.NE || l.Type == PipeType.SE)
                    {
                        l.DistanceFromS = currentPipe.DistanceFromS + 1;
                        neighbours.Add(l);
                    }
                }

                void AddUpPipe()
                {
                    var u = GetPipeByLocation((currentPipe.Location.y - 1, currentPipe.Location.x));
                    if (u == null || u.Visited) return;
                    if (u.Type == PipeType.V || u.Type == PipeType.SW || u.Type == PipeType.SE)
                    {
                        u.DistanceFromS = currentPipe.DistanceFromS + 1;
                        neighbours.Add(u);
                    }
                }

                void AddDownPipe()
                {
                    var d = GetPipeByLocation((currentPipe.Location.y + 1, currentPipe.Location.x));
                    if (d == null || d.Visited) return;
                    if (d.Type == PipeType.V || d.Type == PipeType.NW || d.Type == PipeType.NE)
                    {
                        d.DistanceFromS = currentPipe.DistanceFromS + 1;
                        neighbours.Add(d);
                    }
                }

                return neighbours;
            }


            Pipe GetPipeByLocation((int, int) location)
            {
                return allPipes.FirstOrDefault(pipe => pipe.Location == location);
            }
        }

        static bool CanReachStart(Pipe pipe)
        {
            // Check if there is a path to the starting point 'S'
            return pipe.DistanceFromS != -1;
        }
    }

    public class Pipe
    {
        public ( int y, int x) Location { get; set; }
        public PipeType Type { get; set; }
        public int DistanceFromS { get; set; }
        public bool Visited { get; set; }
        public char Symbol { get; set; }

        public static PipeType GetType(char letter)
        {
            switch (letter)
            {
                case '|':
                    return PipeType.V;
                case '-':
                    return PipeType.H;
                case 'L':
                    return PipeType.NE;
                case 'J':
                    return PipeType.NW;
                case '7':
                    return PipeType.SW;
                case 'F':
                    return PipeType.SE;
                case '.':
                    return PipeType.Ground;
                case 'S':
                    return PipeType.Start;
            }

            return PipeType.Undefined;
        }
    }


    public enum PipeType
    {
        V,
        H,
        NE,
        NW,
        SW,
        SE,
        Ground,
        Start,
        Undefined
    }
}