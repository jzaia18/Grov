using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Authors: Jake Zaia

namespace Grov
{
    enum PickupType
    {
        Weapon,
        Heart,
        Mana,
        Bomb,
        Key,
        Powerup
    }

    class Pickup : Entity
    {
        // ************* Fields ************* //

        private PickupType pickupType;
        
        
        // ************* Properties ************* //

        public PickupType PickupType { get => PickupType; set => pickupType = value; }


        // ************* Constructors ************* //

        public Pickup(PickupType pickupType)
        {
            this.pickupType = pickupType;
        }


        // ************* Methods ************* //

        public override void Update()
        {
            base.Update();
            throw new NotImplementedException();
        }
    }
}
