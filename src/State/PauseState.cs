using Raylib_cs;


namespace my_game.state
{
    public class PauseState : IGameState
    {
        GameStateManager _gameStateManager;
        public void EnterState(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                // Resume the game
            }
        }

        public void Draw()
        {
            Raylib.ClearBackground(Color.DarkGray);
            Raylib.DrawText("Paused", 10, 10, 20, Color.White);
            Raylib.DrawText("Press Escape to Resume", 10, 40, 20, Color.LightGray);
        }

        public void ExitState()
        {

        }
    }
    
}
