using System.Numerics;
using Raylib_cs;

namespace my_game.enemies;

public struct Enemy(string imageKey,Vector2 position, Vector2 velocity, Vector2 size, float rotation = 0f)
{
    public bool isActive = true;
    public string imageKey = imageKey;
    public Vector2 position = position;
    public Vector2 velocity = velocity;
    public Vector2 size = size; 
    public float rotation = rotation;

    public Rectangle GetRect() => new Rectangle(position.X, position.Y, size.X, size.Y);
}
