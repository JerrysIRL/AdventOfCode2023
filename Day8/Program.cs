using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Day8
{
    internal class Program
    {
        private static string input =
            File.ReadAllText(@"C:\Users\sergei.maltcev\Projects\AdventOfCode2023\Day8\Input.txt");

        public static void Main(string[] args)
        {
            Dictionary<string, Tuple<string, string>> map = new Dictionary<string, Tuple<string, string>>();
            var split = input.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var instructions = split[0];
            
            var lines = split[1].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var line in lines)
            {
                var key = line.Substring(0, 3);
                var n1 = line.Substring(7, 3);
                var n2 = line.Substring(12, 3);
                map.Add(key, new Tuple<string, string>(n1, n2));
            }

            //part 1
            string mkey = "AAA";
            int i = 0;
            long iterations = 0;

            var startEnties = map.Keys.Where(k => k[k.Length - 1] == 'A').ToList();
            startEnties.ForEach(s => Console.WriteLine(s));


            bool IsAllEntriesZ()
            {
                var bList = startEnties.Select(s =>s[s.Length-1] == 'Z').ToList();
                foreach (var b in bList)
                {
                    if (!b)
                    {
                        return true;
                    }
                }

                return false;
            }
            /*do
            {
                
                if (i >= instructions.Length)
                {
                    iterations += i;
                    i = 0;
                }
                if (instructions[i] == 'R')
                {
                    mkey = map[mkey].Item2;
                }
                else
                {
                    mkey = map[mkey].Item1;
                }
                i++;
                //iterations++;
            } while (mkey !="ZZZ");*/
            
            Console.WriteLine(iterations + i);
            //Console.WriteLine(mkey);
        }
    }
}