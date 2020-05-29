using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class Game
    {
        public enum eStatus
        {
            TileOutOfRange,
            TileAlreadyRevealed,
            ValidFirstChoice,
            SuccessfulTurn,
            MismatchTiles,
            EndOfGame
        }

        private Board m_PlayingBoard;
        private Player m_FirstPlayer = null;
        private int m_firstPlayerScore;
        private Player m_SecondPlayer = null;
        private int m_secondPlayerScore;
        private Player m_CurrentPlayer = null;
        private Player m_Winner = null;
        private eStatus m_GameStatus;

        public Game(int i_BoardRows, int i_BoardCols, string i_firstPlayerName, string i_secondPlayerName)
        {
            m_PlayingBoard = new Board(i_BoardRows, i_BoardCols);
            m_FirstPlayer = new Player(i_firstPlayerName);
            m_SecondPlayer = new Player(i_secondPlayerName);
            m_CurrentPlayer = FirstPlayer;
        }

        public Board PlayingBoard
        {
            get { return m_PlayingBoard; }
        }

        public Player Winner
        {
            get { return m_Winner; }
        }

        public Player FirstPlayer
        {
            get { return m_FirstPlayer; }
        }

        public Player SecondPlayer
        {
            get { return m_SecondPlayer; }
        }

        public eStatus GameStatus
        {
            get { return m_GameStatus; }
        }

        public int FirstPlayerScore
        {
            get { return  m_firstPlayerScore; }
        }

        public int SecondPlayerScore
        {
            get { return m_secondPlayerScore; }
        }

        public void FlipTile(Point i_userChoice)
        {
            //check logic validity of point
            if (!isInsideBoardRange(i_userChoice))
            {
                m_GameStatus = Game.eStatus.TileOutOfRange;
            }
            else
            {
                if (isRevealed(i_userChoice))
                {
                    m_GameStatus = Game.eStatus.TileAlreadyRevealed;
                }
                else
                {
                    m_PlayingBoard.RevealTile(i_userChoice.X, i_userChoice.Y);

                    if (m_PlayingBoard.FirstRevealedTile == null)   //first turn
                    {
                        m_PlayingBoard.FirstRevealedTile = m_PlayingBoard.BoardMatrix[i_userChoice.X, i_userChoice.Y];
                        m_GameStatus = eStatus.ValidFirstChoice;
                    }
                    else   //second turn
                    {
                        m_PlayingBoard.SecondRevealedTile = m_PlayingBoard.BoardMatrix[i_userChoice.X, i_userChoice.Y];

                        if (m_PlayingBoard.FirstRevealedTile.Value == m_PlayingBoard.SecondRevealedTile.Value)
                        {
                            m_GameStatus = eStatus.SuccessfulTurn;
                            m_PlayingBoard.RevealedTilesCount++;
                            m_PlayingBoard.FirstRevealedTile = null;
                            m_PlayingBoard.SecondRevealedTile = null;

                            if (m_CurrentPlayer == m_FirstPlayer)
                            {
                                m_firstPlayerScore++;
                            }
                            else
                            {
                                m_secondPlayerScore++;
                            }

                            if (m_PlayingBoard.RevealedTilesCount == m_PlayingBoard.NumOfRows*m_PlayingBoard.NumOfCols)
                            {
                                m_GameStatus = eStatus.EndOfGame;
                            }
                        }
                        else
                        {
                            swapTurns();
                            m_GameStatus = eStatus.MismatchTiles;
                        }
                    }

                }
            }
        }


        private void swapTurns()
        {
            if (m_CurrentPlayer == m_FirstPlayer)
            {
                m_CurrentPlayer = SecondPlayer;
            }
            else
            {
                m_CurrentPlayer = m_FirstPlayer;
            }
        }

        private bool isInsideBoardRange(Point i_userChoice)
        {
            return i_userChoice.X <= m_PlayingBoard.NumOfRows && i_userChoice.X >= 0 &&
                   i_userChoice.Y <= m_PlayingBoard.NumOfCols && i_userChoice.Y >= 0;
        }

        private bool isRevealed(Point i_userChoice)
        {
            return m_PlayingBoard.BoardMatrix[i_userChoice.X, i_userChoice.Y].IsReaveled;
        }

        public void FlipTilesFaceDown()
        {
            m_PlayingBoard.FirstRevealedTile.IsReaveled = false;
            m_PlayingBoard.SecondRevealedTile.IsReaveled = false;
        }
    }
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


