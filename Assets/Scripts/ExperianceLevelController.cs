using System;
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
        
        PlayerController.instance.activeWeapon.LevelUp();
        
    }
    
}
