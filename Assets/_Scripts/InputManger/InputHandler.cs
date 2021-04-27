using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AvatarRTS.Units;
using AvatarRTS.Buildings;

namespace AvatarRTS.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;
        private RaycastHit hit; //What we hit with our ray
        public List<Transform> selectedUnits = new List<Transform>();
        public Transform selectedBuilding = null;
        public LayerMask interactableLayer = new LayerMask();
        private bool isDragging = false;
        private Vector3 mousePos;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
        }

        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = Multiselect.GetScreenRect(mousePos, Input.mousePosition);
                Multiselect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                Multiselect.DrawScreenRectBorder(rect, 3, Color.blue);
            }
        }

        public void HandleUnitMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;

                //Create a ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                //Check if we hit something
                if (Physics.Raycast(ray, out hit, 100, interactableLayer))
                {
                    if (hit.transform.GetComponent<BasicObject>() is BasicUnit)
                    {
                        // be able to do stuff with units
                        TargetUnit(hit.transform.gameObject, Input.GetKey(KeyCode.LeftShift));
                    }
                    else if (hit.transform.GetComponent<BasicObject>() is BasicBuilding)
                    {
                        // be able to do stuff with buildings
                        TargetBuilding(hit.transform.gameObject);
                    }
                }
                else
                {
                    isDragging = true;
                    DeselectUnits();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                foreach (GameObject unit in Units.UnitHandler.instance.unitList)
                {
                    if (unit.layer == UnitHandler.instance.InteractablesLayer && IsWithinSelectionBounds(unit))
                    {
                        TargetUnit(unit, true);
                    }
                }
                isDragging = false;
            }

            if (Input.GetMouseButtonDown(1) && HaveSelectedUnits())
            {
                //Create a ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Check if we hit something
                if (Physics.Raycast(ray, out hit))
                {
                    //If we do, then do something with that data
                    try
                    {
                        foreach (Transform unit in selectedUnits)
                        {
                            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactables") ||
                            hit.transform.gameObject.layer == LayerMask.NameToLayer("EnemyUnits"))
                            {
                                unit.GetComponent<BasicObject>().HandlePlayerAction(hit);
                            }
                            else
                            {
                                unit.gameObject.GetComponent<BasicUnit>().MoveUnit(hit.point);
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log($"{e.Message}    -     {e.StackTrace}");
                    }
                }
            }
        }

        private void DeselectUnits()
        {
            if (selectedBuilding != null)
            {
                selectedBuilding.gameObject.GetComponent<Interactables.IBuilding>().OnInteractExit();
                selectedBuilding = null;
            }
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].gameObject.GetComponent<Interactables.IUnit>().OnInteractExit();
            }
            selectedUnits.Clear();
        }

        private bool IsWithinSelectionBounds(GameObject obj)
        {
            //if were not dragging, this shouldnt be called
            if (!isDragging)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds viewPortBounds = Multiselect.GetViewPortBounds(cam, mousePos, Input.mousePosition);

            return viewPortBounds.Contains(cam.WorldToViewportPoint(obj.transform.position));
        }

        private bool HaveSelectedUnits()
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }

            return false;
        }

        private Interactables.IUnit TargetUnit(GameObject obj, bool canMultiselect = false)
        {
            Interactables.IUnit iUnit = obj.GetComponent<Interactables.IUnit>();
            if (iUnit != null)
            {
                if (!canMultiselect)
                {
                    DeselectUnits();
                }
                if (selectedBuilding != null)
                {
                    selectedBuilding.GetComponent<Interactables.IBuilding>().OnInteractExit();
                    selectedBuilding = null;
                }
                
                selectedUnits.Add(iUnit.gameObject.transform);

                iUnit.OnInteractEnter();

                return iUnit;
            }
            else
            {
                Debug.Log($"iUnit was null when selecting unit with TargetUnit() function");
                return null;
            }
        }

        private Interactables.IBuilding TargetBuilding(GameObject obj)
        {
            Interactables.IBuilding iBuilding = obj.GetComponent<Interactables.IBuilding>();
            if (iBuilding != null)
            {
                Debug.Log($"iBuilding script found!");
                DeselectUnits();
                selectedBuilding = iBuilding.gameObject.transform;
                iBuilding.OnInteractEnter();

                return iBuilding;
            }
            else
            {
                Debug.Log($"iBuilding was null when selecting unit with TargetBuilding() function");
                return null;
            }
        }
    }
}
