using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;

        [SerializeField]
        private BasicBuilding barracks;

        private void Awake()
        {
            instance = this;
        }

        public BuildingStatTypes.Base GetBasicBuildingStats(string type)
        {
            BasicBuilding building;
            switch (type)
            {
                case "barrack":
                    building = barracks;
                    break;
                default:
                    DebugHandler.Print($"Building Type: {type} could not be found or does not exist!");
                    return null;
            }

            return building.baseStats;
        }
    }
}