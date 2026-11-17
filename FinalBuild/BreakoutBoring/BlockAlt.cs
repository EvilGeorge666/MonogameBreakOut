using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutBoring
{
    internal class BlockAlt : Block
    {
        public override void UpdateBlockState()
        {
            switch (this.hitCount)
            {
                case 0:
                    this.BlockState = BlockState.Normal;
                    break;
                case 1:
                    this.BlockState = BlockState.Hit;
                    break;
                case 2:
                    this.BlockState = BlockState.Hit2;
                    break;
                case 3:
                    this.BlockState = BlockState.Hit3;
                    break;
                case 4:
                    this.BlockState = BlockState.Broken;
                    break;
                default:
                    this.BlockState = BlockState.Broken;
                    break;
            }

        }
    }
}
