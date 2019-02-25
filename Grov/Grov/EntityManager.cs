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
        // ************* Fields ************* //
        private Player player;
        private List<Enemy> enemies;
        private List<Projectile> hostileProjectiles;
        private List<Projectile> friendlyProjectiles;
        private Dictionary<EnemyType, Texture2D> textureMap;
        private GraphicsDevice graphicsDevice;
        private Random rng;


        // ************* Properties ************* //

        public Player Player { get => player; set => player = value; }


        // ************* Constructor ************* //

        public EntityManager(GraphicsDevice graphicsDevice, Random rng) {
            enemies = new List<Enemy>();
            hostileProjectiles = new List<Projectile>();
            friendlyProjectiles = new List<Projectile>();
            textureMap = new Dictionary<EnemyType, Texture2D>();
            this.graphicsDevice = graphicsDevice;
            this.rng = rng;

            //player = new Player();
        }

        // ************* Methods ************* //

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

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
                    enemies.Add(new Enemy(enemyType, maxHP, melee, fireRate, attackDamage, moveSpeed, projectileSpeed, new Rectangle(rng.Next(graphicsDevice.Viewport.Width), rng.Next(graphicsDevice.Viewport.Height), 100, 100), new Vector2(0,0), rng, null));
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


    }
}
