namespace my_game.state;

public class StateManager
{
    private IGameState? _currentState;

    public void SetState(IGameState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState( this);
    }

    public void UpdateStateTick()
    {
        _currentState?.Update();
    }

    public void DrawStateTick()
    {
        _currentState?.Draw();
    }
    

}