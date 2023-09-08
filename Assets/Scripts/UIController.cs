using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public Slider explvlSlider;
    public TMP_Text explvlText;
    public LevelUpSelectionButton[] levelUpButtons;
    public GameObject levelUpPanel;
    public TMP_Text coinText;

    public void UpdateExperience(int currentExp, int levelExp, int currentLvl)
    {
        explvlSlider.maxValue = levelExp;           // updating the EXP slider
        explvlSlider.value = currentExp;

        explvlText.text = "Level: " + currentLvl.ToString();            // updating the lvl text
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }
}
