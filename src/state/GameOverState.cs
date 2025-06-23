using my_game.state;
using Raylib_cs;

namespace my_game.state
{
    public class GameOverState : IGameState
    {
        public void Update()
        {
            //TODO click to restart the game or go to main menu
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                // Resume the game
            }
        }

        public void Draw()
        {
            Raylib.ClearBackground(Color.DarkGray);
            Raylib.DrawText("Game Over", 10, 10, 20, Color.White);
            Raylib.DrawText("Press Escape to Restart Game", 10, 40, 20, Color.LightGray);
        }
    }    
}

