using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class Board
    {
        private BoardSquare[,] m_PlayingBoard = null;
        int m_NumOfRows;
        int m_NumOfCols;
   
        public Board(int i_NumOfRows, int i_NumOfCols)
        {
            m_NumOfRows = i_NumOfRows;
            m_NumOfCols = i_NumOfCols;
            generatePlayingBoard();
        }

        public BoardSquare[,] PlayingBoard
        {
            get { return m_PlayingBoard; }
        }

        public int NumOfRows
        {
            get { return m_NumOfRows; }
        }

        public int NumOfCols
        {
            get { return m_NumOfCols; }
        }

        //%-change to private
        public void RevealBoardSquare(int i_row, int i_col)
        {
            m_PlayingBoard[i_row, i_col].IsReaveled = true;
        }

        private void generatePlayingBoard()
        {
            char[,] dummyBoard = generateLettersGrid(m_NumOfRows, m_NumOfCols);
            m_PlayingBoard = new BoardSquare[m_NumOfRows, m_NumOfCols];

            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfCols; j++)
                {
                    // %TBD - change the name of Point class
                   // char currentGridColumn = (char)('A' + j);
                    m_PlayingBoard[i, j] = new BoardSquare(dummyBoard[i, j]);
                }
            }
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
    }
}
