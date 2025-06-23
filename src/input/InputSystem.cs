using System.Numerics;
using Raylib_cs;

namespace my_game.input
{
    // Define interfaces for specific input types
    public interface IMovementInput
    {
        Vector2 GetMovement();
    }

    public interface IShootingInput
    {
        bool IsShooting();
    }

    // Implement the InputSystem class
    public class InputSystem : IMovementInput, IShootingInput
    {
        public Vector2 GetMovement()
        {
            Vector2 movement = Vector2.Zero;

            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                movement.X -= 5;
            }

            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                movement.X += 5;
            }

            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                movement.Y -= 5;
            }

            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                movement.Y += 5;
            }

            return movement;
        }

        public Vector2 GetFireDirection(Vector2 playerPosition)
        {
            Vector2 mousePosition = Raylib.GetMousePosition();
            Vector2 direction = Vector2.Normalize(mousePosition - playerPosition);
            return direction;
        }
        
        public bool IsShooting()
        {
            return Raylib.IsMouseButtonPressed(MouseButton.Left) || Raylib.IsKeyPressed(KeyboardKey.Space);
        }
    }
}