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


		// ************* Constructor ************* //

		public Enemy(EnemyType enemyType, int maxHP, bool melee, float fireRate, float attackDamage, float moveSpeed, float projectileSpeed, Rectangle drawPos, Vector2 velocity, Random rng, Texture2D texture) : base(maxHP, melee, fireRate, moveSpeed, attackDamage, projectileSpeed, drawPos, drawPos, new Vector2(drawPos.X, drawPos.Y), velocity, rng, true, texture)
        {
            this.enemyType = enemyType;
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
            hitbox = drawPos;
        }

        private void Attack(Entity target)
        {

        }

		/// <summary>
		/// Handles enemy movement and actions
		/// </summary>
		protected void Move(Entity target)
		{
            Vector2 direction = target.Position - this.position;

            direction.Normalize();

            velocity = direction * moveSpeed;

            position += velocity;

            this.fireDelay = FireRate;
		}
	}
}
