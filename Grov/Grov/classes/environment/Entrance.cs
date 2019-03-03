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
		Closed,
		Locked,
		Bombable
	}

    class Entrance : Tile
    {
		// Fields
		private EntranceState state;
		private Room nextRoom;

		// Properties
		public EntranceState State { get => state; set => state = value; }
		public Room NextRoom { get => nextRoom; set => nextRoom = value; }

        public Entrance(TileType tileType, int x, int y) : base(tileType, x, y)
        {

        }
    }
}
