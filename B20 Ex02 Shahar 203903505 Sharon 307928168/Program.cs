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
            
            //GameHandler game = new GameHandler();
            //StringBuilder board = GameHandlerUI.GenerateConsoleBoard(4,6);
            //Console.WriteLine(board);
            //Console.WriteLine("-------------------------------------------");
            //char[,] test = Board.generateLettersGrid(6, 6);
            //Board pBoard = new Board(rows, columns);

            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < columns; j++)
            //    {
            //        Console.Write(String.Format("{0,-5}", pBoard.Matrix[i, j].Value));
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("\n-------------------------------------------\n");

            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < columns; j++)
            //    {
            //        Console.Write(String.Format("{0,-10}", pBoard.Matrix[i, j].IsRevealed));
            //    }
            //    Console.WriteLine();
            //}
            //Player player1 = new Player("Shahar");
            //Player player2 = new Player("Sharon");
            //Game game = new Game(4, 4, player1, player2);
            //eGameStatus gameStatus = eGameStatus.InProgress;
            //game.SingleMatch(player1, player2, out gameStatus);
            //game.PlayerVsPcMatch(player1, player2, out gameStatus);


            GameHandlerUI.LaunchGame();
        }
    }
}
