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
    enum GameState
    {
        Game,
        Menu,
        PauseMenu,
        Map
    }

    class GameManager
    {
        #region fields
        // ************* Fields ************* //

        private static GameManager instance;
        private bool devmode = true;
        private bool gameExit;
        private Random rng;
        private GameState gameState;

        private GamePadState previousGamePadState;
        private GamePadState currentGamePadState;
        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;
        private MouseState previousMouseState;
        private MouseState currentMouseState;
        
        #endregion

        #region properties
        // ************* Properties ************* //

        public static GameManager Instance { get => instance; }
        public static bool DEVMODE { get => instance.devmode; }
        public static Random RNG { get => instance.rng; }
        public static GameState GameState { get => instance.gameState; set => instance.gameState = value; }
        public static bool GameExit { get => instance.gameExit; set => instance.gameExit = value; }

        public static GamePadState PreviousGamePadState { get => instance.previousGamePadState; }
        public static GamePadState CurrentGamePadState { get => instance.currentGamePadState; }
        public static KeyboardState PreviousKeyboardState { get => instance.previousKeyboardState; }
        public static KeyboardState CurrentKeyboardState { get => instance.currentKeyboardState; }
        public static MouseState PreviousMouseState { get => instance.previousMouseState; }
        public static MouseState CurrentMouseState { get => instance.currentMouseState; }

        #endregion

        #region constructors
        // ************* Constructor ************* //
        private GameManager()
        {
            rng = new Random();
            gameState = GameState.Menu;
        }

        public static void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            if (instance == null)
            {
                instance = new GameManager();
                DisplayManager.Initialize(content, graphicsDevice);
                EntityManager.Initialize();
                FloorManager.Initialize();
                AudioManager.Initialize();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        #endregion

        #region methods
        // ************* Methods ************* //

        public void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();
            currentGamePadState = GamePad.GetState(0);
            currentKeyboardState = Keyboard.GetState();

            AudioManager.Instance.Update(gameTime);

            switch (gameState)
            {
                case GameState.Game:
                    if ((currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape)) || (currentGamePadState.IsButtonDown(Buttons.Start) && !previousGamePadState.IsButtonDown(Buttons.Start)))
                        gameState = GameState.PauseMenu;
                    if ((currentKeyboardState.IsKeyDown(Keys.Tab)) || (currentGamePadState.IsButtonDown(Buttons.Back) && !previousGamePadState.IsButtonDown(Buttons.Back)))
                        gameState = GameState.Map;
                    EntityManager.Instance.Update();
                    FloorManager.Instance.Update();
                    break;
                case GameState.Menu:
                    for (int i = 0; i < DisplayManager.MenuButtons.Count; i++)
                    {
                        if (CurrentMouseState.LeftButton.Equals(ButtonState.Pressed) && DisplayManager.MenuButtons[i].IsHighlighted)
                        {
                            if (i == DisplayManager.MenuButtons.Count - 1)
                            {
                                Exit();
                            }
                            else
                            {
                                EntityManager.Instance.ClearEntities();
                                EntityManager.Instance.ResetPlayer();
                                FloorManager.Instance.FloorNumber = 1;
                                FloorManager.Instance.GenerateFloor();
                                gameState = GameState.Game;
                            }
                        }
                    }
                    break;
                case GameState.PauseMenu:
                    if ((currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape)) || (currentGamePadState.IsButtonDown(Buttons.Start) && !previousGamePadState.IsButtonDown(Buttons.Start)))
                        gameState = GameState.Game;
                    if (currentKeyboardState.IsKeyDown(Keys.Enter))
                        gameState = GameState.Menu;
                    break;
                case GameState.Map:
                    if ((!currentKeyboardState.IsKeyDown(Keys.Tab) && previousKeyboardState.IsKeyDown(Keys.Tab)) || (currentGamePadState.IsButtonDown(Buttons.Back) && !previousGamePadState.IsButtonDown(Buttons.Back)))
                        gameState = GameState.Game;
                    break;
            }

            previousKeyboardState = currentKeyboardState;
            previousGamePadState = currentGamePadState;
            previousMouseState = currentMouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DisplayManager.Instance.Draw(spriteBatch);
        }

        /// <summary>
        /// Helper method to allow for easier exit syntax
        /// </summary>
        public void Exit()
        {
            gameExit = true;
        }
        #endregion
    }
}
