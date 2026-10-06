public class Program
{
    public static void Main()
    {
        bool isPlaying = true;
        while(isPlaying){
            Console.WriteLine("\nWelcome to the Number Guessing Game!");
            string? difficultyChosen;
            int difficultyLevel = 0;
            while (difficultyLevel == 0)
            {
                Console.WriteLine("Please select the difficulty level:\n1. Easy (10 chances)\n2. Medium (5 chances)\n3. Hard (3 chances)");
                Console.Write("Enter your choice: ");
                difficultyChosen = Console.ReadLine();
                switch (difficultyChosen?.ToLower())
                {
                    case "1":
                    case "easy":
                        difficultyLevel = 1;
                        break;
                    case "2":
                    case "medium":
                        difficultyLevel = 2;
                        break;
                    case "3":
                    case "hard":
                        difficultyLevel = 3;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nPlease select a valid difficulty\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
            Console.WriteLine($"\nGreat! You have selected the {GetDifficultyName(difficultyLevel) } difficulty level.");
            Random numberGen = new Random();
            int numberToGuess = numberGen.Next(1,101);
            Console.WriteLine("I'm thinking of a number between 1 and 100.");
            int attempts = GetAttempts(difficultyLevel);
            int totalAttempts = 0;
            Console.WriteLine($"You have {attempts} attempts to guess.");
            Console.WriteLine("Let's start the game!");

            bool hasWon = false;

            string? guessInput;
            while (totalAttempts < attempts && !hasWon)
            {
                Console.Write("\nEnter your guess: ");
                guessInput = Console.ReadLine();
                if (int.TryParse(guessInput, out int guessValue))
                {
                    if (guessValue <= 0 || 100 < guessValue)
                    {
                        Console.WriteLine("I'm thinking of a number between 1 and 100, please pick a number between 1 and 100");
                    }
                    else
                    {
                        totalAttempts++;
                        if (guessValue == numberToGuess)
                        {
                            hasWon = true;
                        }
                        else
                        {
                            Console.Write("Incorrect! My number is ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string guessComparison = numberToGuess < guessValue  ? "smaller" : "greater";
                            Console.Write(guessComparison);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($" than {guessValue}.\n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            }

            if (hasWon)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"\nCongratulations, guessed the number in {totalAttempts} attempts!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine($"\nSorry, you did not guess my number.\nMy number was {numberToGuess}.\nBetter luck next time");
            }

            Console.Write("Do you want to play again? (yes/no)");
            string? playAgain = Console.ReadLine();

            switch (playAgain?.ToLower())
            {
                case "1":
                case "y":
                case "ye":
                case "yes":
                    isPlaying = true;
                    break;
                default:
                    isPlaying = false;
                    break;
            }
        }
        Console.WriteLine("Thanks for playing! See you next time");
    }

    private static int GetAttempts(int difficultyLevel)
    {
        return difficultyLevel switch
        {
            1 => 10,
            2 => 5, 
            3 => 3,
            _ => 0
        };
    }

    public static string GetDifficultyName(int difficultyLevel)
    {
        return difficultyLevel switch
        {
            1 => "easy",
            2 => "medium",
            3 => "hard",
            _ => "invalid"
        };
    }
}