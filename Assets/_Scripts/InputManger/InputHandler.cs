using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AvatarRTS.Units.Player;

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

        // Start is called before the first frame update
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
                    if (AddedUnit(hit.transform, Input.GetKey(KeyCode.LeftShift)))
                    {
                        // be able to do stuff with units
                    }
                    else if (AddedBuilding(hit.transform))
                    {
                        // be able to do stuff with buildings
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
                foreach (Transform child in Player.PlayerManager.instance.playerUnits)
                {
                    foreach (Transform unit in child)
                    {
                        if (IsWithinSelectionBounds(unit))
                        {
                            AddedUnit(unit, true);
                        }
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
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 8: //Interactables Layer
                            //Determine which object was right clicked here and based on the unit clicking do a certain action
                            //for now, just move to whatever interactable object is there.
                            foreach (Transform unit in selectedUnits)
                            {
                                PlayerUnit playerUnit = unit.gameObject.GetComponent<PlayerUnit>();
                                if (playerUnit.baseStats is Units.UnitStatTypes.Healer)
                                {
                                    playerUnit.HandleUnitHeal(hit.transform);
                                }
                                else
                                {
                                    playerUnit.MoveUnit(hit.point);
                                }
                            }
                            break;
                        case 9: //Enemy Units Layer
                            //Attack / set as target
                            foreach (Transform unit in selectedUnits)
                            {
                                PlayerUnit playerUnit = unit.gameObject.GetComponent<PlayerUnit>();
                                if (playerUnit.baseStats is Units.UnitStatTypes.Offensive)
                                {
                                    playerUnit.HandleUnitAttack(hit.transform);
                                }
                                else
                                {
                                    playerUnit.MoveUnit(hit.point);
                                }
                            }
                            break;
                        default:
                            //isDragging = true;
                            foreach (Transform unit in selectedUnits)
                            {
                                PlayerUnit playerUnit = unit.gameObject.GetComponent<PlayerUnit>();
                                playerUnit.MoveUnit(hit.point);
                            }
                            break;
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

        private bool IsWithinSelectionBounds(Transform tf)
        {
            //if were not dragging, this shouldnt be called
            if (!isDragging)
            {
                return false;
            }

            Camera cam = Camera.main;
            Bounds viewPortBounds = Multiselect.GetViewPortBounds(cam, mousePos, Input.mousePosition);

            return viewPortBounds.Contains(cam.WorldToViewportPoint(tf.position));
        }

        private bool HaveSelectedUnits()
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }

            return false;
        }

        private Interactables.IUnit AddedUnit(Transform tf, bool canMultiselect = false)
        {
            Interactables.IUnit iUnit = tf.GetComponent<Interactables.IUnit>();
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
                return null;
            }
        }

        private Interactables.IBuilding AddedBuilding(Transform tf)
        {
            Interactables.IBuilding iBuilding = tf.GetComponent<Interactables.IBuilding>();
            if (iBuilding != null)
            {
                DeselectUnits();
                selectedBuilding = iBuilding.gameObject.transform;
                iBuilding.OnInteractEnter();

                return iBuilding;
            }
            else
            {
                return null;
            }
        }
    }
}
