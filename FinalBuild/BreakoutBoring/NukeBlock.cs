using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutBoring
{
    internal class NukeBlock : MonogameBlock
    {
        Nuke nuke;
        public NukeBlock(Game game)
        : base(game)
        {
            this.block = new Block();
            NormalTextureName = "block_yellow";
            HitTextureName = "block_red";
            blocktype = BlockType.Nuke;
        }
    }
}
