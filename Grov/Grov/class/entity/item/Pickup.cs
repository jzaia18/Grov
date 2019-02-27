using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public Pickup(PickupType pickupType, Rectangle drawPos, Texture2D texture) : base(drawPos, drawPos, new Vector2(drawPos.X, drawPos.Y), new Vector2(0,0), true, texture)
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
