using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Authors: Jake Zaia, Rachel Wong, Jack Hoffman, Duncan Mott

namespace Grov
{
    enum EnemyType
    {
        //Bosses
        Grot = -1,

        //Normies
		Test = 0,
        Shooty = 1
    }

    class Enemy : Creature
    {
        #region fields
        // ************* Fields ************* //

        private EnemyType enemyType;
        private int hitstun;
        private int lungeTime;
        private int timeSinceLunge;
        private bool sturdy;
        private Pathfinder pathfinder;
        private List<Tile> currentPath;
        #endregion

        #region properties
        // ************* Properties ************* //

        public int Hitstun { get => hitstun; set => hitstun = value; }
        public bool Sturdy { get => sturdy; }
        #endregion

        #region constructor
        // ************* Constructor ************* //

        public Enemy(EnemyType enemyType, int maxHP, bool melee, float fireRate, float attackDamage, float moveSpeed, float projectileSpeed, Rectangle drawPos, Vector2 velocity, string weaponName, int lungeTime, bool sturdy) : base(maxHP, melee, fireRate, moveSpeed, attackDamage, projectileSpeed, drawPos, drawPos, new Vector2(drawPos.X, drawPos.Y), velocity, true, DisplayManager.EnemyTextureMap[enemyType])
        {
            this.enemyType = enemyType;
            this.sturdy = sturdy;
            pathfinder = new Pathfinder();
            if (melee)
            {
                this.lungeTime = lungeTime;
            }
            else
            {
                if (weaponName != null && weaponName != "null")
                {
                    this.weapon = new Weapon(@"enemy\" + weaponName, drawPos, false, false);
                }
            }
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        /// <summary>
        /// Updates all necessary values of the enemy class
        /// </summary>
        public override void Update()
        {
            if (IsActive)
            {
                if (this.currentHP <= 0)
                {
                    this.IsActive = false;
                    this.position = new Vector2(0f, 0f);
                    this.drawPos.Width = 0;
                    this.drawPos.Height = 0;
                    return;
                }

                Entity target = EntityManager.Player;

                if (hitstun == 0)
                {
                    this.Attack(target);
                    if (!this.hitbox.Intersects(target.Hitbox))
                    {
                        this.Move(target);
                    }
                    base.Update();
                    hitbox = drawPos;
                }
                else
                {
                    hitstun--;
                }

                if (!melee && this.Weapon != null)
                {
                    weapon.Update();
                    weapon.Position = this.Position; //TEMP
                }
            }
        }

        /// <summary>
        /// Draws the enemy at its position on the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.isActive && this.hitstun == 0)
            {
                base.Draw(spriteBatch);
            }
            //Hitstun
            else if(this.isActive)
            {
                if (texture != null)
                    spriteBatch.Draw(texture.GetNextTexture(), drawPos, Color.Red);
            }
        }

        /// <summary>
        /// Use the enemy's attck if it is prepared
        /// </summary>
        /// <param name="target">The entity the enemy is attacking</param>
        private void Attack(Entity target)
        {
            if (LineOfSight(target))
            {
                if (melee)
                {
                    if (timeSinceLunge == lungeTime)
                        moveSpeed /= 1.5f;
                    else if (timeSinceLunge == 0)
                        moveSpeed *= 1.5f;
                    else if (timeSinceLunge >= 2 * lungeTime)
                        timeSinceLunge = -1;
                    timeSinceLunge++;
                }
                else if (weapon != null && weapon.ReadyToFire(fireRate))
                {
                    Vector2 fireDirection = Vector2.Normalize(new Vector2(EntityManager.Player.Position.X + EntityManager.Player.DrawPos.Width / 2 - this.Position.X - this.Hitbox.Width / 2, EntityManager.Player.Position.Y + EntityManager.Player.DrawPos.Height / 2 - this.Position.Y - this.Hitbox.Height / 2));
                    Weapon.Use(fireDirection);
                }
            }
        }

		/// <summary>
		/// Handles enemy movement and actions
		/// </summary>
		protected void Move(Entity target)
		{
            Vector2 selfPos = new Vector2(this.Hitbox.X + this.Hitbox.Width / 2, this.Hitbox.Y + this.Hitbox.Height / 2);
            Vector2 targetPos = new Vector2(target.Hitbox.X + target.Hitbox.Width / 2, target.Hitbox.Y + target.Hitbox.Height / 2);
            Vector2 direction;

            // If you can walk in a straight line to the target, don't bother pathfinding
            if (LineOfPathing(target))
            {
                direction = targetPos - selfPos;
                direction.Normalize();
                velocity = direction * moveSpeed;
                position += velocity;
            }

            //Otherwise pathfinding is necessary
            else
            {
                // Only run the pathfinding algo again if the player has moved
                if (currentPath == null || currentPath.Count <= 0 || currentPath[0] != FloorManager.Instance.GetTileAt(targetPos))
                {
                    currentPath = pathfinder.GetPathToTarget(position, targetPos);
                }

                // If I've reached the next tile I'm looking for, pop it off the list of tiles to walk to
                if (FloorManager.Instance.GetTileAt(selfPos) == currentPath[currentPath.Count - 1])
                {
                    currentPath.RemoveAt(currentPath.Count - 1);
                }

                //Start walking towards a tile that will bring me closer to my target
                Point targetPoint = currentPath[currentPath.Count - 1].Location;
                direction = new Vector2((targetPoint.X + .5f) * FloorManager.TileWidth - selfPos.X, (targetPoint.Y + .5f) * FloorManager.TileHeight - selfPos.Y);
                direction.Normalize();

                velocity = direction * moveSpeed;
                position += velocity;

                this.fireDelay = FireRate;
            }
        }
        #endregion
    }
}
