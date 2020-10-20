using System;
using System.Numerics;
using Raylib_cs;

namespace RayLibSweeper
{
    public class Map
    {
        public int width = 20;
        public int height = 20;
        public int[,] intMap;
        public int sqaureSpace = 30;
        public int sqaureWidth = 28;
        public Color bombColor = new Color(0, 0, 0, 255);
        public Color grey = new Color(187, 190, 196, 255);
        public Color clicked = new Color(237, 235, 190, 255);



        public void Generate(Bomb bomb)
        {
            Vector2[] bombs = bomb.positions;
            intMap = new int[height, width];

            for (int xfor = 0; xfor < intMap.GetLength(1); xfor++)
            {
                for (int yfor = 0; yfor < intMap.GetLength(0); yfor++)
                {
                    intMap[yfor, xfor] = 0;
                }
            }
            for (int i = 0; i < bombs.Length; i++)
            {
                //intMap[ (int)bombs[i].Y, (int)bombs[i].X ] = 1;
                intMap[(int)bombs[i].Y, (int)bombs[i].X] = 1;
            }
        }

        public Map(int xOk, int yOk)
        {
            width = yOk;
            height = xOk;
        }
        public void Write()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (intMap[y, x] == 1)
                    {
                        Raylib.DrawRectangle(x * sqaureSpace, y * sqaureSpace, sqaureWidth, sqaureWidth, bombColor);
                    }
                    else
                    {
                        Raylib.DrawRectangle(x * sqaureSpace, y * sqaureSpace, sqaureWidth, sqaureWidth, grey);
                    }
                }
            }
        }
    }
}
