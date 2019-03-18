using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Authors: Rachel Wong, Duncan Mott

namespace Grov
{
	enum EntranceState
	{
		Open,
		Closed
	}

    class Entrance
    {
        // Fields
        private EntranceState state;
		private Room nextRoom;
        private List<Tile> tiles;
        private Point location;

		// Properties
		public EntranceState State { get => state; set => state = value; }
		public Room NextRoom { get => nextRoom; set => nextRoom = value; }
        public Point Location { get => location; }

        public Entrance()
        {
            state = EntranceState.Closed;
            tiles = new List<Tile>();
        }

        public void AddTile(Tile tile)
        {
            tiles.Add(tile);
        }

        public void OpenDoor()
        {
            this.State = EntranceState.Open;
            foreach(Tile tile in tiles)
            {
                tile.IsPassable = true;
            }
        }

        public void UpdateLocation(Point point)
        {
            if(location != null)
                this.location = point;
        }
    }
}
