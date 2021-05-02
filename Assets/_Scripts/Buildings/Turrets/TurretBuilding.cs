using AvatarRTS.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Buildings
{
    //[CreateAssetMenu(fileName = "Building", menuName = "New Building/Turret")]
    public class TurretBuilding : BasicBuilding
    {
        public float AttackDamage { get; set; }
        public float AtkSpeed { get; set; }
        public float ProjectileSpeed { get; set; }
        public float ProjectileRange { get; set; }
        public float RotationSpeed { get; set; }

        private float atkCooldown;
        private GameObject bulletPrefab;
        private List<GameObject> projectiles;

        public override void Awake()
        {
            base.Awake();
            Cost = 250;
            MaxHealth = 500;
            Armor = 1;

            AttackDamage = 15;
            AtkSpeed = 0.3f;
            ProjectileSpeed = 20f;
            ProjectileRange = 10f;
            RotationSpeed = 0.075f;

            bulletPrefab = Resources.Load("Prefabs/Bullet", typeof(GameObject)) as GameObject;
            projectiles = new List<GameObject>();
        }

        public void Update()
        {
            DoRotate();
            DoAttack();
            Cleanup();
        }

        public void DoRotate()
        {
            if(RotationSpeed > 0)
                gameObject.transform.Rotate(0f, RotationSpeed, 0f, Space.Self);
        }
        public void DoAttack()
        {
            if (atkCooldown > 0)
            {
                atkCooldown -= Time.deltaTime;
            }

            if (atkCooldown <= 0)
            {
                Vector3 calcTarget = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                float angle = gameObject.transform.rotation.eulerAngles.y;

                angle -= 90;
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

        private void Shoot(Vector3 target)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            BasicProjectile bp = bullet.GetComponent<BasicProjectile>();
            bp.SetTargetAndSpeed(target, ProjectileSpeed);

            projectiles.Add(bullet);

            atkCooldown = AtkSpeed;
        }
    }
}