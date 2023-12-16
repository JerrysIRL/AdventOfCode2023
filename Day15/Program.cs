using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    internal class Program
    {
        static readonly string Input = File.ReadAllText(@"E:\AdventOfCode\AdventOfCode2023\Day15\Input.txt");

        public static void Main(string[] args)
        {
            var sections = Input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var sectionValues = new List<int>();
            foreach (var s in sections)
            {
                int currentValue = 0;
                foreach (var c in s)
                {
                    var value = (int)c;
                    value += currentValue;
                    value *= 17;
                    value %= 256;
                    currentValue = value;
                }

                sectionValues.Add(currentValue);
            }

            var p1 = sectionValues.Sum(i => i);
            Console.WriteLine(p1);
        }
    }
}