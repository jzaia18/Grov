using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

// Authors: Jake Zaia, Rachel Wong

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
        #endregion

        #region properties
        // ************* Properties ************* //
        public static FloorManager Instance { get => instance; }
        public static Random RNG { get => instance.rng; }
        #endregion

        #region constructor
        // ************* Constructor ************* //

        private FloorManager()
        {
            currRoom = new Room(RoomType.Normal);
            rng = new Random();
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

        public void Update()
        {
            currRoom.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currRoom.Draw(spriteBatch);
        }
        #endregion
    }
}
