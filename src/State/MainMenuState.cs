using Raylib_cs;
using my_game.UI;


namespace my_game.state
{
    public class MainMenuState : IGameState
    {
        private GameStateManager? _gameStateManager;
        private MainMenuUI? _menuUI;
        
        public void EnterState(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            _menuUI = new MainMenuUI();
            _menuUI.InitUI();
            _menuUI.MenuOptionSelected += MainMenuUI_OnMenuOptionSelected;
        }

        public void Update()
        {
            _menuUI?.UpdateUI();
        }

        public void Draw()
        {
            Raylib.ClearBackground(Color.DarkBlue); // Background for menu
            _menuUI?.DrawUI();
        }

        public void ExitState()
        {
            // Remove event handler when exiting state to prevent memory leaks
            _menuUI.MenuOptionSelected -= MainMenuUI_OnMenuOptionSelected;
        }
        
        private void MainMenuUI_OnMenuOptionSelected(int optionIndex)
        {
            switch (optionIndex)
            {
                case 0: // Start Game
                    Console.WriteLine("Transitioning to gameplay state");
                    _gameStateManager?.SetState(new GameplayState());
                    break;
                case 1: // Options
                    // You could create an options state and transition to it
                    Console.WriteLine("Options selected - functionality to be implemented");
                    break;
                case 2: // Credits
                    // You could create a credits state and transition to it
                    Console.WriteLine("Credits selected - functionality to be implemented");
                    break;
                case 3: // Exit
                    // Handled in the UI class
                    break;
            }
        }
    }
}
