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
        private Room[,] floorRooms;

		#endregion

		#region properties
		// ************* Properties ************* //
		public static FloorManager Instance { get => instance; }
        public static Random RNG { get => instance.rng; }
		public static int TileWidth { get => 1920 / 32; }
		public static int TileHeight { get => 1080 / 18; }
        public Room CurrRoom { get => currRoom; set => currRoom = value; }
        public Room this[int x, int y]
        {
            get => floorRooms[x,y];
        }

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
            RoomNode[,] floor = new RoomNode[11, 11];
            this.floorRooms = new Room[11, 11];
            List<RoomNode> currentNodes = new List<RoomNode>();

            //Spawn
            floor[5, 5] = new RoomNode(5, 5);

            //Top
            floor[5, 4] = new RoomNode(5, 4, DoorGen.top);
            currentNodes.Add(floor[5, 4]);

            //Left
            floor[4, 5] = new RoomNode(4, 5, DoorGen.left);
            currentNodes.Add(floor[4, 5]);

            //Bottom
            floor[5, 6] = new RoomNode(5, 6, DoorGen.bottom);
            currentNodes.Add(floor[5, 6]);

            //Right
            floor[6, 5] = new RoomNode(6, 5, DoorGen.right);
            currentNodes.Add(floor[6, 5]);


            int instance = 1;
            while (currentNodes.Count > 0)
            {
                List<RoomNode> newNodes = new List<RoomNode>();


                for (int i = 0; i < currentNodes.Count; i++)
                {
                    byte door = 0;

                    for (int x = 0; x < 4; x++)
                    {
                        door <<= 1;
                        if (rng.Next(0, 100) < 55 - (int)(instance * instance * instance))
                        {
                            door |= 1;
                        }
                    }

                    currentNodes[i].Doors = (DoorGen)((byte)currentNodes[i].Doors | door);
                }
                // Loop through all current Nodes
                for (int i = 0; i < currentNodes.Count; i++)
                {
                    
                    //If there is a top door
                    if (((byte)currentNodes[i].Doors & 8) == 8)
                    {
                        //Add a room on top of it
                        //If the room would be out of bounds, remove the door facing that way
                        if (currentNodes[i].Y - 1 < 0)
                        {
                            currentNodes[i].Doors = (DoorGen)((byte)currentNodes[i].Doors & (byte)7);
                        }
                        //If there is no room in that direction, make a new one
                        else if (floor[currentNodes[i].X, currentNodes[i].Y - 1] == null)
                        {
                            floor[currentNodes[i].X, currentNodes[i].Y - 1] = new RoomNode(currentNodes[i].X, currentNodes[i].Y - 1, DoorGen.top);
                            newNodes.Add(floor[currentNodes[i].X, currentNodes[i].Y - 1]);
                        }
                        //If there is a room, give it a door pointing at us
                        else
                        {
                            floor[currentNodes[i].X, currentNodes[i].Y - 1].Doors = (DoorGen)((byte)floor[currentNodes[i].X, currentNodes[i].Y - 1].Doors | (byte)2);
                        }
                    }

                    //If there is a left door
                    if (((byte)currentNodes[i].Doors & 4) == 4)
                    {
                        if(currentNodes[i].X - 1 < 0)
                        {
                            currentNodes[i].Doors = (DoorGen)((byte)currentNodes[i].Doors & (byte)11);
                        }
                        else if (floor[currentNodes[i].X - 1, currentNodes[i].Y] == null)
                        {
                            floor[currentNodes[i].X - 1, currentNodes[i].Y] = new RoomNode(currentNodes[i].X - 1, currentNodes[i].Y, DoorGen.left);
                            newNodes.Add(floor[currentNodes[i].X - 1, currentNodes[i].Y]);
                        }
                        else
                        {
                            floor[currentNodes[i].X - 1, currentNodes[i].Y].Doors = (DoorGen)((byte)floor[currentNodes[i].X - 1, currentNodes[i].Y].Doors | (byte)1);
                        }
                    }
                    //If there is a bottom door
                    if (((byte)currentNodes[i].Doors & 2) == 2)
                    {
                        if (currentNodes[i].Y + 1 > 10)
                        {
                            currentNodes[i].Doors = (DoorGen)((byte)currentNodes[i].Doors & (byte)13);
                        }
                        else if (floor[currentNodes[i].X, currentNodes[i].Y + 1] == null)
                        {
                            floor[currentNodes[i].X, currentNodes[i].Y + 1] = new RoomNode(currentNodes[i].X, currentNodes[i].Y + 1, DoorGen.bottom);
                            newNodes.Add(floor[currentNodes[i].X, currentNodes[i].Y + 1]);
                        }
                        else
                        {
                            floor[currentNodes[i].X, currentNodes[i].Y + 1].Doors = (DoorGen)((byte)floor[currentNodes[i].X, currentNodes[i].Y + 1].Doors | (byte)8);
                        }
                    }
                    //If there is a right door
                    if (((byte)currentNodes[i].Doors & 1) == 1)
                    {
                        if (currentNodes[i].X + 1 > 10)
                        {
                            currentNodes[i].Doors = (DoorGen)((byte)currentNodes[i].Doors & (byte)14);
                        }
                        else if (floor[currentNodes[i].X + 1, currentNodes[i].Y] == null)
                        {
                            floor[currentNodes[i].X + 1, currentNodes[i].Y] = new RoomNode(currentNodes[i].X + 1, currentNodes[i].Y, DoorGen.right);
                            newNodes.Add(floor[currentNodes[i].X + 1, currentNodes[i].Y]);
                        }
                        else
                        {
                            floor[currentNodes[i].X + 1, currentNodes[i].Y].Doors = (DoorGen)((byte)floor[currentNodes[i].X + 1, currentNodes[i].Y].Doors | (byte)4);
                        }
                    }
                }

                currentNodes = newNodes;
                instance++;

            }

            //Add the spawn room
            this.currRoom = new Room(RoomType.Normal, "resources/rooms/spawn.grovlev");
            this.floorRooms[5, 5] = currRoom;


            //Populate the Room array
            List<string> files = new List<string>();
            for (int x = 0; x < floorRooms.GetLength(0); x++)
            {
                for(int y = 0; y < floorRooms.GetLength(1); y++)
                {
                    //See if there is a room at these coordinates
                    if(floor[x,y] != null)
                    {
                        //Retrieve the door configuration
                        byte temp = (byte)floor[x, y].Doors;
                        string path = "";
                        for (byte checker = 8; checker > 0; checker >>= 1)
                        {
                            //if the bit matches the checker, then it is a 1
                            if ((temp & checker) == checker)
                            {
                                path += '1';
                            }
                            else
                            {
                                path += '0';
                            }
                        }

                        //Add all the level files from the specific path
                        path = @"resources\rooms\" + path;
                        files.AddRange(System.IO.Directory.GetFiles(path));
                        //If there isn't already a room there, make one (basically only skips the spawn room)
                        if(floorRooms[x,y] == null)
                        {
                            floorRooms[x, y] = new Room(RoomType.Normal, files[rng.Next(0, files.Count)]);
                        }
                    }
                    files.Clear();
                }
            }

            //Link all the rooms together
            for (int x = 0; x < floorRooms.GetLength(0); x++)
            {
                for (int y = 0; y < floorRooms.GetLength(1); y++)
                {
                    //if there is a room here
                    if(floorRooms[x,y] != null)
                    {
                        //If the room has a top door
                        if(floorRooms[x,y].Top != null)
                        {
                            floorRooms[x, y].Top.NextRoom = floorRooms[x, y - 1];
                        }
                        //If the room has a left door
                        if (floorRooms[x, y].Left != null)
                        {
                            floorRooms[x, y].Left.NextRoom = floorRooms[x - 1, y];
                        }
                        //If the room has a bottom door
                        if (floorRooms[x, y].Bottom != null)
                        {
                            floorRooms[x, y].Bottom.NextRoom = floorRooms[x, y + 1];
                        }
                        //If the room has a right door
                        if (floorRooms[x, y].Right != null)
                        {
                            floorRooms[x, y].Right.NextRoom = floorRooms[x + 1, y];
                        }
                    }
                }
            }

            currRoom.Visited = true;
            //Spawn this room's enemies
            currRoom.SpawnEnemies();
        }

        //Prints the binary representation of a given byte
        private static void PrintBinary(byte b)
        {
            //start with the leftmost bit
            for (byte checker = 128; checker > 0; checker >>= 1)
            {

                //if the bit matches the checker, then it is a 1
                if ((b & checker) == checker)
                {
                    Console.Write('1');
                }
                else
                {
                    Console.Write('0');
                }
            }
            Console.WriteLine();
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
