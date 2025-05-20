using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumberGuessingGame.Utils;

namespace NumberGuessingGame.Resources
{
    public static class NumberGuessing
    {
        public static int Number { get; set; }
        public static int Attempts { get; set; } = 0;
        public static int MaxAttempts { get; set; }

        private static readonly Dictionary<int, string> _difficulty = new()
        {
            {(int)Difficulty.Easy, "Easy"},
            {(int)Difficulty.Medium, "Medium"},
            {(int)Difficulty.Hard, "Hard" }
        };


        public static void RenderMenu()
        {
            Console.WriteLine("Please select the difficulty level:");

            for (int i = 0; i < _difficulty.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_difficulty[i]}");
            }
            
            Console.Write("Enter your choice:");
        }

        public static void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case (int)Difficulty.Easy:
                    MaxAttempts = 10;
                    break;
                case (int)Difficulty.Medium:
                    MaxAttempts = 5;
                    break;
                case (int)Difficulty.Hard:
                    MaxAttempts = 3;
                    break;
            }
        }

        public static void GenerateNumber()
        {
            var random = new Random();
            Number = random.Next(1, 100);
        }

        public static bool CheckGuess(string guess)
        {
            if (CheckIsNaN(guess))
            {
                Console.WriteLine("Please enter a valid number.");
                return false;
            }

            var guessInt = int.Parse(guess);

            if (guessInt == Number)
            {
                Console.WriteLine(NumberGuessingGameResources.CongratulationsMessage);
                Attempts = 0;
                return true;
            }
            if (guessInt < Number)
            {
                Console.WriteLine(NumberGuessingGameResources.NumberGreaterThan.Replace("%guess%",guessInt.ToString()));
                Attempts++;
                return false;
            }

            Console.WriteLine(NumberGuessingGameResources.NumberIsLessThan.Replace("%guess%", guessInt.ToString()));
            Attempts++;
            return false;

        }

        private static bool CheckIsNaN(string guess)
        {
            return !int.TryParse(guess, out _);
        }
    }
}