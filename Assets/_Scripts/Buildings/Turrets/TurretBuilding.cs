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
        protected float AttackDamage = 15,
             AtkSpeed = 1,
             ProjectileSpeed = 20f,
             RotationSpeed = 0.05f;

        private float atkCooldown;
        private GameObject bulletPrefab;
        private List<GameObject> projectiles;

        public override void Awake()
        {
            base.Awake();
            Cost = 250;
            MaxHealth = 500;
            Armor = 1;
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
                Shoot(new Vector3(gameObject.transform.position.x + Random.Range(-50, 0), gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-50, -10)));
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