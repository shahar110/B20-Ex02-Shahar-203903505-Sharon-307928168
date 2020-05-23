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
        private readonly char m_Col;
        private readonly int m_Row;
        private bool m_IsRevealed = false;

        public BoardSquare(char i_SquareColumn, int i_SquareRow, char i_SquareValue)
        {
            m_Col = i_SquareColumn;
            m_Row = i_SquareRow;
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
        }

    }
}
