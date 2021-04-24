using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AvatarRTS.Units
{
    public class UnitStatDisplay : MonoBehaviour
    {
        public float maxHealth, armor, currentHealth;
        [SerializeField] private Image healthBarAmount;
        private bool isPlayerUnit = false;

        // Start is called before the first frame update
        void Start()
        {
            try
            {
                maxHealth = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.health;
                armor = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.armor;
                isPlayerUnit = true;
            }
            catch(Exception)
            {
                Debug.Log("No Player Unit, trying Enemy Unit...");
                try
                {
                    maxHealth = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.health;
                    armor = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.armor;
                    isPlayerUnit = false;
                }
                catch (Exception)
                {
                    Debug.Log("No Unit Scripts found!");
                }
            }

            currentHealth = maxHealth;
        }

        private void Update()
        {
            HandleHealth();
        }

        public void TakeDamage(float damage)
        {
            float totalDamage = damage - armor;
            currentHealth -= totalDamage;
        }

        private void HandleHealth()
        {
            Camera camera = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

            healthBarAmount.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0)
            {
                Debug.Log($"We died ({gameObject.transform.parent.gameObject.ToString()})!");
                Die();
            }
        }

        private void Die()
        {
            if (isPlayerUnit)
            {
                InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform);
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}