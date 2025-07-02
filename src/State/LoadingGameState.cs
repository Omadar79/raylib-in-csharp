using my_game.scenes;

namespace my_game.state;

public class LoadingGameState : IGameState
{
    private SceneLogo _sceneLogo;
    private GameStateManager _gameStateManager;
    public void EnterState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _sceneLogo = new SceneLogo();
        _sceneLogo.Init();
    }

    public void Update()
    {
        if (!_sceneLogo.IsSceneFinished)
        {
            _sceneLogo.Update();
        }
        else
        {
            _gameStateManager.SetState(new GameplayState());
        }
    }

    public void Draw()
    {
    
        if (!_sceneLogo.IsSceneFinished)
        {
            _sceneLogo.Draw();
        }
    }

    public void ExitState() 
    {
      
    }
}