using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class Program
    {
        private const int columns = 6;
        private const int rows = 6;
        public static void Main()
        {
            //StringBuilder board = GameHandler.GenerateConsoleBoard(4,6);
            //Console.WriteLine(board);
            //Console.WriteLine("-------------------------------------------");
            //char[,] test = Game.generateLettersGrid(6, 6);
            //Game game = new Game(rows, columns);
            
            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < columns; j++)
            //    {
            //        Console.Write(String.Format("{0,-5}", playingBoard.PlayingBoard[i, j].Value));
            //    }
            //    Console.WriteLine();
            //}
            //Console.ReadLine();

            
            //test board print with reaveled squares
            Board playingBoard = new Board(4, 6);
            playingBoard.RevealBoardSquare(0, 0);
            playingBoard.RevealBoardSquare(1, 1);
            playingBoard.RevealBoardSquare(2, 2);
            playingBoard.RevealBoardSquare(3, 3);
            
            ConsoleUI ui = new ConsoleUI();
            ui.DrawBoard(playingBoard);

            Console.ReadLine();
        }
    }
}
