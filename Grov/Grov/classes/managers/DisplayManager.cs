using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


// Authors: Jake Zaia, Rachel Wong


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

        // Button stuff
        private Button startButton;
        private Button optionsButton;
        private Button exitButton;
        private Texture2D startTexture_NH;
        private Texture2D startTexture_H;
        private Texture2D optionsTexture_NH;
        private Texture2D optionsTexture_H;
        private Texture2D exitTexture_NH;
        private Texture2D exitTexture_H;
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
            instance.tileTextureMap = new Texture2D[8];
            for (int i = 0; i < instance.tileTextureMap.Length; i++)
            {
                instance.tileTextureMap[i] = ContentManager.Load<Texture2D>("tile" + i);
            }

            // Load all enemy textures into map
            instance.enemyTextureMap = new Dictionary<EnemyType, Texture2D>();
            EnemyTextureMap[EnemyType.Test] = ContentManager.Load<Texture2D>("EnemyHolderSprite");
            instance.courierNew = ContentManager.Load<SpriteFont>("CourierNew");

            // Loading and initializing menu buttons
            instance.startTexture_NH = ContentManager.Load<Texture2D>("button images/StartButton_NoHover");
            instance.startTexture_H = ContentManager.Load<Texture2D>("button images/StartButton_Hover");
            instance.startButton = new Button(new Rectangle(new Point(850, 500), new Point(instance.startTexture_NH.Width, instance.startTexture_NH.Height)));
            instance.startButton.NoHover = instance.startTexture_NH;
            instance.startButton.Hover = instance.startTexture_H;

            instance.optionsTexture_NH = ContentManager.Load<Texture2D>("button images/OptionsButton_NoHover");
            instance.optionsTexture_H = ContentManager.Load<Texture2D>("button images/OptionsButton_Hover");
            instance.optionsButton = new Button(new Rectangle(new Point(850, 500 + instance.optionsTexture_NH.Height), new Point(instance.optionsTexture_NH.Width, instance.optionsTexture_NH.Height)));
            instance.optionsButton.NoHover = instance.optionsTexture_NH;
            instance.optionsButton.Hover = instance.optionsTexture_H;

            instance.exitTexture_NH = ContentManager.Load<Texture2D>("button images/ExitButton_NoHover");
            instance.exitTexture_H = ContentManager.Load<Texture2D>("button images/ExitButton_Hover");
            instance.exitButton = new Button(new Rectangle(new Point(850, 500 + (2 * instance.exitTexture_NH.Height)), new Point(instance.exitTexture_NH.Width, instance.exitTexture_NH.Height)));
            instance.exitButton.NoHover = instance.exitTexture_NH;
            instance.exitButton.Hover = instance.exitTexture_H;
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
                    startButton.Draw(spriteBatch);
                    optionsButton.Draw(spriteBatch);
                    exitButton.Draw(spriteBatch);
                    break;
            }
        }
        #endregion
    }
}
