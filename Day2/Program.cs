using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting;

namespace Day2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var rawInput = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day2\Input.txt");

            int idSum = 0;
            for (int i = 0; i < rawInput.Length; i++)
            {
                GameInfo temp = GameInfo.Parse(rawInput[i]);
                bool validGame = false;
                foreach (var set in temp.SetList)
                {
                    validGame = set.IsValidSet(new SetInfo(12, 13, 14));
                    if (validGame == false)
                    {
                        break;
                    }
                }

                if (validGame)
                {
                    idSum += temp.Id;
                }
            }

            Console.WriteLine(idSum);
            Console.ReadLine();
        }

        public class GameInfo
        {
            public int Id { get; set; }
            public List<SetInfo> SetList { get; set; }

            public GameInfo(int id, List<SetInfo> setList)
            {
                Id = id;
                SetList = setList;
            }

            public static GameInfo Parse(string input)
            {
                var lineSplit = input.Split(':').ToList();
                var idSplit = lineSplit[0].Split(' ');
                int id = Int32.Parse(idSplit[1]);

                // sets
                var rawSetsArr = lineSplit[1].Split(';').Select(s => s.Trim()).ToList();
                var setsList = rawSetsArr.Select(s => SetInfo.Parse(s)).ToList();

                return new GameInfo(id, setsList);
            }
        }

        public class SetInfo
        {
            public int RedAmount { get; set; }
            public int GreenAmount { get; set; }
            public int BlueAmount { get; set; }

            public SetInfo(int red, int green, int blue)
            {
                RedAmount = red;
                GreenAmount = green;
                BlueAmount = blue;
            }

            public static SetInfo Parse(string input)
            {
                var colorArr = input.Split(',').Select(s => s.Trim()).ToList();
                SetInfo outSetInfo = new SetInfo(0, 0, 0);
                foreach (var colorEntry in colorArr)
                {
                    var numberArr = colorEntry.Split(' ').ToList();
                    int colorAmount = Int32.Parse(numberArr[0]);
                    if (numberArr[1] == "red")
                    {
                        outSetInfo.RedAmount = colorAmount;
                    }

                    if (numberArr[1] == "green")
                    {
                        outSetInfo.GreenAmount = colorAmount;
                    }

                    if (numberArr[1] == "blue")
                    {
                        outSetInfo.BlueAmount = colorAmount;
                    }
                }

                return outSetInfo;
            }

            public bool IsValidSet(SetInfo set) => RedAmount <= set.RedAmount
                                                   && GreenAmount <= set.GreenAmount
                                                   && BlueAmount <= set.BlueAmount;
        }
    }
}