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
        public float AttackDamage = 15,
             AtkSpeed = 1,
             ProjectileSpeed = 20,
             RotationSpeed = 0.05f;

        private float atkCooldown;

        public TurretBuilding()
        {
            Health = 500;
            Armor = 1;
        }

        public void Update()
        {
            DoRotate();
            DoAttack();
        }

        public void DoRotate()
        {
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
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Material newMat = Resources.Load(@"Materials/bulletMat", typeof(Material)) as Material;
                sphere.GetComponent<Renderer>().material = newMat;

                sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                //sphere.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-7, -3), gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-3, 3));
                sphere.transform.position = gameObject.transform.position;

                sphere.AddComponent<NavMeshAgent>();
                NavMeshAgent nma = sphere.GetComponent<NavMeshAgent>();
                //nma.velocity = new Vector3(-1,0,0);
                nma.SetDestination(new Vector3(gameObject.transform.position.x + Random.Range(-50, 0), gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-50, -10)));
                nma.speed = ProjectileSpeed;
                nma.acceleration = 999999f;

                atkCooldown = AtkSpeed;
            }
        }
    }
}