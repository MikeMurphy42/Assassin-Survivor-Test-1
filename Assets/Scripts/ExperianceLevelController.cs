using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperianceLevelController : MonoBehaviour



{

    public static ExperianceLevelController instance;

    public ExpPickup pickup;

    private void Awake()
    {
        instance = this;
    }

    public int currentExperiance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int amountToGet)
    {
        currentExperiance += amountToGet;
    }

    public void SpawnExp(Vector3 position)
    {
        Instantiate(pickup, position, Quaternion.identity);
    }
    
    
}
