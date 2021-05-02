using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AvatarRTS.Buildings.BuildingHandler;

namespace AvatarRTS.Buildings
{
    //[CreateAssetMenu(fileName = "Building", menuName = "New Building/TurretSentry")]
    public class TurretSentryBuilding : TurretBuilding
    {
        public override void Awake()
        {
            base.Awake();
            BuildingType = BuildingTypeEnum.TurretSentry;
            //RotationSpeed += 0.1f;
        }
    }
}