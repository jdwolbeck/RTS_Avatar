using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrackingProjectile : BasicProjectile
{
    protected GameObject targetObj;

    public void SetTargetObject(GameObject obj)
    {
        targetObj = obj;
        SetNewTarget(targetObj.transform.position);
    }

    protected override void Update()
    {
        if (targetObj != null)
        {
            target = targetObj.transform.position;
            base.Update();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
