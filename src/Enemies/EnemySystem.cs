using System.Numerics;
using my_game.Graphics;
using Raylib_cs;

namespace my_game.enemies;

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
    
    public void LoadEnemyTextures()
    {
        var image = AssetManager.GetImage("enemy6");
        
        _texture = Raylib.LoadTextureFromImage(image);
        _isLoaded = true;
    }
    
    
    // Adds a new projectile to the pool if there is space
    public void AddEnemy(string imageKey,Vector2 position, Vector2 velocity,Vector2 size, float speed = 150f)
    {
        if (!_isLoaded)
        {
            LoadEnemyTextures();
        }
        
        if (_activeCount >= MAX_ENEMIES)
        {
            return; // Pool full, skip
        }
        _enemies[_activeCount] = new Enemy(imageKey,position, velocity * speed, size);
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
                enemy.position += enemy.velocity * deltaTime;
                // Deactivate if out of screen bounds
                if (enemy.position.X < 0 || enemy.position.X > Raylib.GetScreenWidth() ||
                    enemy.position.Y < 0 || enemy.position.Y > Raylib.GetScreenHeight())
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
            DrawSprite(enemy.position, enemy.size, enemy.rotation);
           
        }
    }
    
    
    private void DrawSprite( Vector2 position, Vector2 size, float rotation = 0f)
    {
        var scale = new Vector2(.5f, .5f);
        var origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f); // Center origin for rotation and scale
        var newRect = new Rectangle(position.X, position.Y, _texture.Width *  scale.X, _texture.Height * scale.Y);
        var rotate = 90f;
        Raylib.DrawTexturePro(_texture
            , new Rectangle(0, 0, _texture.Width, _texture.Height)
            ,newRect
            , origin
            ,rotate
            ,Color.White
        );
        var colliderRect = GetBaseColliderRect(position, scale);
        Raylib.DrawRectanglePro(colliderRect,origin, rotate, Color.Red); // Draw collider rectangle for debugging
        //Raylib.DrawRectangleLinesEx(colliderRect, 1, Color.Red); // Draw collider rectangle for debugging
    }
    
    // Returns a read-only span of all active projectiles (for collision, etc.)
    public Span<Enemy> GetActiveEnemies() => _enemies.AsSpan(0, _activeCount);
    
    
    private Rectangle GetBaseColliderRect(Vector2 position, Vector2 scale)
    {
        // Returns a rectangle centered on the given position, matching the base image size
        return new Rectangle(
            position.X,
            position.Y,
            _texture.Width *  scale.X,
            _texture.Height * scale.Y
        );
    }
}

