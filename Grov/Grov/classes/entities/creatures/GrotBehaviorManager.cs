using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Grov
{
    class GrotBehaviorManager : Enemy
    {
        enum BehaviorMode
        {
            SpinAttack,
            PulseAttack,
            Summon,
            Idle,
            Taunt
        }
 
        private BehaviorMode currentBehavior;
        private int currentFrame;
        private int sign;


        public GrotBehaviorManager(EnemyType enemyType, int maxHP, bool melee, float fireRate, float attackDamage, float moveSpeed, float projectileSpeed, Rectangle drawPos, Vector2 velocity, string weaponName, int lungeTime, bool sturdy) : base(enemyType, maxHP, melee, fireRate, attackDamage, moveSpeed, projectileSpeed, drawPos, velocity,"Grot", 0, true)
        {
            currentBehavior = BehaviorMode.Taunt;
            currentFrame = 0;
        }

        public override void Update()
        {
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
                this.currentBehavior = (BehaviorMode)GameManager.RNG.Next(0, 3); // Generates a new attack
                currentFrame = 0;
            }
        }

        private void Idle()
        {
            if(currentFrame >= 120)
            {
                this.currentBehavior = (BehaviorMode)GameManager.RNG.Next(0, 3); // Generates a new attack
                currentFrame = 0;
            }
        }

        private void SpinAttack()
        {
            float radians = 0f;

            if (currentFrame >= 90)
            {
                if(currentFrame == 90)
                {
                    sign = GameManager.RNG.Next(0, 2) * 2 - 1; // Generates a random direction
                }

                radians = (currentFrame - 90) / 75f * sign;
            }

            this.weapon = new Weapon("Grot", this.drawPos, true, false);
            this.Attack(new Vector2(this.position.X + this.drawPos.Width / 2 + (float)Math.Cos(radians), this.position.Y + this.drawPos.Height / 2  + (float)Math.Sin(radians)));

            if(currentFrame >= 420)
            {
                currentFrame = 0;
                this.currentBehavior = BehaviorMode.Taunt;
            }
        }

        private void PulseAttack()
        {
            this.currentBehavior = BehaviorMode.SpinAttack;
        }

        private void Summon()
        {
            this.currentBehavior = BehaviorMode.SpinAttack;
        }
    }
}
