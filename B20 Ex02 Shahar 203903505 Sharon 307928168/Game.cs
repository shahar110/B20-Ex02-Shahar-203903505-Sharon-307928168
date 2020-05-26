using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class Game
    {
        private Board m_PlayingBoard;
        private Player m_Winner = null;
        private Player m_firstPlayer = null;
        private Player m_secondPlayer = null;
        
        public Game(ConsoleUI i_Ui, int i_BoardRows, int i_BoardCols)
        {
            m_PlayingBoard = new Board(i_BoardRows, i_BoardCols);
        }

        public Player Winner
        {
            get { return m_Winner; }
        }

        public Player FirstPlayer
        {
            get { return m_firstPlayer; }
        }

        public Player SecondPlayer
        {
            get { return m_secondPlayer; }
        }

        //public Game(int i_BoardGridRows, int i_BoardGridCols)
        //{
        //    m_PlayingBoard = generatePlayingBoard(i_BoardGridRows, i_BoardGridCols);
        //}

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
