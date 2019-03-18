using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


// Authors: Jake Zaia


namespace Grov
{
    class DisplayManager
    {
        #region fields
        // ************* Fields ************* //

        private HUD hud;
        private Texture2D[] tileTextureMap;
        private Dictionary<EnemyType, Texture2D> enemyTextureMap;
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private static DisplayManager instance;
        private SpriteFont courierNew;
        #endregion

        #region properties
        // ************* Properties ************* //

        public static DisplayManager Instance { get => instance; }
        public static ContentManager ContentManager { get => instance.contentManager; }
        public static GraphicsDevice GraphicsDevice { get => instance.graphicsDevice; }
        public static Texture2D[] TileTextureMap { get => instance.tileTextureMap; }
        public static Dictionary<EnemyType, Texture2D> EnemyTextureMap { get => instance.enemyTextureMap; }

        #endregion

        #region constructors
        // ************* Constructor ************* //
        private DisplayManager()
        {
        }

        public static void Initialize(ContentManager cm, GraphicsDevice gd)
        {
            if (instance == null)
            {
                instance = new DisplayManager();
                instance.contentManager = cm;
                instance.graphicsDevice = gd;
                instance.hud = new HUD();
                LoadContent();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private static void LoadContent()
        {
            if (instance == null)
                throw new InvalidOperationException("Cannot load content before Display Manager is initialized");

            // Load all tile textures into map
            instance.tileTextureMap = new Texture2D[7];
            for (int i = 0; i < instance.tileTextureMap.Length; i++)
            {
                instance.tileTextureMap[i] = ContentManager.Load<Texture2D>("tile" + i);
            }

            // Load all enemy textures into map
            instance.enemyTextureMap = new Dictionary<EnemyType, Texture2D>();
            EnemyTextureMap[EnemyType.Test] = ContentManager.Load<Texture2D>("EnemyHolderSprite");
            instance.courierNew = ContentManager.Load<SpriteFont>("CourierNew");
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        public void Draw(SpriteBatch spriteBatch, GameState state)
        {
            switch (state)
            {
                case GameState.Game:
                    FloorManager.Instance.Draw(spriteBatch);
                    EntityManager.Instance.Draw(spriteBatch);
                    hud.Draw(spriteBatch);
                    break;
                case GameState.Menu:
                    spriteBatch.DrawString(courierNew, "Grov", new Vector2(900, 200), Color.DarkGreen);
                    spriteBatch.DrawString(courierNew, "Start", new Vector2(890, 500), Color.DarkGreen);
                    spriteBatch.DrawString(courierNew, "Options", new Vector2(850, 600), Color.DarkGreen);
                    spriteBatch.DrawString(courierNew, "Exit", new Vector2(900, 700), Color.DarkGreen);
                    break;
            }
        }
        #endregion
    }
}
