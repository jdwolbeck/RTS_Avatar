using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarRTS.Buildings
{
    public class BuildingHandler : HandlerBase
    {
        public static BuildingHandler instance;

        private void Awake()
        {
            instance = this;
            Debug.Log($"{gameObject.ToString()} Building handler");
        }

        private void Start()
        {
            SetScene();
        }
        private void SetScene()
        {
            //Place a predefined set of Buildings
            //CreateBuilding(TeamEnum.player, BuildingTypeEnum.Barracks, new Vector3(20, 0, -5), Quaternion.identity);
            //CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretBasic, new Vector3(16, 0, 3), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.Spawner, new Vector3(20, 0, -5), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretSentry, new Vector3(10, 0, 1), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretSentry, new Vector3(10, 0, -7), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretMammoth, new Vector3(9, 0, 3), Quaternion.identity);

            CreateBuilding(TeamEnum.enemy, BuildingTypeEnum.Spawner, new Vector3(-20, 0, -5), Quaternion.identity);
            CreateBuilding(TeamEnum.enemy, BuildingTypeEnum.TurretSentry, new Vector3(-10, 0, 1), Quaternion.identity);
            CreateBuilding(TeamEnum.enemy, BuildingTypeEnum.TurretSentry, new Vector3(-10, 0, -7), Quaternion.identity);
            CreateBuilding(TeamEnum.enemy, BuildingTypeEnum.TurretMammoth, new Vector3(-9, 0, 3), Quaternion.identity);
            CreateBuilding(TeamEnum.enemy, BuildingTypeEnum.TurretTesla, new Vector3(-8, 0, -12), Quaternion.identity);
        }
        private GameObject CreateBuilding(TeamEnum team, BuildingTypeEnum type, Vector3 position, Quaternion rotation)
        {
            Transform unitFolder;
            GameObject prefab, unitObject;

            prefab = Resources.Load("Prefabs/" + type.ToString(), typeof(GameObject)) as GameObject;
            unitObject = Instantiate(prefab, position, rotation);
            BasicObject bObj = unitObject.GetComponent<BasicObject>();
            //if(type.ToString().Contains("Turret"))
            //    unitObject.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0), Space.Self);

            switch (team)
            {
                case TeamEnum.player:
                    bObj.TeamColorMat = GameHandler.instance.ColorPlayer;
                    break;
                case TeamEnum.enemy:
                    bObj.TeamColorMat = GameHandler.instance.ColorEnemy;
                    break;
            }

            unitFolder = GameObject.FindWithTag(StringCapitolizeFirstLetter(team.ToString()) + "Buildings").transform;
            unitObject.transform.SetParent(unitFolder);
            unitObject.name = prefab.name;
            bObj.Team = team;
            bObj.InitializeObject();

            return unitObject;
        }
    }
    public enum BuildingTypeEnum
    {
        Unknown,
        Barracks,
        Spawner,
        TurretBasic,
        TurretSentry,
        TurretTesla,
        TurretMammoth
    }
}