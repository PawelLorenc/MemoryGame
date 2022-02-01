// See https://aka.ms/new-console-template for more information
using MemoryGame;

var test = new string[4, 4] { 
    { "xxxxxx", "y", "XXXXXXXXXXXX", "XXXX"},
    { "a", "bbbb", "XXXXXXXXXXXX", "ZZZZ" },
    { "xxxxxx", "y", "XXXXXXXXXXXX", "XXXX"}, 
    { "xxxxxx", "y", "XXXXXXXXXXXX", "XXXX"} } ;


new Board(test).draw();