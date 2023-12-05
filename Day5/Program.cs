﻿using System;
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
            //for (var i = 0; i < seeds.Count; i++)
            List<long> locations = new List<long>();

            for (int i = 0; i < seeds.Count; i += 2)
            {
                for (long j = seeds[i]; j < seeds[i] + seeds[i + 1]; j++)
                {
                    long x = j;
                    foreach (var section in Sections)
                    {
                        x = (MapToRange(x, section));
                    }
                    locations.Add(x);
                }
                Console.WriteLine(i);
            }

            long MapToRange(long seed, List<Map> ss)
            {
                foreach (var map in ss)
                    if (seed >= map.SourceStart && seed < map.SourceStart + map.RangeLength)
                    {
                        var n = seed - map.SourceStart;
                        return map.RangeStart + n;
                    }

                return seed;
            }

            Console.WriteLine(locations.Min());
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