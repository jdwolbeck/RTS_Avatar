using AvatarRTS.InputManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BasicUnit : BasicObject
    {
        protected NavMeshAgent NavAgent;
        // Variables used to define general characteristics of the unit
        public UnitTypeEnum UnitType;
        public string Name;
        public GameObject HumanPrefab;
        public GameObject InfectedPrefab;
        public bool HasPlayerMoveCommand;
        public float AttackSpeed, AttackRange, AggroRange;

        public override void Awake()
        {
            base.Awake();
            Cost = 100;
            MaxHealth = 100;
            Armor = 0;
            AttackSpeed = 2.5f;
            AttackRange = 1;
            AggroRange = 10;
        }

        private void OnEnable()
        {
            NavAgent = GetComponent<NavMeshAgent>();
            CurrentHealth = MaxHealth;
        }

        public override void InitializeObject()
        {
            base.InitializeObject();

            if (Team == TeamEnum.player)
            {
                HealthBar.isPlayerUnit = true;
            }
            else
            {
                HealthBar.isPlayerUnit = false;
            }
        }

        public override void HandlePlayerAction(RaycastHit hit)
        {

        }

        public virtual void MoveUnit(Vector3 _destination)
        {
            HasPlayerMoveCommand = true;
            NavAgent.SetDestination(_destination);
        }

        public float CalculateDistance(Transform target)
        {
            if (target != null)
            {
                return (Vector3.Distance(target.position, transform.position));
            }

            throw new Exception("Tried to calculate distance to a null target!");
        }

        protected override void Die()
        {
            if (Team == TeamEnum.player)
            {
                InputHandler.instance.selectedUnits.Remove(gameObject.transform);
            }
            UnitHandler.instance.unitList.Remove(gameObject);
            base.Die();
        }
    }
}
