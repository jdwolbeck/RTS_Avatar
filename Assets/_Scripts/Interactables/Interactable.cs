using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AvatarRTS.Interactables
{
    public class Interactable : MonoBehaviour
    {
        public bool isInteracting = false;
        public GameObject highlight = null;

        public virtual void Awake()
        {
            HideHighlight();
        }

        public virtual void OnInteractEnter()
        {
            ShowHighlight();
            isInteracting = true;
        }

        public virtual void OnInteractExit()
        {
            HideHighlight();
            isInteracting = false;
        }

        public virtual void ShowHighlight()
        {
            highlight.SetActive(true);
            DebugLogUnitInfo();
        }

        public virtual void HideHighlight()
        {
            highlight.SetActive(false);
        }

        private void DebugLogUnitInfo()
        {
            if (DebugHandler.enableClickPropInfo)
            {
                BasicObject baseObj = gameObject.GetComponent<BasicObject>();

                if (baseObj != null)
                {
                    string debugMsg = baseObj.GetDebugString();

                    if (!String.IsNullOrEmpty(debugMsg))
                    {
                        DebugHandler.Print(debugMsg);
                    }
                }
            }
        }
    }
}