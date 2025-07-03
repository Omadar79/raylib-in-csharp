using my_game.UI;

namespace my_game.state;

public class LoadingGameState : IGameState
{
    private GameStateManager? _gameStateManager;
    private LogoUI? _logoUI;
    public void EnterState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _logoUI = new LogoUI();
        _logoUI.InitUI();
    }

    public void Update()
    {
        if (!_logoUI.IsSceneFinished)
        {
            _logoUI.UpdateUI();
        }
        else
        {
            _gameStateManager?.SetState(new MainMenuState());
        }
    }

    public void Draw()
    {
    
        if (!_logoUI.IsSceneFinished)
        {
            _logoUI.DrawUI();
        }
    }

    public void ExitState() 
    {
      
    }
}