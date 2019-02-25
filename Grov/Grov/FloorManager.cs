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
        // ************* Fields ************* //

        private Room startRoom;
		private Room currRoom;
		private Room bossRoom;
		private int numRooms;
		private int floorNumber;
		private Random rng;
        private Texture2D[] textureMap;
        private ContentManager contentManager;


        // ************* Constructor ************* //

        public FloorManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            textureMap = new Texture2D[7];
            for (int i = 0; i < textureMap.Length; i++)
            {
                textureMap[i] = contentManager.Load<Texture2D>("tile"+i);
            }
            currRoom = new Room(RoomType.Normal, textureMap);
        }

        // ************* Methods ************* //

        public void Update()
        {
            currRoom.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currRoom.Draw(spriteBatch);
        }
    }
}
