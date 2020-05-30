using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class Board
    {
        private readonly Square[,] m_BoardMatrix = null;
        private readonly Point m_BoardSize = new Point(0, 0);

        public Board(int i_BoardGridRows, int i_BoardGridCols)
        {
            m_BoardMatrix = generatePlayingBoard(i_BoardGridRows, i_BoardGridCols);
            m_BoardSize = new Point(i_BoardGridRows, i_BoardGridCols);
        }

        public Square[,] Matrix
        {
            get { return m_BoardMatrix; }
        }

        public Point BoardSize
        {
            get { return m_BoardSize; }
        }

        public char GetSquareValue(Point i_SelectedSquare)
        {
            return m_BoardMatrix[i_SelectedSquare.X, i_SelectedSquare.Y].Value;
        }

        public void RevealSquare(Point i_SelectedSquare)
        {
            m_BoardMatrix[i_SelectedSquare.X, i_SelectedSquare.Y].IsRevealed = true;
        }

        public void UnRevealSquare(Point i_SelectedSquare)
        {
            m_BoardMatrix[i_SelectedSquare.X, i_SelectedSquare.Y].IsRevealed = false;
        }

        public bool IsSquareRevealed(Point i_SelectedSquare)
        {
            return m_BoardMatrix[i_SelectedSquare.X, i_SelectedSquare.Y].IsRevealed;
        }

        public bool CheckIfSquareWithinRange(Point i_SelectedSquare)
        {
            bool withinRange = true;
            if (i_SelectedSquare.X < 0 || i_SelectedSquare.X > m_BoardSize.X - 1)
            {
                withinRange = false;
            }
            else if (i_SelectedSquare.Y < 0 || i_SelectedSquare.Y > m_BoardSize.Y - 1)
            {
                withinRange = false;
            }
            return withinRange;
        }

        public bool CheckIfFullyRevealed()
        {
            bool fullyRevealed = true;
            for (int i = 0; i < m_BoardSize.X; i++)
            {
                for (int j = 0; j < m_BoardSize.Y; j++)
                {
                    if (!m_BoardMatrix[i, j].IsRevealed)
                    {
                        fullyRevealed = false;
                        break;
                    }
                }
            }
            return fullyRevealed;
        }

        private Square[,] generatePlayingBoard(int i_NumOfRows, int i_NumOfCols)
        {
            char[,] dummyBoard = generateLettersGrid(i_NumOfRows, i_NumOfCols);
            Square[,] newBoard = new Square[i_NumOfRows, i_NumOfCols];

            for (int i = 0; i < i_NumOfRows; i++)
            {
                for (int j = 0; j < i_NumOfCols; j++)
                {
                    // %TBD - change the name of Point class
                    char currentGridColumn = (char)('A' + j);
                    newBoard[i, j] = new Square(dummyBoard[i, j]);
                }
            }

            return newBoard;
        }

        public static char[,] generateLettersGrid(int i_NumOfRows, int i_NumOfCols)
        {
            char[,] lettersGrid = new char[i_NumOfRows, i_NumOfCols];
            char[] m_LettersArr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            List<int> gridList = new List<int>();
            FillGridList(gridList, i_NumOfRows * i_NumOfCols);
            int totalGridLength = gridList.Count;
            int lettersCounter = 0;

            Random rand = new Random();
            foreach (char letter in m_LettersArr)
            {
                for (int i = 0; i < 2; i++)
                {
                    int randomIndex = rand.Next(0, gridList.Count - 1);
                    int indexValue = gridList[randomIndex];
                    Point gridCordinate = ExtractMatrixCordinates(indexValue, i_NumOfRows, i_NumOfCols);
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

        public static void FillGridList(List<int> i_ListToFill, int i_LengthToFill)
        {
            for (int i = 0; i < i_LengthToFill; i++)
            {
                i_ListToFill.Add(i);
            }
        }

        public static Point ExtractMatrixCordinates(int i_ListIndex, int i_NumOfRows, int i_NumOfCols)
        {
            int columnNum = i_ListIndex % i_NumOfCols;
            int rowNum = i_ListIndex / i_NumOfCols;
            return new Point(rowNum, columnNum);
        }

        public static int CalculateListIndex(Point i_MatrixCordinates, int i_NumOfRows, int i_NumOfCols)
        {
            return i_MatrixCordinates.X * i_NumOfCols + i_MatrixCordinates.Y;
        }

    }
}
