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
            List<List<int>> HistoryList = new List<List<int>>();
            foreach (var line in Input)
            {
                var numberLine = line.Split(' ').ToList().ConvertAll(e => Int32.Parse(e));
                HistoryList.Add(numberLine);
            }

           
            
        }
        
        public int FindDifference(int nr1, int nr2)
        {
            return Math.Abs(nr1 - nr2);
        }

        public class History
        {
            public List<int> Numbers = new List<int>();
            public int Difference { get; set; }
        }
        
        
    }
}