using System;
using System.Text;

namespace ConsoleUI

{
    public class GameHandler
    {
        Player currentPlayer;

         public enum ePlayerType
        {
            IsHuman,
            IsPC
        }

        //%-this method is the entry point to the program
        public static void StartGameManager()
        {
           ConsoleUI UI = new ConsoleUI();
           ePlayerType secondPlayerType;
           Game thisGame = startNewGame(UI, out secondPlayerType);
 
           if (secondPlayerType == ePlayerType.IsHuman)
           {
                TwoPlayersGame();
           }
           else
           {
                OnePlayerVsComputerGame();
           }


        }

        private static Game startNewGame(ConsoleUI i_UI, out ePlayerType o_SecondPlayerType)
        {
            i_UI.PrintWelcomeMessage();
            string firstPlayerName = i_UI.GetUserName();

            o_SecondPlayerType = i_UI.GetSecondPlayerType();
            
            if (o_SecondPlayerType == ePlayerType.IsHuman)
            {
                string secondPlayerName = i_UI.GetUserName();
            }

            int boardRows, boardCols;
            i_UI.GetBoardSize(out boardCols, out boardRows);

            return new Game(i_Ui, boardRows, boardCols);
        }

        private static void TwoPlayersGame()
        {

        }

        private static void OnePlayerVsComputerGame()
        {

        }

        
    }
}
