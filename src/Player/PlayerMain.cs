using System.Numerics;
using Raylib_cs;


namespace my_game.player;

public class PlayerMain
{
    public event Action PlayerDeadEvent ;
    
    public Vector2 Position { get; set; }
    public Rectangle Collider { get; }

    public float Rotation { get; set; }

    public int Health { get; set; } = 10; // Default health value
    
    private PlayerSprite _playerSprite;

    
    
    public PlayerMain(Vector2 startPosition)
    {
        _playerSprite = new PlayerSprite();
        Position = startPosition;
        Collider = _playerSprite.GetBaseColliderRect(Position);
    }

    
    public void Draw()
    {
        var colliderRect = _playerSprite.GetBaseColliderRect(Position);
        Raylib.DrawRectangleLinesEx(colliderRect, 1, Color.Red); // Draw collider rectangle for debugging
        _playerSprite.Draw(Position, Rotation); // Assuming no rotation for now
        
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            //IsAlive = false;
            Console.WriteLine("Player has died.");
            PlayerDeadEvent?.Invoke();
        }
    }
    
    
    public Rectangle GetColliderRect()
    {
        return _playerSprite.GetBaseColliderRect(Position);
    }
}
