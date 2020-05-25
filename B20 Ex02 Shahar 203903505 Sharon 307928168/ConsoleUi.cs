using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class ConsoleUi
    {
        public void DrawBoard(Board i_PlayingBoard)
        {
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
                    if (i_PlayingBoard.PlayingBoard[i,j].IsReaveled)
                    {
                        Console.Write(String.Format(" {0} ", i_PlayingBoard.PlayingBoard[i, j].Value));
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
