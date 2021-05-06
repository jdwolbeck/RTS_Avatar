using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class TurretTwoPartBuilding : TurretBuilding
    {
        public GameObject TurretHead = null;

        public override void DoRotate()
        {
            if (target != null)
            {
                Vector3 targetPostition = new Vector3(target.transform.position.x,
                                       TurretHead.transform.position.y,
                                       target.transform.position.z);
                TurretHead.transform.LookAt(targetPostition);
            }
            else if (RotationSpeed > 0)
            {
                TurretHead.transform.Rotate(0f, RotationSpeed, 0f, Space.Self);
            }
        }
        public override void DoAttack()
        {
            if (atkCooldown > 0)
            {
                atkCooldown -= Time.deltaTime;
            }

            if (atkCooldown <= 0 && target != null)
            {
                Shoot(target.transform.position);
            }
        }
    }
}