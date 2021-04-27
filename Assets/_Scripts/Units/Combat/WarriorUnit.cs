using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Units
{
    public class WarriorUnit : CombatUnit
    {
        public override void Awake()
        {
            base.Awake();
            Cost = 250;
            MaxHealth = 150;
            Armor = 3;
            AttackSpeed = 1.25f;
            AttackRange = 2;
            Damage = 25;
        }

        //public override void InitializeObject()
        //{
        //    Debug.Log("Warrior Unit - calling parent");
        //    base.InitializeObject();
           
        //}
    }
}
