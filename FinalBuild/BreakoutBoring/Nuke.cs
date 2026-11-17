using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace BreakoutBoring
{
    internal class Nuke : DrawableSprite
    {
        public bool IsActive;
        private float time;
        public Nuke(Game game, Vector2 location) : base (game) 
        {
            this.Location = location;
            IsActive = true;
            Direction = new Vector2(0, 1);
            Speed = 100;
        }

        protected override void LoadContent()
        {
            this.spriteTexture = this.Game.Content.Load<Texture2D>("Nuke");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            FallFromSky();
            base.Update(gameTime);
        }

        private void FallFromSky()
        {
            Location = Location + ((Direction * Speed) * (time / 1000));
            if ((Location.Y > Game.GraphicsDevice.Viewport.Height))
            {
                IsActive = false;
            }
        }
    }
}
