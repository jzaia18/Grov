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
    enum GameState
    {
        Game,
        Menu,
        PauseMenu,
        Map,
        GameOver
    }

    //enum ControlState
    //{
    //    KeyboardMode,
    //    MouseMode,
    //    GamePadMode
    //}

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
                    if ((EntityManager.Player.CurrHP <= 0))
                        gameState = GameState.GameOver;
                    EntityManager.Instance.Update();
                    FloorManager.Instance.Update();
                    break;
                case GameState.Menu:
                    if (currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down) && DisplayManager.MenuPointer < DisplayManager.MenuButtons.Count - 1)
                    {
                        DisplayManager.MenuPointer += 1;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up) && DisplayManager.MenuPointer > 0)
                    {
                        DisplayManager.MenuPointer -= 1;
                    }

                    for (int i = 0; i < DisplayManager.MenuButtons.Count; i++)
                    {
                        // Checking which button the pointer is on
                        if (DisplayManager.MenuPointer == i || DisplayManager.MenuButtons[i].Rect.Contains(currentMouseState.Position))
                        {
                            DisplayManager.MenuPointer = i;
                            DisplayManager.MenuButtons[i].IsHighlighted = true;
                        }
                        else
                        {
                            DisplayManager.MenuButtons[i].IsHighlighted = false;
                        }

                        // If pointer is on button and Enter/Left mouse button is pressed, do whatever the button is supposed to do
                        if (((currentMouseState.LeftButton.Equals(ButtonState.Pressed) && previousMouseState.LeftButton.Equals(ButtonState.Released)) || (!previousKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter)) && DisplayManager.MenuButtons[i].IsHighlighted))
                        {
                            if (DisplayManager.MenuPointer == DisplayManager.MenuButtons.Count - 1)
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

                    if (currentKeyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down) && DisplayManager.PausePointer < DisplayManager.PauseButtons.Count - 1)
                    {
                        DisplayManager.PausePointer += 1;
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up) && DisplayManager.PausePointer > 0)
                    {
                        DisplayManager.PausePointer -= 1;
                    }

                    for (int i = 0; i < DisplayManager.PauseButtons.Count; i++)
                    {
                        // Checking which button the pointer is on
                        if (DisplayManager.PausePointer == i || DisplayManager.PauseButtons[i].Rect.Contains(currentMouseState.Position))
                        {
                            DisplayManager.PausePointer = i;
                            DisplayManager.PauseButtons[i].IsHighlighted = true;
                        }
                        else
                        {
                            DisplayManager.PauseButtons[i].IsHighlighted = false;
                        }

                        // If pointer is on button and Enter/Left mouse button is pressed, do whatever the button is supposed to do
                        if ((currentMouseState.LeftButton.Equals(ButtonState.Pressed) || (!previousKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter)) && DisplayManager.PauseButtons[i].IsHighlighted))
                        {
                            if (DisplayManager.PausePointer == DisplayManager.PauseButtons.Count - 1)
                            {
                                Exit();
                            }
                            else if(DisplayManager.PausePointer == 1)
                            {
                                gameState = GameState.Menu;
                            }
                            else if(DisplayManager.PausePointer == 0)
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
                case GameState.Map:
                    if ((!currentKeyboardState.IsKeyDown(Keys.Tab) && previousKeyboardState.IsKeyDown(Keys.Tab)) || (currentGamePadState.IsButtonDown(Buttons.Back) && !previousGamePadState.IsButtonDown(Buttons.Back)))
                        gameState = GameState.Game;
                    break;
                case GameState.GameOver:
                    if (currentKeyboardState.IsKeyDown(Keys.Enter))
                        gameState = GameState.Menu;
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
