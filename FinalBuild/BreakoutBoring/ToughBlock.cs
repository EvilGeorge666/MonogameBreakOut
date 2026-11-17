using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutBoring
{
    internal class ToughBlock : MonogameBlock
    {
        protected string HitTextureName2, HitTextureName3;
        protected Texture2D HitTexture2, HitTexture3;

        public ToughBlock(Game game) : base(game)
        {
            this.block = new BlockAlt();
            NormalTextureName = "block_earth";
            HitTextureName = "block_earth_Broken";
            HitTextureName2 = "block_earth_Broken2";
            HitTextureName3 = "block_earth_Broken3";
            blocktype = BlockType.Tough;
        }
        protected override void LoadContent()
        {
            this.HitTexture2 = this.Game.Content.Load<Texture2D>(HitTextureName2);
            this.HitTexture3 = this.Game.Content.Load<Texture2D>(HitTextureName3);
            base.LoadContent();
        }
        protected override void updateBlockTexture()
        {

            this.blockstate = this.block.BlockState;
            switch (block.BlockState)
            {
                case BlockState.Normal:
                    this.Visible = true;
                    this.spriteTexture = NormalTexture;
                    break;
                case BlockState.Hit:
                    this.spriteTexture = HitTexture;
                    break;
                case BlockState.Hit2:
                    this.spriteTexture = HitTexture2;
                    break;
                case BlockState.Hit3:
                    this.spriteTexture = HitTexture3;
                    break;
                case BlockState.Broken:
                    this.spriteTexture = NormalTexture;
                    //this.enabled = false;
                    this.Visible = false; //don't show block

                    break;
            }
        }
    }
}
