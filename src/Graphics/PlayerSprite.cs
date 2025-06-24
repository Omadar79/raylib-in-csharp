using System.Numerics;
using Raylib_cs;

namespace my_game.Graphics;

public class PlayerSprite //: ISprite
{
    private static string _resPathTankBottom = Path.Combine(Program.ResourceRootDirectory, "resources","sprites", "ACS_Base.png");
    private static string _resPathTankTop = Path.Combine(Program.ResourceRootDirectory, "resources","sprites", "ACS_Tower.png");
        
    private Texture2D _baseImageTexture = Raylib.LoadTexture(_resPathTankBottom);
    private Texture2D _rotatingImageTexture = Raylib.LoadTexture(_resPathTankTop);
    
    private Vector2 _originTurret = new Vector2(23, 23); // Origin point for turret rotation
    private int _turretRotation = 0; // Rotation of the turret in degrees

    // Default rotation and scale for base and turret
    private float _baseDefaultRotation = 180f;
    private float _turretDefaultRotation = 180f;
    private Vector2 _baseDefaultScale = new(1f, 1f);
    private Vector2 _turretDefaultScale = new(1f, 1f);

    // Setters for default rotation and scale
    public void SetBaseDefaultRotation(float degrees) => _baseDefaultRotation = degrees;
    public void SetTurretDefaultRotation(float degrees) => _turretDefaultRotation = degrees;
    public void SetBaseDefaultScale(Vector2 scale) => _baseDefaultScale = scale;
    public void SetTurretDefaultScale(Vector2 scale) => _turretDefaultScale = scale;

    public PlayerSprite()
    {
      
    }
    

    public void Draw(Vector2 position, float turretRotation, Vector2? baseScale = null, Vector2? turretScale = null)
    {
        // Draw the tank base centered at position
        var baseOrigin = new Vector2(_baseImageTexture.Width / 2f, _baseImageTexture.Height / 2f);
        var baseDrawScale = baseScale ?? _baseDefaultScale;
        Raylib.DrawTexturePro(
            _baseImageTexture,
            new Rectangle(0, 0, _baseImageTexture.Width, _baseImageTexture.Height),
            new Rectangle(position.X, position.Y, _baseImageTexture.Width * baseDrawScale.X, _baseImageTexture.Height * baseDrawScale.Y),
            baseOrigin,
            _baseDefaultRotation,
            Color.White
        );

        // Draw the turret centered and rotated at the same position
        var turretOrigin = _originTurret;
        var turretDrawScale = turretScale ?? _turretDefaultScale;
        Raylib.DrawTexturePro(
            _rotatingImageTexture,
            new Rectangle(0, 0, _rotatingImageTexture.Width, _rotatingImageTexture.Height),
            new Rectangle(position.X, position.Y, _rotatingImageTexture.Width * turretDrawScale.X, _rotatingImageTexture.Height * turretDrawScale.Y),
            turretOrigin,
            _turretDefaultRotation + turretRotation,
            Color.White
        );
    }
    
    public Rectangle GetBaseColliderRect(Vector2 position)
    {
        // Returns a rectangle centered on the given position, matching the base image size
        return new Rectangle(
            position.X - _baseImageTexture.Width / 2f,
            position.Y - _baseImageTexture.Height / 2f,
            _baseImageTexture.Width,
            _baseImageTexture.Height
        );
    }

    

}