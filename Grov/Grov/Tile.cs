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
//Rachel Wong
//Jake Zaia

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
        #region fields
        // ************* Fields ************* //

        private Point location;
		private TileType type;
		private bool isPassable;
		private bool blocksProjectiles;
		private Texture2D texture;

		// Constants
		int tileWidth = 1920 / 32;
		int tileHeight = 1080 / 18;
        #endregion

        #region properties
        // ************* Properties ************* //

        public TileType Type { get => type; set => type = value; }
		public Texture2D Texture { get => texture; set => texture = value; }
		public int TileWidth { get => tileWidth; }
		public int TileHeight { get => tileHeight; }
        #endregion

        #region constructor
        // ************* Constructor ************* //

        /// <summary>
        /// Create a new tile
        /// </summary>
        public Tile(TileType type)
        {
            this.type = type;

            switch ((int)type)
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
        #endregion

        #region methods
        #endregion
    }
}
