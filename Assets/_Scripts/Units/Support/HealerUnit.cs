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
        private bool hasHealOrb = false;
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
            if ((HealCooldown <= 0) && (distance <= AttackRange + 1)
                && (healUnit.CurrentHealth + HealAmount <= healUnit.MaxHealth))
            {
                if (hasHealOrb && healOrb != null)
                {
                    if (healOrb.GetComponent<BasicTrackingProjectile>().HasReachedTarget() == true)
                    {
                        //Destroy our orb and heal the target
                        Destroy(healOrb);
                        hasHealOrb = false;

                        Debug.Log($"Healer healUnit (c, a, m : {healUnit.CurrentHealth}, {HealAmount}, {healUnit.MaxHealth})");
                        healUnit.Heal(HealAmount);
                        HealCooldown = AttackSpeed;
                    }
                }
                else
                {
                    GameObject prefab = Resources.Load("Prefabs/HealerOrb", typeof(GameObject)) as GameObject;
                    healOrb = Instantiate(prefab, transform.position, Quaternion.identity);
                    healOrb.GetComponent<BasicTrackingProjectile>().GenerateHealOrb(healUnit.gameObject);
                    healOrb.GetComponent<BasicTrackingProjectile>().SetSpeed(8f);
                    hasHealOrb = true;
                }
            }
        }
    }
}