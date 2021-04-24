using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class BuildingStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float health, armor, attack;
        }
    }
}