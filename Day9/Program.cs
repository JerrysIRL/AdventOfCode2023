using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;

namespace Day9
{
    internal class Program
    {
        private static readonly string[] Input = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day9\Input.txt");

        public static void Main(string[] args)
        {
            List<List<int>> historyList = new List<List<int>>();
            foreach (var line in Input)
            {
                var numList = line.Split(' ').ToList().ConvertAll(e => Int32.Parse(e));
                historyList.Add(numList);
            }

            int sum = 0;
            
            foreach (var history in historyList)
            {
                List<List<int>> tempList = new List<List<int>>();
                tempList.Add(history);
                var temp = history;
                do
                {
                    temp = CreateNewRow(temp);
                    tempList.Add(temp);
                } while (ReachedZero(tempList));
                
                tempList.Reverse();
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (i + 1 >= tempList.Count)
                    {
                        
                        break;
                    }
                    int newnum = tempList[i].Last() + tempList[i + 1].Last();
                    tempList[i+1].Add(newnum);
                }

                sum += tempList.Last().Last();
            }
            
            Console.WriteLine(sum);

        }

        static bool ReachedZero(List<List<int>> input)
        {
            var boolList = input.Last().Select(n => n == 0);
            foreach (var b in boolList)
            {
                if (!b)
                {
                    return true;
                }
            }

            return false;
        }


        static List<int> CreateNewRow(List<int> history)
        {
            List<int> newRow = new List<int>();
            for (int i = 0; i < history.Count; i++)
            {
                if (i + 1 >= history.Count)
                {
                    break;
                }

                var result = history[i + 1] - history[i];
                newRow.Add(result);
            }

            return newRow;
        }

        static int FindDifference(int nr1, int nr2)
        {
            return nr1 - nr2;
        }

        // public class History
        // {
        //     public List<List<int>> Numbers = new List<List<int>>();
        //     //public int Difference { get; set; }
        // }
    }
}