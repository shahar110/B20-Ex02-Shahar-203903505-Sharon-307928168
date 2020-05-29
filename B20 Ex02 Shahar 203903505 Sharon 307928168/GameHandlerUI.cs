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
            ConsoleUI UI = new ConsoleUI();
            string firstPlayerName, secondPlayerName;
            int boardRows, boardCols;
            Game.ePlayingMode playingMode;

            UI.PrintWelcomeMessage();
            firstPlayerName = UI.GetUserName();
            UI.GetPlayMode(out playingMode);

            if (playingMode == ePlayingMode.PlayerVsPlayer)
            {
                secondPlayerName = UI.GetUserName();
            }
            else
            {
                secondPlayerName = "Computer";
            }

            Player player1 = new Player(firstPlayerName);
            Player player2 = new Player(secondPlayerName);

            UI.GetBoardDimentions(out boardRows, out boardCols);
            
            Game thisGame = new Game(boardRows, boardCols, player1, player2);

            eGameStatus gameStatus;
            eSquareSelectionStatus squareSelectionStatus;

            UI.DrawBoard(thisGame.PlayingBoard);  //%add properties (Game.cs)

            do
            {
                Point playerSelectedTile = new Point();
                playerSelectedTile = UI.GetUserChoice();

                thisGame.PlayerMove(playerSelectedTile, playingMode, out squareSelectionStatus, out gameStatus);
                notifyMoveResult(squareSelectionStatus, playerSelectedTile);
            }
            while (gameStatus == eGameStatus.InProgress); //%-why this isnt while eStatus!=GameFinished???

            UI.PrintEndGameSummery(thisGame);
        }

    //    public static StringBuilder GenerateConsoleBoard(int i_NumOfRows, int i_NumOfCols)
    //    {
    //        StringBuilder board = new StringBuilder();
    //        board.Append("   ");
    //        for (int j = 0; j < i_NumOfCols; j++)
    //        {
    //            board.Append(String.Format("  {0} ",  (char)('A' + j) ));
    //        }
    //        board.AppendLine();
    //        board.Append("   ");
    //        board.Append('=', i_NumOfCols * 4 + 1);
    //        board.AppendLine();

    //        for (int i = 1; i <= i_NumOfRows; i++)
    //        {
    //            board.Append(String.Format(" {0} ",i.ToString()));
    //            // %TBD - remove secondary loop
    //            for (int j = 0; j <= i_NumOfCols; j++)
    //            {
    //                board.Append("|   ");
    //            }
    //            board.AppendLine();
    //            board.Append("   ");
    //            board.Append('=', i_NumOfCols * 4 + 1);
    //            board.AppendLine();
    //        }

    //        return board;
    //    }
    //}
}
