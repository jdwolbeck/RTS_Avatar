using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    //[CreateAssetMenu(fileName = "Building", menuName = "New Building/TurretSentry")]
    public class TurretSentryBuilding : TurretBuilding
    {
        public TurretSentryBuilding()
        {
            RotationSpeed += 0.1f;
        }
    }
}