using System;
using System.Text;

namespace B20_Ex02_MemoryGame
{
    public class GameHandlerUI
    {
        private static void notifyMoveResult(Game.eSquareSelectionStatus i_SquareSelectionStatus, Point i_SelectedSquare, Game i_Game, ConsoleUI i_UI)
        {
            switch (i_SquareSelectionStatus)
            {
                case Game.eSquareSelectionStatus.IllegalSquareSelection:
                    i_UI.PrintSquareOutOfRange();
                    break;

                case Game.eSquareSelectionStatus.SquareAlreadyRevealed:
                    i_UI.PrintSquareAlreadyRevealed();
                    break;

                case Game.eSquareSelectionStatus.PlayerRevealedFirstSquare:
                    if (i_Game.CurrentPlayer.IsPc == true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        i_UI.DrawLettersBoard(i_Game.PlayingBoard);
                        System.Threading.Thread.Sleep(2000);
                    }
                    else
                    {
                        i_UI.DrawLettersBoard(i_Game.PlayingBoard);
                    }

                    break;

                case Game.eSquareSelectionStatus.PlayerRevealedSecondSquare:
                    i_UI.DrawLettersBoard(i_Game.PlayingBoard);
                    break;

                default:
                    break;
            }
        }

        private static void notifyEvaluationResult(Game.ePlayerMoveEvaluationStatus i_PlayerMoveEvaluationStatus, Point i_SelectedSquare, Game i_Game, ConsoleUI i_UI)
        {
            switch (i_PlayerMoveEvaluationStatus)
            {
                case Game.ePlayerMoveEvaluationStatus.SquaresMatch:
                    i_UI.DrawLettersBoard(i_Game.PlayingBoard);
                    break;

                case Game.ePlayerMoveEvaluationStatus.SquaresDontMatch:
                    System.Threading.Thread.Sleep(2000);
                    i_UI.DrawLettersBoard(i_Game.PlayingBoard);
                    break;
            }
        }

        public static void PlayGame(Game i_Game, Game.ePlayingMode i_PlayingMode, ConsoleUI i_UI)
        {
            Game.eGameStatus gameStatus = Game.eGameStatus.Undefined;
            Game.eSquareSelectionStatus squareSelectionStatus = Game.eSquareSelectionStatus.Undefined;
            Game.ePlayerMoveEvaluationStatus playerMoveEvaluationStatus = Game.ePlayerMoveEvaluationStatus.Undefined;

            i_UI.DrawLettersBoard(i_Game.PlayingBoard);
            bool toQuit = false;

            do
            {
                Point playerSelectedSquare = new Point();

                if (i_Game.CurrentPlayer.IsPc == false)
                {
                    if (squareSelectionStatus != Game.eSquareSelectionStatus.IllegalSquareSelection
                   && squareSelectionStatus != Game.eSquareSelectionStatus.SquareAlreadyRevealed)
                    {
                        Console.WriteLine(string.Format(
@"It's now {0}'s turn.
{1}'s score: {2}
{3}'s score: {4}",
    i_Game.CurrentPlayer.Name,
    i_Game.FirstPlayer.Name,
    i_Game.FirstPlayer.Score,
    i_Game.SecondPlayer.Name,
    i_Game.SecondPlayer.Score));
                    }

                    i_UI.GetUserChoice(out playerSelectedSquare, out toQuit);
                }

                if (!toQuit)
                {
                    i_Game.PlayerMove(playerSelectedSquare, i_PlayingMode, out squareSelectionStatus);
                    notifyMoveResult(squareSelectionStatus, playerSelectedSquare, i_Game, i_UI);
                    i_Game.EvaluatePlayerMove(i_PlayingMode, squareSelectionStatus, out playerMoveEvaluationStatus, out gameStatus);
                    notifyEvaluationResult(playerMoveEvaluationStatus, playerSelectedSquare, i_Game, i_UI);
                }
            }
            while (gameStatus == Game.eGameStatus.InProgress && !toQuit);

            if (!toQuit)
            {
                i_UI.PrintEndGameSummary(i_Game, gameStatus);
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

                UI.PrintAnotherRoundMessage();                
                string userInput = Console.ReadLine();
                isAnotherRound = userInput.ToLower().Equals("y") ? true : false;

                if (isAnotherRound == true)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                }
            }
            while (isAnotherRound);

            UI.PrintGoodByeMessage();

        }
    }
}
