using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

// Authors: Jake Zaia, Duncan Mott, Rachel Wong

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
            player = new Player(100, 100, 2, 5, 5, 1, new Rectangle(65, 65, 60, 74), new Rectangle(65, 110, 60, 29), new Vector2(0, 0), null);
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
                //Update enemies, this time actually kill them though
                for(int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Update();
                    if (enemies[i].IsActive == false)
                    {
                        enemies.RemoveAt(i);
                        i--;
                    }
                }
            }

            //Update friendly projectiles
            for (int i = 0; i < friendlyProjectiles.Count; i++)
            {
                friendlyProjectiles[i].Update();
                if (friendlyProjectiles[i].IsActive == false)
                {
                    friendlyProjectiles.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < hostileProjectiles.Count; i++)
            {
                hostileProjectiles[i].Update();
                if (hostileProjectiles[i].IsActive == false)
                {
                    hostileProjectiles.RemoveAt(i);
                    i--;
                }
            }

            HandleTerrainCollisions(player);
            foreach (Enemy enemy in enemies) HandleTerrainCollisions(enemy);
            foreach (Projectile projectile in friendlyProjectiles) HandleTerrainCollisions(projectile);
            foreach (Projectile projectile in hostileProjectiles) HandleTerrainCollisions(projectile);
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
                if (projectile.Hitbox.Intersects(player.DrawPos) && player.IFrames == 0)
                {
                    projectile.IsActive = false;
                    player.CurrHP -= 1f;
                    player.IFrames = MAX_IFRAMES;
                }
            }
        }

        public void HandleTerrainCollisions(Entity entity)
        {
            // Gathering all the tiles that the entities touch
            List<Tile> entityTiles = FloorManager.Instance.CollidesWith(entity);

            foreach(Tile entityTile in entityTiles)
            {
                if ((!entityTile.IsPassable && !(entity is Projectile)) || (entityTile.BlocksProjectiles && (entity is Projectile)))
                {
                    // Temp variables for structs
                    Rectangle temp = entity.Hitbox;

                    // Finding spatial position of obstacle against player
                    int dx = (entityTile.Location.X * FloorManager.TileWidth) - entity.Hitbox.X;
                    int dy = (entityTile.Location.Y * FloorManager.TileHeight) - entity.Hitbox.Y;

                    // Determining where to move player after collision
                    Rectangle overlap = Rectangle.Intersect(entity.Hitbox, new Rectangle(FloorManager.TileWidth * entityTile.Location.X, FloorManager.TileHeight * entityTile.Location.Y, FloorManager.TileWidth, FloorManager.TileHeight));

                    if(overlap.Width > overlap.Height)
                    {
                        if(dy > 0)
                        {
                            temp.Y -= overlap.Height;
                        }
                        else
                        {
                            temp.Y += overlap.Height;
                            
                        }
                    }
                    else
                    {
                        if (dx > 0)
                        {
                            temp.X -= overlap.Width;
                        }
                        else
                        {
                            temp.X += overlap.Width;
                        }
                    }
                    Point delta = temp.Location - entity.Hitbox.Location;
                    entity.DrawPos = new Rectangle(entity.DrawPos.X + delta.X, entity.DrawPos.Y + delta.Y, entity.DrawPos.Width, entity.DrawPos.Height);
                    entity.Position = new Vector2(entity.DrawPos.X, entity.DrawPos.Y);
                    entity.Hitbox = temp;
                    if (entity is Projectile) entity.IsActive = false;
                }
            }
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
                        enemy.Hitstun += player.Weapon.Hitstun;
                    }
                }
            }
        }

        public void SpawnEnemies(EnemyType enemyType, Vector2 position)
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

                enemies.Add(new Enemy(enemyType, maxHP, true, fireRate, attackDamage, moveSpeed, projectileSpeed, new Rectangle((int)position.X, (int)position.Y, 60, 60), new Vector2(0,0)));
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
