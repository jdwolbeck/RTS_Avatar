using AvatarRTS.Units;
using AvatarRTS.InputManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObject : MonoBehaviour
{
    public TeamEnum Team;
    public float Cost = 0, MaxHealth = 0, CurrentHealth = 0, Armor = 0;
    public UnitStatDisplay HealthBar;

    public virtual void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void InitializeObject()
    {
        HealthBar = GetComponentInChildren<UnitStatDisplay>();
        HealthBar.maxHealth = MaxHealth;
        HealthBar.currentHealth = MaxHealth;
        HealthBar.armor = Armor;
    }

    public virtual void HandlePlayerAction(RaycastHit hit)
    {

    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage - Armor;
        HealthBar.currentHealth = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            DebugHandler.Print($"We died ({gameObject.transform.parent.gameObject.ToString()})!");
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        HealthBar.currentHealth = CurrentHealth;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}

public enum TeamEnum
{
    player,
    enemy
}
