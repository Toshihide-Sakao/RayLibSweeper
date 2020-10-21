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
        int[,] vissibleMap;
        public int sqaureSpace = 30;
        public int sqaureWidth = 28;
        public Color bombColor = new Color(156, 49, 37, 255);
        public Color grey = new Color(187, 190, 196, 255);
        public Color clicked = new Color(70, 219, 93, 255);



        public void Generate(Bomb bomb)
        {
            Vector2[] bombs = bomb.positions;
            intMap = new int[height, width];
            vissibleMap = new int[height, width];

            for (int xfor = 0; xfor < intMap.GetLength(1); xfor++)
            {
                for (int yfor = 0; yfor < intMap.GetLength(0); yfor++)
                {
                    if (intMap[yfor, xfor] != 99)
                    {
                        intMap[yfor, xfor] = 0;
                    }
                }
            }
            for (int i = 0; i < bombs.Length; i++)
            {
                intMap[(int)bombs[i].Y, (int)bombs[i].X] = 99;
            }

            for (int x = 0; x < intMap.GetLength(1); x++)
            {
                for (int y = 0; y < intMap.GetLength(0); y++)
                {
                    if (intMap[y, x] != 99)
                    {
                        intMap[y, x] = CheckSorrounding(x, y);
                    }
                }
            }
        }

        int CheckSorrounding(int checkX, int checkY)
        {
            int nearbyBombs = 0;

            for (int changeX = -1; changeX < 2; changeX++)
            {
                for (int changeY = -1; changeY < 2; changeY++)
                {
                    bool xfirst = checkX != 0 || changeX != -1;
                    bool xlast = checkX != width - 1 || changeX != 1;
                    bool yfirst = checkY != 0 || changeY != -1;
                    bool ylast = checkY != height - 1 || changeY != 1;
                    bool middle = changeX != 0 || changeY != 0;
                    if (xfirst)
                    {
                        if (xlast)
                        {
                            if (yfirst)
                            {
                                if (ylast)
                                {
                                    if (middle)
                                    {
                                        if (intMap[checkY + changeY, checkX + changeX] == 99)
                                        {
                                            nearbyBombs++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return nearbyBombs;
        }

        public Map(int xOk, int yOk)
        {
            width = yOk;
            height = xOk;
        }
        public void WriteCheat()
        {
            //float textOffset = sqaureSpace / 3;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Raylib.DrawRectangle(x * sqaureSpace, y * sqaureSpace, sqaureWidth, sqaureWidth, clicked);

                    for (int i = 1; i < 10; i++)
                    {
                        if (i == intMap[y, x])
                        {
                            Raylib.DrawText(i.ToString(), x * sqaureSpace, y * sqaureSpace, 30, Color.BLACK);
                        }
                    }
                    if (intMap[y, x] == 99)
                    {
                        Raylib.DrawRectangle(x * sqaureSpace, y * sqaureSpace, sqaureWidth, sqaureWidth, bombColor);
                    }
                }
            }
        }

        public void Write()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (vissibleMap[y, x] == 1)
                    {
                        Raylib.DrawRectangle(x * sqaureSpace, y * sqaureSpace, sqaureWidth, sqaureWidth, clicked);

                        for (int i = 1; i < 10; i++)
                        {
                            if (i == intMap[y, x])
                            {
                                Raylib.DrawText(i.ToString(), x * sqaureSpace, y * sqaureSpace, 30, Color.BLACK);
                            }
                        }
                        if (intMap[y, x] == 99)
                        {
                            Raylib.DrawRectangle(x * sqaureSpace, y * sqaureSpace, sqaureWidth, sqaureWidth, bombColor);
                        }
                    }
                    else
                    {
                        Raylib.DrawRectangle(x * sqaureSpace, y * sqaureSpace, sqaureWidth, sqaureWidth, grey);
                    }
                }
            }
        }

        public void Clicked(int x, int y)
        {
            vissibleMap[y, x] = 1;
            bool looooop = intMap[y, x] == 0;
            if (looooop)
            {
                for (int xfor = -1; xfor < 2; xfor++)
                {
                    for (int yfor = -1; yfor < 2; yfor++)
                    {
                        vissibleMap[y + yfor, x + xfor] = 1;

                        if (intMap[y + yfor, x + xfor] == 0)
                        {
                            
                        }
                    }
                }
            }
        }

        void checkForEmpty(int x, int y)
        {

        }
    }
}
