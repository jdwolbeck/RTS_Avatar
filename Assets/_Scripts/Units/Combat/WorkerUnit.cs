using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Units.Melee
{ 
    public class WorkerUnit : CombatUnit
    {
        public WorkerUnit()
        {
            Cost = 50;
            MaxHealth = 85;
            Armor = 0;
            AttackSpeed = 2;
            AttackRange = 2;
        }
    }
}