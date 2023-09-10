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
        if (statsUpdated)
        {
            statsUpdated = false;
            SetStats();
        }

        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));

        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].fireRate;

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;
                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
            SFXManager.instance.PlaySFXPitched(2);
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;       // Damage scaler

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;         // Size scaler

        damager.timeBetweenSpawn = stats[weaponLevel].fireRate;         // Activator scale

        damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0f;
    }
}
