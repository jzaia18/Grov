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
    class DisplayManager
    {

        // ************* Fields ************* //
        
        private EntityManager em;
        private FloorManager fm;
        private HUD hud;
        private Player player;

        // ************* Constructor ************* //
        public DisplayManager(EntityManager em, FloorManager fm, HUD hud, Player player)
        {
            this.em = em;
            this.fm = fm;
            this.hud = hud;
            this.player = player;
        }

        // ************* Methods ************* //

        public void Draw(SpriteBatch spriteBatch)
        {
            em.Draw(spriteBatch);
            fm.Draw(spriteBatch);
            //hud.Draw(spriteBatch, player);
        }

    }
}
