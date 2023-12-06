using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    internal class Program
    {
        static readonly int[] Time = new[] { 45, 98, 83, 73 };
        static readonly int[] Distance = new[] { 295, 1734, 1278, 1210 };


        public static void Main(string[] args)
        {
            List<int> recordList = new List<int>();
            P1(recordList);
            
            int p1 = recordList.Aggregate((x, y) => x * y);
            Console.WriteLine(p1);
        }

        private static void P1(List<int> recordList)
        {
            for (int i = 0; i < Time.Length; i++)
            {
                int recordCount = 0;
                for (int j = 0; j < Time[i]; j++)
                {
                    var remainingTime = Time[i] - j;
                    if (j * remainingTime > Distance[i])
                        recordCount++;
                }
                recordList.Add(recordCount);
            }
        }
        
    }
}