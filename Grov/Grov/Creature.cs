using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Authors
 * Jack Hoffman
 * Rachel Wong
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
        protected float attackDamage;
		protected float moveSpeed;

		// ************* Properties ************* //

		public float MaxHP { get => maxHP; set => maxHP = value; }
        public float CurrentHP { get => currentHP; set => currentHP = value; }
        public bool Melee { get => melee; set => melee = value; }
        public float FireRate { get => fireRate; set => fireRate = value; }
        public float AttackDamage { get => attackDamage; set => attackDamage = value; }

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
