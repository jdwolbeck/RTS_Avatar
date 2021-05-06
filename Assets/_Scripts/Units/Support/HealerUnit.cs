using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Units.Support
{
    public class HealerUnit : BasicUnit
    {
        public float HealAmount = 15, HealCooldown = 0;
        private Transform healTarget;
        private BasicObject healUnit;
        private float distance;
        private GameObject healOrb;


        public override void Awake()
        {
            base.Awake();
            Cost = 250;
            MaxHealth = 65;
            Armor = 0;
            AttackSpeed = 5;
            AttackRange = 5;
        }

        protected override void Update()
        {
            if (HealCooldown > 0)
            {
                HealCooldown -= Time.deltaTime;
            }
            if (healUnit != null)
            {
                distance = CalculateDistance(healTarget);
                MoveToTarget();
                HealTarget();
            }
        }

        public override void HandlePlayerAction(RaycastHit hit)
        {
            if (InputManager.InputHandler.instance.selectedUnits.Contains(gameObject.transform) &&
                hit.transform.gameObject.GetComponent<BasicObject>().Team == Team &&
                hit.transform.gameObject.GetComponent<BasicObject>() is BasicUnit)
            {
                HandleUnitHeal(hit.transform);
            }
            else
            {
                MoveUnit(hit.point);
            }
        }

        public override void MoveUnit(Vector3 _destination)
        {
            healUnit = null;
            base.MoveUnit(_destination);
        }

        private void MoveToTarget()
        {

            if (healUnit == null)
            {
                NavAgent.SetDestination(transform.position);
            }
            else
            {
                NavAgent.stoppingDistance = AttackRange;

                if (distance <= AggroRange)
                {
                    NavAgent.SetDestination(healTarget.position);
                }
                else
                {
                    NavAgent.SetDestination(healTarget.position);
                }
            }
        }

        public void HandleUnitHeal(Transform target)
        {
            if (target != null && target.gameObject != gameObject)
            {
                DebugHandler.Print($"{gameObject.ToString()} | {Team.ToString()}: Healer target aquired ({target.ToString()})");
                healTarget = target;
                healUnit = target.GetComponent<BasicObject>();
            }
        }

        private void HealTarget()
        {
            if ((distance <= AttackRange + 1)
                && (healUnit.CurrentHealth + HealAmount <= healUnit.MaxHealth))
            {
                if (HealCooldown <= 0)
                {
                    //Instantiate a HealerOrb prefab under our Healer, set its target to the object we want to heal
                    GameObject prefab = Resources.Load("Prefabs/HealerOrb", typeof(GameObject)) as GameObject;
                    healOrb = Instantiate(prefab, transform.position, Quaternion.identity);
                    HealingProjectile btProj = healOrb.GetComponent<HealingProjectile>();
                    btProj.InitializeProjectile(healUnit.gameObject.transform.position, 8f, gameObject, Team);
                    btProj.SetTargetObject(healUnit.gameObject);
                    btProj.SetHealAmount(HealAmount);

                    //Reset our cooldown to our attackspeed to allow for another heal
                    HealCooldown = AttackSpeed;
                }
            }
        }

        public void ProjectileIsFinished()
        {
           
        }
    }
}