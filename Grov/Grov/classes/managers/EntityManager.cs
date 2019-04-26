using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

// Authors: Jake Zaia, Duncan Mott, Rachel Wong, Jack Hoffman

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
        private List<Pickup> pickups;
        private List<Pickup> newPickups;
        private static EntityManager instance;

        // ************* Constants ************* //

        private int MAX_IFRAMES = 60;
        #endregion

        #region properties
        // ************* Properties ************* //

        public static Player Player { get => instance.player;  }
        public static EntityManager Instance { get => instance; }
        public static List<Pickup> NewPickups { get => instance.newPickups; }
        #endregion

        #region constructor
        // ************* Constructor ************* //

        private EntityManager() {
            enemies = new List<Enemy>();
            hostileProjectiles = new List<Projectile>();
            friendlyProjectiles = new List<Projectile>();
            pickups = new List<Pickup>();
            newPickups = new List<Pickup>();

            //testing
            player = new Player(100, 100, 2, 6, 5, 1, new Rectangle((15  * FloorManager.TileWidth) + FloorManager.TileWidth/2, (8 * FloorManager.TileHeight) + FloorManager.TileWidth/2, 60, 74), 
                new Rectangle((15 * FloorManager.TileWidth) + FloorManager.TileWidth/2, (8 * FloorManager.TileHeight) + FloorManager.TileWidth/2 + 45, 60, 29), new Vector2(0, 0), null);
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

            if (enemies.Count > 0)
            {
                //Update enemies, this time actually kill them though
                for(int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Update();
                    if (enemies[i].IsActive == false)
                    {
                        if (GameManager.RNG.Next(0, 100) <= 12)
                        {
                            pickups.Add(new Pickup(PickupType.Heart, new Rectangle((int)enemies[i].Hitbox.X, (int)enemies[i].Hitbox.Y, 60, 60)));
                            FloorManager.Instance.CurrRoom.PickupsInRoom.Add(pickups[pickups.Count - 1]);
                        }
                        enemies.RemoveAt(i);
                        i--;
                    }
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

            if (pickups.Count > 0)
            {
                //Update pickups, this time actually kill them though
                for (int i = 0; i < pickups.Count; i++)
                {
                    pickups[i].Update();
                    if (pickups[i].IsActive == false)
                    {
                        pickups.RemoveAt(i);
                        i--;
                    }
                }
            }

            HandleTerrainCollisions(player);
            foreach (Enemy enemy in enemies) HandleTerrainCollisions(enemy);
            foreach (Projectile projectile in friendlyProjectiles) HandleTerrainCollisions(projectile);
            foreach (Projectile projectile in hostileProjectiles) HandleTerrainCollisions(projectile);
            foreach (Pickup itemToPickUp in pickups) HandlePickUpCollisions(itemToPickUp);

            //Move new pickups to the old folder
            if (newPickups.Count > 0)
            {
                for (int i = 0; i < newPickups.Count; i++)
                {
                    pickups.Add(newPickups[i]);
                }
                newPickups.Clear();
            }

            HandleEnemyDamageCollisions();
            HandlePlayerDamageCollisions();
            HandleMeleeCollisions();

            if(enemies.Count == 0)
            {
                FloorManager.Instance.CurrRoom.IsCleared = true;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile projectile in friendlyProjectiles) projectile.Draw(spriteBatch);
            foreach (Projectile projectile in hostileProjectiles) projectile.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            foreach (Enemy enemy in enemies) enemy.Draw(spriteBatch);

            foreach (Pickup pickup in pickups) pickup.Draw(spriteBatch);
        }

        public void ClearEntities()
        {
            enemies.Clear();
            hostileProjectiles.Clear();
            friendlyProjectiles.Clear();
            pickups.Clear();
        }

        /// <summary>
        /// This method is called when you start a new name; resets player to default values
        /// </summary>
        public void ResetPlayer()
        {
            player.Position = new Vector2((15 * FloorManager.TileWidth) + FloorManager.TileWidth / 2, (8 * FloorManager.TileHeight) + FloorManager.TileWidth / 2);
            player.Weapon = new Weapon(@"dev\Default", default(Rectangle), false, true);
            player.LastWeaponFired = player.Weapon;
            if (GameManager.DEVMODE == true)
                player.Secondary = new Weapon(@"dev\Dev", default(Rectangle), false, true);
            else
                player.Secondary = null;
            player.CurrHP = 100;
            player.MaxHP = 100;
            player.MaxMP = 100;
            GameManager.Instance.SpawnedWeapons.Clear();
        }

        public void HandlePlayerDamageCollisions()
        {
            foreach(Projectile projectile in hostileProjectiles)
            {
                if (projectile.Hitbox.Intersects(player.DrawPos) && player.IFrames == 0)
                {
                    projectile.IsActive = false;
                    player.CurrHP -= projectile.Damage;
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
                //Kill entity if it goes out of bounds
                if(entityTile.Type == TileType.Death)
                {
                    entity.IsActive = false;
                    return;
                }

                //Noclip entities don't collide
                if (entity is Projectile)
                    if (((Projectile)entity).Noclip)
                        continue;

                if ((!entityTile.IsPassable && !(entity is Projectile)) || (entityTile.BlocksProjectiles && (entity is Projectile)))
                {
                    // Temp variables for structs
                    Rectangle temp = entity.Hitbox;

                    // Finding spatial position of obstacle against player
                    int dx = (entityTile.Location.X * FloorManager.TileWidth) - entity.Hitbox.X;
                    int dy = (entityTile.Location.Y * FloorManager.TileHeight) - entity.Hitbox.Y;

                    // Determining where to move player after collision
                    Rectangle overlap = Rectangle.Intersect(entity.Hitbox, new Rectangle(FloorManager.TileWidth * entityTile.Location.X, FloorManager.TileHeight * entityTile.Location.Y, FloorManager.TileWidth, FloorManager.TileHeight));

                    if (overlap.Width > overlap.Height)
                    {
                        if (dy > 0)
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
                else if(entityTile.Type == TileType.Entrance && entity == player)
                {
                    //Which door did we take?
                    int door = -1; // 0 top 1 left 2 bottom 3 right
                    if (entityTile.Location.Y == 0) //top
                        door = 0;
                    else if (entityTile.Location.X == 0) //left
                        door = 1;
                    else if (entityTile.Location.Y == 17) //bottom
                        door = 2;
                    else if (entityTile.Location.X == 31) //Right
                        door = 3;

                    //We are in a new room now
                    switch (door)
                    {
                        case 0: //From top to bottom
                            FloorManager.Instance.CurrRoom = FloorManager.Instance.CurrRoom.Top.NextRoom;
                            player.Position = new Vector2((FloorManager.Instance.CurrRoom.Bottom.Location.X * 60) - 30, DisplayManager.GraphicsDevice.Viewport.Height - player.DrawPos.Height - (FloorManager.TileHeight * 1.05f));
                            break;
                        case 1: //From Left to Right
                            FloorManager.Instance.CurrRoom = FloorManager.Instance.CurrRoom.Left.NextRoom;
                            player.Position = new Vector2(DisplayManager.GraphicsDevice.Viewport.Width - Player.Hitbox.Width - (FloorManager.TileHeight * 1.05f), (FloorManager.Instance.CurrRoom.Right.Location.Y * 60) - 30);
                            break;
                        case 2: //From Bottom to Top
                            FloorManager.Instance.CurrRoom = FloorManager.Instance.CurrRoom.Bottom.NextRoom;
                            player.Position = new Vector2((FloorManager.Instance.CurrRoom.Top.Location.X * 60) - 30, (FloorManager.TileHeight * 1.05f));
                            break;
                        case 3: //From Right to left
                            FloorManager.Instance.CurrRoom = FloorManager.Instance.CurrRoom.Right.NextRoom;
                            player.Position = new Vector2((FloorManager.TileHeight * 1.05f), (FloorManager.Instance.CurrRoom.Left.Location.Y * 60) - 30);
                            break;
                    }

                    FloorManager.Instance.CurrRoom.Visited = true;

                    //Delete all entities in the room
                    this.ClearEntities();

                    //Spawn enemies
                    if (!FloorManager.Instance.CurrRoom.IsCleared)
                    {
                        FloorManager.Instance.CurrRoom.SpawnEnemies();
                    }
                    FloorManager.Instance.CurrRoom.SpawnPickups();

                    //Rest of the collisions don't matter because we're in a new room now
                    return;
                }
                else if(entityTile.Type == TileType.BossDoor && entity == player)
                {
                    //Clear all entities
                    this.ClearEntities();

                    //Make a new floor
                    FloorManager.Instance.FloorNumber++;
                    FloorManager.Instance.GenerateFloor();

                    //Reset player position
                    player.Position = new Vector2((15 * FloorManager.TileWidth) + FloorManager.TileWidth / 2, (8 * FloorManager.TileHeight) + FloorManager.TileWidth / 2);

                    return;
                }
            }
        }

        public void HandleMeleeCollisions()
        {
            foreach(Enemy enemy in enemies)
            {
                foreach (Enemy secondEnemy in enemies)
                {
                    if (secondEnemy != enemy)
                    {
                        if (enemy.Hitbox.Intersects(secondEnemy.Hitbox))
                        {
                            Rectangle overlap = Rectangle.Intersect(enemy.Hitbox, secondEnemy.Hitbox);

                            if (overlap.Width > overlap.Height)
                            {
                                if (secondEnemy.Position.Y > enemy.Position.Y)
                                    secondEnemy.Position = new Vector2(secondEnemy.Position.X, secondEnemy.Position.Y + overlap.Height);
                                else
                                    secondEnemy.Position = new Vector2(secondEnemy.Position.X, secondEnemy.Position.Y - overlap.Height);
                            }
                            else
                            {
                                if (secondEnemy.Position.X > enemy.Position.X)
                                    secondEnemy.Position = new Vector2(secondEnemy.Position.X + overlap.Width, secondEnemy.Position.Y);
                                else
                                    secondEnemy.Position = new Vector2(secondEnemy.Position.X - overlap.Width, secondEnemy.Position.Y);
                            }
                        }
                    }
                }

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
                        enemy.CurrHP -= projectile.Damage;
                        if(!projectile.Noclip)
                            projectile.IsActive = false;
                        if(!enemy.Sturdy)
                            enemy.Hitstun += player.Weapon.Hitstun;
                    }
                }
            }
        }

        public void HandlePickUpCollisions(Pickup collectible)
        {
            if (player.Hitbox.Intersects(collectible.Hitbox))
            {
                collectible.IsActive = false;
                player.Interact(collectible);
            }
        }

        public void SpawnEnemies(EnemyType enemyType, Vector2 position)
        {
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
                string weaponName = null;
                int lungeTime = 0;
                if (melee)
                    lungeTime = int.Parse(reader.ReadLine());
                else
                    weaponName = reader.ReadLine();
                bool sturdy = bool.Parse(reader.ReadLine());

                if (enemyType == EnemyType.Grot)
                {
                    enemies.Add(new GrotBehaviorManager(EnemyType.Grot, maxHP, melee, fireRate, attackDamage, moveSpeed, projectileSpeed, new Rectangle((int)position.X, (int)position.Y, 60, 60), new Vector2(0, 0), weaponName, lungeTime, sturdy));
                }
                else
                {
                    enemies.Add(new Enemy(enemyType, maxHP, melee, fireRate, attackDamage, moveSpeed, projectileSpeed, new Rectangle((int)position.X, (int)position.Y, 60, 60), new Vector2(0, 0), weaponName, lungeTime, sturdy));
                }
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

        /// <summary>
        /// Adds the given pickup to the list of pickups in the current room
        /// </summary>
        /// <param name="pickup"></param>
        public void SpawnPickup(Pickup pickup)
        {
            this.pickups.Add(pickup);
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
