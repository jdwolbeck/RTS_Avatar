using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : BasicProjectile
{
    protected float Damage { get; set; }

    public void SetDamage(float dmg)
    {
        Damage = dmg;
    }

    protected override void DoAction(Collider collision)
    {
        if (collision != null)
        {
            BasicObject basicObject = collision.GetComponent<BasicObject>();
            if (basicObject != null)
            {
                basicObject.TakeDamage(Damage);
            }
        }
        base.DoAction(collision);
    }

    protected override bool IsTargetedTeam(TeamEnum team)
    {
        return team != Team;
    }
}
