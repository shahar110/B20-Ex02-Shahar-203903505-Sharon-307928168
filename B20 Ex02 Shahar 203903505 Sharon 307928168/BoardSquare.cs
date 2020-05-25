using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class BoardSquare
    {
        private readonly char m_Value;
        //private readonly Point m_Cordinates;
        private bool m_IsRevealed = false;

        public BoardSquare(char i_SquareValue)
        {
            m_Value = i_SquareValue;
        }

        public char Value
        {
            get { return m_Value; }
        }

        //public Point Cordinates
        //{
        //    get { return m_Cordinates; }
        //}

        public bool IsReaveled
        {
            get { return m_IsRevealed; }

            set { m_IsRevealed = value; }
        }

    }
}
