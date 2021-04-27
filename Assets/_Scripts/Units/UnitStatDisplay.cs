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
        public bool isPlayerUnit = false;

        private void Update()
        {
            HandleHealth();
        }

        private void HandleHealth()
        {
            Camera camera = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

            healthBarAmount.fillAmount = currentHealth / maxHealth;
        }
    }
}