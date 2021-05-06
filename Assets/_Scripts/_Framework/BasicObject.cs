using AvatarRTS.Units;
using AvatarRTS.InputManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class BasicObject : MonoBehaviour
{
    public TeamEnum Team;
    public float Cost { get; set; }
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public float Armor { get; set; }
    public UnitStatDisplay HealthBar;

    public virtual void Awake()
    {
        Cost = MaxHealth = CurrentHealth = Armor = 0;
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnEnable()
    {
        CurrentHealth = MaxHealth;
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

    public Collider CheckForEnemyTargets(float maxDistance, bool findClosest)
    {
        float closest = float.MaxValue;
        Collider closestCollider = null;
        Collider[] rangeColliders = Physics.OverlapSphere(transform.position, maxDistance);

        for (int i = 0; i < rangeColliders.Length; i++)
        {
            try
            {
                if ((rangeColliders[i].gameObject.layer == UnitHandler.instance.InteractablesLayer ||
                    rangeColliders[i].gameObject.layer == UnitHandler.instance.EnemyUnitsLayer) &&
                    rangeColliders[i].gameObject.GetComponent<BasicObject>().Team != Team)
                {
                    //If you just want any target then return the first target that meets the criteria
                    if(!findClosest)
                        return rangeColliders[i];

                    float d = Vector3.Distance(rangeColliders[i].gameObject.transform.position, transform.position);

                    if (d < closest)
                    {
                        closest = d;
                        closestCollider = rangeColliders[i];
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"I = {i} exception {e.Message}" + Environment.NewLine + e.StackTrace);
            }
        }

        return closestCollider;
    }

    public string GetDebugString()
    {
        string debugMessage = String.Empty;

        PropertyInfo[] properties = GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            if (type == typeof(string) || type == typeof(float) || type == typeof(double) || type == typeof(int) || type == typeof(bool))
            {
                object v = property.GetValue(this, null);
                string vStr = String.Empty;

                if (v == null)
                    vStr = "[Null]";
                else
                    vStr = v.ToString();

                debugMessage += property.Name + ": " + vStr + Environment.NewLine;
            }
        }

        if (!String.IsNullOrEmpty(debugMessage))
        {
            debugMessage = "======= PropDebug [Click To see] =======" + Environment.NewLine + "Object Type: " + GetType() + Environment.NewLine + debugMessage;
        }

        return debugMessage;
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
