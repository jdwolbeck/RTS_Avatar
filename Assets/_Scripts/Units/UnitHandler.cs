using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AvatarRTS.InputManager;

namespace AvatarRTS.Units
{
    public enum UnitTypeEnum
    {
        Worker,
        Warrior,
        Healer
    };

    public class UnitHandler : HandlerBase
    {
        public static UnitHandler instance;
        public LayerMask InteractablesLayer, EnemyUnitsLayer;
        public List<GameObject> unitList;

        private void Awake()
        {
            instance = this;
            unitList = new List<GameObject>();
        }

        private void Start()
        {
            InteractablesLayer = LayerMask.NameToLayer("Interactables");
            EnemyUnitsLayer = LayerMask.NameToLayer("EnemyUnits");

            SetScene();
        }

        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }

        private void SetScene()
        {
            // Place a predefined set of Player Units
            CreateUnit(TeamEnum.player, UnitTypeEnum.Worker, new Vector3(8, 0, -2), Quaternion.identity);
            CreateUnit(TeamEnum.player, UnitTypeEnum.Warrior, new Vector3(8, 0, 2), Quaternion.identity);
            CreateUnit(TeamEnum.player, UnitTypeEnum.Healer, new Vector3(10, 0, 0), Quaternion.identity);

            //Place a predefined set of Enemy Units
            CreateUnit(TeamEnum.enemy, UnitTypeEnum.Warrior, new Vector3(-8, 0, 0), Quaternion.identity);
        }

        public void CreateUnit(TeamEnum team, UnitTypeEnum type, Vector3 position, Quaternion rotation)
        {
            try
            {
                Transform unitFolder;
                GameObject prefab, unitObject;
                Material newMat;

                DebugHandler.Print($"Creating {team.ToString()} unit of type {type.ToString()}");
                prefab = Resources.Load("Prefabs/" + StringCapitolizeFirstLetter(type.ToString()), typeof(GameObject)) as GameObject;
                newMat = Resources.Load("Materials/" + team.ToString() + "UnitMat", typeof(Material)) as Material;
                
                unitObject = Instantiate(prefab, position, rotation);
                unitObject.GetComponent<Renderer>().material = newMat;
                unitObject.GetComponent<BasicObject>().Team = team;
                unitObject.GetComponent<BasicObject>().InitializeObject();
                unitObject.name = prefab.name;
                unitFolder = GameObject.FindWithTag(StringCapitolizeFirstLetter(team.ToString()) + "Units").transform;
                unitObject.transform.SetParent(unitFolder);

                if (team == TeamEnum.player)
                {
                    unitObject.layer = InteractablesLayer;
                }
                else
                {
                    unitObject.layer = EnemyUnitsLayer;
                }

                unitList.Add(unitObject);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
    }
}
