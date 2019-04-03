using System;
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
        Clump,     // a straight line of packets/clumps of projectiles
        Bubble      //Shield
    }

    class Weapon : Pickup
    {
        #region fields
        // ************* Fields ************* //

        private string name;
        private int fireRate;
        private float atkDamage;
        private float manaCost;
        private int numProjectiles;
        private ShotType shotType;
        private ProjectileType projectileType;
        private float shotSpeed;
        private int fireDelay;
        private int cooldown = 60;
        private int hitstun = 7;
        private int projectileLifeSpan;
        #endregion

        #region properties
        // ************* Properties ************* //

        public string Name { get => name; }
        public float FireRate { get => fireRate; }
        public float AttackDamage { get => atkDamage; }
        public float ManaCost { get => manaCost; }
        public int NumProjectiles { get => numProjectiles; }
        public float ShotSpeed { get => shotSpeed; set => shotSpeed = value; }
        public ShotType ShotType { get => shotType; }
        public int Cooldown { get => cooldown; }
        public int Hitstun { get => hitstun; }
        public int ProjectileLifeSpan { get => projectileLifeSpan; }
        #endregion

        #region constructor
        // ************* Constructor ************* //

        public Weapon(string filename, Rectangle drawPos, AnimatedTexture texture, bool isActive) : base(PickupType.Weapon, drawPos)
        {
            ReadFromFile(@"resources\weapons\" + filename + ".txt");
            this.isActive = isActive;
        }
        #endregion

        #region methods
        // ************* Methods ************* //

        private void ReadFromFile(string filename)
        {
            StreamReader reader = null;
            try {
                reader = new StreamReader(filename);

                name = reader.ReadLine();
                fireRate = int.Parse(reader.ReadLine());
                atkDamage = float.Parse(reader.ReadLine());
                manaCost = float.Parse(reader.ReadLine());
                numProjectiles = int.Parse(reader.ReadLine());
                shotSpeed = float.Parse(reader.ReadLine());
                shotType = (ShotType) Enum.Parse(typeof(ShotType), reader.ReadLine(), true);
                projectileLifeSpan = int.Parse(reader.ReadLine());
                hitstun = int.Parse(reader.ReadLine());
                projectileType = (ProjectileType) Enum.Parse(typeof(ProjectileType), reader.ReadLine(), true);
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

            if (!isActive)
            {
                if (fireDelay < fireRate)
                {
                    fireDelay++;
                }
            }

            base.Update();
        }

        public void Use(Vector2 direction)
        {
            float speedModifier = Math.Abs(direction.Length());

            switch (shotType)
            {
                case ShotType.Normal:
                    
                    float offset = (float) Math.PI / (numProjectiles+1);
                    float playerOffset = (float) Math.Atan2(direction.X, direction.Y);
                    
                    for (int i = 1; i < numProjectiles + 1; i++)
                    {
                        Vector2 projVelocity = shotSpeed * speedModifier * (new Vector2(-1 * (float) Math.Cos(playerOffset + i * offset), (float) Math.Sin(playerOffset + i * offset)));
                        EntityManager.AddProjectile(new Projectile(atkDamage, projectileLifeSpan, true, false, new Rectangle((int)position.X, (int)position.Y, 30, 30), projVelocity, projectileType));
                    }
                    break;
                case ShotType.Radial:
                    float originTheta = (float) Math.Atan2(direction.Y, direction.X);
                    float angleOffset = (float) Math.PI * 2 / numProjectiles;

                    for (int i = 0; i < numProjectiles; i++)
                    {
                        Vector2 projVelocity = shotSpeed * speedModifier * (new Vector2( (float) Math.Cos(originTheta + i*angleOffset), (float) Math.Sin(originTheta + i * angleOffset)));
                        EntityManager.AddProjectile(new Projectile(atkDamage, projectileLifeSpan, true, false, new Rectangle((int)position.X, (int)position.Y, 30, 30), projVelocity, projectileType));
                    }
                    break;
                case ShotType.Spread:
                    float theta = (float)Math.Atan2(direction.Y, direction.X);

                    for (int i = 0; i < numProjectiles; i++) {
                        float phi = theta + GameManager.RNG.Next(-128, 128) / 244.46199f;  // Random "percent" * PI/6   (but simplified)
                        Vector2 projVelocity = shotSpeed * speedModifier * (new Vector2((float) Math.Cos(phi), (float) Math.Sin(phi)));
                        EntityManager.AddProjectile(new Projectile(atkDamage, projectileLifeSpan, true, false, new Rectangle((int)position.X, (int)position.Y, 30, 30), projVelocity, projectileType));
                    }
                    break;
                case ShotType.Bubble:
                    Projectile bubble = new Projectile(atkDamage, projectileLifeSpan, true, true, new Rectangle(EntityManager.Player.DrawPos.X - (170 - EntityManager.Player.DrawPos.Width) / 2, EntityManager.Player.DrawPos.Y - (170 - EntityManager.Player.DrawPos.Height) / 2, 170, 170), new Vector2(0f, 0f), projectileType);
                    EntityManager.AddProjectile(bubble);
                    break;
                default:
                    throw new NotImplementedException();
            }

            this.fireDelay = 0;
        }

        /// <summary>
        /// Checks to see if the weapon is ready to be fired before
        /// </summary>
        /// <param name="multiplier">The entity's firerate mulitplier</param>
        /// <returns>True if ready to fire, False if not</returns>
        public bool ReadyToFire(float multiplier)
        {
            return (this.fireRate <= this.fireDelay * multiplier);
        }
        #endregion
    }
}
