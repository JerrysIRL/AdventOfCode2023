using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
    internal class Program
    {
        private static readonly string[] Input = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day9\Input.txt");

        public static void Main(string[] args)
        {
            var historyList = new List<List<int>>();
            foreach (var line in Input)
            {
                var numList = line.Split(' ').ToList().ConvertAll(e => int.Parse(e));
                historyList.Add(numList);
            }

            var sum = 0;

            foreach (var history in historyList)
            {
                var tempList = new List<List<int>>();
                tempList.Add(history);
                var temp = history;
                do
                {
                    temp = CreateNewRow(temp);
                    tempList.Add(temp);
                } while (ReachedZero(tempList));

                tempList.Reverse();
                for (var i = 0; i < tempList.Count; i++)
                {
                    if (i + 1 >= tempList.Count) break;

                    var newnum = tempList[i + 1].First() - tempList[i].First();
                    tempList[i + 1].Insert(0, newnum);
                }

                sum += tempList.Last().First();
            }

            Console.WriteLine(sum);
        }

        private static bool ReachedZero(List<List<int>> input)
        {
            var boolList = input.Last().Select(n => n == 0);
            foreach (var b in boolList)
                if (!b)
                    return true;

            return false;
        }

        private static List<int> CreateNewRow(List<int> history)
        {
            var newRow = new List<int>();
            for (var i = 0; i < history.Count; i++)
            {
                if (i + 1 >= history.Count) break;

                var result = history[i + 1] - history[i];
                newRow.Add(result);
            }

            return newRow;
        }
    }
}