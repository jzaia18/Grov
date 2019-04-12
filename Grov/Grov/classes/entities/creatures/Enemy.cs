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
        #endregion

        #region properties
        // ************* Properties ************* //

        public int Hitstun { get => hitstun; set => hitstun = value; }
        public bool Sturdy { get => sturdy; }
        #endregion

        #region constrcutor
        // ************* Constructor ************* //

        public Enemy(EnemyType enemyType, int maxHP, bool melee, float fireRate, float attackDamage, float moveSpeed, float projectileSpeed, Rectangle drawPos, Vector2 velocity, string weaponName, int lungeTime, bool sturdy) : base(maxHP, melee, fireRate, moveSpeed, attackDamage, projectileSpeed, drawPos, drawPos, new Vector2(drawPos.X, drawPos.Y), velocity, true, DisplayManager.EnemyTextureMap[enemyType])
        {
            this.enemyType = enemyType;
            this.sturdy = sturdy;
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
        /// Returns whether or not this enemy has line of sight to the target
        /// </summary>
        /// <param name="target">The entity the enemy is checking for LoS</param>
        /// <returns>True if there is LoS</returns>
        public bool LineOfSight(Entity target)
        {

            //TODO TODO TODO TODO TODO

            return true;
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
            Vector2 direction = (target.Position + new Vector2(target.DrawPos.Width/2, target.DrawPos.Height/2)) - (this.position + new Vector2(drawPos.Width/2, drawPos.Height/2));

            direction.Normalize();

            velocity = direction * moveSpeed;

            position += velocity;

            this.fireDelay = FireRate;
		}
        #endregion
    }
}
