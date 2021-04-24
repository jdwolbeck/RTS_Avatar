using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class BuildingActions : MonoBehaviour
    {
        [System.Serializable]

        public class BuildingUnits
        {
            public Units.BasicUnit[] basicUnits;
        }
    }
}