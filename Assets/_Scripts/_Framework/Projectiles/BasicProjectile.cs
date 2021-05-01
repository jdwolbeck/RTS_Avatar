using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public bool targetReached = false;
    private GameObject target;
    private float moveSpeed = 2.5f;

    public void GenerateHealOrb(GameObject obj)
    {
        targetReached = false;
        this.target = obj;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetDir = target.transform.position - this.transform.position;
            transform.position += targetDir * moveSpeed * Time.deltaTime;
            moveSpeed += 0.01f;

            //Our heal orb has gotten to the target, let the set the boolean
            if (Vector3.Distance(target.transform.position, this.transform.position) <= 1)
            {
                targetReached = true;
            }
        }
    }

    public bool HasReachedTarget()
    {
        return targetReached;
    }
}
