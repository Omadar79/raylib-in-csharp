using System.Numerics;
using my_game.Managers;
using Raylib_cs;

namespace my_game.player;

public class PlayerSprite //: ISprite
{
    private Texture2D _currentImageTexture; 
    private Texture2D _imageTextureFacingNorth;
    private Texture2D _imageTextureFacingNorthEast;
    private Texture2D _imageTextureFacingNorthWest;
    private Texture2D _imageTextureFacingEast;
    private Texture2D _imageTextureFacingWest;
    private Texture2D _imageTextureFacingSouth;
    private Texture2D _imageTextureFacingSouthEast;
    private Texture2D _imageTextureFacingSouthWest;
    
    private Vector2 _baseDefaultScale = new(.45f, .45f);

    
    private PlayerFacing _currentPlayerFacing = PlayerFacing.FacingSouth; // Default facing direction
    private float _baseDefaultRotation = 0;
    
    private int _spriteSheetFrameWidth = 180; // Width of each frame in the sprite sheet
    private int _spriteSheetFrameHeight = 300; // Height of each frame in the
    private int _spriteSheetFrameCount = 13; // Number of frames in the sprite sheet
    private int _spriteSheetFrameIndex = 0; // Current frame index in the sprite sheet
    private float _frameTime = 0.1f; // 0.1 seconds per frame
    private float _elapsedTime = 0f;


    // Setters for default rotation and scale
    public void SetBaseDefaultRotation(float degrees) => _baseDefaultRotation = degrees;
   // public void SetTurretDefaultRotation(float degrees) => _turretDefaultRotation = degrees;
    public void SetBaseDefaultScale(Vector2 scale) => _baseDefaultScale = scale;
   // public void SetTurretDefaultScale(Vector2 scale) => _turretDefaultScale = scale;

    public PlayerSprite()
    {
        var playerWalkingImage = GameManager.Instance.assetManager.GetImage("player_walk_s");

        _imageTextureFacingNorth = Raylib.LoadTextureFromImage(playerWalkingImage);
        _imageTextureFacingNorthEast = Raylib.LoadTextureFromImage(playerWalkingImage);
        _imageTextureFacingNorthWest = Raylib.LoadTextureFromImage(playerWalkingImage);
        _imageTextureFacingEast = Raylib.LoadTextureFromImage(playerWalkingImage);
        _imageTextureFacingWest = Raylib.LoadTextureFromImage(playerWalkingImage);
        _imageTextureFacingSouth = Raylib.LoadTextureFromImage(playerWalkingImage);
        _imageTextureFacingSouthEast = Raylib.LoadTextureFromImage(playerWalkingImage);
        _imageTextureFacingSouthWest = Raylib.LoadTextureFromImage(playerWalkingImage);
            

        _currentImageTexture = _imageTextureFacingSouth;

    }

    public void UpdateAnimation(float deltaTime)
    {
        _elapsedTime += deltaTime;

        if (_elapsedTime >= _frameTime)
        {
            _spriteSheetFrameIndex = (_spriteSheetFrameIndex + 1) % _spriteSheetFrameCount; // Loop through frames
            _elapsedTime = 0f;
        }
    }

    public void Draw(Vector2 position, float facingRotation, Vector2? baseScale = null, Vector2? turretScale = null)
    {
        UpdatePlayerFacingByRotation(facingRotation);
        Console.WriteLine($"Drawing player facing {_currentPlayerFacing}");
        var baseDrawScale = baseScale ?? _baseDefaultScale;
        
        int frameX = (_spriteSheetFrameIndex % (_currentImageTexture.Width / _spriteSheetFrameWidth)) * _spriteSheetFrameWidth;
        int frameY = 0;// (_spriteSheetFrameIndex / (_baseImageTexture.Width / _spriteSheetFrameWidth)) * _spriteSheetFrameHeight;

        var sourceRect = new Rectangle(frameX, frameY, _spriteSheetFrameWidth, _spriteSheetFrameHeight);
        var destRect = new Rectangle(position.X, position.Y
            , _spriteSheetFrameWidth * baseDrawScale.X
            , _spriteSheetFrameHeight * baseDrawScale.Y);
        var baseOrigin = new Vector2(_spriteSheetFrameWidth * baseDrawScale.X / 2f, _spriteSheetFrameHeight * baseDrawScale.Y / 2f);
        
        // Draw the tank base centered at position
        //var baseOrigin = new Vector2(_baseImageTexture.Width / 2f, _baseImageTexture.Height / 2f);
        Raylib.DrawTexturePro(
            _currentImageTexture,
            sourceRect, //new Rectangle(0, 0, _playerSpriteRect.Width, _playerSpriteRect.Height),
            destRect,//new Rectangle(position.X, position.Y, _playerSpriteRect.Width * baseDrawScale.X, _playerSpriteRect.Height * baseDrawScale.Y),
            baseOrigin,
            _baseDefaultRotation,
            Color.White
        );

        
        
        // Draw the turret centered and rotated at the same position
       //var turretOrigin = _originTurret;
       //var turretDrawScale = turretScale ?? _turretDefaultScale;
       //Raylib.DrawTexturePro(
       //    _rotatingImageTexture,
       //    new Rectangle(0, 0, _rotatingImageTexture.Width, _rotatingImageTexture.Height),
       //    new Rectangle(position.X, position.Y, _rotatingImageTexture.Width * turretDrawScale.X, _rotatingImageTexture.Height * turretDrawScale.Y),
       //    turretOrigin,
       //    _turretDefaultRotation + turretRotation,
       //    Color.White
       //);
    }
    
    public Rectangle GetBaseColliderRect(Vector2 position)
    {
        // Calculate the scaled width and height of the sprite
        float scaledWidth = _spriteSheetFrameWidth * _baseDefaultScale.X;
        float scaledHeight = _spriteSheetFrameHeight * _baseDefaultScale.Y;
        
        
        return new Rectangle(
            position.X - scaledWidth / 2f,
            position.Y - scaledHeight / 2f,
            scaledWidth,
            scaledHeight
        );
    }

    private void UpdatePlayerFacingByRotation(float rotation)
    {
        int caseIndex = (int)rotation / 45;
        switch (caseIndex)
        {
            case 0:   
                _currentPlayerFacing = PlayerFacing.FacingNorth;
                _currentImageTexture = _imageTextureFacingNorth;
                break;
            case 1:    
                _currentPlayerFacing = PlayerFacing.FacingNorthEast;
                _currentImageTexture = _imageTextureFacingNorthEast;
                break;
            case 2:    
                _currentPlayerFacing = PlayerFacing.FacingEast;
                _currentImageTexture = _imageTextureFacingEast;
                break;
            case 3:    
                _currentPlayerFacing = PlayerFacing.FacingSouthEast;
                _currentImageTexture = _imageTextureFacingSouthEast;
                break;
            case 4:    
                _currentPlayerFacing = PlayerFacing.FacingSouth;
                _currentImageTexture = _imageTextureFacingSouth;
                break;
            case 5:    
                _currentPlayerFacing = PlayerFacing.FacingSouthWest;
                _currentImageTexture = _imageTextureFacingSouthWest;
                break;
            case 6:    
                _currentPlayerFacing = PlayerFacing.FacingWest;
                _currentImageTexture = _imageTextureFacingWest;
                break;
            case 7:
                _currentPlayerFacing = PlayerFacing.FacingNorthWest;
                _currentImageTexture = _imageTextureFacingNorthWest;
                break;
            
            default:
                _currentPlayerFacing = PlayerFacing.FacingSouth;
                _currentImageTexture = _imageTextureFacingSouth;
                break;
        }   
        
    }


   

}

public enum PlayerFacing
{
    FacingNorth,
    FacingSouth,
    FacingEast,
    FacingWest,
    FacingNorthEast,
    FacingNorthWest,
    FacingSouthEast,
    FacingSouthWest
}