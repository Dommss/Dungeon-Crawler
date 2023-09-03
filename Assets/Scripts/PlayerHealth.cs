using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    private void Awake()
    {
        instance = this;
    }


    [SerializeField] private float currentHealth, maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
    }
}
