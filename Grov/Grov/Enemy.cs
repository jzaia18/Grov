using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Authors: Jake Zaia, Rachel Wong

namespace Grov
{
    enum EnemyType
    {
		TestEnemy
    }

    class Enemy : Creature
    {
        // ************* Fields ************* //

        private EnemyType enemyType;
        private float fireDelay;


		// ************* Constructor ************* //

		public Enemy(EnemyType enemyType, int maxHP, bool melee, float fireRate, float attackDamage, float moveSpeed)
        {
            this.enemyType = enemyType;
            this.maxHP = maxHP;
            this.fireRate = fireRate;
            this.fireDelay = fireRate;
            this.attackDamage = attackDamage;
			this.moveSpeed = moveSpeed;
			drawPos = new Rectangle(300, 300, 150, 150);
			position = new Vector2 (300, 300);
			velocity = new Vector2(0f, 0f);
			rng = new Random();
        }

        public void Update(Entity target)
        {
            if (this.hitbox.Intersects(target.Hitbox))
            {
                this.Attack(target);
            }
            else
            {
                this.Move(target);
            }
            base.Update();
        }

        private void Attack(Entity target)
        {
            if(this.fireDelay > 0 && this.hitbox.Intersects(target.Hitbox))
            {
                ((Player)target).CurrentHP -= this.AttackDamage;
            }
        }

		/// <summary>
		/// Handles enemy movement and actions
		/// </summary>
		protected void Move(Entity target)
		{
            if (!target.DrawPos.Intersects(this.drawPos))
            {
                Vector2 direction = target.Position - this.position;

                direction.Normalize();

                velocity = direction * moveSpeed;

                position += velocity;
            }
		}
	}
}
