using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Units
{
    public class CombatUnit : BasicUnit
    {
        public float Damage = 0, AttackCooldown = 0;
        private Transform aggroTarget;
        private BasicObject aggroUnit;
        private float distance;

        private void Update()
        {
            if (AttackCooldown > 0)
            {
                AttackCooldown -= Time.deltaTime;
            }

            if (!HasPlayerMoveCommand)
            {
                if (aggroUnit != null)
                {
                    distance = CalculateDistance(aggroTarget);
                    MoveToTarget();
                    Attack();
                }
                else
                {
                    CheckForEnemyTargets();
                }
            }
            else
            {
                if (!NavAgent.pathPending && NavAgent.remainingDistance <= NavAgent.stoppingDistance && (!NavAgent.hasPath || NavAgent.velocity.sqrMagnitude == 0f))
                {
                    HasPlayerMoveCommand = false;
                }
            }
        }

        public override void HandlePlayerAction(RaycastHit hit)
        {
            if (InputManager.InputHandler.instance.selectedUnits.Contains(gameObject.transform) &&
                hit.transform.gameObject.GetComponent<BasicObject>().Team != Team &&
                hit.transform.gameObject.GetComponent<BasicObject>() is BasicUnit)
            {
                HasPlayerMoveCommand = false;
                HandleUnitAttack(hit.transform);
            }
            else
            {
                MoveUnit(hit.point);
            }
        }
   
        private void CheckForEnemyTargets()
        {
            Collider[] rangeColliders = Physics.OverlapSphere(transform.position, AggroRange);
            for (int i = 0; i < rangeColliders.Length; i++)
            {
                try
                {
                    if ((rangeColliders[i].gameObject.layer == UnitHandler.instance.InteractablesLayer ||
                        rangeColliders[i].gameObject.layer == UnitHandler.instance.EnemyUnitsLayer) &&
                        rangeColliders[i].gameObject.GetComponent<BasicObject>().Team != Team &&
                        Vector3.Distance(rangeColliders[i].gameObject.transform.position, transform.position) <= AggroRange)
                    {
                        aggroTarget = rangeColliders[i].gameObject.transform;
                        aggroUnit = aggroTarget.gameObject.GetComponent<BasicObject>();
                        break;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log($"I = {i} exception {e.Message}");
                }
            }
        }

        public override void MoveUnit(Vector3 _destination)
        {
            base.MoveUnit(_destination);
            aggroUnit = null;
        }

        private void MoveToTarget()
        {

            if (aggroUnit == null)
            {
                NavAgent.SetDestination(transform.position);
            }
            else
            {
                NavAgent.stoppingDistance = (AttackRange + 1);

                if (distance <= AggroRange)
                {
                    NavAgent.SetDestination(aggroTarget.position);
                }
                else
                {
                    NavAgent.SetDestination(aggroTarget.position);
                }
            }
        }

        public void HandleUnitAttack(Transform target)
        {
            if (target != null)
            {
                aggroTarget = target;
                aggroUnit = target.gameObject.GetComponent<BasicObject>();
                DebugHandler.Print($"Target {target.gameObject.ToString()} aquired distance of ({distance = Vector3.Distance(aggroTarget.position, transform.position)})!");
            }
        }

        private void Attack()
        {
            if (AttackCooldown <= 0 && distance <= AttackRange + 1)
            {
                aggroUnit.TakeDamage(Damage);
                AttackCooldown = AttackSpeed;
            }
        }
    }
}
