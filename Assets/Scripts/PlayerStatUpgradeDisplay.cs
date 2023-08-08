using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System;
public class PlayerStatUpgradeDisplay : MonoBehaviour
{


    public TMP_Text valueText, costText;

    public GameObject upgradeButton;

    public void UpdateDisplay(int cost, float oldValue, float newValue)
    {
        valueText.text = "Value: " + oldValue.ToString("F1") + "->" + newValue.ToString("F1");
        //valueText.text = "Value: " + Math.Ceiling(oldValue).ToString() + "->" + Math.Ceiling(newValue).ToString();

        costText.text = "Cost: " + cost;

        if (cost <= CoinController.instance.currentCoins)
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }
    }

    public void ShowMaxLVL()
    {
        valueText.text = "Max LVL";
        costText.text = "Max Lvl";
        upgradeButton.SetActive(false);
    }
    

}
