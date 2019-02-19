using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 * Authors
 * Jack Hoffman
 * Jake Zaia
 */
 
namespace Grov
{
    class Player: Creature
    {
        // ************* Fields ************* //
        private int maxMP;
        private int currMP;
        //private Weapon weapon;
        private int keys;
        private int bombs;


        // ************* Properties ************* //

        public int CurrMP
        {
            get => currMP;
            set
            {
                if (value >= 0)
                {
                    currMP = value;
                }
                else
                {
                    currMP = 0;
                }
            }
        }

        public int MaxMP { get => maxMP; set => maxMP = value; }
        public int Keys { get => keys; set => keys = value; }
        public int Bombs { get => bombs; set => bombs = value; }


        // ************* Constructor ************* //

        public Player(int maxMP)
        {
            this.maxMP = maxMP;
            this.currMP = maxMP;
            keys = 0;
            bombs = 0;
        }


        // ************* Methods ************* //

        private void HandleInput()
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

    }
}
