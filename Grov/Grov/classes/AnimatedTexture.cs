using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Grov
{
    class AnimatedTexture : ICloneable
    {
        #region fields
        // ************* Fields ************* //

        private List<Texture2D> textures;
        private int currframe;

        #endregion

        #region properties
        // ************* Properties ************* //

        public int NumFrames { get => textures.Count; }
        private int CurrFrame { get => currframe; set => currframe = (currframe + value) % textures.Count; }

        #endregion

        #region constructors
        // ************* Constructors ************* //

        public AnimatedTexture()
        {
            textures = new List<Texture2D>();
        }

        public AnimatedTexture(Texture2D firstTexture) : this()
        {
            textures.Add(firstTexture);
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        public object Clone()
        {
            AnimatedTexture newTexture = new AnimatedTexture();
            foreach (Texture2D texture in textures)
            {
                newTexture.AddTexture(texture);
            }
            return newTexture;
        }

        public Texture2D GetNextTexture()
        {
            CurrFrame++;
            return textures[currframe];
        }

        public void AddTexture(Texture2D texture)
        {
            textures.Add(texture);
        }

        #endregion
    }
}
