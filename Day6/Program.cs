using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    internal class Program
    {
        static readonly int[] Time = new[] { 45, 98, 83, 73 };
        static readonly int[] Distance = new[] { 295, 1734, 1278, 1210 };
        static readonly int TimeP2 = 45988373;
        static readonly long DistanceP2 = 295173412781210;


        public static void Main(string[] args)
        {
            List<int> recordList = new List<int>();
            P1(recordList);
            int p1 = recordList.Aggregate((x, y) => x * y);
            int p2 = P2();
            
            Console.WriteLine(p1);
            Console.WriteLine(p2);
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

        private static int P2()
        {
            int recordCount = 0;
            for (long j = 0; j < TimeP2; j++)
            {
                long remainingTime = TimeP2 - j;
                if (j * remainingTime > DistanceP2)
                    recordCount++;
            }

            return recordCount;
        }
    }
}