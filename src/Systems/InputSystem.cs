using System.Numerics;
using Raylib_cs;

namespace my_game.input
{
    // Define interfaces for specific input types
    public interface IMovementInput
    {
        Vector2 GetMovement();
    }
    
    public interface IAimDirectionInput
    {
        Vector2 GetAimDirection(Vector2 playerPosition);
    }


    public interface IShootingInput
    {
        bool IsShooting();
    }

    // Implement the InputSystem class
    public class InputSystem : IMovementInput, IShootingInput, IAimDirectionInput
    {
        
        
        private const float leftStickDeadzone = 0.1f;
        private const float rightStickDeadzone = 0.1f;
        private const float leftTriggerDeadzone = -0.9f;
        private const float rightTriggerDeadzone = -0.9f;
        
        public Vector2 GetMovement()
        {
            // Prefer joystick movement if available
            if (Raylib.IsGamepadAvailable(0))
            {
                float leftStickX = Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftX);
                float leftStickY = Raylib.GetGamepadAxisMovement(0, GamepadAxis.LeftY);
                Vector2 move = new(leftStickX, leftStickY);
                if (move.LengthSquared() > leftStickDeadzone) // Deadzone
                {
                    return Vector2.Normalize(move) * 5f;
                }
            }
            // Fallback to WASD
            Vector2 movement = Vector2.Zero;
            if (Raylib.IsKeyDown(KeyboardKey.A)) movement.X -= 5;
            if (Raylib.IsKeyDown(KeyboardKey.D)) movement.X += 5;
            if (Raylib.IsKeyDown(KeyboardKey.W)) movement.Y -= 5;
            if (Raylib.IsKeyDown(KeyboardKey.S)) movement.Y += 5;
            return movement;
        }
        
        
        public bool IsShooting()
        {
            return Raylib.IsMouseButtonPressed(MouseButton.Left) || Raylib.IsKeyPressed(KeyboardKey.Space) || 
                   (Raylib.IsGamepadAvailable(0) && Raylib.IsGamepadButtonPressed(0, GamepadButton.RightTrigger2));
        }

        public Vector2 GetAimDirection(Vector2 playerPosition)
        {
            //  right joystick --------------------------------
            if (Raylib.IsGamepadAvailable(0))
            {
                float rightStickX = Raylib.GetGamepadAxisMovement(0, GamepadAxis.RightX);
                float rightStickY = Raylib.GetGamepadAxisMovement(0, GamepadAxis.RightY);
                Vector2 aim = new(rightStickX, rightStickY);
                if (aim.LengthSquared() > rightStickDeadzone) // Deadzone
                {
                    return Vector2.Normalize(aim);   
                }
            }
            
            //  Arrow keys ---------------------------------------------
            Vector2 arrowAim = Vector2.Zero;
            if (Raylib.IsKeyDown(KeyboardKey.Right)) arrowAim.X += 1;
            if (Raylib.IsKeyDown(KeyboardKey.Left)) arrowAim.X -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.Up)) arrowAim.Y -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.Down)) arrowAim.Y += 1;
            if (arrowAim.LengthSquared() > 0)
            {
                return Vector2.Normalize(arrowAim);
            }
            
            // Mouse aiming (fallback) --------------------------------
            Vector2 mousePosition = Raylib.GetMousePosition();
            Vector2 direction = Vector2.Normalize(mousePosition - playerPosition);
            return direction;
        }
        
        public float VectorToAimAngle(Vector2 normalizedVector)
        {
            // Calculate angle in radians (0 = up, 90 = right, 180 = down, 270 = left)
            float radians = MathF.Atan2(normalizedVector.X, -normalizedVector.Y);
            float degrees = radians * (180 / MathF.PI);
            return (degrees + 360) % 360;
        }
    }
}