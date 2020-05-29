﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleUI
{
    public class Point
    {
        private int m_X;
        private int m_Y;

        public Point()
        {
            m_X = 0;
            m_Y = 0;
        }

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

        public void Set(int i_DimentionX, int i_DimentionY)
        {
            m_X = i_DimentionX;
            m_Y = i_DimentionY;
        }

        public string GetFormated()
        {
            return string.Format("{0},{1}", m_X, m_Y);
        }
    }
}
