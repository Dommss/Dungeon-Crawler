using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    [SerializeField] EnemyDamager damager;

    [SerializeField] float rotateSpeed;
    [SerializeField] Transform holder, fireballToSpawn;
    [SerializeField] float timeBetweenSpawn;

    float spawnCounter;

    private void Start()
    {
        SetStats();
    }

    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);
        }

        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;       // Damage scaler

        transform.localScale = Vector3.one * stats[weaponLevel].range;         // Size scaler

        timeBetweenSpawn = stats[weaponLevel].fireRate;         // Activator scale

        damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0f;
    }
}
