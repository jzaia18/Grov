using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Authors: Jake Zaia

namespace Grov
{
    enum EnemyType
    {

    }

    class Enemy : Creature
    {
        // ************* Fields ************* //

        private EnemyType enemyType;


        // ************* Constructor ************* //

        public Enemy(EnemyType enemyType, int maxHP, bool melee, double fireRate, double attackDamage)
        {
            this.enemyType = enemyType;
            this.maxHP = maxHP;
            this.fireRate = fireRate;
            this.attackDamage = attackDamage;
        }


        // ************* Constructor ************* //

        public void Update(Entity target)
        {
            throw new NotImplementedException();
        }

        private void Attack(Entity target)
        {

        }
    }
}
