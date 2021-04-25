using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AvatarRTS.Buildings.BuildingHandler;

namespace AvatarRTS.Buildings
{
    //[CreateAssetMenu(fileName = "Building", menuName = "New Building/Barracks")]
    public class BarrackBuilding : BasicBuilding
    {
        public BuildingActions.BuildingUnits Units;

        public BarrackBuilding()
        {
            BuildingType = BuildingTypeEnum.Barracks;
        }
    }
}