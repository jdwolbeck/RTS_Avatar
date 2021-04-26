using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class BasicBuilding : BasicObject
    {
        public BuildingTypeEnum BuildingType;
        public string Name;
        public GameObject BuildingPrefab;

        public BasicBuilding()
        {
            Health = 2500;
            Armor = 5;
        }
    }
}
