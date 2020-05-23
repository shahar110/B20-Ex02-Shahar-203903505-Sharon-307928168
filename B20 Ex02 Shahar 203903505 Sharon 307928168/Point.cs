using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    class Point
    {
        private int m_X;
        private int m_Y;

        public Point(int i_DimentionX, int i_DimentionY)
        {
            m_X = i_DimentionX;
            m_Y = i_DimentionY;
        }

        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

    }
}
