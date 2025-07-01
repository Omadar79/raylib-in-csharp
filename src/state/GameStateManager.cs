namespace my_game.state;

public class GameStateManager
{

    private IGameState _currentState;

    public void SetState(IGameState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState?.EnterState( this);
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void Draw()
    {
        _currentState?.Draw();
    }
    

}