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
 * Rachel Wong
 */
 
namespace Grov
{
    class Player: Creature
    {
        // ************* Fields ************* //

        private int maxMP;
        private int currMP;
        private Weapon weapon;
        private int keys;
        private int bombs;
        private GamePadState gamePadPreviousState;
        private KeyboardState keyboardPreviousState;
        private Vector2 aimDirection;
        private bool isInputKeyboard;
        private float projectileVelocity;

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
        public float ProjectileVelocity { get => projectileVelocity; set => projectileVelocity = value; }


        // ************* Constructor ************* //

        public Player(int maxMP)
        {
            this.maxMP = maxMP;
            this.currMP = maxMP;
			moveSpeed = 5f;
            keys = 0;
            bombs = 0;
            keyboardPreviousState = Keyboard.GetState();
            gamePadPreviousState = GamePad.GetState(0);
            isInputKeyboard = true;
            projectileVelocity = 10f;
        }


        // ************* Methods ************* //

        private void HandleInput()
        {
            base.Update();
            this.Aim();
        }

        /// <summary>
        /// Handles all movement-based calculations and inputs
        /// </summary>
        protected override void Move()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(0);
            Vector2 direction = new Vector2(0f, 0f);

            // Handles keyboard input
            if (isInputKeyboard)
            {
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
                {
                    direction += new Vector2(0f, -1f);
                }
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
                {
                    direction += new Vector2(0f, 1f);
                }
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                {
                    direction += new Vector2(-1f, 0f);
                }
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
                {
                    direction += new Vector2(1f, 0f);
                }
                if(keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && !keyboardPreviousState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    this.Attack();
                }

                direction.Normalize();

                if (keyboardState == keyboardPreviousState && gamePadState != gamePadPreviousState)
                {
                    isInputKeyboard = false;
                }
            }
            // Handles gamepad input
            else
            {
                direction = gamePadState.ThumbSticks.Left;

                if (gamePadState.IsButtonDown(Buttons.RightTrigger) && !gamePadPreviousState.IsButtonDown(Buttons.RightTrigger))
                {
                    this.Attack();
                }

                if(keyboardState != keyboardPreviousState)
                {
                    isInputKeyboard = true;
                }
            }

            velocity = moveSpeed * direction;

            position += velocity;

            velocity = new Vector2(0f, 0f);
            gamePadPreviousState = gamePadState;
            keyboardPreviousState = keyboardState;
        }

        /// <summary>
        /// Calculates the vector for the projectile aim
        /// </summary>
        public void Aim()
        {
            MouseState mouseState = Mouse.GetState();
            
            if (isInputKeyboard)
            {
                aimDirection = new Vector2(mouseState.X - DrawPos.X + DrawPos.Width / 2, mouseState.Y - DrawPos.Y + DrawPos.Height / 2);
            }
            else
            {
                aimDirection = GamePad.GetState(0).ThumbSticks.Right;
            }

            aimDirection.Normalize();
        }

        /// <summary>
        /// Creates a new projectile at the center of the player with a velocity based off of (float) projectileVelocity
        /// </summary>
        public void Attack()
        {
            this.weapon.Use(aimDirection * projectileVelocity);
        }

    }
}
