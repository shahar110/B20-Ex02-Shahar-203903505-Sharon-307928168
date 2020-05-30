using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class Game
    {
        private readonly Board m_PlayingBoard = null;
        private readonly Player m_FirstPlayer = null;
        private readonly Player m_SecondPlayer = null;
        private Player m_CurrentPlayer = null;
        List<int> m_PcAvailableSquaresList = null;

        public enum eTileSelectionStatus
        {
            Undefined,
            AwaitingSelection,
            IllegalSquareSelection,
            SquareAlreadyRevealed,
            PlayerRevealedFirstSquare,
            PlayerRevealedSecondSquare,
        }

        public enum ePlayerMoveEvaluationStatus
        {
            Undefined,
            PlayerMoveNotCompleted,
            SquaresMatch,
            SquaresDontMatch,
        }

        public enum eGameStatus
        {
            Undefined,
            InProgress,
            GameFinished,
            FirstPlayerWon,
            SecondPlayerWon,
            GameTie,
        }

        public enum ePlayingMode
        {
            Undefined,
            InvalidModeSelection,
            PlayerVsPlayer,
            PlayerVsPc,
        }

        public Game(int i_BoardNumOfRows, int i_BoardNumOfCols, string i_FirstPlayerName, 
            string i_SecondPlayerName, ePlayingMode i_PlayingMode)
        {
            bool isPc = i_PlayingMode == ePlayingMode.PlayerVsPc ? true : false;
            m_PlayingBoard = new Board(i_BoardNumOfRows, i_BoardNumOfCols);
            m_FirstPlayer = new Player(i_FirstPlayerName, false);
            m_SecondPlayer = new Player(i_SecondPlayerName, isPc);
            m_CurrentPlayer = m_FirstPlayer;
            if (i_PlayingMode == ePlayingMode.PlayerVsPc)
            {
                m_PcAvailableSquaresList = new List<int>();
                Board.FillGridList(m_PcAvailableSquaresList, i_BoardNumOfRows * i_BoardNumOfCols);
            }

        }

        public Board PlayingBoard
        {
            get { return m_PlayingBoard; }
        }

        public Player FirstPlayer
        {
            get { return m_FirstPlayer; }
        }

        public Player SecondPlayer
        {
            get { return m_SecondPlayer; }
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
        }

        public void PlayerMove(Point i_SelectedSquare, ePlayingMode i_PlayingMode, out eTileSelectionStatus o_SquareSelectionStatus)
        {
            if (i_PlayingMode == ePlayingMode.PlayerVsPc && m_CurrentPlayer.IsPc == true)
            {
                pcSquareSelection(out o_SquareSelectionStatus);
            }
            else
            {
                playerSquareSelection(i_SelectedSquare, out o_SquareSelectionStatus);
            }
        }

        private void swapTurns()
        {
            if (m_CurrentPlayer == m_FirstPlayer)
            {
                m_CurrentPlayer = m_SecondPlayer;
            }
            else
            {
                m_CurrentPlayer = m_FirstPlayer;
            }
        }

        public void EvaluatePlayerMove(ePlayingMode i_PlayingMode, eTileSelectionStatus i_SquareSelectionStatus, out ePlayerMoveEvaluationStatus o_PlayerMoveEvaluationStatus, out eGameStatus o_GameStatus)
        {
            if (i_SquareSelectionStatus == eTileSelectionStatus.PlayerRevealedSecondSquare)
            {
                int firstSquareListValue = Board.CalculateListIndex(m_CurrentPlayer.FirstRevealedSquare, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
                int secondSquareListValue = Board.CalculateListIndex(m_CurrentPlayer.SecondRevealedSquare, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);

                if (m_PlayingBoard.GetSquareValue(m_CurrentPlayer.FirstRevealedSquare) == m_PlayingBoard.GetSquareValue(m_CurrentPlayer.SecondRevealedSquare))
                {
                    m_CurrentPlayer.Score++;
                    if (i_PlayingMode == ePlayingMode.PlayerVsPc && m_CurrentPlayer.IsPc == false)
                    {
                        m_PcAvailableSquaresList.Remove(firstSquareListValue);
                        m_PcAvailableSquaresList.Remove(secondSquareListValue);
                    }
                    m_CurrentPlayer.ResetSquareSelection();
                    o_PlayerMoveEvaluationStatus = ePlayerMoveEvaluationStatus.SquaresMatch;
                }
                else
                {
                    if (i_PlayingMode == ePlayingMode.PlayerVsPc && m_CurrentPlayer.IsPc == true)
                    {
                        m_PcAvailableSquaresList.Add(firstSquareListValue);
                        m_PcAvailableSquaresList.Add(secondSquareListValue);
                    }
                    m_PlayingBoard.UnRevealSquare(m_CurrentPlayer.FirstRevealedSquare);
                    m_PlayingBoard.UnRevealSquare(m_CurrentPlayer.SecondRevealedSquare);
                    m_CurrentPlayer.ResetSquareSelection();
                    o_PlayerMoveEvaluationStatus = ePlayerMoveEvaluationStatus.SquaresDontMatch;
                    swapTurns();
                }

                if (m_PlayingBoard.CheckIfFullyRevealed())
                {
                    o_GameStatus = evaluateGameWinner();
                }
                else
                {
                    o_GameStatus = eGameStatus.InProgress;
                }
            }
            else
            {
                o_PlayerMoveEvaluationStatus = ePlayerMoveEvaluationStatus.PlayerMoveNotCompleted;
                o_GameStatus = eGameStatus.InProgress;
            }
        }

        private eGameStatus evaluateGameWinner()
        {
            eGameStatus gameResult;
            if (m_FirstPlayer.Score > m_SecondPlayer.Score)
            {
                gameResult = eGameStatus.FirstPlayerWon;
            }
            else if (m_FirstPlayer.Score < m_SecondPlayer.Score)
            {
                gameResult = eGameStatus.SecondPlayerWon;
            }
            else
            {
                gameResult = eGameStatus.GameTie;
            }
            return gameResult;
        }

        private void playerSquareSelection(Point i_SelectedSquare, out eTileSelectionStatus o_SquareSelectionStatus)
        {
            if (!m_PlayingBoard.CheckIfSquareWithinRange(i_SelectedSquare))
            {
                o_SquareSelectionStatus = eTileSelectionStatus.IllegalSquareSelection;
            }
            else if (m_PlayingBoard.IsSquareRevealed(i_SelectedSquare))
            {
                o_SquareSelectionStatus = eTileSelectionStatus.SquareAlreadyRevealed;
            }
            else
            {
                m_PlayingBoard.RevealSquare(i_SelectedSquare);
                if (m_CurrentPlayer.FirstRevealedSquare == null)
                {
                    m_CurrentPlayer.FirstRevealedSquare = i_SelectedSquare;
                    o_SquareSelectionStatus = eTileSelectionStatus.PlayerRevealedFirstSquare;
                }
                else
                {
                    m_CurrentPlayer.SecondRevealedSquare = i_SelectedSquare;
                    o_SquareSelectionStatus = eTileSelectionStatus.PlayerRevealedSecondSquare;
                }
            }
        }

        private int pcSquareSelection(out eTileSelectionStatus o_SquareSelectionStatus)
        {
            Random rand = new Random();

            int randomSquaresListIndex = rand.Next(0, m_PcAvailableSquaresList.Count - 1);
            int squareListIndexValue = m_PcAvailableSquaresList[randomSquaresListIndex];
            Point pcSelectedSquare = Board.ExtractMatrixCordinates(squareListIndexValue, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
            m_PcAvailableSquaresList.Remove(squareListIndexValue);

            m_PlayingBoard.RevealSquare(pcSelectedSquare);
            if (m_CurrentPlayer.FirstRevealedSquare == null)
            {
                m_CurrentPlayer.FirstRevealedSquare = pcSelectedSquare;
                o_SquareSelectionStatus = eTileSelectionStatus.PlayerRevealedFirstSquare;
            }
            else
            {
                m_CurrentPlayer.SecondRevealedSquare = pcSelectedSquare;
                o_SquareSelectionStatus = eTileSelectionStatus.PlayerRevealedSecondSquare;
            }

            return squareListIndexValue;
        }

        public void PrintRevealedBoard()
        {
            for (int i = 0; i < m_PlayingBoard.BoardSize.X; i++)
            {
                for (int j = 0; j < m_PlayingBoard.BoardSize.Y; j++)
                {
                    Console.Write(String.Format("{0,-10}", m_PlayingBoard.Matrix[i, j].IsRevealed));
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n-------------------------------------------\n");
        }

        public void PrintValueBoard()
        {
            for (int i = 0; i < m_PlayingBoard.BoardSize.X; i++)
            {
                for (int j = 0; j < m_PlayingBoard.BoardSize.Y; j++)
                {
                    Console.Write(String.Format("{0,-5}", m_PlayingBoard.Matrix[i, j].Value));
                }
                Console.WriteLine();
            }
        }

    }    
}
