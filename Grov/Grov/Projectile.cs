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

        private double lifespan;
        private Entity origin;
        private bool noclip;

        // ************* Properties ************* //

        public double Lifespan { get => lifespan; set => lifespan = value; }
        public Entity Origin { get => origin; set => origin = value; }
        public bool Noclip { get => noclip; set => noclip = value; }


        // ************* Constructor ************* //
        public Projectile(double lifespan, Vector2 velocity, Entity origin, bool noclip)
        {
            this.position = new Vector2(origin.Position.X + origin.DrawPos.Width / 2, origin.Position.Y + origin.DrawPos.Height / 2);
            this.lifespan = lifespan;
            this.velocity = velocity;
            this.origin = origin;
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
