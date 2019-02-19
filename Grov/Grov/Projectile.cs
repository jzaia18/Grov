using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Projectile(double lifespan, Entity origin, bool noclip)
        {
            this.lifespan = lifespan;
            this.origin = origin;
            this.noclip = noclip;
        }

        // ************* Methods ************* //
        public override void Update()
        {
            throw new NotImplementedException();
            base.Update();
        }
    }
}
