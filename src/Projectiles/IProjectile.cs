using System.Numerics;
using Raylib_cs;


namespace my_game.projectiles;

public interface IProjectile
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public int Damage { get; set; }
    public bool IsActive { get; set; }
    public ProjectileSource ProjectileSource { get; set; }
    public Rectangle GetColliderRect();
   
}

public enum ProjectileSource
{
    All, // For generic projectiles that can hit anything
    Player,
    Enemy,
    Environment
}

// Example decorator: Explosive projectile