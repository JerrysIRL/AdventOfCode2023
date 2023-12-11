using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    internal class Program
    {
        private static readonly string Input =
            File.ReadAllText(@"E:\AdventOfCode\AdventOfCode2023\Day8\Input.txt");

        public static void Main(string[] args)
        {
            var map = new Dictionary<string, Tuple<string, string>>();
            
            var split = Input.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var instructions = split[0];

            var lines = split[1].Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var line in lines)
            {
                var key = line.Substring(0, 3);
                var n1 = line.Substring(7, 3);
                var n2 = line.Substring(12, 3);
                map.Add(key, new Tuple<string, string>(n1, n2));
            }

            //part 1
            var i = 0;
            var startEntries = map.Keys.Where(k => k[k.Length - 1] == 'A').ToList();
            var ghostsIteration = new List<int>();

            foreach (var entry in startEntries)
            {
                var mkey = entry;
                var iterations = 0;
                do
                {
                    if (i >= instructions.Length) i = 0;

                    if (instructions[i] == 'R')
                        mkey = map[mkey].Item2;
                    else
                        mkey = map[mkey].Item1;

                    i++;
                    iterations++;
                } while (mkey[entry.Length - 1] != 'Z');

                ghostsIteration.Add(iterations);
            }

            //part 2
            long result = ghostsIteration[0];
            for (var j = 0; j < ghostsIteration.Count - 1; j++)
            {
                result = Lcm(result, ghostsIteration[j + 1]);
            }

            Console.WriteLine(result);
        }

        private static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);

        private static long Lcm(long a, long b)
        {
            return Math.Abs(a * b) / Gcd(a, b);
        }
    }
}