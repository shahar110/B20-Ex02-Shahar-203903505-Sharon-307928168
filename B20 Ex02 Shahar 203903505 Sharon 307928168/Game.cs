using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class Game
    {
        //% this is a test comment
        private BoardSquare[,] m_PlayingBoard = null;
        public Game(int i_BoardGridRows, int i_BoardGridCols)
        {
            m_PlayingBoard = generatePlayingBoard(i_BoardGridRows, i_BoardGridCols);
        }

        public BoardSquare[,] Board
        {
            get { return m_PlayingBoard;  }
        }

        private BoardSquare[,] generatePlayingBoard(int i_NumOfRows, int i_NumOfCols)
        {
            char[,] dummyBoard = generateLettersGrid(i_NumOfRows, i_NumOfCols);
            BoardSquare[,] newBoard = new BoardSquare[i_NumOfRows, i_NumOfCols];

            for (int i = 0; i < i_NumOfRows; i++)
            {
                for (int j = 0; j < i_NumOfCols; j++)
                {
                    // %TBD - change the name of Point class
                    char currentGridColumn = (char)('A' + j);
                    newBoard[i, j] = new BoardSquare(currentGridColumn, i, dummyBoard[i, j]);
                }
            }

            return newBoard;
        }

        public static char[,] generateLettersGrid(int i_NumOfRows, int i_NumOfCols)
        {
            char[,] lettersGrid = new char[i_NumOfRows, i_NumOfCols];
            char[] m_LettersArr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            List<int> gridList = new List<int>();
            fillGridList(gridList, i_NumOfRows * i_NumOfCols);
            int totalGridLength = gridList.Count;
            int lettersCounter = 0;

            Random rand = new Random();
            foreach (char letter in m_LettersArr)
            {
                for (int i = 0; i < 2; i++)
                {
                    int randomIndex = rand.Next(0, gridList.Count - 1);
                    int indexValue = gridList[randomIndex];
                    Point gridCordinate = extractMatrixCordinates(indexValue, i_NumOfRows, i_NumOfCols);
                    lettersGrid[gridCordinate.X, gridCordinate.Y] = letter;
                    gridList.RemoveAt(randomIndex);
                }
                lettersCounter += 2;
                if (lettersCounter >= totalGridLength)
                {
                    break;
                }
            }

            return lettersGrid;
        }

        private static void fillGridList(List<int> i_ListToFill, int i_LengthToFill)
        {
            for (int i = 0; i < i_LengthToFill; i++)
            {
                i_ListToFill.Add(i);
            }
        }

        public static Point extractMatrixCordinates(int i_ListIndex, int i_NumOfRows, int i_NumOfCols)
        {
            int columnNum = i_ListIndex % i_NumOfCols;
            int rowNum = i_ListIndex / i_NumOfCols;
            return new Point(rowNum, columnNum);
        }



        //private static void fillListOfLists(List<List<int>> i_ListToFill, int i_Rows, int i_Cols)
        //{
        //    for (int i = 0; i < i_Rows; i++)
        //    {
        //        List<int> currentColumnList = new List<int>();
        //        for (int j = 0; j < i_Cols; j++)
        //        {
        //            currentColumnList.Add(j);
        //        }
        //        i_ListToFill.Add(currentColumnList);
        //    }
        //}
    }
}
