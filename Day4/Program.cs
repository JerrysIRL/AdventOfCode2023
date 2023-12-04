using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    internal class Program
    {
        public static string[] RawInput { get; } = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day4\Input.txt");

        public static void Main(string[] args)
        {
            List<Game> gameList = new List<Game>();
            int sum = 0;
            for (int i = 0; i < RawInput.Length; i++)
            {
                gameList.Add(Game.Parse(RawInput[i]));
                sum += Game.CalculateGameValue(gameList[i]);
            }
            
            Console.WriteLine(sum);
        }


        public class Game
        {
            private List<int> WinningNumbers { get; set; } = new List<int>();
            private List<int> CurrentNumbers { get; set; } = new List<int>();
            private int Value { get; set; } = 0;

            public static Game Parse(string input)
            {
                var trimmedInput = input.Split(':');
                Game outGame = new Game();
                var winningLine = trimmedInput[1].Split('|')[0];
                var yourLine = trimmedInput[1].Split('|')[1];

                outGame.WinningNumbers = winningLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                outGame.CurrentNumbers = yourLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                
                return outGame;
            }

            public static int CalculateGameValue(Game gameInfo)
            {
                foreach (var currNum in gameInfo.CurrentNumbers)
                {
                    if (gameInfo.WinningNumbers.Contains(currNum))
                    {
                       
                        gameInfo.Value = gameInfo.Value >= 1 ? gameInfo.Value *= 2 : gameInfo.Value = 1;
                    }
                }
                return gameInfo.Value;
            }
        }
    }
}