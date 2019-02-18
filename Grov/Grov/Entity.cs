using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * Authors
 * Jack Hoffman
 */

namespace Grov
{
    class Entity
    {
        // ************* Fields ************* //

        private Rectangle drawPos;
        private Rectangle hitbox;
        private Vector2 position;
        private Vector2 velocity;
        private Random rng;
        private bool isActive;
        protected Texture2D texture;
        

        // ************* Properties ************* //

        public Rectangle DrawPos { get => drawPos; set => drawPos = value; }
        public Rectangle Hitbox { get => hitbox; set => hitbox = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public Random Rng { get => rng; set => rng = value; }
        public bool IsActive { get => isActive; set => IsActive = value; }

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
