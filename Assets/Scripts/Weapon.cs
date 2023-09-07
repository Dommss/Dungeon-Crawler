using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public List<WeaponStats> stats;
    [SerializeField] public int weaponLevel;
    [SerializeField] public Sprite icon;
    [HideInInspector] public bool statsUpdated;

    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
            statsUpdated = true;

            if (weaponLevel >= stats.Count - 1)
            {
                PlayerController.instance.fullyLevelledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
}


[System.Serializable]
public class WeaponStats
{
    [SerializeField] public float speed, damage, range, fireRate, amount, duration;
    [SerializeField] public string upgradeText;
}
