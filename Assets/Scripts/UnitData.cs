using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    [Header("Metadata")]
    public string unitName = "Basic Name";
    public string unitDescription = "Basic Description";
    public Factions faction = Factions.None;
    public EntityType type = EntityType.Unit;
    public bool isSelected = false;

    [Header("Health Settings")]
    public double health = 100;
    public double maxHealth = 100;

    [Header("Shield Settings")]
    public double shield = 0;
    public double regenShield = 5;
    public double maxShield = 50;

    [Header("Movement Settings")]
    public bool isMoving = false;
    public double maxSpeed = 10;

    [Header("Team Settings")]
    public int ownerPlayer = 0;
    public int team = 0; //player can me in teams or alliances, no shooting your own teammates

    [Header("Attack Settings")]
    public double range = 0; // 0 for melee /// TO DO, ADD TO RANGE THE SIZE OF THE UNIT
    public double baseDamage = 0; // 
    public double healthDamage = 0; // bonus damage to hp, auto updated
    public double shieldDamage = 0; // bonus damage to shields, auto updated
    public AttackType attack = AttackType.None; // autoupdates stats
    void Start()
    {

        // auto change damage levels based on attack type
        switch (attack)
        {
            case AttackType.None:
                baseDamage = 0;
                healthDamage = 0;
                shieldDamage = 0;
                break;
            case AttackType.Rocket: // 50 % bonus damage to health, bypasses shield
                healthDamage += baseDamage * 50 / 100;
                baseDamage = 0;
                shieldDamage = 0;
                break;
            case AttackType.Laser: // 10 % bonus damage to health , -10% damage to shields
                healthDamage += baseDamage * 10 / 100;
                shieldDamage -= baseDamage * 10 / 100;
                break;
            case AttackType.Bullet: // 10 % bonus damage to shield , -10% damage to health
                shieldDamage += baseDamage * 10 / 100;
                healthDamage -= baseDamage * 10 / 100;
                break;
            case AttackType.Plasma: // no bonus damage, bonus damage will be added to base damage
                baseDamage += (healthDamage + shieldDamage);
                healthDamage = 0;
                shieldDamage = 0;

                break;

        }
        ChangeEntityType(type);
        if (!UnitSelection.Instance.unitList.Contains(gameObject))
        { UnitSelection.Instance.unitList.Add(gameObject); }

    }
    private void OnDisable()
    {
        // registers to the UnitSelection script
        UnitSelection.Instance.unitList.Remove(gameObject);
    }
    private void OnEnable()
    {
        // registers to the UnitSelection script
        if (!UnitSelection.Instance.unitList.Contains(gameObject))
            { UnitSelection.Instance.unitList.Add(gameObject); }

    }

    /// <summary>
    /// Auto updates the stats as well
    /// </summary>
    /// <param name="entityType"></param>
    public void ChangeEntityType(EntityType entityType)
    {

        type = entityType;
        // auto change stats based on EntityType
        switch (type)
        {
            case EntityType.Unit:
                // no changes
                break;
            case EntityType.Building: // no movement
                maxSpeed = 0;
                break;
            case EntityType.Obstacle: // no attack, no shield
                maxShield = 0;
                attack = AttackType.None;
                break;
            case EntityType.Rocket: // no range, rocketAttack type
                attack = AttackType.Rocket;
                range = 0;
                break;
        }
    }

   
}

public enum AttackType
{
    None,
    Plasma, // balanced
    Rocket, // can be dodged, bypasses shield, devastating to hp
    Laser, // bonus damage to hull
    Bullet, // bonus damage to shields
    //Spawn, 
    //Suicide,
    //Grenade, // area effect
    //Heal,
}

public enum Factions
{
    None,
    Mobilitate, // run the fastest (pigeons)
    Cantitate, // russian tactics (bees)
    Putere, // quality over quantity (dogs)
}

public enum EntityType
{ 

    Unit, // default
    Building, // cannot be moved 
    Obstacle, // no attack, no shield
    Rocket, // melee attack
}