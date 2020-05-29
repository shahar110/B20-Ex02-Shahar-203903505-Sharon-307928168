using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class Game
    {
        private readonly Board m_PlayingBoard = null;
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        List<int> m_PcAvailableSquaresList = null;

        public Game(int i_BoardNumOfRows, int i_BoardNumOfCols, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            m_PlayingBoard = new Board(i_BoardNumOfRows, i_BoardNumOfCols);
            m_FirstPlayer = i_FirstPlayer;
            m_SecondPlayer = i_SecondPlayer;
            m_PcAvailableSquaresList = new List<int>();
            Board.FillGridList(m_PcAvailableSquaresList, i_BoardNumOfRows * i_BoardNumOfCols);
        }

        public void SingleMatch(Player i_FirstPlayer, Player i_SecondPlayer, out eGameStatus o_GameStatus)
        {
            PrintValueBoard();
            PrintRevealedBoard();
            o_GameStatus = eGameStatus.InProgress;
            while (o_GameStatus == eGameStatus.InProgress)
            {
                PlayerTurn(i_FirstPlayer, ePlayingMode.PlayerVsPlayer, out o_GameStatus);
                if (o_GameStatus != eGameStatus.GameFinished)
                {
                    PlayerTurn(i_SecondPlayer, ePlayingMode.PlayerVsPlayer, out o_GameStatus);
                }
            }

            if (i_FirstPlayer.Score > i_SecondPlayer.Score)
            {
                o_GameStatus = eGameStatus.FirstPlayerWon;
            }
            else
            {
                o_GameStatus = eGameStatus.SecondPlayerWon;
            }
        }

        public void PlayerTurn(Player i_CurrentPlayer, ePlayingMode i_PlayingMode, out eGameStatus o_GameStatus)
        {
            eSquareSelectionStatus playerSquareSlectionStatus = eSquareSelectionStatus.AwaitingSelection;
            while (playerSquareSlectionStatus != eSquareSelectionStatus.PlayerRevealedSecondSquare)
            {
                Point playerSelectedSquare = new Point();
                string userInput;
                int parsedUserInput;
                string revealedSquareNum;

                if (i_CurrentPlayer.FirstRevealedSquare == null)
                {
                    revealedSquareNum = "FIRST";
                }
                else
                {
                    revealedSquareNum = "SECOND";
                };

                Console.WriteLine(string.Format("Please enter {0} square row:", revealedSquareNum));
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out parsedUserInput))
                {
                    playerSelectedSquare.X = parsedUserInput - 1;
                }
                Console.WriteLine(string.Format("Please enter {0} square row:", revealedSquareNum));
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out parsedUserInput))
                {
                    playerSelectedSquare.Y = parsedUserInput - 1;
                }

                PlayerSquareSelection(i_CurrentPlayer, playerSelectedSquare, out playerSquareSlectionStatus);
            }
            
            if (m_PlayingBoard.GetSquareValue(i_CurrentPlayer.FirstRevealedSquare) == m_PlayingBoard.GetSquareValue(i_CurrentPlayer.SecondRevealedSquare))
            {
                Console.WriteLine(string.Format("Square {0} of value {1} and Square {2} of value {3} MATCH!!!"
                    , i_CurrentPlayer.FirstRevealedSquare.GetFormated()
                    , m_PlayingBoard.GetSquareValue(i_CurrentPlayer.FirstRevealedSquare)
                    , i_CurrentPlayer.SecondRevealedSquare.GetFormated()
                    , m_PlayingBoard.GetSquareValue(i_CurrentPlayer.SecondRevealedSquare)));
                i_CurrentPlayer.Score++;
                if (i_PlayingMode == ePlayingMode.PlayerVsPc)
                {
                    int firstSquareListValue = Board.CalculateListIndex(i_CurrentPlayer.FirstRevealedSquare, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
                    int secondSquareListValue = Board.CalculateListIndex(i_CurrentPlayer.SecondRevealedSquare, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
                    m_PcAvailableSquaresList.Remove(firstSquareListValue);
                    m_PcAvailableSquaresList.Remove(secondSquareListValue);
                }
                i_CurrentPlayer.ResetSquareSelection();
                PrintValueBoard();
                PrintRevealedBoard();
            }
            else
            {
                Console.WriteLine(string.Format("Square {0} of value {1} and Square {2} of value {3} DOESN'T match!"
                    ,i_CurrentPlayer.FirstRevealedSquare.GetFormated()
                    , m_PlayingBoard.GetSquareValue(i_CurrentPlayer.FirstRevealedSquare)
                    , i_CurrentPlayer.SecondRevealedSquare.GetFormated()
                    , m_PlayingBoard.GetSquareValue(i_CurrentPlayer.SecondRevealedSquare)));
                m_PlayingBoard.UnRevealSquare(i_CurrentPlayer.FirstRevealedSquare);
                m_PlayingBoard.UnRevealSquare(i_CurrentPlayer.SecondRevealedSquare);
                i_CurrentPlayer.ResetSquareSelection();
                PrintValueBoard();
                PrintRevealedBoard();
            }

            if (m_PlayingBoard.CheckIfFullyRevealed())
            {
                o_GameStatus = eGameStatus.GameFinished;
            }
            else
            {
                o_GameStatus = eGameStatus.InProgress;
            }
        }

        private void PlayerSquareSelection(Player i_CurrentPlayer, Point i_SelectedSquare, out eSquareSelectionStatus o_SquareSelectionStatus)
        {
            if (!m_PlayingBoard.CheckIfSquareWithinRange(i_SelectedSquare))
            {
                o_SquareSelectionStatus = eSquareSelectionStatus.IllegalSquareSelection;
                Console.WriteLine(string.Format("Invalid square {0} selection!", i_SelectedSquare.GetFormated()));
            }
            else if (m_PlayingBoard.IsSquareRevealed(i_SelectedSquare))
            {
                o_SquareSelectionStatus = eSquareSelectionStatus.SquareAlreadyRevealed;
                Console.WriteLine(string.Format("Square {0} is already revealed!", i_SelectedSquare.GetFormated()));
            }
            else
            {
                m_PlayingBoard.RevealSquare(i_SelectedSquare);
                if (i_CurrentPlayer.FirstRevealedSquare == null)
                {
                    i_CurrentPlayer.FirstRevealedSquare = i_SelectedSquare;
                    o_SquareSelectionStatus = eSquareSelectionStatus.PlayerRevealedFirstSquare;
                }
                else
                {
                    i_CurrentPlayer.SecondRevealedSquare = i_SelectedSquare;
                    o_SquareSelectionStatus = eSquareSelectionStatus.PlayerRevealedSecondSquare;
                }
                Console.WriteLine(string.Format("Square {0} of value {1} is now revealed!", i_SelectedSquare.GetFormated(), m_PlayingBoard.GetSquareValue(i_SelectedSquare)));
            }
        }


        public void PlayerVsPcMatch(Player i_FirstPlayer, Player i_PcPlayer, out eGameStatus o_GameStatus)
        {
            PrintValueBoard();
            PrintRevealedBoard();
            o_GameStatus = eGameStatus.InProgress;


            while (o_GameStatus == eGameStatus.InProgress)
            {
                PlayerTurn(i_FirstPlayer, ePlayingMode.PlayerVsPc, out o_GameStatus);
                if (o_GameStatus != eGameStatus.GameFinished)
                {
                    PcTurn(i_PcPlayer, out o_GameStatus);
                }
            }

            if (i_FirstPlayer.Score > i_PcPlayer.Score)
            {
                o_GameStatus = eGameStatus.FirstPlayerWon;
            }
            else
            {
                o_GameStatus = eGameStatus.SecondPlayerWon;
            }
        }

        //private int pcSquareSelection(Player i_PcPlayer)
        //{
        //    Random rand = new Random();

        //    int randomSquaresListIndex = rand.Next(0, m_PcAvailableSquaresList.Count - 1);
        //    int squareListIndexValue = m_PcAvailableSquaresList[randomSquaresListIndex];
        //    Point pcSelectedSquare = Board.ExtractMatrixCordinates(squareListIndexValue, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
        //    m_PcAvailableSquaresList.Remove(squareListIndexValue);
        //    i_PcPlayer.FirstRevealedSquare = pcSelectedSquare;

        //    Console.WriteLine(string.Format("PC selected square is {0} of value {1}"
        //        , pcSelectedSquare.GetFormated()
        //        , m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare)));

        //    return squareListIndexValue;
        //}

        public void PcTurn(Player i_PcPlayer, out eGameStatus o_GameStatus)
        {
            //int pcTurnsCounter = 0;
            //List<RememberedItem> rememberedElements = new List<RememberedItem>();

            Random rand = new Random();

            int randomFirstSquaresListIndex = rand.Next(0, m_PcAvailableSquaresList.Count - 1);
            int firstSquareListIndexValue = m_PcAvailableSquaresList[randomFirstSquaresListIndex];
            Point pcFirstSelectedSquare = Board.ExtractMatrixCordinates(firstSquareListIndexValue, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
            m_PcAvailableSquaresList.Remove(firstSquareListIndexValue);
            i_PcPlayer.FirstRevealedSquare = pcFirstSelectedSquare;
            m_PlayingBoard.RevealSquare(pcFirstSelectedSquare);

            int randomSecondSquareListIndex = rand.Next(0, m_PcAvailableSquaresList.Count - 1);
            int secondSquareListIndexValue = m_PcAvailableSquaresList[randomSecondSquareListIndex];
            Point pcSecondSelectedSquare = Board.ExtractMatrixCordinates(secondSquareListIndexValue, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
            m_PcAvailableSquaresList.Remove(secondSquareListIndexValue);
            i_PcPlayer.SecondRevealedSquare = pcSecondSelectedSquare;
            m_PlayingBoard.RevealSquare(pcSecondSelectedSquare);
            //int pcFirstSquareListValue = pcSquareSelection(i_PcPlayer);


            Console.WriteLine(string.Format("PC first selected square is {0} of value {1}"
                ,pcFirstSelectedSquare.GetFormated()
                ,m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare)));

            Console.WriteLine(string.Format("PC second selected square is {0} of value {1}"
                ,pcSecondSelectedSquare.GetFormated()
                ,m_PlayingBoard.GetSquareValue(i_PcPlayer.SecondRevealedSquare)));

            //PcSquareSelection(i_CurrentPlayer, playerSelectedSquare, out playerSquareSlectionStatus);
            
            if (m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare) == m_PlayingBoard.GetSquareValue(i_PcPlayer.SecondRevealedSquare))
            {
                Console.WriteLine(string.Format("Square {0} of value {1} and Square {2} of value {3} MATCH!!!"
                    , i_PcPlayer.FirstRevealedSquare.GetFormated()
                    , m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare)
                    , i_PcPlayer.SecondRevealedSquare.GetFormated()
                    , m_PlayingBoard.GetSquareValue(i_PcPlayer.SecondRevealedSquare)));
                i_PcPlayer.Score++;
                i_PcPlayer.ResetSquareSelection();
                PrintValueBoard();
                PrintRevealedBoard();
            }
            else
            {
                Console.WriteLine(string.Format("Square {0} of value {1} and Square {2} of value {3} DOESN'T match!"
                    , i_PcPlayer.FirstRevealedSquare.GetFormated()
                    , m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare)
                    , i_PcPlayer.SecondRevealedSquare.GetFormated()
                    , m_PlayingBoard.GetSquareValue(i_PcPlayer.SecondRevealedSquare)));
                m_PlayingBoard.UnRevealSquare(i_PcPlayer.FirstRevealedSquare);
                m_PlayingBoard.UnRevealSquare(i_PcPlayer.SecondRevealedSquare);
                m_PcAvailableSquaresList.Add(firstSquareListIndexValue);
                m_PcAvailableSquaresList.Add(secondSquareListIndexValue);
                i_PcPlayer.ResetSquareSelection();
                PrintValueBoard();
                PrintRevealedBoard();
            }

            if (m_PlayingBoard.CheckIfFullyRevealed())
            {
                o_GameStatus = eGameStatus.GameFinished;
            }
            else
            {
                o_GameStatus = eGameStatus.InProgress;
            }
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

    public enum eSquareSelectionStatus
    {
        AwaitingSelection,
        IllegalSquareSelection,
        SquareAlreadyRevealed,
        PlayerRevealedFirstSquare,
        PlayerRevealedSecondSquare,
    }

    public enum eGameStatus
    {
        InProgress,
        GameFinished,
        FirstPlayerWon,
        SecondPlayerWon,
    }

    public enum ePlayingMode
    {
        PlayerVsPlayer,
        PlayerVsPc,
    }
}
