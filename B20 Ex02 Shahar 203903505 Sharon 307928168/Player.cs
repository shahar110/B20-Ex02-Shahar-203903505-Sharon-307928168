using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class Player
    {
        private readonly string m_Name;
        private int m_CurrentScore = 0;
        private Point m_FirstRevealedSquare = null;
        //private bool m_IsFirstSquareRevealed;
        private Point m_SecondRevealedSquare = null;
        //private bool m_IsSecondSquareRevealed;

        public Player(string i_PlayerName)
        {
            m_Name = i_PlayerName;
        }

        public Point FirstRevealedSquare
        {
            get { return m_FirstRevealedSquare; }
            set { m_FirstRevealedSquare = value; }
        }

        public Point SecondRevealedSquare
        {
            get { return m_SecondRevealedSquare; }
            set { m_SecondRevealedSquare = value; }
        }

        public int Score
        {
            get { return m_CurrentScore; }
            set { m_CurrentScore = value; }
        }

        public void ResetSquareSelection()
        {
            //m_FirstRevealedSquare.Set(-1, -1);
            //m_SecondRevealedSquare.Set(-2, -2);
            //m_IsFirstSquareRevealed = false;
            //m_IsSecondSquareRevealed = false;
            m_FirstRevealedSquare = null;
            m_SecondRevealedSquare = null;
        }
    }
}
