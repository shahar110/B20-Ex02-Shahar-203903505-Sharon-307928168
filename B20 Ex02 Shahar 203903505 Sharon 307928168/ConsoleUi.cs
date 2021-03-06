﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class ConsoleUI
    {
        public void DrawBoard(Board i_PlayingBoard)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.Write("    ");
            for (int j = 0; j < i_PlayingBoard.NumOfCols; j++)
            {
                Console.Write(String.Format("{0, -4}", (char)('A' + j)));
            }

            Console.WriteLine();
            StringBuilder seperatorLine = new StringBuilder();
            seperatorLine.Append("  ");
            seperatorLine.Append('=', i_PlayingBoard.NumOfCols * 4 + 1);
            Console.WriteLine(seperatorLine);
            
            for (int i = 0; i < i_PlayingBoard.NumOfRows; i++)
            {
                Console.Write(String.Format("{0} |", i+1));
                for (int j = 0; j < i_PlayingBoard.NumOfCols; j++)
                {
                    if (i_PlayingBoard.BoardMatrix[i,j].IsReaveled)
                    {
                        Console.Write(String.Format(" {0} ", i_PlayingBoard.BoardMatrix[i, j].Value));
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    Console.Write("|");
                }
                Console.WriteLine();
                Console.Write(seperatorLine);
                Console.WriteLine();
            }
        }
           
        //%- change the the code repetition
        public void GetBoardDimentions(out int o_NumOfCols, out int o_NumOfRows)
        {
            Console.WriteLine("Please enter requested board size (available sizes are 4X4, 4X6, 6X4 or 6X6).");

            Console.Write("Number of columns: ");
            int requestedNumOfCols;
            string numOfCols = Console.ReadLine();
        
            while (!int.TryParse(numOfCols, out requestedNumOfCols))
            {
                Console.Write("Bad input, please enter a valid number:");
                numOfCols = Console.ReadLine();
            }

            Console.Write("Number of rows: ");
            int requestedNumOfRows;           
            string numOfRows = Console.ReadLine();

            while (!int.TryParse(numOfCols, out requestedNumOfRows))
            {
                Console.WriteLine("Bad input, please enter a valid number:");
                numOfCols = Console.ReadLine();
            }

            o_NumOfCols = requestedNumOfCols;
            o_NumOfRows = requestedNumOfRows;
        }
                
        public Point GetUserChoice()
        {
            Console.Write("Please choose a tile: ");
            string userChoice = Console.ReadLine();

            while(!isValidChoice(userChoice))
            {
                Console.WriteLine("Your choice is not of type Letter and number, please try again:");
                userChoice = Console.ReadLine();
            }

            int matrixCol = convertColumn(userChoice[0]);
            int matrixRow = convertRow(userChoice[1]);

            return new Point(matrixRow,matrixCol);
        }


        private int convertColumn(char i_ColumnLetter)
        {
            return i_ColumnLetter - 'A';
        }

        public void GetSecondPlayerType(out GameHandlerUI.ePlayerType o_secondPlayerType)
        {
            GameHandlerUI.ePlayerType secondPlayerTypeChoice = GameHandlerUI.ePlayerType.IsHuman;
            Console.WriteLine("Press 'y' to play against human player, or " +
                              "press any other key to play againt the computer");
            string userInput = Console.ReadLine();

            if (!userInput.Equals("y"))
            {
                secondPlayerTypeChoice = GameHandlerUI.ePlayerType.IsPC;
            }

            o_secondPlayerType = secondPlayerTypeChoice;
        }

        private int convertRow(char i_Row)
        {          
            return (int)Char.GetNumericValue(i_Row) - 1;
        }

        private bool isValidChoice (string i_userChoice)
        {
            bool isValidChoice = true;

            if (i_userChoice.Length != 2)
            {
                isValidChoice = false;
            }

            else
            {
                if (!(i_userChoice[0] >= 'A' && i_userChoice[0] <= 'Z'))
                {
                    isValidChoice = false;
                }
                else
                {
                    if (!Char.IsNumber(i_userChoice[1]))
                    {
                        isValidChoice = false;
                    }
                }
            }

            return isValidChoice;
        }

        public string GetUserName()
        {
            Console.WriteLine("Please enter player name: ");
            string userName = Console.ReadLine();
            return userName;
        }

        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("Hello! Welcome to Memory-Game");
        }

        public static void PrintEndGameSummery(Game i_Game)
        {
            string summeryMessage = string.Format(
@"Game finished!
The Winner is: {0}
{1}'s score: {2}
{3}'s score: {4}",
                i_Game.Winner.Name,
                i_Game.FirstPlayer.Name,
                i_Game.FirstPlayerScore,
                i_Game.SecondPlayer.Name,
                i_Game.SecondPlayerScore);

            Console.WriteLine(summeryMessage);
        }

        public static void PrintAnotherRoundMessage()
        {
            Console.WriteLine("Would you like another round?");
        }

        public void printTileOutOfRange()
        {
            Console.WriteLine("The tile you chose is out of board range. Please try again: ");
        }

        public void printTileAlreadyRevealed()
        {
            Console.WriteLine("The tile you chose has already been revealed. Please try again: ");
        }

        public static void PrintGoodByeMessage()
        {
            Console.WriteLine("Thank you for playing, Goodbye! (:");
        }

        //public static StringBuilder GenerateConsoleBoard(int i_NumOfRows, int i_NumOfCols)
        //{
        //    StringBuilder board = new StringBuilder();
        //    board.Append("   ");
        //    for (int j = 0; j < i_NumOfCols; j++)
        //    {
        //        board.Append(String.Format("  {0} ",  (char)('A' + j) ));
        //    }
        //    board.AppendLine();
        //    board.Append("   ");
        //    board.Append('=', i_NumOfCols * 4 + 1);
        //    board.AppendLine();

        //    for (int i = 1; i <= i_NumOfRows; i++)
        //    {
        //        board.Append(String.Format(" {0} ",i.ToString()));
        //        // %TBD - remove secondary loop
        //        for (int j = 0; j <= i_NumOfCols; j++)
        //        {
        //            board.Append("|   ");
        //        }
        //        board.AppendLine();
        //        board.Append("   ");
        //        board.Append('=', i_NumOfCols * 4 + 1);
        //        board.AppendLine();
        //    }

        //    return board;
        //}
    }
}
