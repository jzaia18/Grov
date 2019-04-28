using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Grov
{
    class Grot : Enemy
    {
        enum BehaviorMode
        {
            Taunt = 0,
            SpinAttack = 1,
            PulseAttack = 2,
            Summon = 3,
            Idle = 4
        }
 
        private BehaviorMode currentBehavior;
        private int currentFrame;
        private int sign;


        public Grot(EnemyType enemyType, int maxHP, bool melee, float fireRate, float attackDamage, float moveSpeed, float projectileSpeed, Rectangle drawPos, Vector2 velocity, string weaponName, int lungeTime, bool sturdy) : base(enemyType, maxHP, melee, fireRate, attackDamage, moveSpeed, projectileSpeed, drawPos, velocity,"Grot", 0, true)
        {
            currentBehavior = BehaviorMode.Taunt;
            currentFrame = 0;
            sign = 1;
        }

        public override void Update()
        {
            if(this.currentHP <= 0)
            {
                this.isActive = false;
            }
            currentFrame++;
            switch (currentBehavior)
            {
                case BehaviorMode.Taunt:
                    this.Taunt();
                    break;
                case BehaviorMode.Idle:
                    this.Idle();
                    break;
                case BehaviorMode.SpinAttack:
                    this.SpinAttack();
                    break;
                case BehaviorMode.PulseAttack:
                    this.PulseAttack();
                    break;
                case BehaviorMode.Summon:
                    this.Summon();
                    break;
            }
        }

        private void Taunt()
        {
            if(currentFrame >= 60)
            {
                this.currentBehavior = (BehaviorMode)GameManager.RNG.Next(1, 5); // Generates a new attack
                currentFrame = 0;
            }
        }

        private void Idle()
        {
            if(currentFrame >= 120)
            {
                this.currentBehavior = (BehaviorMode)GameManager.RNG.Next(1, 5); // Generates a new attack
                currentFrame = 0;
            }
        }

        private void SpinAttack()
        {
            double radians = 0f;

            if(currentFrame % 90 == 0)
            {
                sign = GameManager.RNG.Next(0, 2) * 2 - 1; // Generates a random direction
            }

            radians = ((currentFrame / 2) % 360) * (Math.PI/180) * sign;

            this.weapon = new Weapon(@"enemy\Grot", this.drawPos, true, false);
            weapon.Use(Vector2.Normalize(new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians))));

            if(currentFrame >= 420)
            {
                currentFrame = 0;
                this.currentBehavior = BehaviorMode.Idle;
            }
        }

        private void PulseAttack()
        {
            this.weapon = new Weapon(@"enemy\Forest Giant", this.drawPos, true, false);

            if (currentFrame % 40 == 0)
            {
                weapon.Use(new Vector2(1, 1));
            }

            if (currentFrame >= 150)
            {
                this.currentBehavior = (BehaviorMode)GameManager.RNG.Next(1, 5); // Generates a new attack
                currentFrame = 0;
            }
        }

        private void Summon()
        {
            if (currentFrame >= 0)
            {
                this.currentBehavior = (BehaviorMode)GameManager.RNG.Next(1, 5); // Generates a new attack
                currentFrame = 0;
            }
        }
    }
}
