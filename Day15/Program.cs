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
            List<Box> boxesList = new List<Box>();
            for (int i = 0; i < 256; i++)
            {
                var box = new Box()
                {
                    Id = i
                };
                boxesList.Add(box);
            }

            foreach (var s in sections)
            {
                //part 1
                var currentValue = CalculateHash(s);
                sectionValues.Add(currentValue);

                //part 2
                Lense lense = Lense.Parse(s);
                var labelHash = CalculateHash(lense.Label);
                var box = boxesList[labelHash];
                if (lense.Operation == 0)
                {
                    var lensToReplace = box.Lenses.Find(l => l.Label == lense.Label);
                    if (lensToReplace != null)
                    {
                        int index = boxesList[labelHash].Lenses.IndexOf(lensToReplace);
                        boxesList[labelHash].Lenses[index] = lense;
                    }
                    else
                        boxesList[labelHash].Lenses.Add(lense);
                }
                else
                {
                    var lensToRemove = box.Lenses.Find(l => l.Label == lense.Label);
                    if (lensToRemove != null)
                        box.Lenses.Remove(lensToRemove);
                }
            }

            var p1 = sectionValues.Sum(i => i);
            Console.WriteLine(p1);

            int p2 = 0;
            foreach (var box in boxesList)
            {
                foreach (var lense in box.Lenses)
                {
                    p2 += box.CalculateLensPower(lense);
                }
            }
            Console.WriteLine(p2);
        }

        private static int CalculateHash(string s)
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

            return currentValue;
        }

        private class Box
        {
            public int Id { get; set; }
            public List<Lense> Lenses { get; set; } = new List<Lense>();

            public int CalculateLensPower(Lense lense)
            {
                return (Id + 1) * (Lenses.IndexOf(lense) + 1) * lense.FocalLength;
            }
        }

        public class Lense
        {
            public string Label { get; set; }
            public byte Operation { get; set; } = 0;
            public int FocalLength { get; set; }

            public static Lense Parse(string input)
            {
                Lense outLense = new Lense();
                if (input.Contains('='))
                {
                    outLense.Operation = 0;
                    input = input.Replace('=', ' ');
                }
                else
                {
                    outLense.Operation = 1;
                    input = input.Replace('-', ' ');
                }

                var split = input.Split(' ');
                outLense.Label = split[0];
                if (outLense.Operation == 0)
                    outLense.FocalLength = Int32.Parse(split[1]);

                return outLense;
            }
        }
    }
}