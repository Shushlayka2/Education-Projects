﻿using System;
using System.Collections.Generic;

namespace Snake
{
    class VerticalLine : Figure
    {
        public VerticalLine(int yUp, int yDown, int x, char sym)
        {
            pList = new List<Point>();
            for (int y = yUp; y <= yDown; y++)
            {
                Point p = new Point(x, y, sym);
                pList.Add(p);
            }
        }

        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (Point p in pList)
            {
                p.Draw();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
