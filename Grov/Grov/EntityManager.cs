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


        // ************* Properties ************* //

        public Player Player { get => player; set => player = value; }


        // ************* Constructor ************* //

        public EntityManager() {
            enemies = new List<Enemy>();
            hostileProjectiles = new List<Projectile>();
            friendlyProjectiles = new List<Projectile>();
            textureMap = new Dictionary<EnemyType, Texture2D>();
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
                double fireRate = Double.Parse(reader.ReadLine());
                double attackDamage = Double.Parse(reader.ReadLine());

                while (numEnemies-- > 0)
                    enemies.Add(new Enemy(enemyType, maxHP, melee, fireRate, attackDamage));
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
