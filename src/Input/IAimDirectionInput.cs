using System.Numerics;

namespace my_game.input;

public interface IAimDirectionInput
{
    Vector2 GetAimDirection(Vector2 playerPosition);
}