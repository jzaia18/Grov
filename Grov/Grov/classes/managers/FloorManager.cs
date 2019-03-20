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
        private enum DoorGen : byte
        {
            top = 8,
            left = 4,
            bottom = 2,
            right = 1
        }

        #region fields
        // ************* Fields ************* //

        private Room startRoom;
		private Room currRoom;
		private Room bossRoom;
		private int numRooms;
		private int floorNumber;
		private Random rng;
        private static FloorManager instance;
        private DoorGen[,] floor = new DoorGen[11, 11];

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
            rng = new Random();

            GenerateFloor();

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


        public void GenerateFloor()
        {
            List<DoorGen> currentNodes = new List<DoorGen>();
            Point location = new Point(5, 5);



            //Spawn
            floor[5, 5] = (DoorGen)15;
            bool isDone = false;
            currentNodes.Add(floor[5, 5]);

            

            while (!isDone)
            {
                //CreateAdjacent(floor[5, 5], location, currentNodes);

                for (int i = 0; i < currentNodes.Count; i++)
                {
                    //If there is a top door
                    if (((byte)currentNodes[i] & 8) == 8)
                    {
                        //Add a room on top of it
                        if (floor[location.X, location.Y - 1] == 0)
                        {
                            floor[location.X, location.Y - 1] = (DoorGen)2;
                            currentNodes.Add(floor[location.X, location.Y - 1]);
                        }
                    }
                    //If there is a left door
                    if (((byte)currentNodes[i] & 4) == 4)
                    {
                        if (floor[location.X - 1, location.Y] == 0)
                        {
                            floor[location.X - 1, location.Y] = (DoorGen)1;
                            currentNodes.Add(floor[location.X - 1, location.Y]);
                        }
                    }
                    //If there is a bottom door
                    if (((byte)currentNodes[i] & 2) == 2)
                    {
                        if (floor[location.X, location.Y + 1] == 0)
                        {
                            floor[location.X, location.Y + 1] = (DoorGen)8;
                            currentNodes.Add(floor[location.X, location.Y + 1]);
                        }
                    }
                    //If there is a right door
                    if (((byte)currentNodes[i] & 1) == 1)
                    {
                        if (floor[location.X + 1, location.Y] == 0)
                        {
                            floor[location.X + 1, location.Y] = (DoorGen)4;
                            currentNodes.Add(floor[location.X + 1, location.Y]);
                        }
                    }
                }
                isDone = true;

            }


            this.currRoom = new Room(RoomType.Normal, "spawn");
            currRoom.SpawnEnemies();
        }

        private void CreateAdjacent(DoorGen currentRoom, Point location, List<DoorGen> currentNodes)
        {
            for (int i = 0; i < currentNodes.Count; i++)
            {
                //If there is a top door
                if (((byte)currentNodes[i] & 8) == 8)
                {
                    //Add a room on top of it
                    if (floor[location.X, location.Y - 1] == 0)
                    {
                        floor[location.X, location.Y - 1] = (DoorGen)2;
                        currentNodes.Add(floor[location.X, location.Y - 1]);
                    }
                }
                //If there is a left door
                if (((byte)currentNodes[i] & 4) == 4)
                {
                    if (floor[location.X - 1, location.Y] == 0)
                    {
                        floor[location.X - 1, location.Y] = (DoorGen)1;
                        currentNodes.Add(floor[location.X - 1, location.Y]);
                    }
                }
                //If there is a bottom door
                if (((byte)currentNodes[i] & 2) == 2)
                {
                    if (floor[location.X, location.Y + 1] == 0)
                    {
                        floor[location.X, location.Y + 1] = (DoorGen)8;
                        currentNodes.Add(floor[location.X, location.Y + 1]);
                    }
                }
                //If there is a right door
                if (((byte)currentNodes[i] & 1) == 1)
                {
                    if (floor[location.X + 1, location.Y] == 0)
                    {
                        floor[location.X + 1, location.Y] = (DoorGen)4;
                        currentNodes.Add(floor[location.X + 1, location.Y]);
                    }
                }
            }
        }

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
            if (entity.Hitbox.X / TileWidth < 0 || (entity.Hitbox.X + entity.Hitbox.Width) / TileWidth > 31 || entity.Hitbox.Y / TileWidth < 0 || (entity.Hitbox.Y + entity.Hitbox.Height) / TileWidth > 17)
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
