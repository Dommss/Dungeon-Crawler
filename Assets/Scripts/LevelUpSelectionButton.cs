using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    [SerializeField] public TMP_Text upgradeDescText, nameLevelText;
    [SerializeField] public Image weaponIcon;
    private Weapon assignedWeapon;

    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        if (theWeapon.gameObject.activeSelf == true)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;          // Description updater
            weaponIcon.sprite = theWeapon.icon;         // Icon updater
            nameLevelText.text = theWeapon.name + " - Level: " + (theWeapon.weaponLevel + 1);         // Name updater
        }
        else
        {
            upgradeDescText.text = "Unlock " + theWeapon.name + "!";        // Changes the description of a new unlock
            weaponIcon.sprite = theWeapon.icon;
            nameLevelText.text = theWeapon.name;        // Refurbishes the name of the unlock so that it doesn't say the level of it
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
            Time.timeScale = 1f;
        }
    }
}
