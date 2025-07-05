namespace my_game.state
{
    
    public interface IGameState
    {
        void EnterState(StateManager stateManager);
        void Update();
        void Draw();
        void ExitState();

    }

}