using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarRTS.Interactables
{
    public class IBuilding : Interactable
    {
        public override void OnInteractEnter()
        {
            base.OnInteractEnter();
            //Add stuff along with general Interactable function
        }

        public override void OnInteractExit()
        {
            base.OnInteractExit();
        }
    }
}