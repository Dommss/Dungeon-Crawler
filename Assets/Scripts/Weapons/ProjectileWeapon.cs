using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] EnemyDamager damager;
    [SerializeField] Projectile projectile;
    private float shotCounter;

    [SerializeField] float weaponRange;
    [SerializeField] LayerMask whatIsEnemy;

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

        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            shotCounter = stats[weaponLevel].fireRate;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            if (enemies.Length > 0)
            {
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                }

                SFXManager.instance.PlaySFXPitched(Random.Range(3, 5));
            }
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;       // Damage scaler
        damager.lifeTime = stats[weaponLevel].duration;     // Lifetime scaler
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;         // Size scaler
        shotCounter = 0f;
        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}
