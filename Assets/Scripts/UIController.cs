using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider expLvlSlider;
    public TMP_Text expLvlText;

    public LevelUpSelectionButton[] LevelUpSelectionButtons;

    public GameObject levelUpPanel;

    public TMP_Text coinText;

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;

    public TMP_Text timerText;

    //public int a, b;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExperiance(int currentExp, int levelExp, int currentLvl)
    {
        expLvlSlider.maxValue = levelExp;
        expLvlSlider.value = currentExp;

        expLvlText.text = "LVL - " + currentLvl;
    }

    public void SkipLVLUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateCoins()
    {
        coinText.text = "coins - " + CoinController.instance.currentCoins;
    }
    
    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
    }
    
    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
    }
    
    public void PurchasePickUpRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
    }
    
    public void PurchaseMaxWeapons()
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }
    
}
