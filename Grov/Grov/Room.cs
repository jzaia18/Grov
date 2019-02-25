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

        public Room(RoomType type)
        {
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
			for (int row = 0; row < tiles.Length; row++)
			{
				for (int col = 0; col < tiles[col].Length; col++)
				{
					Tile cur = tiles[row][col];

					if (cur.Texture == null)
					{
						Texture2D rect = new Texture2D(null, cur.TileWidth, cur.TileHeight);
						if (tiles[row][col].Type == TileType.Floor)
						{
							spriteBatch.Draw(rect, new Vector2(cur.TileWidth * col, cur.TileHeight * row), new Color(153, 213, 100));
						}
					}
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
