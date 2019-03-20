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
        private bool isHighlighted;
        #endregion

        #region properties
        public Texture2D NoHover { get => noHover; set => noHover = value; }
        public Texture2D Hover { get => hover; set => hover = value; }
        public Rectangle Rect { get => rect; }
        public bool IsHighlighted { get => isHighlighted; }
        #endregion

        #region constructor
        //Constructor
        public Button(Rectangle rect)
        {
            this.rect = rect;
            isHighlighted = false;
        }
        #endregion

        #region methods
        //Draws button image depending on whether mouse is hovering over it or not
        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState ms = Mouse.GetState();

            if (rect.Contains(ms.X, ms.Y - 10))
            {
                isHighlighted = true;
                spriteBatch.Draw(hover, rect, Color.White);
            }
            else
            {
                isHighlighted = false;
                spriteBatch.Draw(noHover, rect, Color.White);
            }
        }
        #endregion
    }
}
