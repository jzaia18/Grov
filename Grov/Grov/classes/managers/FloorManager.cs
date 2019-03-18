using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

//debugging
using System.Diagnostics;

// Authors: Jake Zaia, Rachel Wong, Duncan Mott

namespace Grov
{
    class FloorManager
    {
        #region fields
        // ************* Fields ************* //

        private Room startRoom;
		private Room currRoom;
		private Room bossRoom;
		private int numRooms;
		private int floorNumber;
		private Random rng;
        private static FloorManager instance;

		#endregion

		#region properties
		// ************* Properties ************* //
		public static FloorManager Instance { get => instance; }
        public static Random RNG { get => instance.rng; }
		public static int TileWidth { get => 1920 / 32; }
		public static int TileHeight { get => 1080 / 18; }
        public Room CurrRoom { get => currRoom; set => currRoom = value; }

		#endregion

		#region constructor
		// ************* Constructor ************* //

		private FloorManager()
        {
            currRoom = new Room(RoomType.Normal, "spawn");
            currRoom.SpawnEnemies();
            currRoom.Top.NextRoom = new Room(RoomType.Normal, "testDoorBottom");
            currRoom.Top.NextRoom.Bottom.NextRoom = currRoom;
            currRoom.Right.NextRoom = new Room(RoomType.Normal, "testLevel");
            currRoom.Right.NextRoom.Left.NextRoom = currRoom;
            rng = new Random();
            floorNumber = 1;
        }

        public static void Initialize()
        {
            if (instance == null)
            {
                instance = new FloorManager();
            }
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        public void Update()
        {
            currRoom.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currRoom.Draw(spriteBatch);
        }

		/// <summary>
		/// Checks to see if an entity's hitbox collides with a tile
		/// </summary>
		/// <param name="entity"> The entity to check collision with </param>
		/// <returns> List of tiles that entity passes through </returns>
		public List<Tile> CollidesWith(Entity entity)
		{
			List<Tile> tilesTouched = new List<Tile>();
			List<Tile> cornerTiles = new List<Tile>(); // Tiles touched by entity corners

            //Death
            if (entity.Hitbox.X / TileWidth < 0 || entity.Hitbox.X / TileWidth > 31 || entity.Hitbox.Y / TileWidth < 0 || entity.Hitbox.Y / TileWidth > 18)
            {
                tilesTouched.Add(new Tile(TileType.Death, 0, 0));
                return tilesTouched;
            }

			cornerTiles.Add(currRoom[entity.Hitbox.X / TileWidth, entity.Hitbox.Y / TileHeight]);
			cornerTiles.Add(currRoom[(entity.Hitbox.X + entity.Hitbox.Width) / TileWidth, entity.Hitbox.Y / TileHeight]);
			cornerTiles.Add(currRoom[entity.Hitbox.X / TileWidth, (entity.Hitbox.Y + entity.Hitbox.Height) / TileHeight]);
			cornerTiles.Add(currRoom[(entity.Hitbox.X + entity.Hitbox.Width) / TileWidth, (entity.Hitbox.Y + entity.Hitbox.Height) / TileHeight]);

			foreach(Tile cornerTile in cornerTiles)
			{
				int curTilesTouchedCount = tilesTouched.Count;

				if(tilesTouched.Count == 0)
				{
					tilesTouched.Add(cornerTile);
				}
				else
				{
                    // Checks if current cornerTile is already added into tilesTouched list
                    if(!tilesTouched.Contains(cornerTile)) tilesTouched.Add(cornerTile);
				}
			}
            return tilesTouched;
		}
        #endregion
    }
}
