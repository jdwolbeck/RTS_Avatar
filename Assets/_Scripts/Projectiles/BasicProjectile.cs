using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    protected bool targetReached = false;
    protected float MoveSpeed = 2.5f;
    protected Vector3 target = Vector3.zero;
    protected TeamEnum Team;
    protected GameObject parentObject;
    private DateTime reachedTargetTime;

    public void InitializeProjectile(Vector3 projTarget, float projSpeed, GameObject projParent, TeamEnum projTeam)
    {
        target = projTarget;
        MoveSpeed = projSpeed;
        parentObject = projParent;
        Team = projTeam;
    }

    protected virtual void Update()
    {
        if (!targetReached)
        {
            if (target != Vector3.zero)
            {
                Vector3 targetDir = target - this.transform.position;
                targetDir.Normalize(); //Must normalize this output since the direction should not be scaled with distance but rather MoveSpeed should be used to scale
                transform.position += (targetDir * (MoveSpeed * Time.deltaTime));

                //We've reached our target position, set the boolean
                if (Vector3.Distance(target, this.transform.position) <= 0.2f)
                {
                    targetReached = true;
                    reachedTargetTime = DateTime.Now;
                }
            }
        }
        else if((DateTime.Now - reachedTargetTime).TotalSeconds>= 2)
        {
            Destroy(gameObject);
        }
    }

    public void SetNewTarget(Vector3 pos)
    {
        targetReached = false;
        target = pos;
    }

    public bool HasReachedTarget()
    {
        return targetReached;
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (collision != null && parentObject != null)
        {
            BasicObject basicObject = collision.gameObject.GetComponent<BasicObject>();
            if (collision.gameObject != parentObject && basicObject != null && IsTargetedTeam(basicObject.Team))
            {
                //Our projectile has gotten to a GameObject on the targeted team, lets set the boolean
                DoAction(collision);
            }
        }
    }

    protected virtual void DoAction(Collider collision)
    {
        targetReached = false;
        Destroy(gameObject);
    }

    protected virtual bool IsTargetedTeam(TeamEnum team)
    {
        return false;
    }
}
