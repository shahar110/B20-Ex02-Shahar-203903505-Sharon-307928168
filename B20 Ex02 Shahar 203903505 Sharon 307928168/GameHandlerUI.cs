using System;
using System.Text;

namespace ConsoleUI

{
    public class GameHandlerUI
    {
      
        public enum ePlayerType
        {
            IsHuman,
            IsPC
        }

        //%-this method is the entry point to the program
        public static void StartGame()
        {
            ConsoleUI UI = new ConsoleUI();
            string firstPlayerName, secondPlayerName;
            int boardRows, boardCols;
            ePlayerType secondPlayerType;

            firstPlayerName = UI.GetUserName();
            UI.GetSecondPlayerType(out secondPlayerType);

            if (secondPlayerType == ePlayerType.IsHuman)
            {
                secondPlayerName = UI.GetUserName();
            }
            else
            {
                secondPlayerName = "Computer";
            }

            UI.GetBoardDimentions(out boardRows, out boardCols);

            Game thisGame = new Game(boardRows, boardCols, firstPlayerName, secondPlayerName);

            if (secondPlayerType == ePlayerType.IsHuman)
            {
                playPlayerVsPlayersGame(thisGame, UI);
            }
            else
            {
                PlayOnePlayerVsComputer(thisGame, UI);
            }


            //get name of the first player
            //first player decides whether this is a 2-Players game or 1-Player game
            //if (2-player game) --> enter second player name 
            //else --> set second player name to "computer"
            //first player enter board dimentions

            //now we set a new game with the properties above: Game thisGame = new Game(board dimentions, Player1, Player2, typeOfSecondPlayer)
            //do:
            //if (2-PlayersGame)  --> call method TwoPlayersGame(thisGame, UI);
            //if (1-PlayerGame)   --> call method OnePlayerVsComputerGame(thisGame, UI);

            //Game ended - print scores and who's the winner
            //ask the user if he wants another round
            //if so --> back to loop start.
        }

        private static void continueByGameStatus(Game i_Game, ConsoleUI i_UI)
        {
            if (i_Game.GameStatus == Game.eStatus.TileOutOfRange)
            {
                i_UI.printTileOutOfRange();
            }

            if (i_Game.GameStatus == Game.eStatus.TileAlreadyRevealed)
            {
                i_UI.printTileAlreadyRevealed();
            }

            if (i_Game.GameStatus == Game.eStatus.ValidFirstChoice)
            {
                i_UI.DrawBoard(i_Game.PlayingBoard);
                //Add message:  enter your second choice
            }

            if (i_Game.GameStatus == Game.eStatus.SuccessfulTurn)
            {
                i_UI.DrawBoard(i_Game.PlayingBoard);
                //Add message: well done!
            }

            if (i_Game.GameStatus == Game.eStatus.MismatchTiles)
            {
                i_UI.DrawBoard(i_Game.PlayingBoard);
                //Show mismatched couple for 2 sec.
                //Add message: oops! You didn't find a couple...
                
            }
        }

        private static void playPlayerVsPlayersGame(Game i_Game, ConsoleUI i_UI)
        {
            i_UI.DrawBoard(i_Game.PlayingBoard);
            Point userChoice;

            do
            {
                userChoice = i_UI.GetUserChoice();
                i_Game.FlipTile(userChoice);
                continueByGameStatus(i_Game, i_UI);
                if (i_Game.GameStatus == Game.eStatus.MismatchTiles)
                {
                    i_Game.FlipTilesFaceDown();
                    i_UI.DrawBoard(i_Game.PlayingBoard);
                }

            }
            while (i_Game.GameStatus != Game.eStatus.EndOfGame);  //%-add option q to quit
        }

        private static void PlayOnePlayerVsComputer(Game i_Game, ConsoleUI i_UI)
        {

        }       
    }
}
