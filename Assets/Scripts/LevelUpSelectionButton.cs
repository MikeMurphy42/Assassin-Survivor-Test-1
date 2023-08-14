using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;

    private Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        if (theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLvl].upgradeText;
            weaponIcon.sprite = theWeapon.icon;
            nameLevelText.text = theWeapon.name + " - LVL " + theWeapon.weaponLvl;
        }
        else
        {
            upgradeDescText.text = "Unlock " + theWeapon.name;
            weaponIcon.sprite = theWeapon.icon;
            nameLevelText.text = theWeapon.name;
        }

        assignedWeapon = theWeapon;
    }

    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            if (assignedWeapon.gameObject.activeSelf == true)
            {
                assignedWeapon.LevelUp();
            }
            else
            {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }

            UIController.instance.levelUpPanel.SetActive(false);
            UIController.instance.StartSkipLevelTransition(); // Call the StartSkipLevelTransition method from UIController
        }
    }
}