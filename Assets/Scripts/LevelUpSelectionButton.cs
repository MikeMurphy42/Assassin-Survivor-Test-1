using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{

    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;

    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        upgradeDescText.text = theWeapon.stats[theWeapon.weaponLvl].upgradeText;
        weaponIcon.sprite = theWeapon.icon;

        nameLevelText.text = theWeapon.name + " - LVL " + theWeapon.weaponLvl;
    }
}