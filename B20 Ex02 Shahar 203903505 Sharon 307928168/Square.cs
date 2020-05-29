using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class Square
    {
        private readonly char m_Value;
        private bool m_IsRevealed = false;

        public Square(char i_SquareValue)
        {
            m_Value = i_SquareValue;
        }

        public char Value
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
