using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


// Authors: Jack Hoffman, Jake Zaia, Rachel Wong, Duncan Mott


namespace Grov
{
    class DisplayManager
    {
        #region fields
        // ************* Fields ************* //

        private HUD hud;
        private AnimatedTexture[] tileTextureMap;
        private Dictionary<EnemyType, AnimatedTexture> enemyTextureMap;
        private Dictionary<ProjectileType, AnimatedTexture> projectileTextureMap;
        private Dictionary<PickupType, AnimatedTexture> pickupTextureMap;
        private Dictionary<ProjectileType, AnimatedTexture> weaponTextureMap;
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private static DisplayManager instance;
        private SpriteFont courierNew;

        // Button stuff
        private Dictionary<MenuButtons, Texture2D[]> menuButtonTextureMap;
        private List<Button> menuButtons;
        private int menuPointer;
        private Dictionary<PauseButtons, Texture2D[]> pauseButtonTextureMap;
        private List<Button> pauseButtons;
        private int pausePointer;

        private Texture2D title;
        private Texture2D pauseTitle;
        private Texture2D dimScreen;
        private Texture2D map;

        private Texture2D mapMarkerRoom;
        #endregion

        #region properties
        // ************* Properties ************* //

        public static DisplayManager Instance { get => instance; }
        public static ContentManager ContentManager { get => instance.contentManager; }
        public static GraphicsDevice GraphicsDevice { get => instance.graphicsDevice; }
        public static AnimatedTexture[] TileTextureMap { get => instance.tileTextureMap; }
        public static Dictionary<EnemyType, AnimatedTexture> EnemyTextureMap { get => instance.enemyTextureMap; }
        public static Dictionary<ProjectileType, AnimatedTexture> ProjectileTextureMap { get => instance.projectileTextureMap; }
        public static Dictionary<PickupType, AnimatedTexture> PickupTextureMap { get => instance.pickupTextureMap; }
        public static Dictionary<ProjectileType, AnimatedTexture> WeaponTextureMap { get => instance.weaponTextureMap; }
        public static List<Button> MenuButtons { get => instance.menuButtons; }
        public static int MenuPointer { get => instance.menuPointer; set => instance.menuPointer = value; }
        public static List<Button> PauseButtons { get => instance.pauseButtons; }
        public static int PausePointer { get => instance.pausePointer; set => instance.pausePointer = value; }

        #endregion

        #region constructors
        // ************* Constructor ************* //

        private DisplayManager()
        {
            menuButtons = new List<Button>();
            menuPointer = 0;
            pauseButtons = new List<Button>();
            pausePointer = 0;
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
            instance.tileTextureMap = new AnimatedTexture[8];
            for (int i = 0; i < instance.tileTextureMap.Length; i++)
                instance.tileTextureMap[i] = new AnimatedTexture(ContentManager.Load<Texture2D>("tiles/tile" + i));

            // Load all enemy textures into map
            instance.enemyTextureMap = new Dictionary<EnemyType, AnimatedTexture>();

            EnemyTextureMap[EnemyType.Test] = new AnimatedTexture(ContentManager.Load<Texture2D>("animations/Test_Walking/000"));
            Texture2D duplicateTest = ContentManager.Load<Texture2D>("animations/Test_Walking/001"); // This texture will be used twice
            EnemyTextureMap[EnemyType.Test].AddTexture(duplicateTest);
            EnemyTextureMap[EnemyType.Test].AddTexture(ContentManager.Load<Texture2D>("animations/Test_Walking/002"));
            EnemyTextureMap[EnemyType.Test].AddTexture(duplicateTest);

            EnemyTextureMap[EnemyType.Shooty] = new AnimatedTexture(ContentManager.Load<Texture2D>("animations/Shooty_Walking/000"));
            Texture2D duplicateShooty = ContentManager.Load<Texture2D>("animations/Shooty_Walking/001");
            EnemyTextureMap[EnemyType.Shooty].AddTexture(duplicateShooty);
            EnemyTextureMap[EnemyType.Shooty].AddTexture(ContentManager.Load<Texture2D>("animations/Shooty_Walking/002"));
            EnemyTextureMap[EnemyType.Shooty].AddTexture(duplicateShooty);
            EnemyTextureMap[EnemyType.Shooty].AddTexture(ContentManager.Load<Texture2D>("animations/Shooty_Walking/003"));

            EnemyTextureMap[EnemyType.Grot] = new AnimatedTexture(ContentManager.Load<Texture2D>("EnemyHolderSprite"));
            EnemyTextureMap[EnemyType.Turret] = new AnimatedTexture(ContentManager.Load<Texture2D>("EnemyHolderSprite"));
            EnemyTextureMap[EnemyType.ForestGiant] = new AnimatedTexture(ContentManager.Load<Texture2D>("EnemyHolderSprite"));

            // Load all projectile textures into map
            instance.projectileTextureMap = new Dictionary<ProjectileType, AnimatedTexture>();
            foreach (ProjectileType ptype in Enum.GetValues(typeof(ProjectileType)))
            {
                Instance.projectileTextureMap[ptype] = new AnimatedTexture(ContentManager.Load<Texture2D>("projectiles/" + Enum.GetName(typeof(ProjectileType), ptype)));
            }

            // load all pickup textures into map
            instance.pickupTextureMap = new Dictionary<PickupType, AnimatedTexture>();
            foreach (PickupType typeOfPickup in Enum.GetValues(typeof(PickupType)))
            {
                if(typeOfPickup == PickupType.Weapon)
                {
                    Instance.pickupTextureMap[typeOfPickup] = null;
                }
                else
                {
                    Instance.pickupTextureMap[typeOfPickup] = new AnimatedTexture(ContentManager.Load<Texture2D>("pickups/" + Enum.GetName(typeof(PickupType), typeOfPickup)));
                }
            }

            // Load weapon pick up textures
            instance.weaponTextureMap = new Dictionary<ProjectileType, AnimatedTexture>();
            foreach (ProjectileType ptype in Enum.GetValues(typeof(ProjectileType))) //player weapons
            {
                Instance.weaponTextureMap[ptype] = new AnimatedTexture(ContentManager.Load<Texture2D>("pickups/weapons/" + Enum.GetName(typeof(ProjectileType), ptype)));
            }

            // Load title textures
            instance.title = ContentManager.Load<Texture2D>("Title");

            // Load SpriteFonts
            instance.courierNew = ContentManager.Load<SpriteFont>("CourierNew");

            // Loading and initializing menu buttons
            instance.menuButtonTextureMap = new Dictionary<MenuButtons, Texture2D[]>();
            foreach (MenuButtons menuButton in Enum.GetValues(typeof(MenuButtons)))
            {
                instance.menuButtonTextureMap[menuButton] = new Texture2D[2];
                instance.menuButtonTextureMap[menuButton][0] = ContentManager.Load<Texture2D>("button images/" + Enum.GetName(typeof(MenuButtons), menuButton) + "Button_NoHover");
                instance.menuButtonTextureMap[menuButton][1] = ContentManager.Load<Texture2D>("button images/" + Enum.GetName(typeof(MenuButtons), menuButton) + "Button_Hover");

                // Temp variable for new button
                Button newButton = new Button(new Rectangle(new Point(820, 600 + (instance.menuButtons.Count * (instance.menuButtonTextureMap[menuButton][0].Height + 15))), 
                                                            new Point(instance.menuButtonTextureMap[menuButton][0].Width, instance.menuButtonTextureMap[menuButton][0].Height)));
                instance.menuButtons.Add(newButton);
                newButton.NoHover = instance.menuButtonTextureMap[menuButton][0];
                newButton.Hover = instance.menuButtonTextureMap[menuButton][1];
            }

            instance.dimScreen = ContentManager.Load<Texture2D>("dimColor");
            instance.map = ContentManager.Load<Texture2D>("Map");
            instance.mapMarkerRoom = ContentManager.Load<Texture2D>("MapMarkers");

            // Loading and initializing pause textures
            instance.pauseTitle = ContentManager.Load<Texture2D>("PausedLabel");

            instance.pauseButtonTextureMap = new Dictionary<PauseButtons, Texture2D[]>();
            foreach (PauseButtons pauseButton in Enum.GetValues(typeof(PauseButtons)))
            {
                instance.pauseButtonTextureMap[pauseButton] = new Texture2D[2];
                instance.pauseButtonTextureMap[pauseButton][0] = ContentManager.Load<Texture2D>("button images/" + Enum.GetName(typeof(PauseButtons), pauseButton) + "Button_NoHover");
                instance.pauseButtonTextureMap[pauseButton][1] = ContentManager.Load<Texture2D>("button images/" + Enum.GetName(typeof(PauseButtons), pauseButton) + "Button_Hover");

                // Temp variable for new button
                Button newButton = new Button(new Rectangle(new Point(820, 600 + (instance.pauseButtons.Count * (instance.pauseButtonTextureMap[pauseButton][0].Height + 15))),
                                                            new Point(instance.pauseButtonTextureMap[pauseButton][0].Width, instance.pauseButtonTextureMap[pauseButton][0].Height)));
                instance.pauseButtons.Add(newButton);
                newButton.NoHover = instance.pauseButtonTextureMap[pauseButton][0];
                newButton.Hover = instance.pauseButtonTextureMap[pauseButton][1];
            }
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (GameManager.GameState)
            {
                case GameState.Game:
                    FloorManager.Instance.Draw(spriteBatch);
                    EntityManager.Instance.Draw(spriteBatch);
                    hud.Draw(spriteBatch);
                    break;
                case GameState.PauseMenu:
                    FloorManager.Instance.Draw(spriteBatch);
                    EntityManager.Instance.Draw(spriteBatch);
                    for (int i = 0; i < pauseButtons.Count; i++)
                    {
                        pauseButtons[i].Draw(spriteBatch);
                    }
                    hud.Draw(spriteBatch);
                    spriteBatch.Draw(pauseTitle, new Rectangle(750, 300, pauseTitle.Width, pauseTitle.Height), Color.White);
                    //Dim the screen
                    spriteBatch.Draw(dimScreen, new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case GameState.Menu:
                    spriteBatch.Draw(title, new Rectangle(600, 200, title.Width + 300, title.Height), Color.White);
                    for (int i = 0; i < menuButtons.Count; i++)
                    {
                        menuButtons[i].Draw(spriteBatch);
                    }
                    break;
                case GameState.ConfirmationMenu:
                    break;
                case GameState.Map:
                    FloorManager.Instance.Draw(spriteBatch);
                    EntityManager.Instance.Draw(spriteBatch);
                    hud.Draw(spriteBatch);
                    //Dim the screen
                    spriteBatch.Draw(dimScreen, new Rectangle(0, 0, 1920, 1080), Color.White);
                    spriteBatch.Draw(map, new Rectangle(0, 0, 1920, 1080), Color.White);
                    this.DrawMap(spriteBatch);
                    break;
                case GameState.GameOver:
                    FloorManager.Instance.Draw(spriteBatch);
                    EntityManager.Instance.Draw(spriteBatch);
                    spriteBatch.Draw(dimScreen, new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
            }
        }

        private void DrawMap(SpriteBatch spriteBatch)
        {
            Point mapPoint = new Point((DisplayManager.GraphicsDevice.Viewport.Width / 2), (DisplayManager.GraphicsDevice.Viewport.Height / 2) - 40);
            /*
            //Spawn doors
            spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X + 60, mapPoint.Y + 20, 50, 50), new Rectangle(256, 0, 256, 256), Color.White); // Right
            spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X - 30, mapPoint.Y + 20, 50, 50), new Rectangle(256, 0, 256, 256), Color.White); // Left
            spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X + 65, mapPoint.Y - 25, 50, 50), new Rectangle(256, 0, 256, 256), Color.White, 1.57079632679f, new Vector2(0,0), SpriteEffects.None, 0f); // Top
            spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X + 15, mapPoint.Y + 105, 50, 50), new Rectangle(256, 0, 256, 256), Color.White, -1.57079632679f, new Vector2(0, 0), SpriteEffects.None, 0f); // Bottom
            //Spawn room
            spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X, mapPoint.Y, 80, 80), new Rectangle(0, 0, 256, 256), Color.White);
            */

            //Draw doors
            for(int x = 0; x < 11; x++)
            {
                for(int y = 0; y < 11; y++)
                {
                    if(FloorManager.Instance[x,y] != null)
                    {
                        if (FloorManager.Instance[x,y].Visited || GameManager.DEVMODE)
                        {
                            //Draw Doors
                            mapPoint = new Point((x * 90) + (DisplayManager.GraphicsDevice.Viewport.Width / 4) + 30, (y * 85) + ((DisplayManager.GraphicsDevice.Viewport.Height / 4) - 200));
                            if (FloorManager.Instance[x, y].Right != null)
                                spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X + 60, mapPoint.Y + 20, 50, 50), new Rectangle(256, 0, 256, 256), Color.White); // Right
                            if (FloorManager.Instance[x, y].Left != null)
                                spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X - 30, mapPoint.Y + 20, 50, 50), new Rectangle(256, 0, 256, 256), Color.White); // Left
                            if (FloorManager.Instance[x, y].Top != null)
                                spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X + 65, mapPoint.Y - 25, 50, 50), new Rectangle(256, 0, 256, 256), Color.White, 1.57079632679f, new Vector2(0, 0), SpriteEffects.None, 0f); // Top
                            if (FloorManager.Instance[x, y].Bottom != null)
                                spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X + 15, mapPoint.Y + 105, 50, 50), new Rectangle(256, 0, 256, 256), Color.White, -1.57079632679f, new Vector2(0, 0), SpriteEffects.None, 0f); // Bottom
                        }
                    }
                }
            }
            //Draw rooms after doors, so they always appear on top
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    if (FloorManager.Instance[x, y] != null)
                    {
                        if (FloorManager.Instance[x, y].Visited || GameManager.DEVMODE)
                        {
                            //Draw Rooms
                            mapPoint = new Point((x * 90) + (DisplayManager.GraphicsDevice.Viewport.Width / 4) + 30, (y * 85) + ((DisplayManager.GraphicsDevice.Viewport.Height / 4) - 200));
                            if (FloorManager.Instance[x, y] == FloorManager.Instance.CurrRoom)
                                spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X, mapPoint.Y, 80, 80), new Rectangle(0, 256, 256, 256), Color.Orange);
                            else if(FloorManager.Instance[x, y].Type == RoomType.Boss)
                                spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X, mapPoint.Y, 80, 80), new Rectangle(0, 256, 256, 256), Color.Red);
                            else if (FloorManager.Instance[x, y].Type == RoomType.Treasure)
                                spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X, mapPoint.Y, 80, 80), new Rectangle(0, 256, 256, 256), Color.Yellow);
                            else
                                spriteBatch.Draw(mapMarkerRoom, new Rectangle(mapPoint.X, mapPoint.Y, 80, 80), new Rectangle(0, 0, 256, 256), Color.White);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
