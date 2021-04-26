using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AvatarRTS.InputManager;

namespace AvatarRTS.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;

        private void Awake()
        {
            instance = this;
            SetBasicStats(playerUnits);
            SetBasicStats(enemyUnits);
        }

        // Update is called once per frame
        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }

        public void SetBasicStats(Transform type)
        {
            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();
                    //TODO: remove this??? var stats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    if (type == playerUnits)
                    {
                        
                        Units.Player.PlayerUnit pU = tf.GetComponent<Units.Player.PlayerUnit>();
                        pU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }
                    else if (type == enemyUnits)
                    {
                        //set enemy stats
                        //playerUnit = unit.GetComponent<Player.PlayerUnit>();
                        Units.Enemy.EnemyUnit eU = tf.GetComponent<Units.Enemy.EnemyUnit>();
                        eU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }

                    //if we have any upgrades - add themnow
                    //add up
                }
            }
        }
    }
}
