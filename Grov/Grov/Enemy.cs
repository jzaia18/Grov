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

        public Enemy()
        {

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
