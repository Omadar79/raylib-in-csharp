namespace my_game.state
{
    
    public interface IGameState
    {
        void EnterState(GameStateManager gameStateManager);
        void Update();
        void Draw();
        void ExitState();

    }

}