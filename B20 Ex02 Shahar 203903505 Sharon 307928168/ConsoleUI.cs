using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02_MemoryGame
{

    public class ConsoleUI
    {
        public void DrawLettersBoard(Board i_PlayingBoard)
        {
            char letterToPrint;

            Ex02.ConsoleUtils.Screen.Clear();
            Console.Write("    ");
            for (int j = 0; j < i_PlayingBoard.BoardSize.Y; j++)
            {
                Console.Write(String.Format("{0, -4}", (char)('A' + j)));
            }

            Console.WriteLine();
            StringBuilder seperatorLine = new StringBuilder();
            seperatorLine.Append("  ");
            seperatorLine.Append('=', i_PlayingBoard.BoardSize.Y * 4 + 1);
            Console.WriteLine(seperatorLine);

            for (int i = 0; i < i_PlayingBoard.BoardSize.X; i++)
            {
                Console.Write(String.Format("{0} |", i + 1));
                for (int j = 0; j < i_PlayingBoard.BoardSize.Y; j++)
                {
                    if (i_PlayingBoard.Matrix[i, j].IsRevealed)
                    {
                        letterToPrint = (char)(i_PlayingBoard.Matrix[i, j].Value + 'A');
                        Console.Write(String.Format(" {0} ", letterToPrint));
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

        public void GetBoardDimentions(out int o_NumOfCols, out int o_NumOfRows)
        {
            Console.WriteLine(string.Format(
@"Please enter the desired board size:
1. 4X4
2. 4X6
3. 6X4
4. 6X6"));
            bool isSelectionValid = true;
            do
            {
                string userInput = Console.ReadLine();
                o_NumOfCols = 0;
                o_NumOfRows = 0;

                switch (userInput)
                {
                    case "1":
                        o_NumOfCols = 4;
                        o_NumOfRows = 4;
                        break;

                    case "2":
                        o_NumOfCols = 4;
                        o_NumOfRows = 6;
                        break;

                    case "3":
                        o_NumOfCols = 6;
                        o_NumOfRows = 4;
                        break;

                    case "4":
                        o_NumOfCols = 6;
                        o_NumOfRows = 6;
                        break;

                    default:
                        Console.WriteLine(string.Format("Invalid board size selection!{0}Please try again...", Environment.NewLine));
                        isSelectionValid = false;
                        break;
                }
            }
            while (!isSelectionValid);

        }

        public void GetPlayingMode(out Game.ePlayingMode o_PlayingMode)
        {
            Console.WriteLine(String.Format(
@"Please select the desired playing mode:
1. Multiplayer (Player VS Player)
2. Single (VS PC)"
            ));

            do
            {
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        o_PlayingMode = Game.ePlayingMode.PlayerVsPlayer;
                        break;

                    case "2":
                        o_PlayingMode = Game.ePlayingMode.PlayerVsPc;
                        break;

                    default:
                        Console.WriteLine(string.Format("Invalid game mode selection!{0}Please try again...", Environment.NewLine));
                        o_PlayingMode = Game.ePlayingMode.InvalidModeSelection;
                        break;
                }
            }
            while (o_PlayingMode == Game.ePlayingMode.InvalidModeSelection);
        }

        public string GetUserName()
        {
            Console.WriteLine("Please enter player name: ");
            string userName = Console.ReadLine();
            return userName;
        }

        public void PrintWelcomeMessage()
        {
            Console.WriteLine("Hello! Welcome to Memory-Game");
        }

        public void GetUserChoice(out Point o_SquareSelection, out bool o_ToQuit)
        {
            Console.Write("Please select a square to reveal, or enter 'Q' to quit current game:");
            string userChoice = Console.ReadLine();
            o_SquareSelection = null;
            o_ToQuit = false;

            if (!userChoice.Equals("Q"))
            {
                while (!isValidChoice(userChoice))
                {
                    Console.WriteLine(string.Format(
@"Your selection is not in the right format.
Please enter the column Letter followed by the row number with no spaces in between (e.g: B3)"));
                    userChoice = Console.ReadLine();
                }

                int matrixCol = convertColumn(userChoice[0]);
                int matrixRow = convertRow(userChoice[1]);

                o_SquareSelection = new Point(matrixRow, matrixCol);
            }
            else
            {
                o_ToQuit = true;
            }

        }

        private int convertColumn(char i_ColumnLetter)
        {
            return i_ColumnLetter - 'A';
        }

        private int convertRow(char i_Row)
        {
            return (int)Char.GetNumericValue(i_Row) - 1;
        }

        private bool isValidChoice(string i_userChoice)
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

        public void PrintAnotherRoundMessage()
        {
            Console.WriteLine("Would you like another round? Press 'y' for yes, press any other key to quit");
        }

        public void PrintTileOutOfRange()
        {
            Console.WriteLine("The tile you chose is out of board range. Please try again: ");
        }

        public void PrintTileAlreadyRevealed()
        {
            Console.WriteLine("The tile you chose has already been revealed. Please try again: ");
        }

        public void PrintGoodByeMessage()
        {
            Console.WriteLine("Thank you for playing, Goodbye! (:");
        }

        public void PrintEndGameSummary(Game i_Game, Game.eGameStatus i_GameStatus)
        {
            StringBuilder gameResultMessage = new StringBuilder();  

            switch (i_GameStatus)
            {
                case Game.eGameStatus.FirstPlayerWon:
                    gameResultMessage.Append("The Winner is: ");
                    gameResultMessage.Append(i_Game.FirstPlayer.Name);
                    break;

                case Game.eGameStatus.SecondPlayerWon:
                    gameResultMessage.Append("The Winner is: ");
                    gameResultMessage.Append(i_Game.SecondPlayer.Name);
                    break;

                case Game.eGameStatus.GameTie:
                    gameResultMessage.Append("Game Tie!");
                    break;
            }

            string summaryMessage = string.Format(
@"Game finished!
{0}
{1}'s score: {2}
{3}'s score: {4}",
                gameResultMessage,
                i_Game.FirstPlayer.Name,
                i_Game.FirstPlayer.Score,
                i_Game.SecondPlayer.Name,
                i_Game.SecondPlayer.Score);
            
            Console.WriteLine(summaryMessage);
        }
    }
}
