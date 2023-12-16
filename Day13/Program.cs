using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day13
{
    internal class Program
    {
        private static string input = File.ReadAllText(@"E:\AdventOfCode\AdventOfCode2023\Day13\Input.txt");

        public static void Main(string[] args)
        {
            var patterns = input.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);


            int sum = 0;
            foreach (var pattern in patterns)
            {
                var lines = pattern.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var rows = AddRows(lines);
                var nonUnique = GetNonUnique(rows);
                var diff = rows.Count - nonUnique.Count;
                Console.WriteLine(diff);

                if (FindReflection(nonUnique, diff))
                {
                }

                //Console.WriteLine(nonUnique.Count / 2 + diff);
                Console.WriteLine("---------");

                nonUnique = GetNonUnique(lines);
                diff = lines.Count - nonUnique.Count;
                if (FindReflection(nonUnique, diff))
                {
                }
                

                Console.WriteLine("---------");
            }
        }


        private static bool FindReflection(List<string> input, int diff)
        {
            int matchCount = 0;
            for (int h = 0; h < input.Count; h++)
            {
                if (h + 1 >= input.Count)
                    break;
                if (String.Equals(input[h], input[h + 1]))
                {
                    matchCount++;
                    if (matchCount >= 1)
                    {
                        Console.WriteLine("Reflection found at index: " + (h + 1 + diff));
                        return true;
                    }
                }
            }

            return false;
        }

        private static List<string> AddRows(List<string> lines)
        {
            List<string> rows = new List<string>();
            for (int i = 0; i < lines[0].Length; i++)
            {
                string row = "";
                for (int j = 0; j < lines.Count; j++)
                {
                    row += lines[j][i];
                }

                rows.Add(row);
            }

            return rows;
        }


        private static List<string> GetNonUnique(List<string> rows)
        {
            var groupedRows = rows.GroupBy(r => r);
            var nonUniqueRows = groupedRows.Where(g => g.Count() > 1).SelectMany(g => g).ToList();

            //nonUniqueRows.ForEach(s =>Console.WriteLine(s));
            return rows.Where(row => nonUniqueRows.Contains(row)).ToList();
        }
    }
}