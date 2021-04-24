using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;
        public UnitStatTypes.Base baseStats;
        private Transform aggroTarget;
        private UnitStatDisplay aggroUnit;
        public bool hasAggro = false;
        public float atkCooldown;
        private float distance;

        private void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (atkCooldown > 0)
            {
                atkCooldown -= Time.deltaTime;
            }

            if(hasAggro)
            {
                if (aggroTarget != null)
                {
                    distance = Vector3.Distance(aggroTarget.position, transform.position);
                }
                MoveToAggroTarget();
                Attack();
            }
        }

        public void MoveUnit(Vector3 _destination)
        {
            navAgent.SetDestination(_destination);
        }

        public void HandleUnitAttack(Transform target)
        {
            if (target != null)
            {
                DebugHandler.Print($"Player Unit target Aquired! ({target.ToString()})");
                aggroTarget = target;
                aggroUnit = target.gameObject.GetComponentInChildren<UnitStatDisplay>();
                hasAggro = true;
            }
        }

        private void MoveToAggroTarget()
        {
            
            if (aggroTarget == null)
            {
                navAgent.SetDestination(transform.position);
                hasAggro = false;
            }
            else
            {
                navAgent.stoppingDistance = (baseStats.atkRange + 1);

                if (distance <= baseStats.aggroRange)
                {
                    navAgent.SetDestination(aggroTarget.position);
                }
                else
                {
                    navAgent.SetDestination(aggroTarget.position);
                }
            }
        }

        private void Attack()
        {
            if (atkCooldown <= 0 && distance <= baseStats.atkRange + 1)
            {
                aggroUnit.TakeDamage(baseStats.attack);
                atkCooldown = baseStats.atkSpeed;
            }
        }
    }
}
