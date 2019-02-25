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

        public Projectile(float lifespan, bool isFromPlayer, bool noclip, Rectangle drawPos, Vector2 velocity, Random rng, Texture2D texture) : base(drawPos, drawPos, new Vector2(drawPos.X, drawPos.Y), velocity, rng, true, texture)
        {
            this.lifespan = lifespan;
            this.isFromPlayer = isFromPlayer;
            this.noclip = noclip;
        }

        // ************* Methods ************* //

        public override void Update()
        {
            this.position += velocity;
            this.drawPos = new Rectangle((int)(this.position.X), (int)(this.position.Y), this.drawPos.Width, this.drawPos.Height);
            this.hitbox = this.drawPos;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.hitbox, Color.White);
            spriteBatch.Draw(this.texture, new Rectangle(this.drawPos.X + this.drawPos.Width / 2, this.drawPos.Y + this.drawPos.Height / 2, this.drawPos.Width, this.drawPos.Height), null, Color.White, (float)Math.Atan2(this.velocity.Y, this.velocity.X), new Vector2(this.drawPos.Width, this.drawPos.Height), SpriteEffects.None, 0f);
        }
    }
}
