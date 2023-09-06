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
        upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;          // Description updater

        weaponIcon.sprite = theWeapon.icon;         // Icon updater

        nameLevelText.text = theWeapon.name + " - Level: " + theWeapon.weaponLevel;         // Name updater

        assignedWeapon = theWeapon;
    }

    public void SelectUpgrade()
    {
        if (assignedWeapon != null)
        {
            assignedWeapon.LevelUp();
            UIController.instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
