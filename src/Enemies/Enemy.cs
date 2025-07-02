using System.Numerics;
using Raylib_cs;

namespace my_game.Enemies;

public struct Enemy(Texture2D texture, Vector2 position, Vector2 velocity, float scale = 1f, float rotation = 0f, int health = 2)
{
    public bool IsActive { get; set; }  = true;
    public Vector2 Position { get; set; }  = position;
    public Vector2 Velocity { get; set; }  = velocity;
    public Texture2D Texture { get; set; } = texture;
    public float Rotation { get; set; } = rotation;
    public float Scale { get; set; }  = scale; // Default scale of 1
    public int Health { get; set; } = health; // Default health
    
    public Vector2 GetOrigin (Rectangle destinationRect)
    {
        return new Vector2(destinationRect.Width / 2f, destinationRect.Height/ 2f);
    }

    public Rectangle GetSourceRect => new Rectangle(
        0,
        0,
        Texture.Width,
        Texture.Height
    );

    public Rectangle GetDestinationRect=> new Rectangle(
            Position.X ,
            Position.Y ,
            Texture.Width * Scale,
            Texture.Height * Scale
        );
 
    public Rectangle GetColliderRect => new Rectangle(
        Position.X - (Texture.Width * Scale / 2f),
        Position.Y - (Texture.Height * Scale / 2f),
        Texture.Width * Scale,
        Texture.Height * Scale
    );

    public (Vector2 center, float radius) GetColliderCircleTuple()
    {
        var center = new Vector2((Texture.Width * Scale / 2f), (Texture.Height * Scale / 2f));
        var radius = MathF.Max(Texture.Width, Texture.Height) * Scale / 2f;
        return (center, radius);
    }
    
}
