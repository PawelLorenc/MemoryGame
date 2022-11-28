using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.IO;

namespace MemoryGame
{
    internal class GameLogic
    {
        private Board board;
        private string difficultyLevel;
        private int chancesLeft;
        private List<string> discovered = new List<string>();



        private void level(int chance, int rows, int columns, string difficultyLevel)
        {
 
            var worldShuffler = new WordShuffler();
            discovered.Clear();
            this.chancesLeft = chance;
            this.difficultyLevel = difficultyLevel;
            string[,] newGame = worldShuffler.randomWordsToList(rows, columns);
            this.board = new Board(newGame);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            while (chancesLeft > 0)
            {
                printGame();
                var field1 = readValidField();
                printGame();
                var field2 = readValidField();

                if (field1 != field2 && board.getWord(field1) == board.getWord(field2))
                {
                    if (!discovered.Contains(field1))
                    {
                        discovered.Add(field1);
                    }
                    if (!discovered.Contains(field2))
                    {
                        discovered.Add(field2);
                    }
                    if (discovered.Count == rows * columns)
                    {
                        break;
                    }
                    chancesLeft++;
                }
                chancesLeft--;

                printGame();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();

                board.hideWord(field1);
                board.hideWord(field2);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            printGame();
            if (chancesLeft > 0)
            {
                Console.WriteLine("Congratulations! You won the game in " + (chance - chancesLeft) + " moves.");
                Console.WriteLine("It took you " + elapsedMs / 1000 + " seconds to finish the game.");
                Console.WriteLine("Provide your name: ");
                string name = Console.ReadLine();
                addHighScore(elapsedMs, chance - chancesLeft, name);
                addHighScoreExpanded(elapsedMs, chance, difficultyLevel, name);

            } else
            {
                Console.WriteLine("You lost :(");
                Console.WriteLine("It took you " + elapsedMs / 1000 + " sseconds to finish the game.");
            }
        }

        private void printGame()
        {
            Console.Clear();
            Console.WriteLine("Difficulty level: " + difficultyLevel + ". " + "You have " + chancesLeft + " chances to guess.");
            for (int i = 0; i < discovered.Count; i++)
            {
                board.showWord(discovered[i]);
            }
            board.draw();
        }
        private string readValidField()
        {
            var field = Console.ReadLine().ToUpper();
            while (!board.validateField(field))
            {
                Console.WriteLine("Incorrect parameter");
                field = Console.ReadLine().ToUpper();
            }
            board.showWord(field);
            return field;
        }

        public void startTheGame()
        {
            while (true)
            {
                Console.WriteLine("Choose difficulty level. Type Hard or Easy.");
                string difficultyLevel = Console.ReadLine();
                while (!difficultyLevel.ToLower().Equals("easy") && !difficultyLevel.ToLower().Equals("hard"))
                {
                    Console.WriteLine("Incorrect difficulty level. Type Hard or Easy.");
                    difficultyLevel = Console.ReadLine();
                    if (difficultyLevel.ToLower().Equals("hard") || difficultyLevel.ToLower().Equals("easy"))
                    {
                        break;
                    }
                }

                if (difficultyLevel.ToLower().Equals("easy"))
                {
                    level(10, 2, 4, "easy");
                }

                if (difficultyLevel.ToLower().Equals("hard"))
                {
                    level(15, 4, 4, "hard");
                }

                Console.WriteLine("Would you like to play again? Type Yes to play or No to quit.");
                string playAgain = Console.ReadLine().ToLower();
                while (playAgain != "yes" && playAgain != "no")
                {
                    Console.WriteLine("Incorrect command. Type Yes to play again or No to quit the game.");
                    playAgain = Console.ReadLine().ToLower();

                }
                if (playAgain == "no")
                {
                    break;
                }
            }
        }
        private void addHighScore(long time, int chances, string name)
        {
            DateTime today = DateTime.Today;
            if (name == "")
            {
                name = "No name";
            }

            string filePath = @"highScore.txt";
            string stringToAdd = (name + " | " + today + " | " + chances + " | " + time / 1000+ "seconds\r\n");
            File.AppendAllText(filePath, stringToAdd);

        }
        private void addHighScoreExpanded(long time, int chances, string difficultyLevel, string name)
        {
            int weight = 0;
            if(difficultyLevel == "hard")
            {
                weight = 10;
            } else
            {
                weight = 1;
            }
            double points = (((1000 - (time / 1000)) * weight) / chances);
            if (name == "")
            {
                name = "No name";
            }

            string filePath = @"highScore2.txt";
            string stringToAdd = (name + "|" + points + " \r\n");
            File.AppendAllText(filePath, stringToAdd);
            var logFile = File.ReadAllLines(filePath);
            List<HighScoreEntry> list = new List<HighScoreEntry>();
            for(int i = 0; i < logFile.Length; i++)
            {
                var tokens = logFile[i].Split('|');
                list.Add(new HighScoreEntry(tokens[0], Int32.Parse(tokens[1])));
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int y = i+1; y < list.Count; y++)
                {
                    if(list[i].getScore() < list[y].getScore())
                    {
                        var temp = list[i];
                        list[i] = list[y];
                        list[y] = temp;
                    }
                }
            }
            for(int i =0; i < list.Count && i < 10;i++)
            {
                Console.WriteLine((i+1) + ". " + list[i].getScore() + " " + list[i].getPlayerName());
            }
        }
    }
}
