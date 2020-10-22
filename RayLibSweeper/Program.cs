using System.Numerics;
using System;
using Raylib_cs;

namespace RayLibSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            int height = 30;
            int width = 30;
            bool again = false;

            Map map = new Map(width, height);
            Bomb bomb = new Bomb(map, 60);
            map.Generate(bomb);
            //map.Write();

            //Console.ReadLine();

            Raylib.InitWindow(width * map.sqaureSpace, height * map.sqaureSpace, "Gaming time");



            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.BLACK);
                if (!map.gameOver)
                {
                    map.Write();

                    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        PressedTiles(map, 0);
                    }
                    else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
                    {
                        PressedTiles(map, 1);
                    }
                }
                else
                {
                    Raylib.DrawText("Gameover", 50, 50, 100, Color.WHITE);

                    Raylib.DrawRectangle(40, 200, 300, 50, Color.WHITE);
                    Raylib.DrawText("play again", 50, 200, 40, Color.BLACK);

                    StartMenuPress(map, bomb, width, height);
                }


                Raylib.EndDrawing();
            }
        }

        static void PressedTiles(Map map, int mouseButton)
        {
            int xbruh = Raylib.GetMouseX() / map.sqaureSpace;
            int ybruh = Raylib.GetMouseY() / map.sqaureSpace;
            if (mouseButton == 0)
            {
                map.Clicked(xbruh, ybruh);
            }
            else
            {
                map.RightClicked(xbruh, ybruh);
            }

        }

        static void StartMenuPress(Map map, Bomb bomb, int width, int height)
        {
            Vector2 pos = Raylib.GetMousePosition();

            if (pos.X < 340 && pos.X > 40 && pos.Y < 240 && pos.Y > 200 && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                bomb = new Bomb(map, 200);
                map.Generate(bomb);

                map.gameOver = false;
            }
        }
    }
}
