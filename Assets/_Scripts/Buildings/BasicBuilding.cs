using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building/Basic")]
    public class BasicBuilding : ScriptableObject
    {
        public enum buildingType
        {
            Unknown,
            Barracks,
            TurretSentry
        }

        public buildingType type;
        public new string name;
        public GameObject buildingPrefab;
        public BuildingActions.BuildingUnits Units;
        public BuildingStatTypes.Base baseStats;
    }
}
