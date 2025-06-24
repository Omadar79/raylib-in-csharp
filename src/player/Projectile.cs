using System.Numerics;
using Raylib_cs;

namespace my_game.player;

public struct Projectile(Vector2 position, Vector2 velocity, int damage)
{
    public bool isActive = true;
    public Vector2 Position = position;
    public Vector2 Velocity = velocity;
    public int Damage = damage;
    public static readonly Vector2 Size = new(10, 10); // Default size
    public Rectangle GetRect() => new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
}
