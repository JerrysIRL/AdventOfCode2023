using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var rawInput = File.ReadAllLines("E:\\AdventOfCode\\AdventOfCode2023\\Day1\\Day1\\Input.txt");
            string[] input = new string[rawInput.Length];

            // part one
            int sum = 0;
            for (int i = 0; i < rawInput.Length; i++)
            {
                var str = new string((from c in rawInput[i] where char.IsDigit(c) select c).ToArray());

                if (str.Length < 1)
                {
                    str = string.Format("{0}{0}", str);
                }

                var firstAndLast = string.Format("{0}{1}", str[0], str[str.Length - 1]);
                input[i] = firstAndLast;

                sum += int.Parse(input[i]);
            }


            // part two
            var stringNumbers = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            sum = 0;
            for (int i = 0; i < rawInput.Length; i++)
            {
                var str = "";
                var line = rawInput[i];
                for (int k = 0; k < line.Length; k++)
                {
                    var c = line[k];
                    if (Char.IsNumber(c))
                    {
                        str += c;
                    }
                    else
                    {
                        for (int j = 0; j < stringNumbers.Length; j++)
                        {
                            if (k + stringNumbers[j].Length > line.Length)
                            {
                                continue;
                            }

                            var subStr = line.Substring(k, stringNumbers[j].Length);
                            if (subStr == stringNumbers[j])
                            {
                                str += (j + 1).ToString();
                            }
                        }
                    }
                }

                var firstAndLast = string.Format("{0}{1}", str[0], str[str.Length - 1]);
                input[i] = firstAndLast;
                
                sum += int.Parse(input[i]);
            }

            Console.WriteLine(sum);

            Console.ReadLine();
        }
    }
}