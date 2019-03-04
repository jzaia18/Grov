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
 * Duncan Mott
 */
 
namespace Grov
{
    class Player: Creature
    {
        // ************* Fields ************* //

        private float maxMP;
        private float currMP;
        private int cooldown;
        private bool stoppedFiring;
        private Weapon weapon;
        private int keys;
        private int bombs;
        private GamePadState gamePadPreviousState;
        private KeyboardState keyboardPreviousState;
        private MouseState mousePreviousState;
        private GamePadState gamePadState;
        private KeyboardState keyboardState;
        private MouseState mouseState;
        private Vector2 aimDirection;
        private bool isInputKeyboard;
        private int Iframes;

        // ************* Properties ************* //

        public float CurrMP
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

        public float MaxMP { get => maxMP; set => maxMP = value; }
        public int Keys { get => keys; set => keys = value; }
        public int Bombs { get => bombs; set => bombs = value; }
        public Weapon Weapon { get => weapon; set => weapon = value; }
        public int IFrames { get => Iframes; set => Iframes = value; }

        // ************* Constructor ************* //

        public Player(float maxHP, float maxMP, float fireRate, float moveSpeed, float attackDamage, float projectileSpeed, Rectangle drawPos, Rectangle hitbox, Vector2 velocity, Texture2D texture) : base(maxHP, false, fireRate, moveSpeed, attackDamage, projectileSpeed, drawPos, hitbox, new Vector2(drawPos.X, drawPos.Y), velocity, true, texture)
        {
            this.maxMP = maxMP;
            this.currMP = maxMP;
            keys = 0;
            bombs = 0;
            keyboardPreviousState = Keyboard.GetState();
            gamePadPreviousState = GamePad.GetState(0);
            mousePreviousState = Mouse.GetState();
            isInputKeyboard = true;
            weapon = new Weapon("Default", default(Rectangle), null, null, false);
        }


        // ************* Methods ************* //

        /// <summary>
        /// Updates location and initiates all needed calculations
        /// </summary>
        public override void Update()
        {
            mouseState = Mouse.GetState();
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();
            this.Aim();
            base.Update();
            this.hitbox = DrawPos;
            this.weapon.Update();

            if(this.currMP < this.MaxMP && cooldown == 0 && weapon.ReadyToFire(fireRate))
            {
                this.currMP += .5f;
            }

            if(this.Iframes > 0)
            {
                Iframes--;
            }

            //Weapon cooldown
            if (cooldown > 0 && stoppedFiring) cooldown--;

            gamePadPreviousState = gamePadState;
            keyboardPreviousState = keyboardState;
            mousePreviousState = mouseState;
        }

        /// <summary>
        /// Handles all movement-based calculations and inputs
        /// </summary>
        protected override void Move()
        {
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
                if ((keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space)
                    || mouseState.LeftButton.Equals(ButtonState.Pressed)) && this.currMP > 0 && weapon.ReadyToFire(fireRate) && cooldown == 0)
                {
                    this.Attack();
                    if (currMP <= 0) cooldown += weapon.Cooldown;
                    stoppedFiring = false;
                }

                //Stop the cooldown from recharging until button is released
                if(stoppedFiring == false && !(keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space)
                    || mouseState.LeftButton.Equals(ButtonState.Pressed)))
                {
                    stoppedFiring = true;
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
                direction.Y = -direction.Y;

                if (gamePadState.IsButtonDown(Buttons.RightTrigger) && this.currMP >= 0 && weapon.ReadyToFire(fireRate) && cooldown == 0)
                {
                    this.Attack();
                    if (currMP <= 0) cooldown += weapon.Cooldown;
                    stoppedFiring = false;
                }

                //Stop the cooldown from recharging until button is released
                if (stoppedFiring == false && !gamePadState.IsButtonDown(Buttons.RightTrigger))
                {
                    stoppedFiring = true;
                }

                if (keyboardState != keyboardPreviousState)
                {
                    isInputKeyboard = true;
                }
            }

            velocity = moveSpeed * direction;

            if (float.IsNaN(direction.X))
            {
                direction.X = 0;
            }
            if (float.IsNaN(direction.Y))
            {
                direction.Y = 0;
            }

            velocity = moveSpeed * direction;

            position += velocity;
            
            velocity = new Vector2(0f, 0f);
            weapon.Position = new Vector2(this.position.X + this.DrawPos.Width / 2, this.position.Y + this.DrawPos.Height / 2);
        }

        /// <summary>
        /// Calculates the vector for the projectile aim
        /// </summary>
        public void Aim()
        {
            Vector2 direction = new Vector2(0f, 0f);
            bool isMouse = true;

            // Handles keyboard input
            if (isInputKeyboard)
            {
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                {
                    direction += new Vector2(0f, -1f);
                    isMouse = false;
                }
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                {
                    direction += new Vector2(0f, 1f);
                    isMouse = false;
                }
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                {
                    direction += new Vector2(-1f, 0f);
                    isMouse = false;
                }
                if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                {
                    direction += new Vector2(1f, 0f);
                    isMouse = false;
                }
                if (isMouse)
                {
                    direction = new Vector2(mouseState.X - (DrawPos.X + DrawPos.Width / 2), mouseState.Y - (DrawPos.Y + DrawPos.Height / 2));
                }


                if(gamePadState.Triggers.Right != 0f && gamePadPreviousState.Triggers.Right == 0f)
                {
                    isInputKeyboard = false;
                }
            }
            // Handles gamepad input
            else
            {
                direction = GamePad.GetState(0).ThumbSticks.Right;
                direction.Y = -direction.Y;
                if (mouseState.LeftButton.Equals(ButtonState.Pressed) && !mousePreviousState.LeftButton.Equals(ButtonState.Pressed))
                {
                    isInputKeyboard = true;
                }
            }

            // Normalize and finish calculations
            direction.Normalize();
            if (!float.IsNaN(direction.Y) || !float.IsNaN(direction.X))
            {
                aimDirection = direction;

                if (float.IsNaN(aimDirection.X))
                {
                    aimDirection.X = 0f;
                }
                if (float.IsNaN(aimDirection.Y))
                {
                    aimDirection.Y = 0f;
                }
            }
            if(aimDirection == null)
            {
                aimDirection = new Vector2(1f, 0f);
            }
        }

        /// <summary>
        /// Creates a new projectile at the center of the player with a velocity based off of (float) projectileVelocity
        /// </summary>
        public void Attack()
        {
            this.weapon.Use(aimDirection * projectileSpeed);
            this.currMP -= weapon.ManaCost;
        }

    }
}
