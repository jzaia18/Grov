using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		// Properties
		public EntranceState State { get => state; set => state = value; }
		public Room NextRoom { get => nextRoom; set => nextRoom = value; }

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
    }
}
