using System.Numerics;
using my_game.Managers;
using Raylib_cs;

namespace my_game.Enemies;

public class EnemySystem
{
    // Maximum number of projectiles allowed at once
    private const int MAX_ENEMIES = 128; // Tune as needed
    // Pre-allocated array for projectile pooling
    private readonly Enemy[] _enemies = new Enemy[MAX_ENEMIES];
    // Number of currently active projectiles
    private int _activeCount = 0;
    private Texture2D _texture;
    private bool _isLoaded = false;
    
    public EnemySystem()
    {
        Console.WriteLine("Enemy System Initialized");// Load enemy textures on initialization
      
    }
    private void LoadEnemyTextures()
    {
        var image = GameManager.Instance.assetManager.GetImage("enemy6");
        
        _texture = Raylib.LoadTextureFromImage(image);
        _isLoaded = true;
    }
    
    public void AddEnemy(Vector2 position, Vector2 velocity, float scale = 1f, float speed = 150f)
    {
        if (!_isLoaded)
        {
            Console.WriteLine("Loading enemy textures");
            LoadEnemyTextures();
        }
        
        if (_activeCount >= MAX_ENEMIES)
        {
            return; // Pool full, skip
        }
        Console.WriteLine("Add new enemy");
        _enemies[_activeCount] = new Enemy(_texture, position, velocity * speed, scale, 180f, 2);
        _activeCount++;
    }
    
    // Updates all active projectiles: moves them and deactivates if out of bounds
    public void UpdateEnemies(float deltaTime)
    {
        int i = 0;
        while (i < _activeCount)
        {
            ref Enemy enemy = ref _enemies[i];
            if (enemy.IsActive)
            {
                // Move projectile based on velocity and speed
                enemy.Position += enemy.Velocity * deltaTime;
                // Deactivate if out of screen bounds
                if (enemy.Position.X < 0 || enemy.Position.X > Raylib.GetScreenWidth() ||
                    enemy.Position.Y < 0 || enemy.Position.Y > Raylib.GetScreenHeight())
                {
                    enemy.IsActive = false;
                }
                
                //TODO: Add collision detection logic here
            }
            if (!enemy.IsActive)
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
            DrawEnemySprite(enemy);
            DrawEnemyColliders(enemy);
        }
    }
    
    private void DrawEnemySprite(Enemy enemy)
    {
        Raylib.DrawTexturePro(enemy.Texture
            , enemy.GetSourceRect
            , enemy.GetDestinationRect
            , enemy.GetOrigin(enemy.GetDestinationRect)
            , enemy.Rotation
            ,Color.White
        );
    }
    private void DrawEnemyColliders(Enemy enemy)
    {
        var colliderRect = enemy.GetColliderRect;
        Raylib.DrawRectangleLinesEx(colliderRect, 1, Color.Red);
        var center = enemy.GetColliderCircleTuple().center;
        var radius = enemy.GetColliderCircleTuple().radius;
        Raylib.DrawCircleLinesV(center, radius, Color.Green);
    }
    
    // Returns a read-only span of all active projectiles (for collision, etc.)
    public Span<Enemy> GetActiveEnemies() => _enemies.AsSpan(0, _activeCount);
    
}

