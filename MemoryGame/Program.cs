// See https://aka.ms/new-console-template for more information
using MemoryGame;


var worldShuffler = new WordShuffler();
string[,] newGame = worldShuffler.randomWordsToList(2, 4);

new Board(newGame).draw();