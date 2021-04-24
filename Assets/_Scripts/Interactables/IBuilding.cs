using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Interactables
{
    public class IBuilding : Interactable
    {
        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
            //Add stuff along with general Interactable function
            
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Material newMat = Resources.Load(@"Materials/bulletMat", typeof(Material)) as Material;
            sphere.GetComponent<Renderer>().material = newMat;

            sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            sphere.transform.position = new Vector3(gameObject.transform.position.x + Random.Range(-7, -3), gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-3, 3));
 
            sphere.AddComponent<NavMeshAgent>();
            NavMeshAgent nma = sphere.GetComponent<NavMeshAgent>();
            nma.SetDestination(new Vector3(gameObject.transform.position.x + Random.Range(-50, 0), gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-50, 0)));
            nma.speed = 20f;
            nma.acceleration = 999999f;
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }
    }
}