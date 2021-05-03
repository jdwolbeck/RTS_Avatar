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
        public float RotationSpeed { get; set; }

        protected float atkCooldown;
        protected GameObject target;
        protected GameObject bulletPrefab;
        protected List<GameObject> projectiles;

        public ParticleSystem GunFireParticles;

        public override void Awake()
        {
            base.Awake();
            Cost = 250;
            MaxHealth = 2000;
            Armor = 5;

            AttackDamage = 15;
            AtkSpeed = 2f;
            ProjectileSpeed = 20f;
            ProjectileRange = 10f;
            RotationSpeed = 0.075f;

            bulletPrefab = Resources.Load("Prefabs/Bullet", typeof(GameObject)) as GameObject;
            projectiles = new List<GameObject>();

            if(GunFireParticles != null)
                GunFireParticles.Stop();
        }

        protected override void Update()
        {
            FindTarget();
            DoRotate();
            DoAttack();
            Cleanup();
        }

        public void FindTarget()
        {
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
                Vector3 calcTarget = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                float angle = gameObject.transform.rotation.eulerAngles.y;

                angle *= Mathf.Deg2Rad;

                calcTarget.x = ProjectileRange * Mathf.Sin(angle) + gameObject.transform.position.x;
                calcTarget.z = ProjectileRange * Mathf.Cos(angle) + gameObject.transform.position.z;

                Shoot(calcTarget);
            }
        }
        public void Cleanup()
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                if (projectiles[i].GetComponent<BasicProjectile>().HasReachedTarget())
                {
                    Destroy(projectiles[i]);
                    projectiles.RemoveAt(i);
                }
            }
        }

        protected void Shoot(Vector3 target)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BasicProjectile bp = bullet.GetComponent<BasicProjectile>();
            bp.SetTargetAndSpeed(target, ProjectileSpeed);

            projectiles.Add(bullet);

            atkCooldown = AtkSpeed;

            if (GunFireParticles != null)
            {
                GunFireParticles.Stop();

                var playDir = GunFireParticles.main;
                playDir.duration = 0.2f;

                GunFireParticles.Play();
            }
        }
    }
}