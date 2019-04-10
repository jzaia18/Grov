using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Authors: Jake Zaia, Rachel Wong, Duncan Mott

namespace Grov
{
    enum EnemyType
    {
		Test = 0,
        Shooty = 1
    }

    class Enemy : Creature
    {
        // ************* Fields ************* //

        private EnemyType enemyType;
        private int hitstun;
        private int lungeTime;
        private int timeSinceLunge;

        // ************* Properties ************* //
        public int Hitstun { get => hitstun; set => hitstun = value; }

        // ************* Constructor ************* //

        public Enemy(EnemyType enemyType, int maxHP, bool melee, float fireRate, float attackDamage, float moveSpeed, float projectileSpeed, Rectangle drawPos, Vector2 velocity, string weaponName, int lungeTime) : base(maxHP, melee, fireRate, moveSpeed, attackDamage, projectileSpeed, drawPos, drawPos, new Vector2(drawPos.X, drawPos.Y), velocity, true, DisplayManager.EnemyTextureMap[enemyType])
        {
            this.enemyType = enemyType;
            if (melee)
            {
                this.lungeTime = lungeTime;
                Console.WriteLine(lungeTime);
            }
            else
            {
                if (weaponName != null && weaponName != "null")
                {
                    this.Weapon = new Weapon(@"enemy\" + weaponName, drawPos, false, false);
                }
            }
        }

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
                    weapon.Position = this.Position; //TEMP
                }
            }
        }

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

        private void Attack(Entity target)
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
            else
            {
                Vector2 fireDirection = Vector2.Normalize(new Vector2(EntityManager.Player.Position.X + EntityManager.Player.DrawPos.Width / 2 - this.Position.X - this.Hitbox.Width / 2, EntityManager.Player.Position.Y + EntityManager.Player.DrawPos.Height / 2 - this.Position.Y - this.Hitbox.Height / 2));
                Weapon.Use(fireDirection);
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
	}
}
