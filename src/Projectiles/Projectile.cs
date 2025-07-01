using System.Numerics;
using Raylib_cs;

namespace my_game.projectiles;

public struct Projectile(Vector2 position, Vector2 velocity, int damage, ProjectileSource projectileSource) : IProjectile
{
    public Vector2 Position { get; set; } = position;
    
    public static readonly Vector2 Size = new(10, 10); // Default size
    public Vector2 Velocity { get; set; }= velocity;
    public int Damage { get; set; } = damage;
    public bool IsActive { get; set; } = true;
    public ProjectileSource ProjectileSource { get; set; } = projectileSource;


    public Rectangle GetColliderRect()
    { 
        return new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
    }

}
