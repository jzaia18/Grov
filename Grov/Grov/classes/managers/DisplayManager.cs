using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


// Authors: Jake Zaia, Rachel Wong, Duncan Mott


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
        private Dictionary<string, AnimatedTexture> weaponTextureMap;
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private static DisplayManager instance;
        private SpriteFont courierNew;

        // Button stuff
        private List<Button> menuButtons;
        private int menuPointer;
        private List<Button> pauseButtons;
        private int pausePointer;

        private Texture2D startTexture_NH;
        private Texture2D startTexture_H;
        private Texture2D optionsTexture_NH;
        private Texture2D optionsTexture_H;
        private Texture2D exitTexture_NH;
        private Texture2D exitTexture_H;
        private Texture2D returnTexture_NH;
        private Texture2D returnTexture_H;
        private Texture2D restartTexture_NH;
        private Texture2D restartTexture_H;
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
        public static Dictionary<string, AnimatedTexture> WeaponTextureMap { get => instance.weaponTextureMap; }
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
            EnemyTextureMap[EnemyType.Test] = new AnimatedTexture(ContentManager.Load<Texture2D>("EnemyHolderSprite"));

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
                Instance.pickupTextureMap[typeOfPickup] = new AnimatedTexture(ContentManager.Load<Texture2D>("pickups/" + Enum.GetName(typeof(PickupType), typeOfPickup)));
            }

            // TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP TEMP
            // use same placeholder for all weapons
            instance.weaponTextureMap = new Dictionary<string, AnimatedTexture>();
            foreach (string filename in Weapon.GetAllFilenames())
            {
                Instance.weaponTextureMap[filename] = new AnimatedTexture(ContentManager.Load<Texture2D>("projectiles/Fire"));
            }
            Instance.weaponTextureMap[@"dev\Dev"] = new AnimatedTexture(ContentManager.Load<Texture2D>("projectiles/Fire"));

            // Load SpriteFonts
            instance.courierNew = ContentManager.Load<SpriteFont>("CourierNew");

            // Loading and initializing menu buttons
            instance.startTexture_NH = ContentManager.Load<Texture2D>("button images/StartButton_NoHover");
            instance.startTexture_H = ContentManager.Load<Texture2D>("button images/StartButton_Hover");
            instance.menuButtons.Add(new Button(new Rectangle(new Point(820, 500), new Point(instance.startTexture_NH.Width, instance.startTexture_NH.Height))));
            instance.menuButtons[0].NoHover = instance.startTexture_NH;
            instance.menuButtons[0].Hover = instance.startTexture_H;
            instance.menuButtons[0].IsHighlighted = true;

            instance.optionsTexture_NH = ContentManager.Load<Texture2D>("button images/OptionsButton_NoHover");
            instance.optionsTexture_H = ContentManager.Load<Texture2D>("button images/OptionsButton_Hover");
            instance.menuButtons.Add(new Button(new Rectangle(new Point(820, 500 + instance.optionsTexture_NH.Height), new Point(instance.optionsTexture_NH.Width, instance.optionsTexture_NH.Height))));
            instance.menuButtons[1].NoHover = instance.optionsTexture_NH;
            instance.menuButtons[1].Hover = instance.optionsTexture_H;

            instance.exitTexture_NH = ContentManager.Load<Texture2D>("button images/ExitButton_NoHover");
            instance.exitTexture_H = ContentManager.Load<Texture2D>("button images/ExitButton_Hover");
            instance.menuButtons.Add(new Button(new Rectangle(new Point(820, 500 + (2 * instance.exitTexture_NH.Height)), new Point(instance.exitTexture_NH.Width, instance.exitTexture_NH.Height))));
            instance.menuButtons[2].NoHover = instance.exitTexture_NH;
            instance.menuButtons[2].Hover = instance.exitTexture_H;

            // Loading and initializing pause textures
            instance.dimScreen = ContentManager.Load<Texture2D>("PauseDim");
            instance.map = ContentManager.Load<Texture2D>("Map");
            instance.mapMarkerRoom = ContentManager.Load<Texture2D>("MapMarkers");

            instance.restartTexture_NH = ContentManager.Load<Texture2D>("button images/RestartButton_NoHover");
            instance.restartTexture_H = ContentManager.Load<Texture2D>("button images/RestartButton_Hover");
            instance.pauseButtons.Add(new Button(new Rectangle(new Point(820, 500), new Point(instance.restartTexture_NH.Width, instance.restartTexture_NH.Height))));
            instance.pauseButtons[0].NoHover = instance.restartTexture_NH;
            instance.pauseButtons[0].Hover = instance.restartTexture_H;

            instance.returnTexture_NH = ContentManager.Load<Texture2D>("button images/ReturnMenuButton_NoHover");
            instance.returnTexture_H = ContentManager.Load<Texture2D>("button images/ReturnMenuButton_Hover");
            instance.pauseButtons.Add(new Button(new Rectangle(new Point(820, 500 + instance.returnTexture_NH.Height), new Point(instance.returnTexture_NH.Width, instance.returnTexture_NH.Height))));
            instance.pauseButtons[1].NoHover = instance.returnTexture_NH;
            instance.pauseButtons[1].Hover = instance.returnTexture_H;

            instance.pauseButtons.Add(new Button(new Rectangle(new Point(820, 500 + (2 * instance.exitTexture_NH.Height)), new Point(instance.exitTexture_NH.Width, instance.exitTexture_NH.Height))));
            instance.pauseButtons[2].NoHover = instance.exitTexture_NH;
            instance.pauseButtons[2].Hover = instance.exitTexture_H;
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
                    //Dim the screen
                    spriteBatch.Draw(dimScreen, new Rectangle(0, 0, 1920, 1080), Color.White);
                    break;
                case GameState.Menu:
                    spriteBatch.DrawString(courierNew, "Grov", new Vector2(880, 200), Color.Black);
                    for (int i = 0; i < menuButtons.Count; i++)
                    {
                        menuButtons[i].Draw(spriteBatch);
                    }
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
