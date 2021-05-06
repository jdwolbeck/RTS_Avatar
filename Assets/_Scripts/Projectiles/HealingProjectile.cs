using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingProjectile : BasicTrackingProjectile
{
    protected float HealAmount { get; set; }

    public void SetHealAmount(float healValue)
    {
        HealAmount = healValue;
    }

    protected override void DoAction(Collider collision)
    {
        if (targetObj != null)
        {
            BasicObject basicObject = targetObj.GetComponent<BasicObject>();
            if (basicObject != null)
            {
                basicObject.Heal(HealAmount);
            }
        }
        base.DoAction(collision);
    }

    protected override bool IsTargetedTeam(TeamEnum team)
    {
        return team == Team;
    }
}
