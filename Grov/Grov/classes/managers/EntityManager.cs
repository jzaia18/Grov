using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

// Authors: Jake Zaia

namespace Grov
{
    class EntityManager
    {
        #region fields
        // ************* Fields ************* //

        private Player player;
        private List<Enemy> enemies;
        private List<Projectile> hostileProjectiles;
        private List<Projectile> friendlyProjectiles;
        private Random rng;
        private static EntityManager instance;

        // ************* Constants ************* //

        private int MAX_IFRAMES = 60;
        #endregion

        #region properties
        // ************* Properties ************* //

        public static Player Player { get => instance.player; }
        public static EntityManager Instance { get => instance; }
        public static Random RNG { get => instance.rng; }
        #endregion

        #region constructor
        // ************* Constructor ************* //

        private EntityManager() {
            enemies = new List<Enemy>();
            hostileProjectiles = new List<Projectile>();
            friendlyProjectiles = new List<Projectile>();
            rng = new Random();

            //testing
            player = new Player(100, 100, 2, 5, 5, 1, new Rectangle(0, 0, 107, 132), new Rectangle(0, 0, 215, 265), new Vector2(0, 0), null);
            SpawnEnemies(1, EnemyType.Test);
        }

        public static void Initialize()
        {
            if (instance == null)
            {
                instance = new EntityManager();
            }
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        public void Update()
        {
            Player.Update();
            if (enemies.Count > 0)
            {
                foreach (Enemy enemy in enemies) enemy.Update();
            }
        
            foreach (Projectile projectile in friendlyProjectiles) projectile.Update();
            foreach (Projectile projectile in hostileProjectiles) projectile.Update();

            HandleTerrainCollisions();
            HandlePlayerDamageCollisions();
            HandleEnemyDamageCollisions();
            HandleMeleeCollisions();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            foreach (Enemy enemy in enemies) enemy.Draw(spriteBatch);
            foreach (Projectile projectile in friendlyProjectiles) projectile.Draw(spriteBatch);
            foreach (Projectile projectile in hostileProjectiles) projectile.Draw(spriteBatch);
        }

        public void HandlePlayerDamageCollisions()
        {
            foreach(Projectile projectile in hostileProjectiles)
            {
                if (projectile.Hitbox.Intersects(player.Hitbox) && player.IFrames == 0)
                {
                    projectile.IsActive = false;
                    player.CurrHP -= 1f;
                    player.IFrames = MAX_IFRAMES;
                }
            }
        }

        public void HandleTerrainCollisions()
        {
            
        }

        public void HandleMeleeCollisions()
        {
            foreach(Enemy enemy in enemies)
            {
                if (enemy.Melee)
                {
                    if (enemy.Hitbox.Intersects(player.Hitbox) && Player.IFrames == 0)
                    {
                        player.CurrHP -= 10f;
                        player.IFrames = MAX_IFRAMES;
                    }
                }
            }
        }

        public void HandleEnemyDamageCollisions()
        {
            foreach(Projectile projectile in friendlyProjectiles)
            {
                foreach(Enemy enemy in enemies)
                {
                    if (projectile.Hitbox.Intersects(enemy.Hitbox))
                    {
                        enemy.CurrHP -= 1f;
                        projectile.IsActive = false;
                    }
                }
            }
        }

        public void SpawnEnemies(int numEnemies, EnemyType enemyType)
        {

            //TODO: MUST TEST
            string filename = @"resources\enemies\" + enemyType + ".txt"; 
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(filename);

                string name = reader.ReadLine();
                int maxHP = Int32.Parse(reader.ReadLine());
                bool melee = Boolean.Parse(reader.ReadLine());
                float fireRate = float.Parse(reader.ReadLine());
                float attackDamage = float.Parse(reader.ReadLine());
                float moveSpeed = float.Parse(reader.ReadLine());
                float projectileSpeed = float.Parse(reader.ReadLine());

                while (numEnemies-- > 0)
                    enemies.Add(new Enemy(enemyType, maxHP, true, fireRate, attackDamage, moveSpeed, projectileSpeed, new Rectangle(rng.Next(DisplayManager.GraphicsDevice.Viewport.Width), rng.Next(DisplayManager.GraphicsDevice.Viewport.Height), 100, 100), new Vector2(0,0)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void RemoveEnemy(Enemy enemy)
        {
            throw new NotImplementedException();
        }

        public static void AddProjectile(Projectile projectile)
        {
            if (projectile.IsFromPlayer)
            {
                instance.friendlyProjectiles.Add(projectile);
            } else
            {
                instance.hostileProjectiles.Add(projectile);
            }
        }



        #endregion
    }
}
