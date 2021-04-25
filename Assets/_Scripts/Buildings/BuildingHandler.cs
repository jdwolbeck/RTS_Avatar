using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;

        private void Awake()
        {
            instance = this;
        }
    }
}