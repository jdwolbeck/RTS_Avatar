using AvatarRTS.InputManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class BasicBuilding : BasicObject
    {
        public BuildingTypeEnum BuildingType;
        public string Name;
        public GameObject BuildingPrefab;

        public BasicBuilding()
        {
            Cost = 500;
            MaxHealth = 2500;
            Armor = 5;
        }

        protected override void Die()
        {
            if (Team == TeamEnum.player && InputHandler.instance.selectedBuilding == gameObject.transform)
            {
                InputHandler.instance.selectedBuilding = null;
            }
            base.Die();
        }
    }
}
