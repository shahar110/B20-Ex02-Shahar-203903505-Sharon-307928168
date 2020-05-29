using System;
using System.Text;

namespace ConsoleUI
{
    public class GameHandlerUI
    {
        //private Game currentGame;

        public static void Start()
        {
            Console.WriteLine();
        }

        private static void notifyMoveResult(eSquareSelectionStatus squareSelectionStatus, Point i_SelectedSquare)
        {
            switch (squareSelectionStatus)
            {
                case eSquareSelectionStatus.IllegalSquareSelection:
                    Console.WriteLine(string.Format("Invalid square {0} selection!", i_SelectedSquare.GetFormated()));
                    break;

                case eSquareSelectionStatus.SquareAlreadyRevealed:
                    Console.WriteLine(string.Format("Square {0} is already revealed!", i_SelectedSquare.GetFormated()));
                    break;

                case eSquareSelectionStatus.PlayerRevealedFirstSquare:
                    Console.WriteLine(string.Format("Square {0} is now revealed!", i_SelectedSquare.GetFormated()));
                    break;

                case eSquareSelectionStatus.PlayerRevealedSecondSquare:
                    Console.WriteLine(string.Format("Square {0} of value is now revealed!", i_SelectedSquare.GetFormated()));
                    break;

                case eSquareSelectionStatus.SquaresMatch:
                    Console.WriteLine(string.Format("Squares MATCH!!!"));
                    break;

                case eSquareSelectionStatus.SquaresDontMatch:
                    Console.WriteLine(string.Format("Squares DOESN'T match!"));
                    break;

                default:
                    break;
            }
        }

        public static void PlayerUiMatch()
        {
            Player player1 = new Player("Shahar");
            Player player2 = new Player("Sharon");
            Game game = new Game(4, 4, player1, player2);
            //PrintValueBoard();
            //PrintRevealedBoard();
            eGameStatus gameStatus;
            eSquareSelectionStatus squareSelectionStatus;
            do
            {
                Point playerSelectedSquare = new Point();
                string userInput;
                int parsedUserInput;

                Console.WriteLine(string.Format("Please enter square row:"));
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out parsedUserInput))
                {
                    playerSelectedSquare.X = parsedUserInput - 1;
                }

                Console.WriteLine(string.Format("Please enter square col:"));
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out parsedUserInput))
                {
                    playerSelectedSquare.Y = parsedUserInput - 1;
                }

                game.PlayerMove(playerSelectedSquare, ePlayingMode.PlayerVsPlayer, out squareSelectionStatus, out gameStatus);
                notifyMoveResult(squareSelectionStatus, playerSelectedSquare);
            }
            while (gameStatus == eGameStatus.InProgress);
        }

        public static StringBuilder GenerateConsoleBoard(int i_NumOfRows, int i_NumOfCols)
        {
            StringBuilder board = new StringBuilder();
            board.Append("   ");
            for (int j = 0; j < i_NumOfCols; j++)
            {
                board.Append(String.Format("  {0} ",  (char)('A' + j) ));
            }
            board.AppendLine();
            board.Append("   ");
            board.Append('=', i_NumOfCols * 4 + 1);
            board.AppendLine();

            for (int i = 1; i <= i_NumOfRows; i++)
            {
                board.Append(String.Format(" {0} ",i.ToString()));
                // %TBD - remove secondary loop
                for (int j = 0; j <= i_NumOfCols; j++)
                {
                    board.Append("|   ");
                }
                board.AppendLine();
                board.Append("   ");
                board.Append('=', i_NumOfCols * 4 + 1);
                board.AppendLine();
            }

            return board;
        }
    }
}
