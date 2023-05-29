using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{

    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;

    private Weapon assignWeapon;

    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        upgradeDescText.text = theWeapon.stats[theWeapon.weaponLvl].upgradeText;
        weaponIcon.sprite = theWeapon.icon;

        nameLevelText.text = theWeapon.name + " - LVL " + theWeapon.weaponLvl;

        assignWeapon = theWeapon;
    }


    public void SelectUpgrade()
    {
        if (assignWeapon != null)
        {
            assignWeapon.LevelUp();
            
            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = .2f;
        }
    }
}
