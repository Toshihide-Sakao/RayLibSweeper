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
                

                Raylib.EndDrawing();
            }
        }
    }
}
