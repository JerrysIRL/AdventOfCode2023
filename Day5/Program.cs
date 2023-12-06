using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    internal class Program
    {
        private static readonly string rawInput = File.ReadAllText(@"E:\AdventOfCode\AdventOfCode2023\Day5\Input.txt");

        public static void Main(string[] args)
        {
            var seeds = new List<long>
            {
                1367444651, 99920667, 3319921504, 153335682, 67832336, 139859832, 2322838536, 666063790, 1591621692, 111959634, 442852010, 119609663, 733590868, 56288233, 2035874278, 85269124,
                4145746192, 55841637, 864476811, 347179760
            };
            List<List<Map>> sections;
            var sectionsString = rawInput.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            sections = sectionsString.Select(s =>
            {
                var section = s.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                section.RemoveAt(0);
                var mapList = new List<Map>();
                foreach (var line in section)
                    mapList.Add(Map.Parse(line));
                mapList.Reverse();

                return mapList;
            }).ToList();

            //part 1
            List<long> stlList = new List<long>();
            for (var i = 0; i < seeds.Count; i++)
            {
                var temp = seeds[i];
                foreach (var section in sections)
                {
                    temp = SeedToLocation(seeds[i], section);
                }

                stlList.Add(temp);
            }

            var p1 = stlList.Min();
            Console.WriteLine(p1);

            // part 2
            //sections.ForEach(l => l.ForEach(m => Console.WriteLine(m.SourceStart)));
            sections.Reverse();

            long x = 0;
            while (true)
            {
                long temp = x;
                foreach (var section in sections)
                {
                    temp = LocationToSeed(temp, section);
                }

                if (IsAnyInRange(temp, seeds))
                {
                    Console.WriteLine(x);
                    break;
                }
                x++;
            }

        }
        static bool IsAnyInRange(long seed, List<long> seeds)
        {
            for (int i = 0; i < seeds.Count; i += 2)
            {
                long rangeStart = seeds[i];

                // Check if the next seed is within bounds
                if (i + 1 < seeds.Count)
                {
                    long nextSeed = seeds[i] + seeds[i + 1];
                    if (IsInRange(seed, rangeStart, nextSeed))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool IsInRange(long seed, long rangeStart, long nextSeed)
        {
            return seed >= rangeStart && seed < nextSeed;
        }

        static long LocationToSeed(long location, List<Map> mapList)
        {
            foreach (var map in mapList)
            {
                if (location >= map.RangeStart && location < map.RangeStart + map.RangeLength)
                {
                    var n = location - map.RangeStart;
                    return map.SourceStart + n;
                }
            }

            return location;
        }

        static long SeedToLocation(long seed, List<Map> mapList)
        {
            foreach (var map in mapList)
            {
                if (seed >= map.SourceStart && seed < map.SourceStart + map.RangeLength)
                {
                    var n = seed - map.SourceStart;
                    return map.RangeStart + n;
                }
            }

            return seed;
        }


        public class Map
        {
            public long RangeStart { get; set; }
            public long SourceStart { get; set; }
            public long RangeLength { get; set; }

            public static Map Parse(string input)
            {
                var temp = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var outMap = new Map
                {
                    RangeStart = long.Parse(temp[0]),
                    SourceStart = long.Parse(temp[1]),
                    RangeLength = long.Parse(temp[2])
                };

                return outMap;
            }
        }
    }
}