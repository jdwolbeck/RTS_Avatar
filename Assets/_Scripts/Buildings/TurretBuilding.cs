using AvatarRTS.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building/Turret")]
    public class TurretBuilding : BasicBuilding
    {
        public new BuildingStatTypes.Turret baseStats; 
    }
}