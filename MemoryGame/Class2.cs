using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class WordShuffler
    {

        private List<string> DrawRandomWords (int numberOfWords)
        {
            List<string> listOfRandomWords = new List<string>();
            List<string> listCopy = loadWords();
            Random random = new Random();
            for (int i = 0; i < numberOfWords; i++)
            {
                int randomNumber = random.Next(0, listCopy.Count);
                listOfRandomWords.Add(listCopy.ElementAt(randomNumber));
                listCopy.RemoveAt(randomNumber); 
            }
            List<string> copyListOfRandomWords = new List<string>(listOfRandomWords);
            for(int i = 0; i < numberOfWords; i++)
            {
                int randomNumber = random.Next(0, numberOfWords + i);
                int randomNumber2 = random.Next(0, copyListOfRandomWords.Count());
                listOfRandomWords.Insert(randomNumber, copyListOfRandomWords.ElementAt(randomNumber2));
                copyListOfRandomWords.RemoveAt(randomNumber2);
            }
            return listOfRandomWords;

        }

        private List<string> loadWords()
        {
            List<string> wordList = new List<string>();
            wordList = MemoryGame.Resource1.Words.Split("\r\n").ToList();
            return wordList;
        
        }

        public string[,] randomWordsToList(int rows, int columns)
        {
            string[,] array2d = new string[rows, columns];
            int counter = 0;
            List<string> toLoad = DrawRandomWords((rows * columns) / 2);
            for(int i = 0; i < rows; i++)
            {
                for(int y = 0; y < columns; y++)
                {
                    array2d[i, y] = toLoad[counter++];
                }
            }
            return array2d;       
            
        }

    }
}
