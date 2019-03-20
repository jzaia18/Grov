﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grov
{
    public enum DoorGen : byte
    {
        top = 8,
        left = 4,
        bottom = 2,
        right = 1
    }

    class RoomNode
    {
        #region fields
        private int x;
        private int y;
        private DoorGen doors;
        #endregion

        #region properties
        public int X { get => x; }
        public int Y { get => y; }
        public DoorGen Doors { get => doors; set => doors = value; }
        #endregion


        #region constructors
        public RoomNode(int x, int y, DoorGen previousRoomDoor)
        {
            this.x = x;
            this.y = y;
            if(previousRoomDoor == DoorGen.bottom)
            {
                this.doors = DoorGen.top;
            }
            else if(previousRoomDoor == DoorGen.left)
            {
                this.doors = DoorGen.right;
            }
            else if(previousRoomDoor == DoorGen.top)
            {
                this.doors = DoorGen.bottom;
            }
            else if(previousRoomDoor == DoorGen.right)
            {
                this.doors = DoorGen.left;
            }
        }

        public RoomNode(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.doors = (DoorGen)15;
        }
        #endregion
    }
}
