using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakoutBoring
{
    class BlockManager : DrawableGameComponent
    {
        public List<MonogameBlock> Blocks { get; private set; } //List of Blocks the are managed by Block Manager

        //Dependancy on Ball
        Ball ball;

        Paddle paddle;

        List<MonogameBlock> blocksToRemove; //list of block to remove probably because they were hit

        List<Nuke> nukes;
        List<Nuke> nukesToRemove;

        Random random;

        /// <summary>
        /// BlockManager hold a list of blocks and handles updating, drawing a block collision
        /// </summary>
        /// <param name="game">Reference to Game</param>
        /// <param name="ball">Refernce to Ball for collision</param>
        public BlockManager(Game game, Ball b, Paddle p)
            : base(game)
        {
            this.Blocks = new List<MonogameBlock>();
            this.blocksToRemove = new List<MonogameBlock>();
            this.nukes = new List<Nuke>();
            this.nukesToRemove = new List<Nuke>();
            random = new Random();
            
            this.ball = b;
            this.paddle = p;
        }

        public override void Initialize()
        {
            LoadLevel();
            base.Initialize();
        }

        /// <summary>
        /// Replacable Method to Load a Level by filling the Blocks List with Blocks
        /// </summary>
        protected virtual void LoadLevel()
        {
            Blocks.Clear();
            CreateBlockArrayByWidthAndHeight(24, ScoreManager.Level + 1, 1);
        }

        /// <summary>
        /// Simple Level lays out multiple levels of blocks
        /// </summary>
        /// <param name="width">Number of blocks wide</param>
        /// <param name="height">Number of blocks high</param>
        /// <param name="margin">space between blocks</param>
        private void CreateBlockArrayByWidthAndHeight(int width, int height, int margin)
        {
            MonogameBlock b;
            //Create Block Array based on with and hieght
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    int randBlockType = random.Next(0, 100);
                    if (randBlockType > 5)
                    {
                        if(randBlockType > 15)
                        {
                            if(randBlockType > 40)
                            {
                                b = new MonogameBlock(this.Game);
                            }
                            else
                            {
                                b = new ToughBlock(this.Game);
                            }
                        }
                        else
                        {
                            b = new NukeBlock(this.Game);
                        }
                    }
                    else
                    {
                        b = new BigBlock(this.Game);
                    }
                    b.Initialize();
                    b.Location = new Vector2(5 + (w * b.SpriteTexture.Width + (w * margin)), 50 + (h * b.SpriteTexture.Height + (h * margin)));
                    Blocks.Add(b);
                }
            }
        }
        
        bool reflected; //the ball should only reflect once even if it hits two bricks
        public override void Update(GameTime gameTime)
        {
            this.reflected = false; //only reflect once per update
            UpdateCheckBlocksForCollision(gameTime);
            UpdateCheckNukesForCollision(gameTime);
            UpdateBlocks(gameTime);
            UpdateNukes(gameTime);
            UpdateRemoveDisabledBlocks(gameTime);
            UpdateRemoveDisabledNukes(gameTime);
            

            base.Update(gameTime);
        }

        private void UpdateBlocks(GameTime gameTime)
        {
            foreach (var block in Blocks)
            {
                block.Update(gameTime);
            }
        }
        
        private void UpdateNukes(GameTime gameTime)
        {
            foreach (var nuke in nukes)
            {
                nuke.Update(gameTime);
                if(!nuke.IsActive)
                {
                    nukesToRemove.Add(nuke);
                }
            }
        }

        /// <summary>
        /// Removes disabled blocks from list
        /// </summary>
        private void UpdateRemoveDisabledBlocks(GameTime gameTime)
        {
            //remove disabled blocks
            foreach (var block in blocksToRemove)
            {
                Blocks.Remove(block);
                
                if(block.blocktype == BlockType.Nuke)
                {
                    SpawnNuke(block.Location);
                }
                if (block.blocktype == BlockType.Paddle)
                {
                    paddle.StartPowerUp();
                }

                ScoreManager.Score++;
            }
            blocksToRemove.Clear();
            if(Blocks.Count <= 0)
            {
                ball.resetBall(gameTime);
                ScoreManager.Level++;
                if (ScoreManager.Level > ScoreManager.MaxLevel)
                {
                    ScoreManager.state = GameState.Win;
                }
                else
                {
                    LoadLevel();
                }
            }
        }

        private void UpdateRemoveDisabledNukes(GameTime gameTime)
        {

            //remove disabled blocks
            foreach (var nuke in nukesToRemove)
            {
                nukes.Remove(nuke);
            }
            nukesToRemove.Clear();
        }

        private void SpawnNuke(Vector2 location)
        {
            Nuke n = new Nuke(Game, location);
            n.Initialize();
            nukes.Add(n);
        }

        private void UpdateCheckBlocksForCollision(GameTime gameTime)
        {
            foreach (MonogameBlock b in Blocks)
            {
                if (b.Enabled) //Only chack active blocks
                {
                    b.Update(gameTime); //Update Block
                    //Ball Collision
                    if (b.Intersects(ball)) //chek rectagle collision between ball and current block 
                    {
                        //hit
                        b.HitByBall(ball);
                        if (b.BlockState == BlockState.Broken)
                        {
                            /*
                            if(b.blocktype == BlockType.Nuke)
                            {
                                SpawnNuke(b.Location);
                            }
                            */
                            blocksToRemove.Add(b);  //Ball is hit add it to remove list
                        }
                        if (!reflected) //only reflect once
                        {
                            ball.Reflect(b);
                            this.reflected = true;
                        }
                    }
                }
            }
        }

        private void UpdateCheckNukesForCollision(GameTime gameTime)
        {
            foreach (Nuke n in nukes)
            {
                if (n.Enabled) //Only chack active blocks
                {
                    n.Update(gameTime); //Update Block
                    //paddle Collision
                    if (n.Intersects(paddle)) //chek rectagle collision between paddle and nuke
                    {
                        //hit
                        n.IsActive = false;
                        ScoreManager.Lives--;
                        if (ScoreManager.Lives <= 0)
                        {
                            ScoreManager.state = GameState.Gameover;
                            ball.resetBall(gameTime);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Block Manager Draws blocks they don't draw themselves
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            foreach (var block in this.Blocks)
            {
                if(block.Visible)   //respect block visible property
                    block.Draw(gameTime);
            }
            foreach (var nuke in this.nukes)
            {
                if(nuke.IsActive) nuke.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
