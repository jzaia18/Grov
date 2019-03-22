using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;


namespace Grov
{

    enum GameState
    {
        Game,
        Menu,
        PauseMenu,
        Map
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Customizable display options
            //TODO: when we make options, these should be properties/in another class
        public int windowHeight = 1080;
        public int windowWidth = 1920;
        public bool fullScreen = true;
        public bool hardwareSwitch = true;
        public GameState state;

        private GamePadState gamePadPreviousState;
        private KeyboardState keyboardPreviousState;
        private MouseState mousePreviousState;
        private GamePadState gamePadState;
        private KeyboardState keyboardState;
        private MouseState mouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            DisplayManager.Initialize(Content, GraphicsDevice);
            EntityManager.Initialize();
            FloorManager.Initialize();

            state = GameState.Menu;

            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            //Set the game to Fullscreen mode
            graphics.IsFullScreen = fullScreen;
            //This makes it a "soft" fullscreen, AKA borderless fullscreen. Less efficient but faster to initialize
            graphics.HardwareModeSwitch = hardwareSwitch;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            EntityManager.Player.Texture = new AnimatedTexture(Content.Load<Texture2D>("MageholderSprite"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {}

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            switch (state)
            {
                case GameState.Game:
                    if( (keyboardState.IsKeyDown(Keys.Escape) && !keyboardPreviousState.IsKeyDown(Keys.Escape)) || (gamePadState.IsButtonDown(Buttons.Start) && !gamePadPreviousState.IsButtonDown(Buttons.Start)))
                        state = GameState.PauseMenu;
                    if ((keyboardState.IsKeyDown(Keys.Tab)) || (gamePadState.IsButtonDown(Buttons.Back) && !gamePadPreviousState.IsButtonDown(Buttons.Back)))
                        state = GameState.Map;
                    EntityManager.Instance.Update();
                    FloorManager.Instance.Update();
                    break;
                case GameState.Menu:
                    MouseState ms = Mouse.GetState();

                    for(int i = 0; i < DisplayManager.MenuButtons.Count; i++)
                    {
                        if(ms.LeftButton.Equals(ButtonState.Pressed) && DisplayManager.MenuButtons[i].IsHighlighted)
                        {
                            if (i == DisplayManager.MenuButtons.Count - 1)
                            {
                                Exit();
                            }
                            else
                            {
                                state = GameState.Game;
                            }
                        }
                    }
                    break;
                case GameState.PauseMenu:
                    if ((keyboardState.IsKeyDown(Keys.Escape) && !keyboardPreviousState.IsKeyDown(Keys.Escape)) || (gamePadState.IsButtonDown(Buttons.Start) && !gamePadPreviousState.IsButtonDown(Buttons.Start)))
                        state = GameState.Game;
                    if (keyboardState.IsKeyDown(Keys.Enter))
                        Exit();
                    break;
                case GameState.Map:
                    if ((!keyboardState.IsKeyDown(Keys.Tab) && keyboardPreviousState.IsKeyDown(Keys.Tab)) || (gamePadState.IsButtonDown(Buttons.Back) && !gamePadPreviousState.IsButtonDown(Buttons.Back)))
                        state = GameState.Game;
                    break;
            }

            keyboardPreviousState = keyboardState;
            gamePadPreviousState = gamePadState;
            mousePreviousState = mouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            spriteBatch.Begin();

            DisplayManager.Instance.Draw(spriteBatch, state);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
