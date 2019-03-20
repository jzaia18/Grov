using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


// Authors: Rachel Wong


namespace Grov
{
    class Button
    {
        #region fields
        private Texture2D noHover;
        private Texture2D hover;
        private Rectangle rect;
        #endregion

        #region properties
        public Texture2D NoHover { get => noHover; set => noHover = value; }
        public Texture2D Hover { get => hover; set => hover = value; }
        public Rectangle Rect { get => rect; }
        #endregion

        #region constructor
        //Constructor
        public Button(Rectangle rect)
        {
            this.rect = rect;
        }
        #endregion

        #region methods
        //Draws button image depending on whether mouse is hovering over it or not
        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState ms = Mouse.GetState();

            if (rect.Contains(ms.X, ms.Y - 10))
            {
                spriteBatch.Draw(hover, rect, Color.White);
            }
            else
            {
                spriteBatch.Draw(noHover, rect, Color.White);
            }
        }
        #endregion
    }
}
