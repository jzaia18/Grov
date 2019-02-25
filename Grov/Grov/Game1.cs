﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Diagnostics;

namespace Grov
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        EntityManager entityManager;
        DisplayManager displayManager;
        FloorManager floorManager;

        Random rng;

        //debug
        Room testRoom;
        Player player;
		Enemy enemy;
        HUD HUD;

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
            player = new Player(10, 10, 2, 5, 5, 10, new Rectangle(0, 0, 200, 300), new Rectangle(0, 0, 200, 300), new Vector2(0, 0), rng, null);
            enemy = new Enemy(EnemyType.TestEnemy, 10, true, 60f, 1f, 3f, 3, new Rectangle(100, 100, 200, 300), new Vector2(0, 0), rng, null);

            testRoom = new Room(RoomType.Normal);

            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            HUD = new HUD(player, Content);

            entityManager = new EntityManager(GraphicsDevice, rng);
            floorManager = new FloorManager();
            displayManager = new DisplayManager(entityManager, floorManager, HUD, player);

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
            HUD.Initialize();
            player.Texture = Content.Load<Texture2D>("MageholderSprite");
			enemy.Texture = Content.Load<Texture2D>("EnemyHolderSprite");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
			enemy.Update(player);

            //System.Console.WriteLine(player.CurrentHP);

			//Debug.WriteLine(enemy.Position.ToString() + ", " + enemy.DrawPos.ToString());

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
			enemy.Draw(spriteBatch);
            HUD.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
