using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Units
{
    public class UnitStatTypes : MonoBehaviour
    { 
        public class Base
        {
            public float cost, health, armor = 0, atkSpeed, atkRange, aggroRange = 10;
        }

        // **************** All definitions for offensive units ******************
        public class Offensive : Base
        {
            public float damage;
        }

        public class Worker : Offensive
        {
            public Worker()
            {
                cost = 25;
                health = 50;
                armor = 0;
                atkSpeed = 2;
                atkRange = 2;
                damage = 10; 
            }
        }

        public class Warrior : Offensive
        { 
            public Warrior()
            {
                cost = 100;
                health = 150;
                armor = 3;
                atkSpeed = 1.25f;
                atkRange = 2;
                damage = 25;
            }
        }

        // **************** All definitions for support units ******************
        public class Support : Base
        {
            public float healAmount;
        }

        public class Healer : Support
        {
            public Healer()
            {
                cost = 250;
                health = 75;
                armor = 0;
                atkSpeed = 5;
                atkRange = 5;
                healAmount = 15;
            }
        }
    }
}
