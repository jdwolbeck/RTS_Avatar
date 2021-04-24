using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;

        private void Awake()
        {
            instance = this;
        }

        public BuildingStatTypes.Base GetBasicBuildingStats(string type)
        {
            switch (type)
            {
                case "barrack":
                    return new BuildingStatTypes.Base();
                case "turretsentr":
                    return new BuildingStatTypes.Sentry();
                default:
                    DebugHandler.Print($"Building Type: {type} could not be found or does not exist!");
                    return null;
            }
        }
    }
}