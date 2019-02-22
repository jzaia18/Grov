using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Brought to you in part by:
//Duncan Mott

namespace Grov
{
    enum TileType
    {
        Floor = 0,
        Wall = 1,
        Entrance = 2,
        Water = 3,
        Rock = 4,
        Stump = 5,
        Bridge = 6
    }
    class Tile
    {

        private Point location;
        private TileType tileType;
        private bool isPassable;
        private bool blocksProjectiles;
        private Texture2D texture;

        /// <summary>
        /// Create a new tile
        /// </summary>
        public Tile(TileType tileType)
        {
            this.tileType = tileType;

            switch ((int)tileType)
            {
                //Floor
                case 0:
                    isPassable = true;
                    blocksProjectiles = false;
                    break;
                //Border wall
                case 1:
                    isPassable = false;
                    blocksProjectiles = true;
                    break;
                //Door
                case 2:
                    throw new NotImplementedException();
                    break;
                //Water
                case 3:
                    isPassable = false;
                    blocksProjectiles = false;
                    break;
                //Rock
                case 4:
                    isPassable = false;
                    blocksProjectiles = true;
                    break;
                //Stump
                case 5:
                    isPassable = false;
                    blocksProjectiles = false;
                    break;
                //Bridge
                case 6:
                    isPassable = true;
                    blocksProjectiles = false;
                    break;
            }
        }

        /// <summary>
        /// Draw function for each tile in the room
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

    }
}
