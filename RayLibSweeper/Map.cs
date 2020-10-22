using System;
using System.Collections.Generic;
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
        public Color flagColor = new Color(65, 141, 240, 255);



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
                    if (IfBySide(checkX, checkY, changeX, changeY))
                    {
                        if (intMap[checkY + changeY, checkX + changeX] == 99)
                        {
                            nearbyBombs++;
                        }
                    }
                }
            }

            return nearbyBombs;
        }

        bool IfBySide(int x, int y, int changeX, int changeY)
        {
            bool xfirst = x != 0 || changeX != -1;
            bool xlast = x != width - 1 || changeX != 1;
            bool yfirst = y != 0 || changeY != -1;
            bool ylast = y != height - 1 || changeY != 1;
            bool middle = changeX != 0 || changeY != 0;

            return xfirst && xlast && yfirst && ylast && middle;
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
                    else if (vissibleMap[y, x] == 2)
                    {
                        Raylib.DrawRectangle(x * sqaureSpace, y * sqaureSpace, sqaureWidth, sqaureWidth, flagColor);
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
            if (intMap[y, x] == 0)
            {
                for (int xfor = -1; xfor < 2; xfor++)
                {
                    for (int yfor = -1; yfor < 2; yfor++)
                    {
                        if (IfBySide(x, y, xfor, yfor))
                        {
                            vissibleMap[y + yfor, x + xfor] = 1;
                            List<Vector2> emptyList = checkForEmpty(x + xfor, y + yfor);
                            for (int i = 0; i < emptyList.Count; i++)
                            {
                                ChnageEmptys(emptyList[i]);
                            }
                        }
                    }
                }
            }
        }

        public void RightClicked(int x, int y)
        {
            if (vissibleMap[y, x] == 2)
            {
                vissibleMap[y, x] = 0;
            }
            else
            {
                vissibleMap[y, x] = 2;
            }
        }

        List<Vector2> checkForEmpty(int x, int y)
        {
            List<Vector2> emptyPlaces = new List<Vector2>();
            for (int xfor = -1; xfor < 2; xfor++)
            {
                for (int yfor = -1; yfor < 2; yfor++)
                {
                    if (IfBySide(x, y, xfor, yfor))
                    {
                        if (intMap[y + yfor, x + xfor] == 0)
                        {
                            emptyPlaces.Add(new Vector2(x + xfor, y + yfor));
                        }
                    }

                }
            }
            return emptyPlaces;
        }

        void ChnageEmptys(Vector2 ok)
        {
            for (int xfor = -1; xfor < 2; xfor++)
            {
                for (int yfor = -1; yfor < 2; yfor++)
                {
                    if (IfBySide((int)ok.X, (int)ok.Y, xfor, yfor))
                    {
                        if (intMap[(int)(ok.Y + yfor), (int)(ok.X + xfor)] == 0)
                        {
                            vissibleMap[(int)(ok.Y + yfor), (int)(ok.X + xfor)] = 1;
                        }
                    }
                }
            }
        }
    }


}

