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

        public Board(string[,] board)
        {
            this.board = board;
        }

        public void draw()
        {
            Console.Write("  ");
            for(int i = 1; i - 1 < board.GetLength(0); i++)
            {
                Console.Write(i.ToString().PadRight(20, ' '));
            }
            Console.WriteLine();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write(Convert.ToChar('A' + i) + " ");
                for (int j = 0; j < board.GetLength(1); j++)
                {
            
                    Console.Write(board[i, j].PadRight(20, ' ')); 
                }
                Console.WriteLine();
            }
            

        }


    }
}
