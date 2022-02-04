using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class Board
    {
        string[,] board;
        bool[,] visible;

        public Board(string[,] board)
        {
            this.board = board;

            this.visible = new bool[board.GetLength(0), board.GetLength(1)];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    visible[i, j] = false;
                }
            }
        }

        public bool validateField(string field)
        {
            if(field.Length != 2)
            {
                   return false;
            }
            int[] index = getIndex(field);
            return index[0] >= 0 && index[0] < board.GetLength(0) 
                && index[1] >= 0 && index[1] < board.GetLength(1);
        }

        int[] getIndex(string field)
        {
            field = field.ToUpper();
            return new int[] { field[0] - 'A', field[1] - '1' };
        }

        public void draw()
        {
            Console.Write("  ");
            for(int i = 1; i - 1 < board.GetLength(1); i++)
            {
                Console.Write(i.ToString().PadRight(20, ' '));
            }
            Console.WriteLine();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write(Convert.ToChar('A' + i) + " ");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    var word = visible[i, j] ? board[i, j] : "X";
                    Console.Write(word.PadRight(20, ' ')); 
                }
                Console.WriteLine();
            }          
        }

        public void hideWord(string field)
        {
            int[] index = getIndex(field);
            visible[index[0], index[1]] = false;
        }


        public void showWord(string field)
        {
            int[] index = getIndex(field);
            visible[index[0], index[1]] = true;
        }
        public string getWord(string field)
        {
            int[] index = getIndex(field);
            return board[index[0],index[1]];
        }
    }
}
