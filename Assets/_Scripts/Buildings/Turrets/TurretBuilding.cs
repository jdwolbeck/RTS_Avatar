using AvatarRTS.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Buildings
{
    public class TurretBuilding : BasicBuilding
    {
        public float AttackDamage { get; set; }
        public float AtkSpeed { get; set; }
        public float ProjectileSpeed { get; set; }
        public float ProjectileRange { get; set; }
        protected float RotationSpeed { get; set; }

        public GameObject bulletPrefab;
        protected float atkCooldown { get; set; }
        protected int cannonIterator { get; set; }
        protected GameObject target;
        protected List<GameObject> cannons;

        public override void Awake()
        {
            base.Awake();
            Cost = 250;
            MaxHealth = 2000;
            Armor = 5;

            AttackDamage = 30f;
            AtkSpeed = 2f;
            ProjectileSpeed = 20f;
            ProjectileRange = 10f;
            RotationSpeed = 0.075f;

            //if(bulletPrefab == null)
            //    bulletPrefab = Resources.Load("Prefabs/ProjectileMissile", typeof(GameObject)) as GameObject;

            cannons = new List<GameObject>();

            //Search the object children for the Cannon Component
            SearchForObjectCannons();
            cannonIterator = 0;
        }

        protected override void Update()
        {
            base.Update();
            FindTarget();
            DoRotate();
            DoAttack();
        }

        public void FindTarget()
        {
            if (target != null)
            {
                float d = Vector3.Distance(target.transform.position, transform.position);
                if (d > ProjectileRange)
                    target = null;
            }

            if (target == null)
            {
                Collider c = CheckForEnemyTargets(ProjectileRange, true);

                if (c != null)
                {
                    target = c.gameObject;
                }
            }
        }
        public virtual void DoRotate()
        {
            if (target != null)
            {
                Vector3 targetPostition = new Vector3(target.transform.position.x,
                                       this.transform.position.y,
                                       target.transform.position.z);
                this.transform.LookAt(targetPostition);
            }
            else if(RotationSpeed > 0)
            {
                gameObject.transform.Rotate(0f, RotationSpeed, 0f, Space.Self);
            }
        }
        public virtual void DoAttack()
        {
            if (atkCooldown > 0)
            {
                atkCooldown -= Time.deltaTime;
            }

            if (atkCooldown <= 0 && target != null)
            {
                Vector3 calcTarget = new Vector3(gameObject.transform.position.x, target.transform.GetComponent<Collider>().bounds.center.y, gameObject.transform.position.z);
                float angle = gameObject.transform.rotation.eulerAngles.y;

                angle *= Mathf.Deg2Rad;

                calcTarget.x = ProjectileRange * Mathf.Sin(angle) + gameObject.transform.position.x;
                calcTarget.z = ProjectileRange * Mathf.Cos(angle) + gameObject.transform.position.z;

                Shoot(calcTarget);
            }
        }

        protected void SearchForObjectCannons()
        {
            foreach (Transform child in transform)
            {
                if (child != null)
                {
                    if (child.GetComponent<CannonComponenet>() != null)
                    {
                        cannons.Add(child.gameObject);
                    }
                    else
                    {
                        foreach (Transform innerChild in child)
                        {
                            if (innerChild != null && innerChild.GetComponent<CannonComponenet>() != null)
                            {
                                cannons.Add(innerChild.gameObject);
                            }
                        }
                    }
                }
            }

            //Attack speed buff for additional cannons
            if (cannons.Count > 1)
                AtkSpeed = AtkSpeed / cannons.Count;

            //Debug.Log($"Total Cannons: = {cannons.Count}");
        }
        protected void StopAllCannonFireAnimations()
        {
            for (int i = 0; i < cannons.Count; i++)
            {
                CannonComponenet c = cannons[i].GetComponent<CannonComponenet>();

                if (c != null && c.GunFireParticles != null)
                    c.GunFireParticles.Stop();
            }
        }
        protected void Shoot(Vector3 targetVec)
        {
            Vector3 bulletStartPos = transform.position;

            //Logic for alternating fire between multiple cannons if the object has multiple.
            if (cannons.Count > 0)
            {
                CannonComponenet c = cannons[cannonIterator].GetComponent<CannonComponenet>();
                bulletStartPos = cannons[cannonIterator].transform.position;

                //If the cannon object has a particle effect then activate it
                if (c.GunFireParticles != null)
                {
                    c.GunFireParticles.Stop();

                    var playDir = c.GunFireParticles.main;
                    playDir.duration = 0.2f;

                    c.GunFireParticles.Play();
                }

                cannonIterator++;
                if (cannonIterator >= cannons.Count)
                    cannonIterator = 0;
            }

            GameObject bullet = Instantiate(bulletPrefab, bulletStartPos, Quaternion.identity);
            DamageProjectile bp = bullet.GetComponent<DamageProjectile>();
            bp.InitializeProjectile(targetVec, ProjectileSpeed, gameObject, Team);
            bp.SetDamage(AttackDamage);
            bp.transform.LookAt(targetVec);

            atkCooldown = AtkSpeed;
        }
    }
}