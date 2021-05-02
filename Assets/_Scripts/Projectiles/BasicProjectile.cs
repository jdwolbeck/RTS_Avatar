using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    protected bool targetReached = false;
    protected bool Accelerate = false;
    protected float MoveSpeed = 2.5f;
    protected Vector3 target = Vector3.zero;

    public void SetTarget(Vector3 pos)
    {
        targetReached = false;
        target = pos;
    }
    public void SetTargetAndSpeed(Vector3 pos, float speedIn)
    {
        SetTarget(pos);
        SetSpeed(speedIn);
    }
    public void SetSpeed(float speedIn)
    {
        MoveSpeed = speedIn;
    } 

    protected virtual void Update()
    {
        if (target != Vector3.zero)
        {
            Vector3 targetDir = target - this.transform.position;
            targetDir.Normalize(); //Must normalize this output since the direction should not be scaled with distance but rather MoveSpeed should be used to scale
            transform.position += (targetDir * (MoveSpeed * Time.deltaTime));

            if (Accelerate)
            {
                MoveSpeed += 0.01f;
            }

            //Our projectile has gotten to the target, lets set the boolean
            if (Vector3.Distance(target, this.transform.position) <= 1)
            {
                targetReached = true;
            }
        }
    }

    public bool HasReachedTarget()
    {
        return targetReached;
    }
}
