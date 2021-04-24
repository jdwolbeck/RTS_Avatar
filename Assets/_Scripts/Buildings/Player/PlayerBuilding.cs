using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static AvatarRTS.Buildings.BasicBuilding;

namespace AvatarRTS.Buildings.Player
{
    public class PlayerBuilding : MonoBehaviour
    {
        public buildingType bType;
        public BuildingStatTypes.Base baseStats;
        private float atkCooldown;

        public void Update()
        {
            switch (bType)
            {
                case buildingType.TurretSentry:
                    Rotate();
                    Attack();
                    break;
            }
        }

        public void Rotate()
        {
            BuildingStatTypes.Turret tStats = ((BuildingStatTypes.Turret)baseStats);
            gameObject.transform.Rotate(0f, tStats.rotationSpeed, 0f, Space.Self);
        }
        public void Attack()
        {
            if (atkCooldown > 0)
            {
                atkCooldown -= Time.deltaTime;
            }

            if (atkCooldown <= 0)
            {
                BuildingStatTypes.Turret tStats = ((BuildingStatTypes.Turret)baseStats);
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
                nma.speed = tStats.projectileSpeed;
                nma.acceleration = 999999f;

                atkCooldown = tStats.atkSpeed;
            }
        }
    }
}
