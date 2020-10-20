using System;
using System.Numerics;

namespace RayLibSweeper
{
    public class Bomb
    {
        static Random generator = new Random();
        public Vector2[] positions;

        public Bomb(Map map, int amount) 
        {
            positions = new Vector2[amount];
            for (int i = 0; i < amount; i++)
            {
                positions[i].X = generator.Next(0, map.width);
            }
            for (int i = 0; i < amount; i++)
            {
                positions[i].Y = generator.Next(0, map.height);
            }
        }

        // public Bombs(int amount, Map map) 
        // {
        //     GeneratePositions(map, amount);
        // }
    }
}
