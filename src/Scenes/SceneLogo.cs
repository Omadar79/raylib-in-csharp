using Raylib_cs;
namespace my_game.scenes
{
    public class SceneLogo 
    {
        private int _frameCounter;
        private bool _sceneFinished;    // Flag to indicate if the scene is finished
        
        private int _logoPositionX;
        private int _logoPositionY;

        private int _lettersCount;

        private int _topSideRectWidth;
        private int _leftSideRectHeight;

        private int _bottomSideRectWidth;
        private int _rightSideRectHeight;

        private int _state;              // Logo animation states
        private float _alpha = 1.0f;         // Useful for fading

        public bool IsSceneFinished => _sceneFinished; // Property to check if the scene is finished
        
        public  void Init()
        {
            _frameCounter = 0;
            _sceneFinished = false;


            _logoPositionX = Raylib.GetScreenWidth() / 2 - 128;
            _logoPositionY = Raylib.GetScreenHeight() / 2 - 128;

            _lettersCount = 0;

            _topSideRectWidth = 16;
            _leftSideRectHeight = 16;

            _bottomSideRectWidth = 16;
            _rightSideRectHeight = 16;

            _state = 0;              // Logo animation states
            _alpha = 1.0f;
            
            
        }

        public  void Update()
        {
            if (_state == 0)                 // State 0: Top-left square corner blink logic
            {
                _frameCounter++;

                if (_frameCounter == 80)
                {
                    _state = 1;
                    _frameCounter = 0;      // Reset counter... will be used later...
                }
            }
            else if (_state == 1)            // State 1: Bars animation logic: top and left
            {
                _topSideRectWidth += 8;
                _leftSideRectHeight += 8;

                if (_topSideRectWidth == 256)
                {
                    _state = 2;
                }
            }
            else if (_state == 2)            // State 2: Bars animation logic: bottom and right
            {
                _bottomSideRectWidth += 8;
                _rightSideRectHeight += 8;

                if (_bottomSideRectWidth == 256)
                {
                    _state = 3;
                }
            }
            else if (_state == 3)            // State 3: "raylib" text-write animation logic
            {
                _frameCounter++;

                if (_lettersCount < 10)
                {
                    if (_frameCounter / 12 != 0)   // Every 12 frames, one more letter!
                    {
                        _lettersCount++;
                        _frameCounter = 0;
                    }
                }
                else    // When all letters have appeared, just fade out everything
                {
                    if (_frameCounter > 200)
                    {
                        _alpha -= 0.02f;

                        if (_alpha <= 0.0f)
                        {
                            _alpha = 0.0f;
                            _sceneFinished = true;
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            Raylib.ClearBackground(Color.RayWhite);
            if (_state == 0)         // Draw blinking top-left square corner
            {
                if ((_frameCounter / 10) % 2 != 0)
                {
                    Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, 16, Color.Black);
                }
            }
            else if (_state == 1)    // Draw bars animation: top and left
            {
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRectWidth, 16, Color.Black);
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, _leftSideRectHeight, Color.Black);
            }
            else if (_state == 2)    // Draw bars animation: bottom and right
            {
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRectWidth, 16, Color.Black);
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, _leftSideRectHeight, Color.Black);

                Raylib.DrawRectangle(_logoPositionX + 240, _logoPositionY, 16, _rightSideRectHeight, Color.Black);
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 240, _bottomSideRectWidth, 16, Color.Black);
            }
            else if (_state == 3)    // Draw "raylib" text-write animation + "powered by"
            {
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRectWidth, 16, Raylib.Fade(Color.Black, _alpha));
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 16, 16, _leftSideRectHeight - 32, Raylib.Fade(Color.Black, _alpha));

                Raylib.DrawRectangle(_logoPositionX + 240, _logoPositionY + 16, 16, _rightSideRectHeight - 32, Raylib.Fade(Color.Black, _alpha));
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 240, _bottomSideRectWidth, 16, Raylib.Fade(Color.Black, _alpha));

                Raylib.DrawRectangle(Raylib.GetScreenWidth() / 2 - 112, Raylib.GetScreenHeight() / 2 - 112, 224, 224, Raylib.Fade(Color.RayWhite, _alpha));
                Raylib.DrawText("raylib", Raylib.GetScreenWidth() / 2 - 44, Raylib.GetScreenHeight() / 2 + 48, 50, Raylib.Fade(Color.Black, _alpha));


                if (_frameCounter > 20)
                {
                    Raylib.DrawText("powered by", _logoPositionX, _logoPositionY - 27, 20, Raylib.Fade(Color.DarkGray, _alpha));
                }
            }
        }

  
    }
}