using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Grov
{
    class HUD
    {
        // ************* Fields ************* //

        private Player player;
        private ContentManager contentManager;
        Texture2D healthBarFull;
        Texture2D healthBarEmpty;
        Texture2D manaBarFull;
        Texture2D manaBarEmpty;

        // ************* Constructors ************* //

        public HUD()
        {
            healthBarFull = DisplayManager.ContentManager.Load<Texture2D>("HealthBarFullSprite");
            healthBarEmpty = DisplayManager.ContentManager.Load<Texture2D>("HealthBarEmptySprite");
            //manaBarFull = contentManager.Load<Texture2D>("ManaBarFullSprite");
            //manaBarEmpty = contentManager.Load<Texture2D>("ManaBarEmptySprite");
        }

        // ************* Methods ************* //

        public void Initialize()
        {
        }

        public void Draw(SpriteBatch sb)
        {
            DrawHealth(sb);
            //DrawMana(sb);
        }

        // ************* Helper Methods ************* //

        private void DrawHealth(SpriteBatch sb)
        {
            sb.Draw(healthBarEmpty, new Rectangle(10, 10, 250, 150), Color.White);
            sb.Draw(healthBarFull, new Rectangle(10, 10, 250, 150), new Rectangle(0, 0,(int)(healthBarFull.Width * player.CurrentHP / player.MaxHP), healthBarFull.Height), Color.White);
        }

        private void DrawMana(SpriteBatch sb)
        {
            sb.Draw(manaBarEmpty, new Rectangle(10, 10 + 10 + manaBarEmpty.Height, manaBarEmpty.Width, manaBarEmpty.Height), Color.White);
            sb.Draw(manaBarFull, new Rectangle(10, 10, manaBarFull.Width, manaBarFull.Height), new Rectangle(10, 10 + 10 + healthBarFull.Height, (int)(manaBarFull.Width * player.CurrentHP / player.MaxHP), manaBarFull.Height), Color.White);
        }
    }
}
