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
        UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.activeWeapon);
    }
}
