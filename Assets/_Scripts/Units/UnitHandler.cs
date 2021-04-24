using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AvatarRTS.Player;

namespace AvatarRTS.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        [SerializeField]
        private BasicUnit worker, warrior, healer;
        [SerializeField]
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
            BasicUnit unit;

            switch (type)
            {
                case "worker":
                    unit = worker;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                case "healer":
                    unit = healer;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }

            return unit.baseStats;
        }
    }
}
