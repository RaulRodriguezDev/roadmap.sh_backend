using NumberGuessingGame.Resources;
using System.Runtime.CompilerServices;

var isStillPlaying = true;

while (isStillPlaying)
{
    Console.Clear();
    Console.WriteLine(NumberGuessingGameResources.WelcomeMessage);
    Console.WriteLine(NumberGuessingGameResources.GameInstructions);
    NumberGuessing.RenderMenu();
    var difficulty = Console.ReadLine();

    if (string.IsNullOrEmpty(difficulty))
    {
        Console.WriteLine("Please select a valid option.");
        continue;
    }

    else
    {
        Console.Clear();
        NumberGuessing.SetDifficulty(int.Parse(difficulty));
        NumberGuessing.GenerateNumber();

        while (NumberGuessing.Attempts < NumberGuessing.MaxAttempts)
        {
            Console.WriteLine($"Attempts Reamining: {NumberGuessing.MaxAttempts - NumberGuessing.Attempts}");
            Console.WriteLine("Enter your guess:");
            var guess = Console.ReadLine();

            if (string.IsNullOrEmpty(guess))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }
            else
            {
                var response = NumberGuessing.CheckGuess(guess);

                if(!response && NumberGuessing.Attempts >= NumberGuessing.MaxAttempts)
                {
                    Console.WriteLine(NumberGuessingGameResources.GameOverMessage.Replace("%number%",NumberGuessing.Number.ToString()));
                }
            }
        }

        Console.WriteLine(NumberGuessingGameResources.PlayAgainMessage);
        var playAgain = Console.ReadLine();

        isStillPlaying = playAgain?.ToLower() == "y";
    }
}
