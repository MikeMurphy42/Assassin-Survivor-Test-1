//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExperianceLevelController : MonoBehaviour

{

    public static ExperianceLevelController instance;

    public ExpPickup pickup;

    public int currentExperiance;
    
    public List<int> expLevels;
    public int currentlevel = 1, levelCount = 100;

    public List<Weapon> weaponsToUpgrade;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetExp(int amountToGet)
    {
        currentExperiance += amountToGet;

        if (currentExperiance >= expLevels[currentlevel])
        {
            LevlUp();
        }
        
        UIController.instance.UpdateExperiance(currentExperiance, expLevels[currentlevel], currentlevel);
        
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    void LevlUp()
    {
        currentExperiance -= expLevels[currentlevel];
        
        currentlevel++;

        if (currentlevel >= expLevels.Count)
        {
            currentlevel = expLevels.Count - 1;
        }
        
        //PlayerController.instance.activeWeapon.LevelUp();
        
        UIController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f;
        
        //UIController.instance.LevelUpSelectionButtons[1].UpdateButtonDisplay(PlayerController.instance.activeWeapon);
        //UIController.instance.LevelUpSelectionButtons[0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[0]);
        
        //UIController.instance.LevelUpSelectionButtons[1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        //UIController.instance.LevelUpSelectionButtons[2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[1]);
        
        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        
        // Test (succeded)
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)// test (succeded)
        {
            availableWeapons.AddRange(PlayerController.instance.assignedWeapons);
        }
        
        if (availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)// test (succeded)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }
        
        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.LevelUpSelectionButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }
        
        for (int i = 0; i < UIController.instance.LevelUpSelectionButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.instance.LevelUpSelectionButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.LevelUpSelectionButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
