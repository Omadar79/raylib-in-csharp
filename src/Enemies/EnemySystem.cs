using System.Numerics;
using Raylib_cs;

namespace my_game.Enemies
{
    public class EnemySystem
    {
        // Maximum number of projectiles allowed at once
        private const int MAX_ENEMIES = 128; // Tune as needed
        // Pre-allocated array for projectile pooling
        private readonly Enemy[] _enemies = new Enemy[MAX_ENEMIES];
        // Number of currently active projectiles
        private int _activeCount = 0;
        
        
        // Adds a new projectile to the pool if there is space
        public void AddEnemy(Vector2 position, Vector2 velocity,Vector2 size, float speed = 150f)
        {
            if (_activeCount >= MAX_ENEMIES)
            {
                return; // Pool full, skip
            }
            _enemies[_activeCount] = new Enemy(position, velocity * speed, size);
            _activeCount++;
        }
        
        
        // Updates all active projectiles: moves them and deactivates if out of bounds
        public void UpdateEnemies(float deltaTime)
        {
            int i = 0;
            while (i < _activeCount)
            {
                ref Enemy enemy = ref _enemies[i];
                if (enemy.isActive)
                {
                    // Move projectile based on velocity and speed
                    enemy.Position += enemy.Velocity * deltaTime;
                    // Deactivate if out of screen bounds
                    if (enemy.Position.X < 0 || enemy.Position.X > Raylib.GetScreenWidth() ||
                        enemy.Position.Y < 0 || enemy.Position.Y > Raylib.GetScreenHeight())
                    {
                        enemy.isActive = false;
                    }
                    
                    //TODO: Add collision detection logic here
                }
                if (!enemy.isActive)
                {
                    // Remove inactive projectile by swapping with the last active one and reducing count
                    _enemies[i] = _enemies[_activeCount - 1];
                    _activeCount--;
                    continue; 
                }
                i++;
            }
        }
        
        // Draws all active Enemies
        public void DrawEnemies()
        {
            for (int i = 0; i < _activeCount; i++)
            {
                ref Enemy enemy = ref _enemies[i];
                Raylib.DrawRectangleV(enemy.Position, enemy.Size, Color.Red);
            
            }
        }
        
        // Returns a read-only span of all active projectiles (for collision, etc.)
        public Span<Enemy> GetActiveEnemies() => _enemies.AsSpan(0, _activeCount);
    }
}
