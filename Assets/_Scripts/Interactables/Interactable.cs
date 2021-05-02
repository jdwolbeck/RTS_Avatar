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
                    string debugMessage = String.Empty;

                    PropertyInfo[] properties = baseObj.GetType().GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        if (type == typeof(string) || type == typeof(float) || type == typeof(double) || type == typeof(int) || type == typeof(bool))
                        {
                            object v = property.GetValue(baseObj, null);
                            string vStr = String.Empty;

                            if (v == null)
                                vStr = "[Null]";
                            else
                                vStr = v.ToString();

                            debugMessage += property.Name + ": " + vStr + Environment.NewLine;
                        }
                    }

                    if (!String.IsNullOrEmpty(debugMessage))
                    {
                        debugMessage = "======= PropDebug [Click To see] =======" + Environment.NewLine + "Object Type: " + baseObj.GetType() + Environment.NewLine + debugMessage;
                        DebugHandler.Print(debugMessage);
                    }
                }
            }
        }
    }
}