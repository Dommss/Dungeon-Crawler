using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public float currentHealth, maxHealth;
    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = PlayerStatController.instance.health[0].value;
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        healthSlider.value = currentHealth;
    }
}
