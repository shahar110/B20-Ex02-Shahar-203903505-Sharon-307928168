using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B20_Ex02_MemoryGame
{
    public class RememberedItem
    {
        private Point m_SquareLocation;
        private char m_SquareValue;
        private int m_SeenInTurn;

        public RememberedItem(Point i_SquareLocation, char i_SquareValue, int i_SeenInTurn)
        {
            m_SquareLocation = i_SquareLocation;
            m_SquareValue = i_SquareValue;
            m_SeenInTurn = i_SeenInTurn;
        }

        public Point Location
        {
            get { return m_SquareLocation; }
        }

        public char Value
        {
            get { return m_SquareValue; }
        }

        public int SeenInTurn
        {
            get { return m_SeenInTurn; }
        }
    }
}
