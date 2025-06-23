using System.Numerics;
using Raylib_cs;
using my_game.Enemies;

namespace my_game.player
{
    
    public class ProjectileSystem
    {
        // Maximum number of projectiles allowed at once
        private const int MAX_PROJECTILES = 1024; // Tune as needed
        // Pre-allocated array for projectile pooling
        private readonly Projectile[] _projectiles = new Projectile[MAX_PROJECTILES];
        // Number of currently active projectiles
        private int _activeCount = 0;

        // Adds a new projectile to the pool if there is space
        public void AddProjectile(Vector2 position, Vector2 velocity, float speed = 600f, int damage = 1)
        {
            if (_activeCount >= MAX_PROJECTILES)
            {
                return; // Pool full, skip
            }
            _projectiles[_activeCount] = new Projectile(position, velocity * speed, damage);
            _activeCount++;
        }

        // Updates all active projectiles: moves them and deactivates if out of bounds
        // also checks for collisions with enemies
        public void UpdateProjectiles(float deltaTime, Span<Enemy> enemies)
        {
            int i = 0;
            while (i < _activeCount)
            {
                ref Projectile projectile = ref _projectiles[i];
                if (projectile.isActive)
                {
                    // Move projectile based on velocity and speed
                    projectile.Position += projectile.Velocity * deltaTime;
                    // Deactivate if out of screen bounds
                    if (projectile.Position.X < 0 || projectile.Position.X > Raylib.GetScreenWidth() ||
                        projectile.Position.Y < 0 || projectile.Position.Y > Raylib.GetScreenHeight())
                    {
                        projectile.isActive = false;
                    }
                    // Collision detection with enemies
                    for (int j = 0; j < enemies.Length; j++)
                    {
                        ref Enemy enemy = ref enemies[j];
                        if (enemy.isActive && Raylib.CheckCollisionRecs(projectile.GetRect(), enemy.GetRect()))
                        {
                            projectile.isActive = false;
                            enemy.isActive = false;
                            break; // Stop checking other enemies for this projectile
                        }
                    }
                }
                if (!projectile.isActive)
                {
                    // Remove inactive projectile by swapping with the last active one and reducing count
                    _projectiles[i] = _projectiles[_activeCount - 1];
                    _activeCount--;
                    continue; 
                }
                i++;
            }
        }

        // Draws all active projectiles
        public void DrawProjectiles()
        {
            for (int i = 0; i < _activeCount; i++)
            {
                ref Projectile projectile = ref _projectiles[i];
                Raylib.DrawCircleV(projectile.Position, 5, Color.Yellow);
            }
        }
        
        // Returns a read-only span of all active projectiles (for collision, etc.)
        public ReadOnlySpan<Projectile> GetActiveProjectiles() => _projectiles.AsSpan(0, _activeCount);
    }
}