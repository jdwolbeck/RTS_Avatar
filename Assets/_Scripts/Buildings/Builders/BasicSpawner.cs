using AvatarRTS.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Buildings
{
    public class BasicSpawner : BasicBuilding
    {
        public Vector3 Waypoint { get; set; }
        public UnitTypeEnum SpawnUnit = UnitTypeEnum.Warrior;
        public float SpawnRate { get; set; }
        protected float spawnCooldown { get; set; }

        public override void Awake()
        {
            base.Awake();
            Cost = 250;
            MaxHealth = 2000;
            Armor = 5;
            SpawnRate = 2f;

            Waypoint = new Vector3(0,0,0);
        }

        protected override void Update()
        {
            base.Update();

            if (spawnCooldown > 0)
            {
                spawnCooldown -= Time.deltaTime;
            }

            if (spawnCooldown <= 0)
            {
                GameObject unit = UnitHandler.instance.CreateUnit(Team, SpawnUnit, gameObject.transform.position, Quaternion.identity);
                CombatUnit cu = unit.GetComponent<CombatUnit>();

                if (cu != null)
                {
                    cu.AttackMoveUnit(Waypoint);
                }

                spawnCooldown = SpawnRate;
            }
        }
    }
}