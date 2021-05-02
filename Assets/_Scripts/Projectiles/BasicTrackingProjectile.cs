using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrackingProjectile : BasicProjectile
{
    private GameObject targetObj;

    public void GenerateHealOrb(GameObject obj)
    {
        Accelerate = true;
        targetObj = obj;
        SetTarget(targetObj.transform.position);
    }

    protected override void Update()
    {
        if (targetObj != null)
        {
            target = targetObj.transform.position;
            base.Update();
        }
    }
}
