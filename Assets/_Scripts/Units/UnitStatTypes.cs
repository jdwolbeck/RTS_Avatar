using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Units
{
    public class UnitStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float cost, attack, atkRange, atkSpeed, health, armor, aggroRange;
        }
    }
}
