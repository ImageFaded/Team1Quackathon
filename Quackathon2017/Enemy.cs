using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quackathon2017
{
    class Enemy
    {
        //Initalise stats
        private int health;
        private int attack;
        private int defense;
        private int element;
        private int temp;
        public int AttackEnemy()
        //Attacks 
        {
            return attack;
        }
        public bool LoseHealth(int damage)
        {
            if(defense >= damage)
            {
                temp = defense;
                defense = (damage - 1);
            }
            health = health - (damage - defense);
            defense = temp;
            if(health < 1)
            {
                return true;
            } else
            {
                return false;
            }
        }
        
    }


}
