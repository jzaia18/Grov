using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Authors
 * Jack Hoffman
 */ 

namespace Grov
{
    abstract class Creature: Entity
    {
        // ************* Fields ************* //

        private int maxHP;
        private int currentHP;
        private bool melee;
        private double fireRate;
        private int attackDamage;

        // ************* Properties ************* //

        public int MaxHP { get => maxHP; set => maxHP = value; }
        public int CurrentHP { get => currentHP; set => currentHP = value; }
        public bool Melee { get => melee; set => melee = value; }
        public double FireRate { get => fireRate; set => fireRate = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }

        // ************* Methods ************* //

        public override void Update()
        {
            this.Move();
            base.Update();
        }

        // ************* Helper Methods ************* //

        public void Move(){ }
    }
}
