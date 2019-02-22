﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


// Authors: Jake Zaia, Duncan Mott

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

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

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
