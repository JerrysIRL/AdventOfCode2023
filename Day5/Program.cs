using System;
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
            var seeds = new List<uint>
            {
                1367444651, 99920667, 3319921504, 153335682, 67832336, 139859832, 2322838536, 666063790, 1591621692, 111959634, 442852010, 119609663, 733590868, 56288233, 2035874278, 85269124,
                4145746192, 55841637, 864476811, 347179760
            };
            var Sections = new List<List<Map>>();
            var sections = rawInput.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            Sections = sections.Select(s =>
            {
                var section = s.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                section.RemoveAt(0);
                var mapList = new List<Map>();
                foreach (var line in section) mapList.Add(Map.Parse(line));

                return mapList;
            }).ToList();

            for (var i = 0; i < seeds.Count; i++)
                foreach (var section in Sections)
                    seeds[i] = MapToRange(seeds[i], section);


            uint MapToRange(uint seed, List<Map> ss)
            {
                foreach (var map in ss)
                    if (seed >= map.SourceStart && seed < map.SourceStart + map.RangeLength)
                    {
                        var n = seed - map.SourceStart;
                        return map.RangeStart + n;
                    }

                return seed;
            }

            seeds.Sort();
            Console.WriteLine(seeds.Min());
        }

        public class Map
        {
            public uint RangeStart { get; set; }
            public uint SourceStart { get; set; }
            public uint RangeLength { get; set; }

            public static Map Parse(string input)
            {
                var temp = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var outMap = new Map
                {
                    RangeStart = uint.Parse(temp[0]),
                    SourceStart = uint.Parse(temp[1]),
                    RangeLength = uint.Parse(temp[2])
                };

                return outMap;
            }
        }
    }
}