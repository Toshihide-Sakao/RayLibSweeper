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
            

            Map map = new Map(width, height);
            Bomb bomb = new Bomb(map, 200);
            map.Generate(bomb);
            //map.Write();

            //Console.ReadLine();

            Raylib.InitWindow(width * map.sqaureSpace, height * map.sqaureSpace, "Gaming time");

            

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.BLACK);

                map.Write();
                
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    PressedTiles(map, 0);
                }
                else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
                {
                    PressedTiles(map, 1);
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
    }
}
