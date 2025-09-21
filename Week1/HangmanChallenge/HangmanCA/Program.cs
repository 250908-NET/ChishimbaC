using System;

namespace HangmanCA
{
    class Program
    {
        static readonly string[] wordlist = { 
            "word", 
            "sharp", 
            "random", 
            "rhythm", 
            "jazz", 
            "read", 
            "tree", 
            "hangman", 
            "bird", 
            "food" 
        }; 
        
        public static void Main(string[] args)
        {
            // indicate application startup
            Console.WriteLine("Starting Hangman Game...");
            
            Random rand = new Random();
            string word = wordlist[rand.Next(wordlist.Length)];
            char[] displayWords = new string('_', word.Length).ToCharArray();
            int lives = 6; // lose at zero
            List<char> guessedLetters = new List<char>(); 
            List<char> wrongUsedLetters = new List<char>(); 
            

            // placeholder for game setup and initlialization
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("=== HANGMAN GAME ===");
            // placeholder for game loop
            while (lives > 0) {
                // print header
                Console.WriteLine($"Lives left: {lives}\n");
                Console.WriteLine($"(insert hangman image here)");
                Console.WriteLine($"\t{string.Join("", displayWords)}"); // print displayWords
                string incorrectLetters = (wrongUsedLetters.Count > 0) ? string.Join(", ", wrongUsedLetters) : "nothing";
                Console.WriteLine($"Incorrect guesses: {incorrectLetters}"); // print already guessed
                Console.Write($"> "); // input request

                // input letter
                string input = Console.ReadLine() ?? ""; 
                
                // normalize input (lowercase, trim whitespace)
                input = input.Trim(); 
                input = input.ToLower();

                // validate: must be length 1
                if (input.Length < 1) {
                    Console.WriteLine("Error: Please enter a letter to guess!\n");
                    continue;
                } else if (input.Length > 1) {
                    Console.WriteLine("Error: Please enter only one letter!\n");
                    continue;
                } 

                // convert string to char
                char guessedLetter = char.Parse(input);
                
                // calidate: must be a valid letter in the english alphabet
                if (guessedLetter < 97 || guessedLetter > 122) {
                    // not a letter
                    Console.WriteLine("Error: Please enter a valid english letter!\n");
                    continue;
                }

                // is letter used already?
                if (guessedLetters.Contains(guessedLetter)) {
                    Console.WriteLine("You already guessed that letter!\n");
                    continue;
                } else {
                    // add letter to guessedLetters
                    guessedLetters.Add(guessedLetter);
                }
                
                // is letter in word?
                if (word.Contains(guessedLetter)) {
                    // correct guess
                    Console.WriteLine("Correct guess!\n");
                    for (int i = 0; i < word.Length; i++) {
                        if (word[i] == guessedLetter) {
                            displayWords[i] = guessedLetter;
                        }
                    }
                } else {
                    // incorrect guess, lose a life
                    Console.WriteLine("Incorrect guess...\n");
                    wrongUsedLetters.Add(guessedLetter);
                    lives--;
                }

                // did you win yet
                if (!displayWords.Contains('_')) {
                    break;
                }
            }

            Console.WriteLine($"The word was: {word}");            

            // if lives > 0, you won! yay!
            if (lives > 0) {
                
                Console.WriteLine("You won! Yay!\n");
            } else {
                Console.WriteLine("You lost... Better luck next time!\n");
            }

            // indicate application shutdown
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Console.WriteLine("Exiting Hangman Game...");
        }
    }
}
