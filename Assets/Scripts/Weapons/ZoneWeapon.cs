using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    [SerializeField] EnemyDamager damager;
    [SerializeField] private float spawnTime, spawnCounter;

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = spawnTime;
            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;       // Damage scaler
        damager.lifeTime = stats[weaponLevel].duration;     // Lifetime scaler
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;         // Size scaler
        damager.timeBetweenSpawn = stats[weaponLevel].speed;         // Activator scaler
        spawnTime = stats[weaponLevel].fireRate;

        spawnCounter = 0f;
    }
}
