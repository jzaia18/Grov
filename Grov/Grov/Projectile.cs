using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

// Authors: Jake Zaia

namespace Grov
{
    class Projectile : Entity
    {
        // ************* Fields ************* //

        private float lifespan;
        private bool isFromPlayer;
        private bool noclip;

        // ************* Properties ************* //

        public float Lifespan { get => lifespan; set => lifespan = value; }
        public bool IsFromPlayer { get => isFromPlayer; set => isFromPlayer = value; }
        public bool Noclip { get => noclip; set => noclip = value; }


        // ************* Constructor ************* //

        public Projectile(float lifespan, Vector2 position, Vector2 velocity, bool isFromPlayer, bool noclip)
        {
            this.lifespan = lifespan;
            this.position = position;
            this.velocity = velocity;
            this.isFromPlayer = isFromPlayer;
            this.noclip = noclip;
        }

        // ************* Methods ************* //

        public override void Update()
        {
            this.position += velocity;
            base.Update();
        }
    }
}
