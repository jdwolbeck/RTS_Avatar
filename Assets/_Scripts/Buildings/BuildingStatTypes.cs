using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class BuildingStatTypes : ScriptableObject
    {
        public class Base
        {
            public float health = 2500, 
                         armor = 5;
        }
        public class Turret : Base
        {
            public float attack = 15,
                         atkSpeed = 1, 
                         projectileSpeed = 20, 
                         rotationSpeed = 0.05f;
            public Turret()
            {
                health = 500;
                armor = 1;
            }
        }
        public class Sentry : Turret
        {
            public Sentry()
            {
                rotationSpeed += 0.1f;
            }
        }
    }
}