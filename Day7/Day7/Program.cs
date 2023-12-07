using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day7
{
    internal class Program
    {
        static readonly string[] RawInput = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day7\Input.txt");

        public enum HandType
        {
            Undefined,
            High,
            OnePair,
            TwoPair,
            Triple,
            FullHouse,
            Quad,
            Five
        }

        static readonly char[] Symbols = new[] { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };

        public static void Main(string[] args)
        {
            List<Hand> HandList = new List<Hand>();
            foreach (var line in RawInput)
            {
                var temp = line.Split(' ');
                var value = temp[0].GroupBy(c => c).Select(g => new Tuple<char, int>(g.Key, g.Count())).ToList();
                var jAmount = value.SingleOrDefault(t => t.Item1 == 'J')?.Item2 ?? 0;
                var filteredValue = value.Where(t => t.Item1 != 'J').ToList();
                //Console.WriteLine(jNumber);

                Hand hand = new Hand()
                {
                    Cards = temp[0],
                    Bid = Int32.Parse(temp[1]),
                    Type = Hand.GetType(filteredValue, jAmount)
                };
                HandList.Add(hand);
            }
            //HandList.ForEach(s => Console.WriteLine(s.Cards + " " +  s.Type));


            var sortedList = HandList.OrderBy(h => h).ToList();
            //sortedList.ForEach(s => Console.WriteLine(s.Cards + " " + s.Type));
            //
            var sum = sortedList.Select((x, i) => x.Bid * (i + 1)).Sum();
            Console.WriteLine(sum);

            Console.ReadLine();
        }

        public class Hand : IComparable<Hand>
        {
            public string Cards { get; set; }
            public int Bid { get; set; }
            public HandType Type { get; set; }

            public static HandType GetType(List<Tuple<char, int>> tupleList, int jokers)
            {
                if (tupleList.Count == 5)
                {
                    return HandType.High;
                }

                if (tupleList.Count == 4)
                {
                    return HandType.OnePair;
                }

                if (tupleList.Count == 3 && tupleList.Max(t => t.Item2 + jokers) == 2)
                {
                    return HandType.TwoPair;
                }

                if (tupleList.Count == 3 && tupleList.Max(t => t.Item2 + jokers) == 3)
                {
                    return HandType.Triple;
                }

                if (tupleList.Count == 2 && tupleList.Max(t => t.Item2 + jokers) == 3)
                {
                    return HandType.FullHouse;
                }

                if (tupleList.Count == 2 && tupleList.Max(t => t.Item2 + jokers) == 4)
                {
                    return HandType.Quad;
                }

                if (tupleList.Count == 1 && tupleList.Max(t => t.Item2 + jokers) == 5 || jokers == 5)
                {
                    return HandType.Five;
                }

                return HandType.Undefined;
            }

            public int CompareTo(Hand other)
            {
                var subtract = Type - other.Type;

                if (subtract != 0)
                {
                    return subtract;
                }

                for (int i = 0; i < this.Cards.Length; i++)
                {
                    var cOne = Array.IndexOf(Symbols, this.Cards[i]);
                    var cTwo = Array.IndexOf(Symbols, other.Cards[i]);
                    if (cOne > cTwo)
                    {
                        return 1;
                    }

                    if (cTwo > cOne)
                    {
                        return -1;
                    }
                }

                return 0;
            }
        }
    }
}