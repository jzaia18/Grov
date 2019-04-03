using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Authors: Jake Zaia, Rachel Wong

namespace Grov
{
    enum PickupType
    {
        Weapon = 0,
        Heart = 1,
        //Bomb,
        //Key,
        //Powerup
    }

    class Pickup : Entity
    {
        #region fields
        // ************* Fields ************* //

        private PickupType pickupType;

        #endregion

        #region properties
        // ************* Properties ************* //

        public PickupType PickupType { get => pickupType; set => pickupType = value; }

        #endregion

        #region constructor
        // ************* Constructors ************* //

        public Pickup(PickupType pickupType, Rectangle drawPos, AnimatedTexture texture) : base(drawPos, drawPos, new Vector2(drawPos.X, drawPos.Y), new Vector2(0,0), true, texture)
        {
            this.pickupType = pickupType;
        }
        #endregion


        #region methods
        // ************* Methods ************* //

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.isActive && (this.texture != null))
            {
                base.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
