using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        private ShotType shotType;


        // ************* Properties ************* //
        
        public string Name { get => name; }
        public double FireRate { get => fireRate; }
        public double AttackDamage { get => atkDamage; }
        public int ManaCost { get => manaCost; }
        public int NumProjectiles { get => numProjectiles; }
        public ShotType ShotType { get => shotType; }

        // ************* Constructor ************* //

        public Weapon(string filename) : base(PickupType.Weapon)
        {
            readFromFile(@"weapons\" + filename + ".txt");
        }


        // ************* Methods ************* //

        private void readFromFile(string filename)
        {
            StreamReader reader = null;
            try {
                reader = new StreamReader(filename);

                name = reader.ReadLine();
                fireRate = Double.Parse(reader.ReadLine());
                atkDamage = Double.Parse(reader.ReadLine());
                manaCost = Int32.Parse(reader.ReadLine());
                numProjectiles = Int32.Parse(reader.ReadLine());
                shotType = (ShotType) Enum.Parse(typeof(ShotType), reader.ReadLine(), true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

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
