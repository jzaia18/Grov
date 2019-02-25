using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


// Authors: Jake Zaia


namespace Grov
{
    class DisplayManager
    {
        #region fields
        // ************* Fields ************* //

        private HUD hud;
        private Texture2D[] tileTextureMap;
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private static DisplayManager instance;
        #endregion

        #region properties
        // ************* Properties ************* //

        public static DisplayManager Instance { get => instance; }
        public static ContentManager ContentManager { get => instance.contentManager; }
        public static GraphicsDevice GraphicsDevice { get => instance.graphicsDevice; }
        public static Texture2D[] TileTextureMap { get => instance.tileTextureMap; }


        #endregion

        #region constructors
        // ************* Constructor ************* //
        private DisplayManager()
        {
            //this.hud = new HUD(em.Player, contentManager);
        }

        public static void Initialize(ContentManager cm, GraphicsDevice gd)
        {
            if (instance == null)
            {
                instance = new DisplayManager();
                instance.contentManager = cm;
                instance.graphicsDevice = gd;
            }
        }

        public static void LoadContent()
        {
            if (instance == null)
                throw new InvalidOperationException("Cannot load content before Display Manager is initialized");

            instance.tileTextureMap = new Texture2D[7];
            for (int i = 0; i < instance.tileTextureMap.Length; i++)
            {
                instance.tileTextureMap[i] = ContentManager.Load<Texture2D>("tile" + i);
            }
        }
        #endregion

        // ************* Methods ************* //

        public void Draw(SpriteBatch spriteBatch)
        {
            EntityManager.Instance.Draw(spriteBatch);
            FloorManager.Instance.Draw(spriteBatch);
            hud.Draw(spriteBatch);
        }
    }
}
