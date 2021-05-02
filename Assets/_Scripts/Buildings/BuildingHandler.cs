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
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.Barracks, new Vector3(20, 0, -5), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretPack1, new Vector3(20, 0, 5), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretPack2, new Vector3(16, 0, 3), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretPack3, new Vector3(14, 0, 1), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretPack4, new Vector3(12, 0, 5), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretPack5, new Vector3(14, 0, 3), Quaternion.identity);
            CreateBuilding(TeamEnum.player, BuildingTypeEnum.TurretPack6, new Vector3(20, 0, 1), Quaternion.identity);
        }
        private void CreateBuilding(TeamEnum team, BuildingTypeEnum type, Vector3 position, Quaternion rotation)
        {
            Transform unitFolder;
            GameObject prefab, unitObject;

            prefab = Resources.Load("Prefabs/" + type.ToString(), typeof(GameObject)) as GameObject;
            unitObject = Instantiate(prefab, position, rotation);

            unitObject.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0), Space.Self);

            //TODO Logic for building health bars
            //unitStatDisplay = prefab.transform.GetComponentInChildren<UnitStatDisplay>();
            //unitStatDisplay.InitializeUnitStatDisplay();
            unitFolder = GameObject.FindWithTag(StringCapitolizeFirstLetter(team.ToString()) + "Buildings").transform;
                
            unitObject.transform.SetParent(unitFolder);
            unitObject.name = prefab.name;
        }
    }
    public enum BuildingTypeEnum
    {
        Unknown,
        Barracks,
        TurretSentry,
        TurretPack1,
        TurretPack2,
        TurretPack3,
        TurretPack4,
        TurretPack5,
        TurretPack6
    }
}