using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : Weapon
{

    [SerializeField] EnemyDamager damager;
    private float throwCounter;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }

        throwCounter -= Time.deltaTime;
        if (throwCounter <= 0)
        {
            throwCounter = stats[weaponLevel].fireRate;

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);
            }

            SFXManager.instance.PlaySFXPitched(6);
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;       // Damage scaler
        damager.lifeTime = stats[weaponLevel].duration;     // Lifetime scaler
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;         // Size scaler
        throwCounter = 0f;
    }
}
