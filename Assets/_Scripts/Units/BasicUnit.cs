using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit/Basic")]
    public class BasicUnit : ScriptableObject
    {
        // List of available unit types
        public enum unitType
        {
            Worker,
            Warrior,
            Healer
        };

        // Variables used to define general characteristics of the unit
        [Space(15)]
        [Header("Unit Settings")]

        public unitType type;
        public string unitName;
        public GameObject humanPrefab;
        public GameObject infectedPrefab;

        // Specific variables for each unit
        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(40)]

        public UnitStatTypes.Base baseStats;
    }
}
