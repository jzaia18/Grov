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

		public Enemy(EnemyType enemyType, int maxHP, bool melee, double fireRate, double attackDamage, float moveSpeed)
        {
            this.enemyType = enemyType;
            this.maxHP = maxHP;
            this.fireRate = fireRate;
            this.attackDamage = attackDamage;
			this.moveSpeed = moveSpeed;
			drawPos = new Rectangle(300, 300, 150, 150);
			position = new Vector2 (300, 300);
			velocity = new Vector2(0f, 0f);
			rng = new Random();
        }

        public void Update(Entity target)
        {
			base.Update();
        }

        private void Attack(Entity target)
        {

        }

		/// <summary>
		/// Handles enemy movement and actions
		/// </summary>
		protected override void Move()
		{
			Vector2 direction = new Vector2(0f, 0f);

			direction += new Vector2(rng.Next(-1, 2), rng.Next(-1, 2));
			direction.Normalize();

			if (float.IsNaN(direction.X))
			{
				direction.X = 0;
			}
			if (float.IsNaN(direction.Y))
			{
				direction.Y = 0;
			}

			velocity = moveSpeed * direction;
			position += velocity;

			velocity = new Vector2(0f, 0f);
		}
	}
}
