﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


// Authors: Jake Zaia

namespace Grov
{
    enum ShotType
    {
        Normal,   // symmetrical cone in one direction
        Radial,   // laterally from all sides of the character
        Spread,   // in a cone, but more randomly spread out
        Clump     // a straight line of packets/clumps of projectiles
    }

    class Weapon : Pickup
    {
        #region fields
        // ************* Fields ************* //

        private string name;
        private float fireRate;
        private float atkDamage;
        private int manaCost;
        private int numProjectiles;
        private ShotType shotType;
        private float shotSpeed;
        private Texture2D projectileTexture;
        #endregion

        #region properties
        // ************* Properties ************* //

        public string Name { get => name; }
        public float FireRate { get => fireRate; }
        public float AttackDamage { get => atkDamage; }
        public int ManaCost { get => manaCost; }
        public int NumProjectiles { get => numProjectiles; }
        public float ShotSpeed { get => shotSpeed; set => shotSpeed = value; }
        public ShotType ShotType { get => shotType; }
        public Texture2D ProjectileTexture { get => projectileTexture; set => projectileTexture = value; }
        #endregion

        #region constructor
        // ************* Constructor ************* //

        public Weapon(string filename, Rectangle drawPos, Texture2D texture, Texture2D projectileTexture, bool isActive) : base(PickupType.Weapon, drawPos, texture)
        {
            readFromFile(@"resources\weapons\" + filename + ".txt");
            this.isActive = isActive;
            this.projectileTexture = projectileTexture;
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        private void readFromFile(string filename)
        {
            StreamReader reader = null;
            try {
                reader = new StreamReader(filename);

                name = reader.ReadLine();
                fireRate = float.Parse(reader.ReadLine());
                atkDamage = float.Parse(reader.ReadLine());
                manaCost = int.Parse(reader.ReadLine());
                numProjectiles = int.Parse(reader.ReadLine());
                shotSpeed = float.Parse(reader.ReadLine());
                shotType = (ShotType) Enum.Parse(typeof(ShotType), reader.ReadLine(), true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public override void Update()
        {
            //TODO: restrict usage based on whether or not it is active
            // Active => item is on ground
            // Inactive => item is usable and held by player

            throw new NotImplementedException();
            base.Update();
        }

        public void Use(Vector2 direction)
        {
            float projectileLifeSpan = 0;

            switch (shotType)
            {
                case ShotType.Normal:
                    
                    float offset = (float) Math.PI / (numProjectiles+1);
                    float playerOffset = (float) Math.Atan2(direction.X, direction.Y);
                    float speedModifier = Math.Abs(direction.Length());

                    for (int i = 1; i < numProjectiles + 1; i++)
                    {
                        Vector2 projVelocity = shotSpeed * speedModifier * (new Vector2(-1 * (float) Math.Cos(playerOffset + i * offset), (float) Math.Sin(playerOffset + i * offset)));
                        EntityManager.AddProjectile(new Projectile(projectileLifeSpan, true, false, new Rectangle((int)position.X, (int)position.Y, 30, 30), projVelocity, projectileTexture));
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion
    }
}