using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] TMP_Text damageText;

    [SerializeField] float lifetime;
    private float lifeCounter;

    [SerializeField] float floatSpeed = 1f;

    void Update()
    {
        if (lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;

            if (lifeCounter <= 0)
            {
                DamageNumberController.instance.PlaceInPool(this);
            }
        }

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void Setup(int damageDisplay)
    {
        lifeCounter = lifetime;
        damageText.text = damageDisplay.ToString();
    }
}
