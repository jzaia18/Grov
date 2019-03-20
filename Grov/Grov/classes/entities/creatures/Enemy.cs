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
		Test = 0
    }

    class Enemy : Creature
    {
        // ************* Fields ************* //

        private EnemyType enemyType;
        private int hitstun;

        // ************* Properties ************* //
        public int Hitstun { get => hitstun; set => hitstun = value; }

        // ************* Constructor ************* //

        public Enemy(EnemyType enemyType, int maxHP, bool melee, float fireRate, float attackDamage, float moveSpeed, float projectileSpeed, Rectangle drawPos, Vector2 velocity) : base(maxHP, melee, fireRate, moveSpeed, attackDamage, projectileSpeed, drawPos, drawPos, new Vector2(drawPos.X, drawPos.Y), velocity, true, DisplayManager.EnemyTextureMap[enemyType])
        {
            this.enemyType = enemyType;
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
