using System.Numerics;
using Raylib_cs;
using my_game.state;

namespace my_game.player
{
    public class Player
    {
        private static Vector2 _playerPosition;
        public Player(int startingScreenWidth, int startingScreenHeight)
        {
            _playerPosition = new Vector2(startingScreenWidth / 2, startingScreenHeight - 50);

        }

        
    }
}