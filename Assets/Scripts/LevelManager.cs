using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    
    private void Awake()
    {
        instance = this;
    }

    
    private bool gameActive;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

    public void EndLVL()
    {
        
    }
    
}
