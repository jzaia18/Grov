﻿using System;
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
            player = new Player(10, 10, 2, 5, 5, 10, new Rectangle(0, 0, 215, 265), new Rectangle(0, 0, 215, 265), new Vector2(0, 0), null);
            enemies.Add(new Enemy(EnemyType.TestEnemy, 10, true, 60f, 1f, 3f, 3, new Rectangle(100, 100, 200, 200), new Vector2(0, 0)));
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
            foreach (Enemy enemy in enemies) enemy.Update();
            foreach (Projectile projectile in friendlyProjectiles) projectile.Update();
            foreach (Projectile projectile in hostileProjectiles) projectile.Update();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            foreach (Enemy enemy in enemies) enemy.Draw(spriteBatch);
            foreach (Projectile projectile in friendlyProjectiles) projectile.Draw(spriteBatch);
            foreach (Projectile projectile in hostileProjectiles) projectile.Draw(spriteBatch);
        }

        public void HandleCollisions()
        {

        }

        public void SpawnEnemies(int numEnemies, EnemyType enemyType)
        {

            //TODO: MUST TEST
            string filename = @"enemies\" + enemyType + ".txt"; 
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
                    enemies.Add(new Enemy(enemyType, maxHP, melee, fireRate, attackDamage, moveSpeed, projectileSpeed, new Rectangle(rng.Next(DisplayManager.GraphicsDevice.Viewport.Width), rng.Next(DisplayManager.GraphicsDevice.Viewport.Height), 100, 100), new Vector2(0,0)));
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
