using System.Numerics;
using Raylib_cs;

namespace my_game.enemies;

public struct Enemy(Texture2D texture, Vector2 position, Vector2 velocity, float scale = 1f, float rotation = 0f)
{
    public bool isActive = true;
    public bool isVisible = true; // Default visibility

    public Vector2 position = position;
    public Vector2 velocity = velocity;


    public Texture2D texture = texture;
    public float rotation = rotation;
    public float scale = scale; // Default scale of 1
    
    
    public Vector2 GetOrigin (Rectangle destinationRect)
    {
        return new Vector2(destinationRect.Width / 2f, destinationRect.Height/ 2f);
    }

    public Rectangle GetSourceRect => new Rectangle(
        0,
        0,
        texture.Width,
        texture.Height
    );

    public Rectangle GetDestinationRect=> new Rectangle(
            position.X ,
            position.Y ,
            texture.Width * scale,
            texture.Height * scale
        );
 
    public Rectangle GetColliderRect => new Rectangle(
        position.X - (texture.Width * scale / 2f),
        position.Y - (texture.Height * scale / 2f),
        texture.Width * scale,
        texture.Height * scale
    );

    public (Vector2 center, float radius) GetColliderCircleTuple()
    {
        var center = new Vector2((texture.Width * scale / 2f), (texture.Height * scale / 2f));
        var radius = MathF.Max(texture.Width, texture.Height) * scale / 2f;
        return (center, radius);
        
    }
    
    
}
