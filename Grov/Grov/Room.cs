using System;
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
		// ************* Fields ************* //

		private Entrance top, bottom, left, right;
		private RoomType type;
		private bool isCleared;
		private Tile[][] tiles;
		private Texture2D[] tile_textures;


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


        // ************* Constructor ************* //

        public Room(RoomType type, Texture2D[] textures)
        {
			tile_textures = textures;
            isCleared = false;
            tiles = new Tile[32][];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new Tile[18];
            }
            this.type = type;

            ReadFromFile("testNoDoor");
        }


        // ************* Methods ************* //

		/// <summary>
		/// Determines state of entrances (open or closed)
		/// </summary>
        public void Update()
        {
			if (isCleared)
			{
				if(left != null)
				{
					left.State = EntranceState.Open;
				}
				if (right != null)
				{
					right.State = EntranceState.Open;
				}
				if (top != null)
				{
					left.State = EntranceState.Open;
				}
				if (bottom != null)
				{
					left.State = EntranceState.Open;
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

					spriteBatch.Draw(tile_textures[(int)cur.Type], new Rectangle(cur.TileWidth * x, cur.TileHeight * y, cur.TileWidth, cur.TileHeight), Color.White);
				}
			}
		}

        private void ReadFromFile(string filename)
        {
            filename = @"rooms\" + filename + ".grovlev";

            FileStream stream = File.OpenRead(filename);
            BinaryReader reader = null;

            try
            {
                reader = new BinaryReader(stream);

                //Height
                for (int i = 0; i < 18; i++)
                {
                    //Width
                    for(int ii = 0; ii < 32; ii++)
                    {
                        int thisTile = reader.ReadInt32();

                        tiles[ii][i] = new Tile((TileType)thisTile);
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
    }
}
