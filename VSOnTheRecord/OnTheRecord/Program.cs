using System;
using System.Collections.Generic;
using System.Text;
using OnTheRecord.Map;

namespace OnTheRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            PlainTileState p = new PlainTileState();
/*
            int[,] w = new int[7, 7]{
                {0, 0, 1, 1, 1, 0, 0},
                {0, 1, 3, 3, 3, 1, 0},
                {1, 3, 3, 5, 3, 3, 1},
                {1, 3, 5, 0, 5, 3, 1},
                {1, 3, 3, 5, 3, 3, 1},
                {0, 1, 3, 3, 3, 1, 0},
                {0, 0, 1, 1, 1, 0, 0}
*/
            int[,] w = new int[7, 5]{
                {0, 0, 2, 0, 0},
                {0, 2, 4, 2, 0},
                {0, 2, 5, 2, 0},
                {1, 3, 0, 3, 1},
                {0, 2, 5, 2, 0},
                {0, 2, 4, 2, 0},
                {0, 0, 2, 0, 0}
/*
            int[,] w = new int[11, 7]{
                {15, 3, 0, 0, 0, 3, 15},
                {3, 3, 0, 0, 0, 3, 3},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 3, 0, 0, 0},
                {0, 0, 0, 3, 0, 0, 0},
                {0, 0, 1, 0, 1, 0, 0},
                {0, 0, 0, 3, 0, 0, 0},
                {0, 0, 0, 3, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {3, 3, 0, 0, 0, 3, 3},
                {15, 3, 0, 0, 0, 3, 15}
*/
            };
            Room r = new Room(80, 50, 0.55f, p, w);
            r.PrintMatrix();
        }
    }
}
