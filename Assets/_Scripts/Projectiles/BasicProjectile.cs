using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    private static float DESTROY_DELAY = 2f;

    public GameObject ProjectileVisualObject;
    protected bool targetReached = false;
    protected float MoveSpeed = 2.5f;
    protected Vector3 target = Vector3.zero;
    protected TeamEnum Team;
    protected GameObject parentObject;
    private float destroyCountdown;

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
                    FinishProjectile();
                }
            }
        }
        else
        {
            destroyCountdown -= Time.deltaTime;

            if(destroyCountdown <= 0f)
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
        FinishProjectile();
    }

    protected virtual void FinishProjectile()
    {
        destroyCountdown = DESTROY_DELAY;
        targetReached = true;

        //Remove the rigidbody component so that it does not collide with anyone else while we wait for it to dissapear
        Destroy(this.GetComponent<Rigidbody>());
        if (ProjectileVisualObject != null)
        {
            ProjectileVisualObject.GetComponent<Renderer>().enabled = false;
        }
    }

    protected virtual bool IsTargetedTeam(TeamEnum team)
    {
        return false;
    }
}
