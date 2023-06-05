using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public List<WeaponStats> stats;
    public int weaponLvl;

    [HideInInspector]
    public bool statsUpdated;

    public Sprite icon;
    
    public void LevelUp()
    {
        if (weaponLvl < stats.Count - 1)
        {
            weaponLvl++;

            statsUpdated = true;

            if (weaponLvl >= stats.Count - 1)
            {
                PlayerController.instance.fullyLevelledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
    
}

[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, timeBetweenAttacks, amount, duration;
    public string upgradeText;
}
