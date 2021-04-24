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
        public unitType type;
        public string unitName;
        public GameObject humanPrefab;
        public GameObject infectedPrefab;

        //Specific stats for the Base class
        public UnitStatTypes.Base baseStats;
    }
}
