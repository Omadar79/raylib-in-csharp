using System.Numerics;
using my_game.Graphics;
using Raylib_cs;


namespace my_game.player
{
    public class Player
    {
        
        public Vector2 Position { get; set; }
        public Rectangle Collider { get; }

        public float Rotation { get; set; }
        
        private PlayerSprite _playerSprite;

        
        public Player(Vector2 startPosition)
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
        
    }
}