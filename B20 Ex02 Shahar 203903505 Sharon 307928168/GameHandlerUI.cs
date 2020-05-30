﻿using System;
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

        private static void notifyMoveResult(Game.eTileSelectionStatus squareSelectionStatus, Point i_SelectedSquare, Game i_Game, ConsoleUI i_UI)
        {
            switch (squareSelectionStatus)
            {
                case Game.eTileSelectionStatus.IllegalSquareSelection:
                    i_UI.PrintTileOutOfRange();
                    break;

                case Game.eTileSelectionStatus.SquareAlreadyRevealed:
                    i_UI.PrintTileAlreadyRevealed();
                    break;

                case Game.eTileSelectionStatus.PlayerRevealedFirstSquare:
                    if (i_Game.CurrentPlayer.IsPc == true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        i_UI.DrawBoard(i_Game.PlayingBoard);
                        System.Threading.Thread.Sleep(2000);
                    }
                    else
                    {
                        i_UI.DrawBoard(i_Game.PlayingBoard);
                    }
                    break;

                case Game.eTileSelectionStatus.PlayerRevealedSecondSquare:
                    i_UI.DrawBoard(i_Game.PlayingBoard);
                    break;

                default:
                    break;
            }
        }

        private static void notifyEvaluationResult(Game.ePlayerMoveEvaluationStatus playerMoveEvaluationStatus, Point i_SelectedSquare, Game i_Game, ConsoleUI i_UI)
        {
            switch (playerMoveEvaluationStatus)
            {
                case Game.ePlayerMoveEvaluationStatus.SquaresMatch:
                    i_UI.DrawBoard(i_Game.PlayingBoard);
                    break;

                case Game.ePlayerMoveEvaluationStatus.SquaresDontMatch:
                    System.Threading.Thread.Sleep(2000);
                    i_UI.DrawBoard(i_Game.PlayingBoard);
                    break;
            }
        }

        public static void PlayGame(Game i_Game, Game.ePlayingMode i_PlayingMode, ConsoleUI i_UI)
        {
            Game.eGameStatus gameStatus = Game.eGameStatus.Undefined;
            Game.eTileSelectionStatus squareSelectionStatus = Game.eTileSelectionStatus.Undefined;
            Game.ePlayerMoveEvaluationStatus playerMoveEvaluationStatus = Game.ePlayerMoveEvaluationStatus.Undefined;

            i_UI.DrawBoard(i_Game.PlayingBoard);
            bool toQuit = false;

            do
            {
                Point playerSelectedTile = new Point();

                if (i_Game.CurrentPlayer.IsPc == false)
                {
                    if (squareSelectionStatus != Game.eTileSelectionStatus.IllegalSquareSelection
                   && squareSelectionStatus != Game.eTileSelectionStatus.SquareAlreadyRevealed)
                    {
                        Console.WriteLine(string.Format(
@"It's now {0}'s turn.
{1}'s score: {2}
{3}'s score: {4}"
    , i_Game.CurrentPlayer.Name
    , i_Game.FirstPlayer.Name
    , i_Game.FirstPlayer.Score
    , i_Game.SecondPlayer.Name
    , i_Game.SecondPlayer.Score));
                    }

                    i_UI.GetUserChoice(out playerSelectedTile, out toQuit);
                }

                if (!toQuit)
                {
                    i_Game.PlayerMove(playerSelectedTile, i_PlayingMode, out squareSelectionStatus);
                    notifyMoveResult(squareSelectionStatus, playerSelectedTile, i_Game, i_UI);
                    i_Game.EvaluatePlayerMove(i_PlayingMode, squareSelectionStatus, out playerMoveEvaluationStatus, out gameStatus);
                    notifyEvaluationResult(playerMoveEvaluationStatus, playerSelectedTile, i_Game, i_UI);
                }
            }
            while (gameStatus == Game.eGameStatus.InProgress && !toQuit); //%-why this isnt while eStatus!=GameFinished???

            if (!toQuit)
            {
                i_UI.PrintEndGameSummary(i_Game, gameStatus);
            }
            else
            {
                Console.WriteLine("Good Bye!");
            }
        }

        public static void LaunchGame()
        {
            ConsoleUI UI = new ConsoleUI();
            string firstPlayerName, secondPlayerName;
            int boardRows, boardCols;
            Game.ePlayingMode playingMode;
            bool isAnotherRound = true;

            do
            {
                UI.PrintWelcomeMessage();
                firstPlayerName = UI.GetUserName();
                UI.GetPlayingMode(out playingMode);

                if (playingMode == Game.ePlayingMode.PlayerVsPlayer)
                {
                    secondPlayerName = UI.GetUserName();
                }
                else
                {
                    secondPlayerName = "Computer";
                }

                UI.GetBoardDimentions(out boardRows, out boardCols);

                Game thisGame = new Game(boardRows, boardCols, firstPlayerName, secondPlayerName, playingMode);

                PlayGame(thisGame, playingMode, UI);

                Console.WriteLine("Another round? Press 'y' for yes, press any other key to quit");
                string userInput = Console.ReadLine();
                isAnotherRound = userInput.ToLower().Equals("y") ? true : false;

                if (isAnotherRound == true)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                }

            } while (isAnotherRound);

            UI.PrintGoodByeMessage();
        }

        //public static void PlayerVsPcMatch()
        //{
        //    Player player1 = new Player("Shahar");
        //    Player player2 = new Player("PC");
        //    player2.IsPc = true;

        //    Game game = new Game(4, 4, player1, player2);
        //    Game.eGameStatus gameStatus;
        //    Game.eTileSelectionStatus squareSelectionStatus;
        //    game.PrintValueBoard();
        //    game.PrintRevealedBoard();
        //    do
        //    {
        //        Point playerSelectedSquare = new Point();
        //        if (game.CurrentPlayer.IsPc == false)
        //        {
        //            string userInput;
        //            int parsedUserInput;

        //            Console.WriteLine(string.Format("Please enter square row:"));
        //            userInput = Console.ReadLine();
        //            if (int.TryParse(userInput, out parsedUserInput))
        //            {
        //                playerSelectedSquare.X = parsedUserInput - 1;
        //            }

        //            Console.WriteLine(string.Format("Please enter square col:"));
        //            userInput = Console.ReadLine();
        //            if (int.TryParse(userInput, out parsedUserInput))
        //            {
        //                playerSelectedSquare.Y = parsedUserInput - 1;
        //            }
        //        }

        //        //game.PlayerMove(playerSelectedSquare, Game.ePlayingMode.PlayerVsPc, out squareSelectionStatus, out gameStatus);
        //        //notifyMoveResult(squareSelectionStatus, playerSelectedSquare, game);
        //    }
        //    while (gameStatus == Game.eGameStatus.InProgress);
        //}

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
}
