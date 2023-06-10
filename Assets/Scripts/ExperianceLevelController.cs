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

    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

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
        
        UIController.instance.levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
        
        weaponsToUpgrade.Clear();
        List<Weapon> availableWeapons = new List<Weapon>();

        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);  // changed to unassignedWeapons
        }
        
        for (int i = 0; i < 3; i++)
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
