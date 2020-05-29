using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class Game
    {
        public enum eTileSelectionStatus
        {
            AwaitingSelection,
            IllegalSquareSelection,
            SquareAlreadyRevealed,
            PlayerRevealedFirstSquare,
            PlayerRevealedSecondSquare,
            SquaresMatch,
            SquaresDontMatch,
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

        private readonly Board m_PlayingBoard = null;
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        private Player m_CurrentPlayer = null;
        List<int> m_PcAvailableSquaresList = null;

        public Game(int i_BoardNumOfRows, int i_BoardNumOfCols, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            m_PlayingBoard = new Board(i_BoardNumOfRows, i_BoardNumOfCols);
            m_FirstPlayer = i_FirstPlayer;
            m_SecondPlayer = i_SecondPlayer;
            m_CurrentPlayer = i_FirstPlayer;
            m_PcAvailableSquaresList = new List<int>();
            Board.FillGridList(m_PcAvailableSquaresList, i_BoardNumOfRows * i_BoardNumOfCols);
        }

        //public void SingleMatch(Player i_FirstPlayer, Player i_SecondPlayer, out eGameStatus o_GameStatus)
        //{
        //    PrintValueBoard();
        //    PrintRevealedBoard();
        //    o_GameStatus = eGameStatus.InProgress;
        //    while (o_GameStatus == eGameStatus.InProgress)
        //    {
        //        PlayerTurn(i_FirstPlayer, ePlayingMode.PlayerVsPlayer, out o_GameStatus);
        //        if (o_GameStatus != eGameStatus.GameFinished)
        //        {
        //            PlayerTurn(i_SecondPlayer, ePlayingMode.PlayerVsPlayer, out o_GameStatus);
        //        }
        //    }

        //    if (i_FirstPlayer.Score > i_SecondPlayer.Score)
        //    {
        //        o_GameStatus = eGameStatus.FirstPlayerWon;
        //    }
        //    else
        //    {
        //        o_GameStatus = eGameStatus.SecondPlayerWon;
        //    }
        //}

        public void PlayerMove(Point i_SelectedSquare, ePlayingMode i_PlayingMode, out eTileSelectionStatus o_SquareSelectionStatus, out eGameStatus o_GameStatus)
        {
            playerSquareSelection(i_SelectedSquare, out o_SquareSelectionStatus);

            if (o_SquareSelectionStatus == eTileSelectionStatus.PlayerRevealedSecondSquare)
            {
                evalueatePlayerMove(i_PlayingMode, out o_SquareSelectionStatus);
                swapTurns();

                if (m_PlayingBoard.CheckIfFullyRevealed())
                {
                    o_GameStatus = eGameStatus.GameFinished;
                }
                else
                {
                    o_GameStatus = eGameStatus.InProgress;
                }
            }
            else
            {
                o_GameStatus = eGameStatus.InProgress;
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

        private void evalueatePlayerMove(ePlayingMode i_PlayingMode, out eTileSelectionStatus o_SquareSelectionStatus)
        {
            if (m_PlayingBoard.GetSquareValue(m_CurrentPlayer.FirstRevealedSquare) == m_PlayingBoard.GetSquareValue(m_CurrentPlayer.SecondRevealedSquare))
            {
                m_CurrentPlayer.Score++;
                if (i_PlayingMode == ePlayingMode.PlayerVsPc)
                {
                    int firstSquareListValue = Board.CalculateListIndex(m_CurrentPlayer.FirstRevealedSquare, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
                    int secondSquareListValue = Board.CalculateListIndex(m_CurrentPlayer.SecondRevealedSquare, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
                    m_PcAvailableSquaresList.Remove(firstSquareListValue);
                    m_PcAvailableSquaresList.Remove(secondSquareListValue);
                }
                m_CurrentPlayer.ResetSquareSelection();
                o_SquareSelectionStatus = eTileSelectionStatus.SquaresMatch;
                PrintValueBoard();
                PrintRevealedBoard();

            }
            else
            {
                m_PlayingBoard.UnRevealSquare(m_CurrentPlayer.FirstRevealedSquare);
                m_PlayingBoard.UnRevealSquare(m_CurrentPlayer.SecondRevealedSquare);
                m_CurrentPlayer.ResetSquareSelection();
                o_SquareSelectionStatus = eTileSelectionStatus.SquaresDontMatch;
                PrintValueBoard();
                PrintRevealedBoard();
            }
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

        //public void PlayerTurn(Player i_CurrentPlayer, ePlayingMode i_PlayingMode, out eGameStatus o_GameStatus)
        //{
        //    eSquareSelectionStatus playerSquareSlectionStatus = eSquareSelectionStatus.AwaitingSelection;
        //    while (playerSquareSlectionStatus != eSquareSelectionStatus.PlayerRevealedSecondSquare)
        //    {
        //        Point playerSelectedSquare = new Point();
        //        string userInput;
        //        int parsedUserInput;
        //        string revealedSquareNum;

        //        if (i_CurrentPlayer.FirstRevealedSquare == null)
        //        {
        //            revealedSquareNum = "FIRST";
        //        }
        //        else
        //        {
        //            revealedSquareNum = "SECOND";
        //        };

        //        Console.WriteLine(string.Format("Please enter {0} square row:", revealedSquareNum));
        //        userInput = Console.ReadLine();
        //        if (int.TryParse(userInput, out parsedUserInput))
        //        {
        //            playerSelectedSquare.X = parsedUserInput - 1;
        //        }
        //        Console.WriteLine(string.Format("Please enter {0} square row:", revealedSquareNum));
        //        userInput = Console.ReadLine();
        //        if (int.TryParse(userInput, out parsedUserInput))
        //        {
        //            playerSelectedSquare.Y = parsedUserInput - 1;
        //        }

        //        PlayerSquareSelection(i_CurrentPlayer, playerSelectedSquare, out playerSquareSlectionStatus);
        //    }

        //    if (m_PlayingBoard.GetSquareValue(i_CurrentPlayer.FirstRevealedSquare) == m_PlayingBoard.GetSquareValue(i_CurrentPlayer.SecondRevealedSquare))
        //    {
        //        Console.WriteLine(string.Format("Square {0} of value {1} and Square {2} of value {3} MATCH!!!"
        //            , i_CurrentPlayer.FirstRevealedSquare.GetFormated()
        //            , m_PlayingBoard.GetSquareValue(i_CurrentPlayer.FirstRevealedSquare)
        //            , i_CurrentPlayer.SecondRevealedSquare.GetFormated()
        //            , m_PlayingBoard.GetSquareValue(i_CurrentPlayer.SecondRevealedSquare)));
        //        i_CurrentPlayer.Score++;
        //        if (i_PlayingMode == ePlayingMode.PlayerVsPc)
        //        {
        //            int firstSquareListValue = Board.CalculateListIndex(i_CurrentPlayer.FirstRevealedSquare, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
        //            int secondSquareListValue = Board.CalculateListIndex(i_CurrentPlayer.SecondRevealedSquare, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
        //            m_PcAvailableSquaresList.Remove(firstSquareListValue);
        //            m_PcAvailableSquaresList.Remove(secondSquareListValue);
        //        }
        //        i_CurrentPlayer.ResetSquareSelection();
        //        PrintValueBoard();
        //        PrintRevealedBoard();
        //    }
        //    else
        //    {
        //        Console.WriteLine(string.Format("Square {0} of value {1} and Square {2} of value {3} DOESN'T match!"
        //            ,i_CurrentPlayer.FirstRevealedSquare.GetFormated()
        //            , m_PlayingBoard.GetSquareValue(i_CurrentPlayer.FirstRevealedSquare)
        //            , i_CurrentPlayer.SecondRevealedSquare.GetFormated()
        //            , m_PlayingBoard.GetSquareValue(i_CurrentPlayer.SecondRevealedSquare)));
        //        m_PlayingBoard.UnRevealSquare(i_CurrentPlayer.FirstRevealedSquare);
        //        m_PlayingBoard.UnRevealSquare(i_CurrentPlayer.SecondRevealedSquare);
        //        i_CurrentPlayer.ResetSquareSelection();
        //        PrintValueBoard();
        //        PrintRevealedBoard();
        //    }

        //    if (m_PlayingBoard.CheckIfFullyRevealed())
        //    {
        //        o_GameStatus = eGameStatus.GameFinished;
        //    }
        //    else
        //    {
        //        o_GameStatus = eGameStatus.InProgress;
        //    }
        //}


        //public void PlayerVsPcMatch(Player i_FirstPlayer, Player i_PcPlayer, out eGameStatus o_GameStatus)
        //{
        //    PrintValueBoard();
        //    PrintRevealedBoard();
        //    o_GameStatus = eGameStatus.InProgress;


        //    while (o_GameStatus == eGameStatus.InProgress)
        //    {
        //        PlayerTurn(i_FirstPlayer, ePlayingMode.PlayerVsPc, out o_GameStatus);
        //        if (o_GameStatus != eGameStatus.GameFinished)
        //        {
        //            PcTurn(i_PcPlayer, out o_GameStatus);
        //        }
        //    }

        //    if (i_FirstPlayer.Score > i_PcPlayer.Score)
        //    {
        //        o_GameStatus = eGameStatus.FirstPlayerWon;
        //    }
        //    else
        //    {
        //        o_GameStatus = eGameStatus.SecondPlayerWon;
        //    }
        //}

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

        //public void PcTurn(Player i_PcPlayer, out eGameStatus o_GameStatus)
        //{
        //    //int pcTurnsCounter = 0;
        //    //List<RememberedItem> rememberedElements = new List<RememberedItem>();

        //    Random rand = new Random();

        //    int randomFirstSquaresListIndex = rand.Next(0, m_PcAvailableSquaresList.Count - 1);
        //    int firstSquareListIndexValue = m_PcAvailableSquaresList[randomFirstSquaresListIndex];
        //    Point pcFirstSelectedSquare = Board.ExtractMatrixCordinates(firstSquareListIndexValue, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
        //    m_PcAvailableSquaresList.Remove(firstSquareListIndexValue);
        //    i_PcPlayer.FirstRevealedSquare = pcFirstSelectedSquare;
        //    m_PlayingBoard.RevealSquare(pcFirstSelectedSquare);

        //    int randomSecondSquareListIndex = rand.Next(0, m_PcAvailableSquaresList.Count - 1);
        //    int secondSquareListIndexValue = m_PcAvailableSquaresList[randomSecondSquareListIndex];
        //    Point pcSecondSelectedSquare = Board.ExtractMatrixCordinates(secondSquareListIndexValue, m_PlayingBoard.BoardSize.X, m_PlayingBoard.BoardSize.Y);
        //    m_PcAvailableSquaresList.Remove(secondSquareListIndexValue);
        //    i_PcPlayer.SecondRevealedSquare = pcSecondSelectedSquare;
        //    m_PlayingBoard.RevealSquare(pcSecondSelectedSquare);
        //    //int pcFirstSquareListValue = pcSquareSelection(i_PcPlayer);


        //    Console.WriteLine(string.Format("PC first selected square is {0} of value {1}"
        //        ,pcFirstSelectedSquare.GetFormated()
        //        ,m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare)));

        //    Console.WriteLine(string.Format("PC second selected square is {0} of value {1}"
        //        ,pcSecondSelectedSquare.GetFormated()
        //        ,m_PlayingBoard.GetSquareValue(i_PcPlayer.SecondRevealedSquare)));

        //    //PcSquareSelection(i_CurrentPlayer, playerSelectedSquare, out playerSquareSlectionStatus);
            
        //    if (m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare) == m_PlayingBoard.GetSquareValue(i_PcPlayer.SecondRevealedSquare))
        //    {
        //        Console.WriteLine(string.Format("Square {0} of value {1} and Square {2} of value {3} MATCH!!!"
        //            , i_PcPlayer.FirstRevealedSquare.GetFormated()
        //            , m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare)
        //            , i_PcPlayer.SecondRevealedSquare.GetFormated()
        //            , m_PlayingBoard.GetSquareValue(i_PcPlayer.SecondRevealedSquare)));
        //        i_PcPlayer.Score++;
        //        i_PcPlayer.ResetSquareSelection();
        //        PrintValueBoard();
        //        PrintRevealedBoard();
        //    }
        //    else
        //    {
        //        Console.WriteLine(string.Format("Square {0} of value {1} and Square {2} of value {3} DOESN'T match!"
        //            , i_PcPlayer.FirstRevealedSquare.GetFormated()
        //            , m_PlayingBoard.GetSquareValue(i_PcPlayer.FirstRevealedSquare)
        //            , i_PcPlayer.SecondRevealedSquare.GetFormated()
        //            , m_PlayingBoard.GetSquareValue(i_PcPlayer.SecondRevealedSquare)));
        //        m_PlayingBoard.UnRevealSquare(i_PcPlayer.FirstRevealedSquare);
        //        m_PlayingBoard.UnRevealSquare(i_PcPlayer.SecondRevealedSquare);
        //        m_PcAvailableSquaresList.Add(firstSquareListIndexValue);
        //        m_PcAvailableSquaresList.Add(secondSquareListIndexValue);
        //        i_PcPlayer.ResetSquareSelection();
        //        PrintValueBoard();
        //        PrintRevealedBoard();
        //    }

        //    if (m_PlayingBoard.CheckIfFullyRevealed())
        //    {
        //        o_GameStatus = eGameStatus.GameFinished;
        //    }
        //    else
        //    {
        //        o_GameStatus = eGameStatus.InProgress;
        //    }
        //}

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
