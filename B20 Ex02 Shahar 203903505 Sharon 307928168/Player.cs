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
        private Point m_SecondRevealedSquare = null;
        private bool m_IsPc = false;

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
            m_FirstRevealedSquare = null;
            m_SecondRevealedSquare = null;
        }

        public bool IsPc
        {
            get { return m_IsPc; }
            set { m_IsPc = value; }
        }
    }
}
