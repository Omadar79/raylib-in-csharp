namespace my_game.state
{
    public class GameStateManager
    {
        private IGameState currentState;

        public void SetState(IGameState state)
        {
            currentState = state;
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void Draw()
        {
            currentState?.Draw();
        }
    }
    
}

