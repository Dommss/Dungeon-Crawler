using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{

    [SerializeField] TMP_Text damageText;

    [SerializeField] float lifetime;
    private float lifeCounter;

    void Start()
    {
        lifeCounter = lifetime;
    }


    void Update()
    {
        if (lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;

            if (lifeCounter <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Setup(int damageDisplay)
    {
        lifeCounter = lifetime;

        damageText.text = damageDisplay.ToString();
    }
}
