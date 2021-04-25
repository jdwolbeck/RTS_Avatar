using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AvatarRTS.Player;
using System;

namespace AvatarRTS.Units
{
    public class UnitHandler : MonoBehaviour
    {

        public enum team
        {
            player,
            enemy
        }

        public enum unitType
        {
            worker,
            warrior,
            healer
        };

        public static UnitHandler instance;
        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
            Debug.Log($"Unit handler {this.gameObject.ToString()}");
        }

        private void Start()
        {
            pUnitLayer = LayerMask.NameToLayer("Interactables");
            eUnitLayer = LayerMask.NameToLayer("EnemyUnits");

            SetScene();
        }

        private void SetScene()
        {
            // Place a predefined set of Player Units
            CreateUnit(team.player, unitType.healer, new Vector3(2, 0, 5), Quaternion.identity);

            //Place a predefined set of Enemy Units

            //Place a predefined set of Building
        }

        public UnitStatTypes.Base GetBasicUnitStats(string type)
        {
            switch (type)
            {
                case "worker":
                    return new UnitStatTypes.Worker();
                case "warrior":
                    return new UnitStatTypes.Warrior();
                case "healer":
                    return new UnitStatTypes.Healer();
                default:
                    DebugHandler.Print($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
        }

        public void CreateUnit(team team, unitType type, Vector3 position, Quaternion rotation)
        {
            Transform unitFolder;
            string teamString;
            GameObject prefab, unitObject;
            UnitStatDisplay unitStatDisplay;
            Units.Player.PlayerUnit playerUnit;
            Units.Enemy.EnemyUnit enemyUnit;

            //Obtain the correct prefab based on the team
            switch (team)
            {
                case team.player:
                    teamString = "Player ";
                    prefab = Resources.Load("Prefabs/humanUnit", typeof(GameObject)) as GameObject;
                    unitObject = Instantiate(prefab, position, rotation);
                    playerUnit = prefab.transform.GetComponent<Units.Player.PlayerUnit>();
                    playerUnit.baseStats = GetBasicUnitStats(type.ToString());
                    break;
                case team.enemy:
                    teamString = "Enemy ";
                    prefab = Resources.Load("Prefabs/infectedUnit", typeof(GameObject)) as GameObject;
                    unitObject = Instantiate(prefab, position, rotation);
                    enemyUnit = prefab.transform.GetComponent<Units.Enemy.EnemyUnit>();
                    enemyUnit.baseStats = GetBasicUnitStats(type.ToString());
                    break;
                default:
                    DebugHandler.Print($"Team enum {team} not defined!");
                    return;
            }

            //Spawn in the prefab GameObject at the specific location/rotation

            unitStatDisplay = prefab.transform.GetComponentInChildren<UnitStatDisplay>();
            unitStatDisplay.InitializeUnitStatDisplay();

            //Based on the type of unit we are creating, put it in the right folder and give it its stats.
            switch (type)
            {
                case unitType.worker:
                    unitFolder = GameObject.FindWithTag(teamString + "Workers").transform;
                    break;
                case unitType.warrior:
                    unitFolder = GameObject.FindWithTag(teamString + "Warriors").transform;
                    break;
                case unitType.healer:
                    unitFolder = GameObject.FindWithTag(teamString + "Healers").transform;
                    break;
                default:
                    DebugHandler.Print($"Type enum {type} not defined!");
                    return;
            }

            unitObject.name = prefab.name;
            unitObject.transform.SetParent(unitFolder);
        }
    }
}
