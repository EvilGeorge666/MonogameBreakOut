using Microsoft.Xna.Framework;
using SharpDX.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutBoring
{
    internal class BigBlock:MonogameBlock
    {
        Nuke nuke;
        public BigBlock(Game game)
        : base(game)
        {
            this.block = new Block();
            NormalTextureName = "block_greenWaveBig";
            HitTextureName = "block_greenWaveSmall";
            blocktype = BlockType.Paddle;
        }
    }
}
