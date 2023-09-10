using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public static ExperienceController instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("Experience")]
    [SerializeField] int currentExp;
    [SerializeField] ExpPickup pickup;

    [Header("Levels")]
    [SerializeField] List<int> expLevels;
    [SerializeField] int currentLevel = 1, levelCount = 100;

    [Header("Weapons")]
    [SerializeField] public List<Weapon> weaponsToUpgrade;

    private void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet)
    {
        currentExp += amountToGet;

        if (currentExp >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExp, expLevels[currentLevel], currentLevel);          // UI Controls
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    void LevelUp()
    {
        currentExp -= expLevels[currentLevel];
        currentLevel++;

        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }

        UIController.instance.levelUpPanel.SetActive(true);     // Trigger lvlup UI
        Time.timeScale = 0f;        // Pause Game
        // UIController.instance.levelUpButtons[0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[0]);
        // UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        // UIController.instance.levelUpButtons[2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[1]);

        weaponsToUpgrade.Clear();
        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);

        if (availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLevelledWeapons.Count < PlayerController.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        PlayerStatController.instance.UpdateDisplay();
    }
}
