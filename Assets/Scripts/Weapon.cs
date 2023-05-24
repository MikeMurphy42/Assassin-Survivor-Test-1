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
        }
    }
    
}

[System.Serializable]
public class WeaponStats
{
    public float speed, damage, range, timeBetweenAttacks, amount, duration, size;
    public string upgradeText;
}
