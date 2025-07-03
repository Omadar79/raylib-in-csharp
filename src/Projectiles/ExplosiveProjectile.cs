using System.Numerics;
using Raylib_cs;

namespace my_game.projectiles;

public class ExplosiveProjectile(Vector2 position, Vector2 velocity, int damage, ProjectileSource projectileSource) : IProjectile
{
    public Vector2 Position { get; set; } = position;
    public Vector2 Velocity { get; set; } = velocity;
    public int Damage { get; set; } = damage;
    public bool IsActive { get; set; }
    private readonly int _explosionRadius;
    
    public Rectangle GetColliderRect()
    {
        return new Rectangle(Position.X, Position.Y, 10, 10);
    }

    public ProjectileSource ProjectileSource { get; set; }


    private void Explode()
    {
        Raylib.DrawCircleV(Position, _explosionRadius, Color.Red);
        Console.WriteLine($"Explosion at {Position} with radius {_explosionRadius}");
    }

 
  
}