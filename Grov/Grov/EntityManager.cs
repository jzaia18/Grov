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
    class EntityManager
    {
        // ************* Fields ************* //
        private Player player;
        private List<Enemy> enemies;
        private List<Projectile> hostileProjectiles;
        private List<Projectile> friendlyProjectiles;
        private Dictionary<EnemyType, Texture2D> textureMap;


        // ************* Properties ************* //

        public Player Player { get => player; set => player = value; }


        // ************* Constructor ************* //

        public EntityManager() {
            enemies = new List<Enemy>();
            hostileProjectiles = new List<Projectile>();
            friendlyProjectiles = new List<Projectile>();
            textureMap = new Dictionary<EnemyType, Texture2D>();
        }

        // ************* Methods ************* //

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
