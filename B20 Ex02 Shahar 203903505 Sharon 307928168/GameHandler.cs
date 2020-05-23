using System;
using System.Text;

namespace ConsoleUI
{
    public class GameHandler
    {
        //private Game currentGame;

        public static void Start()
        {
            Console.WriteLine();
        }

        private void TwoPlayersGame()
        {

        }

        private void OnePlayerGame()
        {

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
