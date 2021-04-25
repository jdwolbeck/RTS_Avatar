using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class BasicBuilding : MonoBehaviour
    {
        public enum buildingType
        {
            Unknown,
            Barracks,
            TurretSentry
        }

        public buildingType BuildingType;
        public string Name;
        public GameObject BuildingPrefab;

        //Basic Building Stats
        public float Health = 2500,
             Armor = 5;
    }
}
