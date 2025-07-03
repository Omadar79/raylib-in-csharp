using Raylib_cs;
using System.Numerics;
using my_game.input;


namespace my_game.UI;

public class MainMenuUI  : IUserInterface
{
    // Menu options
    private readonly string[] _menuOptions = { "Start Game", "Options", "Credits", "Exit" };
    private int _selectedOptionIndex = 0;
    private readonly float _itemSpacing = 50.0f;
    private Vector2 _menuPosition;
    private readonly Color _selectedColor = new(255, 255, 0, 255); // Yellow
    private readonly Color _normalColor = new(200, 200, 200, 255); // Light gray
    private readonly int _fontSize = 30;
    private bool _canNavigate = true;
    private readonly float _navigationCooldown = 0.15f; // Seconds between allowed navigation inputs
    private float _currentCooldown = 0;
    
    // Property to access the currently selected option
    public int SelectedOption => _selectedOptionIndex;
    
    // Event for menu selection
    public event Action<int>? MenuOptionSelected;
    
    public void InitUI()
    {
        // Center the menu on screen
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();
        
        _menuPosition = new Vector2(
            screenWidth / 2.0f - 100.0f, 
            screenHeight / 2.0f - (_menuOptions.Length * _itemSpacing) / 2.0f
        );
    }

    public void UpdateUI()
    {
        float deltaTime = Raylib.GetFrameTime();
        
        // Handle cooldown for menu navigation
        if (!_canNavigate)
        {
            _currentCooldown -= deltaTime;
            if (_currentCooldown <= 0)
            {
                _canNavigate = true;
            }
        }
        
        // Menu navigation
        if (_canNavigate)
        {
            // Keyboard controls
            if (Raylib.IsKeyPressed(KeyboardKey.Down) || Raylib.IsKeyPressed(KeyboardKey.S))
            {
                _selectedOptionIndex = (_selectedOptionIndex + 1) % _menuOptions.Length;
                ResetNavigationCooldown();
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.Up) || Raylib.IsKeyPressed(KeyboardKey.W))
            {
                _selectedOptionIndex = (_selectedOptionIndex - 1 + _menuOptions.Length) % _menuOptions.Length;
                ResetNavigationCooldown();
            }
            
            // Gamepad controls if available
            if (Raylib.IsGamepadAvailable(0))
            {
                if (Raylib.IsGamepadButtonPressed(0, GamepadButton.LeftFaceDown))
                {
                    _selectedOptionIndex = (_selectedOptionIndex + 1) % _menuOptions.Length;
                    ResetNavigationCooldown();
                }
                else if (Raylib.IsGamepadButtonPressed(0, GamepadButton.LeftFaceUp))
                {
                    _selectedOptionIndex = (_selectedOptionIndex - 1 + _menuOptions.Length) % _menuOptions.Length;
                    ResetNavigationCooldown();
                }
            }
        }

        // Handle selection
        if (Raylib.IsKeyPressed(KeyboardKey.Enter) || 
            Raylib.IsMouseButtonPressed(MouseButton.Left) ||
            (Raylib.IsGamepadAvailable(0) && Raylib.IsGamepadButtonPressed(0, GamepadButton.RightFaceDown)))
        {
            HandleMenuSelection();
        }

        // Check for mouse hover over menu items
        Vector2 mousePos = Raylib.GetMousePosition();
        for (int i = 0; i < _menuOptions.Length; i++)
        {
            float yPos = _menuPosition.Y + i * _itemSpacing;
            float width = Raylib.MeasureText(_menuOptions[i], _fontSize);
            
            if (mousePos.X >= _menuPosition.X && 
                mousePos.X <= _menuPosition.X + width &&
                mousePos.Y >= yPos && 
                mousePos.Y <= yPos + _fontSize)
            {
                _selectedOptionIndex = i;
                break;
            }
        }
    }

    public void DrawUI()
    {
        // Draw title
        string title = "My Game";
        int titleSize = 60;
        float titleWidth = Raylib.MeasureText(title, titleSize);
        Raylib.DrawText(title, (int)(Raylib.GetScreenWidth() / 2 - titleWidth / 2), 100, titleSize, Color.White);
        
        // Draw menu options
        for (int i = 0; i < _menuOptions.Length; i++)
        {
            Color color = (i == _selectedOptionIndex) ? _selectedColor : _normalColor;
            float yPos = _menuPosition.Y + i * _itemSpacing;
            
            // Draw selection indicator
            if (i == _selectedOptionIndex)
            {
                Raylib.DrawText(">", (int)_menuPosition.X - 30, (int)yPos, _fontSize, _selectedColor);
            }
            
            Raylib.DrawText(_menuOptions[i], (int)_menuPosition.X, (int)yPos, _fontSize, color);
        }
        
        // Draw instructions at bottom of screen
        Raylib.DrawText("Use Arrow Keys/WASD to navigate, Enter/Click to select", 
            10, Raylib.GetScreenHeight() - 40, 20, Color.Gray);
    }

    private void HandleMenuSelection()
    {
        // Trigger the event to notify listeners
        MenuOptionSelected?.Invoke(_selectedOptionIndex);
        
        switch (_selectedOptionIndex)
        {
            case 0: // Start Game
                Console.WriteLine("Starting game...");
                // Transition to gameplay state would be handled in the MainMenuState
                break;
            case 1: // Options
                Console.WriteLine("Options menu selected");
                // Show options menu
                break;
            case 2: // Credits
                Console.WriteLine("Credits selected");
                // Show credits
                break;
            case 3: // Exit
                Console.WriteLine("Exit selected");
                // Exit game
                Raylib.CloseWindow();
                break;
        }
    }
    
    private void ResetNavigationCooldown()
    {
        _canNavigate = false;
        _currentCooldown = _navigationCooldown;
    }
}