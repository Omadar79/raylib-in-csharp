using System.Numerics;
using Raylib_cs;

namespace my_game.Enemies
{
    public struct Enemy(Vector2 position, Vector2 velocity, Vector2 size)
    {
        public bool isActive = true;
        public Vector2 Position = position;
        public Vector2 Velocity = velocity;
        public Vector2 Size = size; 
        //public static readonly Vector2 Size = new(20, 20); // Default size

        public Rectangle GetRect() => new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
    }
}
