using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B20_Ex02_MemoryGame
{
    public class Player
    {
        private readonly string m_Name;
        private int m_CurrentScore = 0;
        private Point m_FirstRevealedSquare = null;
        private Point m_SecondRevealedSquare = null;
        private readonly bool m_IsPc = false;

        public Player(string i_PlayerName, bool i_IsPc)
        {
            m_Name = i_PlayerName;
            m_IsPc = i_IsPc;
        }

        public string Name
        {
            get { return m_Name; }
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
        }
    }
}
