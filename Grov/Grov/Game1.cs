using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Grov
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //debug
        Player player;
		Enemy enemy;
        public static List<Projectile> projectiles = new List<Projectile>();

        //public EntityManager EntityManager { get => entityManager; set => entityManager = value; }
        //public DisplayManager DisplayManager { get => displayManager; set => displayManager = value; }
        //public FloorManager FloorManager { get => floorManager; set => floorManager = value; }



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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            EntityManager.Initialize();
            FloorManager.Initialize();
            DisplayManager.Initialize(Content, GraphicsDevice);

            this.player = new Player(10, 10, 2, 5, 5, 10, new Rectangle(0, 0, 215, 265), new Rectangle(0, 0, 215, 265), new Vector2(0, 0), null);
            enemy = new Enemy(EnemyType.TestEnemy, 10, true, 60f, 1f, 3f, 3, new Rectangle(100, 100, 200, 200), new Vector2(0, 0), null);

            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();

            IsMouseVisible = true;




            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.Texture = Content.Load<Texture2D>("MageholderSprite");
			enemy.Texture = Content.Load<Texture2D>("EnemyHolderSprite");
            player.Weapon.ProjectileTexture = Content.Load<Texture2D>("FireballholderSprite");
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
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update();
            }

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
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
