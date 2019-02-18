using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Grov
{
    class Entity
    {
        // ************* Fields ************* //
        protected Texture2D texture;

        // ************* Properties ************* //

        public Rectangle DrawPos { get; set; }
        public Rectangle Hitbox { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Random Rng { get; set; }
        public bool IsActive { get; set; }

        // ************* Constructors ************* //

        public Entity()
        {
            // TODO: write code
        }

        // ************* Methods ************* //

        /// <summary>
        /// Updates draw position to equal position
        /// </summary>
        public virtual void Update()
        {
            DrawPos = new Rectangle((int)(Position.X + .5f), (int)(Position.Y + .5f), DrawPos.Width, DrawPos.Height);
        }

        /// <summary>
        /// Draws the object to its corresponding draw position
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DrawPos, Color.White);
        }

        /// <summary>
        /// Checks to see if an entity's hitbox collides with another
        /// </summary>
        /// <param name="entity">The entity to check collision with</param>
        /// <returns>True if collided, False if not</returns>
        public virtual bool CollidesWith(Entity entity)
        {
            return Hitbox.Intersects(entity.Hitbox);
        }
    }
}
