using Raylib_cs;


namespace my_game.state
{
    public class MainMenuState : IGameState
    {
        GameStateManager _gameStateManager;
        public void EnterState(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                // Transition logic can be handled here
                Console.WriteLine("Start Game!");
            }
        }

        public void Draw()
        {
            Raylib.ClearBackground(Color.DarkGray);
            Raylib.DrawText("Menu State", 10, 10, 20, Color.White);
            Raylib.DrawText("Press ENTER to start", 10, 40, 20, Color.LightGray);
        }

        public void ExitState()
        {

        }
    }
}   
