using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class HighScoreEntry
    {
        private String playerName;
        private int score;

        public int getScore()
        {
            return this.score;
        }
        public string getPlayerName()
        {
            return this.playerName;
        }

        public HighScoreEntry(string playerName, int score)
        {
            this.score = score;
            this.playerName = playerName;
        }



    }




}
