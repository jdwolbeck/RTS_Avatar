using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    //[CreateAssetMenu(fileName = "Building", menuName = "New Building/Barracks")]
    public class BarrackBuilding : BasicBuilding
    {
        public BuildingActions.BuildingUnits Units;
    }
}