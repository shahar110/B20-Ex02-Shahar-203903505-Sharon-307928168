using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class Player
    {
        private readonly string m_Name;
        //private int m_Score = 0;
        //private Point m_FirstRevealedSquare;
        //private Point m_SecondRevealedSquare;

        public Player(string i_PlayerName)
        {
            m_Name = i_PlayerName;
        }

        public string Name
        {
            get { return m_Name; }
        }

        //public Point FirstRevealedSquare
        //{
        //    get { return m_FirstRevealedSquare; }
        //    set { m_FirstRevealedSquare = value; }
        //}

        //public Point SecondRevealedSquare
        //{
        //    get { return m_SecondRevealedSquare; }
        //    set { m_SecondRevealedSquare = value; }
        //}
    }
}
