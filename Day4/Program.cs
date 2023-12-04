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
            var gameList = new List<Game>();
            var sum = 0;
            int amount;
            for (var i = 0; i < RawInput.Length; i++)
            {
                gameList.Add(Game.Parse(RawInput[i]));
                sum += Game.CalculateGameValue(gameList[i]);
            }

            //part two
            for (var i = 0; i < RawInput.Length; i++)
            {
                for (var j = 1; j <= gameList[i].Matches && i + j < RawInput.Length; j++)
                {
                    gameList[i + j].Iterations += gameList[i].Iterations;
                }
            }

            amount = gameList.Sum(game => game.Iterations);
            Console.WriteLine(sum);
            Console.WriteLine(amount);
        }

        public class Game
        {
            private List<int> WinningNumbers { get; set; } = new List<int>();
            private List<int> CurrentNumbers { get; set; } = new List<int>();
            private int Value { get; set; }
            public int Matches { get; set; }
            public int Iterations { get; set; } = 1;

            public static Game Parse(string input)
            {
                var trimmedInput = input.Split(':');
                var winningLine = trimmedInput[1].Split('|')[0];
                var yourLine = trimmedInput[1].Split('|')[1];

                var outGame = new Game();
                outGame.WinningNumbers = winningLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                outGame.CurrentNumbers = yourLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                return outGame;
            }

            public static int CalculateGameValue(Game gameInfo)
            {
                foreach (var currNum in gameInfo.CurrentNumbers)
                    if (gameInfo.WinningNumbers.Contains(currNum))
                    {
                        gameInfo.Matches += 1;
                        gameInfo.Value = gameInfo.Value >= 1 ? gameInfo.Value *= 2 : gameInfo.Value = 1;
                    }

                return gameInfo.Value;
            }
        }
    }
}