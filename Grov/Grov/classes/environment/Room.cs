﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


// Authors: Jake Zaia, Duncan Mott, Rachel Wong

namespace Grov
{
    enum RoomType
    {
        Normal,
        Boss,
        Shop,
        treasure,
        Secret
    }

	class Room
	{
        #region fields
        // ************* Fields ************* //

        private Entrance top, bottom, left, right;
		private RoomType type;
		private bool isCleared;
        private string filename;

		private Tile[][] tiles;
        #endregion

        #region properties
        // ************* Properties ************* //

        public Entrance Top { get => top; set => top = value; }
        public Entrance Bottom { get => bottom; set => bottom = value; }
        public Entrance Left { get => left; set => left = value; }
        public Entrance Right { get => right; set => right = value; }
        public RoomType Type { get => type; set => type = value; }
        public bool IsCleared { get => isCleared; set => isCleared = value; }

        public Tile this[int x, int y]
        {
            get => tiles[x][y];
            set => tiles[x][y] = value;
        }
        #endregion

        #region constructor
        // ************* Constructor ************* //

        public Room(RoomType type, string fileName)
        {
            isCleared = false;
            tiles = new Tile[32][];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Tile[18];
            }
            this.type = type;

            this.filename = fileName;
            ReadFromFile(fileName);


        }
        #endregion

        #region methods
        // ************* Methods ************* //

        /// <summary>
        /// Determines state of entrances (open or closed)
        /// </summary>
        public void Update()
        {
            if (isCleared)
            {
                if (left != null)
                {
                    left.OpenDoor();
                }
                if (right != null)
                {
                    right.OpenDoor();
                }
                if (top != null)
                {
                    top.OpenDoor();
                }
                if (bottom != null)
                {
                    bottom.OpenDoor();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
			for (int x = 0; x < tiles.Length; x++)
			{
				for (int y = 0; y < tiles[x].Length; y++)
				{
					Tile cur = tiles[x][y];

                    if(cur.Type != TileType.Entrance)
					    spriteBatch.Draw(DisplayManager.TileTextureMap[(int)cur.Type], 
						                 new Rectangle(FloorManager.TileWidth * x, FloorManager.TileHeight * y, FloorManager.TileWidth, FloorManager.TileHeight), 
									     Color.White);
                    else if(isCleared)
                        spriteBatch.Draw(DisplayManager.TileTextureMap[(int)TileType.Floor],
                                         new Rectangle(FloorManager.TileWidth * x, FloorManager.TileHeight * y, FloorManager.TileWidth, FloorManager.TileHeight),
                                         Color.White);
                    else
                        spriteBatch.Draw(DisplayManager.TileTextureMap[(int)TileType.Wall],
                                         new Rectangle(FloorManager.TileWidth * x, FloorManager.TileHeight * y, FloorManager.TileWidth, FloorManager.TileHeight),
                                         Color.White);
                }
			}
		}

        /// <summary>
        /// Read room arrays from the room file
        /// </summary>
        /// <param name="filename"></param>
        private void ReadFromFile(string filename)
        {
            filename = @"resources\rooms\" + filename + ".grovlev";

            FileStream stream = File.OpenRead(filename);
            BinaryReader reader = null;

            try
            {
                reader = new BinaryReader(stream);

                //Handle tiles
                //Height
                for (int y = 0; y < 18; y++)
                {
                    //Width
                    for(int x = 0; x < 32; x++)
                    {
                        int thisTile = reader.ReadInt32();

                        tiles[x][y] = new Tile((TileType)thisTile, x, y);
                    }
                }

                //Check top/bottom for door
                for(int i = 0; i < 32; i++)
                {
                    //Top
                    if(tiles[i][0].Type == TileType.Entrance)
                    {
                        if (top == null)
                            top = new Entrance();
                        top.AddTile(tiles[i][0]);
                        top.UpdateLocation(new Point(i, 0));
                    }
                    //Bottom
                    if (tiles[i][17].Type == TileType.Entrance)
                    {
                        if (bottom == null)
                            bottom = new Entrance();
                        bottom.AddTile(tiles[i][17]);
                        bottom.UpdateLocation(new Point(i, 17));
                    }
                }
                //Check left for door
                for (int i = 0; i < 18; i++)
                {
                    //Left
                    if (tiles[0][i].Type == TileType.Entrance)
                    {
                        if (left == null)
                            left = new Entrance();
                        left.AddTile(tiles[0][i]);
                        left.UpdateLocation(new Point(0, i));
                    }
                    //Right
                    if (tiles[31][i].Type == TileType.Entrance)
                    {
                        if (right == null)
                            right = new Entrance();
                        right.AddTile(tiles[31][i]);
                        right.UpdateLocation(new Point(31, i));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public void SpawnEnemies()
        {
            filename = @"resources\rooms\" + filename + ".grovlev";

            FileStream stream = File.OpenRead(filename);
            BinaryReader reader = null;

            try
            {
                reader = new BinaryReader(stream);

                //Skip the tile info
                for(int i = 0; i < 32 * 18; i++)
                {
                    reader.ReadInt32();
                }

                //Handle Enemies
                int enemyCount = reader.ReadInt32();

                for (int i = 0; i < enemyCount; i++)
                {
                    int x = reader.ReadInt32();
                    int y = reader.ReadInt32();
                    EnemyType type = (EnemyType)reader.ReadInt32();

                    EntityManager.Instance.SpawnEnemies(type, new Vector2(x * FloorManager.TileWidth, y * FloorManager.TileHeight));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        #endregion
    }
}
