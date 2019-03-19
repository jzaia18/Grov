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
        private float damage;
        private ShotType type;

        // ************* Properties ************* //

        public float Lifespan { get => lifespan; set => lifespan = value; }
        public bool IsFromPlayer { get => isFromPlayer; set => isFromPlayer = value; }
        public bool Noclip { get => noclip; set => noclip = value; }
        public float Damage { get => damage; }
        public ShotType Type { get => type; }

        // ************* Constructor ************* //

        public Projectile(float damage, float lifespan, bool isFromPlayer, bool noclip, Rectangle drawPos, Vector2 velocity, Texture2D texture, ShotType type) : base(drawPos, drawPos,new Vector2(drawPos.X, drawPos.Y), velocity, true, texture)
        {
            this.damage = damage;
            this.lifespan = lifespan;
            this.isFromPlayer = isFromPlayer;
            this.noclip = noclip;
            this.type = type;
        }

        // ************* Methods ************* //

        public override void Update()
        {
            if (this.isActive)
            {
                this.lifespan -= 1;
                this.position += velocity;
                this.drawPos = new Rectangle((int)(this.position.X), (int)(this.position.Y), this.drawPos.Width, this.drawPos.Height);
                this.hitbox = this.drawPos;
                if(this.lifespan == 0)
                {
                    this.isActive = false;
                }
            }
            else
            {
                if (this.drawPos.Width > 0)
                {
                    this.IsActive = false;
                    this.position = new Vector2(0f, 0f);
                    this.drawPos.Width = 0;
                    this.drawPos.Height = 0;
                }
                return;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.isActive)
            {
                if (type != ShotType.Bubble)
                    spriteBatch.Draw(this.texture, new Rectangle(this.drawPos.X + this.drawPos.Width / 2, this.drawPos.Y + this.drawPos.Height / 2, this.drawPos.Width, this.drawPos.Height), null, Color.White, (float)Math.Atan2(this.velocity.Y, this.velocity.X), new Vector2(this.drawPos.Width, this.drawPos.Height), SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(this.texture, new Rectangle(this.drawPos.X, this.drawPos.Y, this.drawPos.Width, this.drawPos.Height), null, Color.White);
            }
        }
    }
}
