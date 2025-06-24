using System.IO;
using System.Numerics;

namespace my_game.Graphics
{

    public interface ISpriteAnimation
    {
       
    }
    
    public interface ISprite
    {
        void Draw(Vector2 position, float rotation);
    }
}
