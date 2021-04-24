using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AvatarRTS.Player;

namespace AvatarRTS.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            pUnitLayer = LayerMask.NameToLayer("Interactables");
            eUnitLayer = LayerMask.NameToLayer("EnemyUnits");
        }

        public UnitStatTypes.Base GetBasicUnitStats(string type)
        {
            switch (type)
            {
                case "worker":
                    return new UnitStatTypes.Worker();
                case "warrior":
                    return new UnitStatTypes.Warrior();
                case "healer":
                    return new UnitStatTypes.Healer();
                default:
                    DebugHandler.Print($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
        }
    }
}
