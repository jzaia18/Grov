using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Authors: Jake Zaia

namespace Grov
{
    enum ShotType
    {
        Normal,   // symmetrical cone in one direction
        Radial,   // laterally from all sides of the character
        Spread,   // in a cone, but more randomly spread out
        Clump     // a straight line of packets/clumps of projectiles
    }

    class Weapon : Pickup
    {
        // ************* Fields ************* //

        private string name;
        private double fireRate;
        private double atkDamage;
        private int manaCost;
        private int numProjectiles;


        // ************* Properties ************* //
        
        public string Name { get => name; }
        public double FireRate { get => fireRate; }
        public double AttackDamage { get => atkDamage; }
        public int ManaCost { get => manaCost; }
        public int NumProjectiles { get => numProjectiles; }


        // ************* Constructor ************* //

        public Weapon(string name) : base(PickupType.Weapon)
        {
            this.name = name;

            //TODO: use name to read in other data from a file
        }


        // ************* Methods ************* //

        public override void Update()
        {
            //TODO: restrict usage based on whether or not it is active
            // Active => item is on ground
            // Inactive => item is usable and held by player

            throw new NotImplementedException();
            base.Update();
        }

        public void Use()
        {
            throw new NotImplementedException();
        }
    }
}
