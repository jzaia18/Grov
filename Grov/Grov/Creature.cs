using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Authors
 * Jack Hoffman
 * Rachel Wong
 * Jake Zaia
 */ 

namespace Grov
{
    abstract class Creature: Entity
    {
        // ************* Fields ************* //

        protected float maxHP;
        protected float currentHP;
        protected bool melee;
        protected float fireRate;
        protected float fireDelay;
        protected float moveSpeed;
        protected float attackDamage;
        protected float projectileSpeed;

		// ************* Properties ************* //

		public float MaxHP { get => maxHP; set => maxHP = value; }
        public float CurrentHP { get => currentHP; set => currentHP = value; }
        public bool Melee { get => melee; set => melee = value; }
        public float FireRate { get => fireRate; set => fireRate = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float AttackDamage { get => attackDamage; set => attackDamage = value; }
        public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }

        // ************* Constructor ************* //

        public Creature(float maxHP, bool melee, float fireRate, float moveSpeed, float attackDamage, float projectileSpeed, Rectangle drawPos, Rectangle hitbox, Vector2 position, Vector2 velocity, Random rng, bool isActive, Texture2D texture) : base(drawPos, hitbox, position, velocity, rng, isActive, texture)
        {
            this.maxHP = maxHP;
            this.currentHP = maxHP;
            this.melee = melee;
            this.fireRate = fireRate;
            this.fireDelay = fireRate;
            this.moveSpeed = moveSpeed;
            this.attackDamage = attackDamage;
            this.projectileSpeed = projectileSpeed;
        }
        
        // ************* Methods ************* //

        public override void Update()
        {
            this.Move();
            base.Update();
        }

        // ************* Helper Methods ************* //

        protected virtual void Move(){ }
    }
}
