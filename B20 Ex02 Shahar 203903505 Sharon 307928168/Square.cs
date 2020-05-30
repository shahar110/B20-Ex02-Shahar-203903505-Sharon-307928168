using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B20_Ex02_MemoryGame
{
    public class Square
    {
        private readonly int m_Value;
        private bool m_IsRevealed = false;

        public Square(int i_SquareValue)
        {
            m_Value = i_SquareValue;
        }

        public int Value
        {
            get { return m_Value; }
        }

        public bool IsRevealed
        {
            get { return m_IsRevealed; }
            set { m_IsRevealed = value; }
        }
    }
}
