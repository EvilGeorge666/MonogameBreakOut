using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutBoring
{
    internal class LevelManager
    {
        Game game;
        BlockManager blockManager;
        Paddle paddle;

        InputHandler input;
        public LevelManager(Game game, BlockManager blockManager, Paddle paddle) 
        { 
            this.game = game;
            this.blockManager = blockManager;
            this.paddle = paddle;
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
        }

        /*
        public void Update()
        {
            HandleInput();
            if (ScoreManager.Lives <= 0)
            {
                GameOver();
            }
        }
        */

        public void RestartGame()
        {
            ScoreManager.SetupNewGame();
            paddle.SetInitialLocation();
            blockManager.Initialize();
        }

        public void HandleInput()
        {
            if(ScoreManager.state == GameState.Gameover || ScoreManager.state == GameState.Win)
            {
                if (input.KeyboardState.IsKeyDown(Keys.Enter))
                {
                    RestartGame();
                }
            }
        }

    }
}
