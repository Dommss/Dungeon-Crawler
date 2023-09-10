using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackWeapon : Weapon
{
    [SerializeField] EnemyDamager damager;
    [SerializeField] float attackCounter;
    private Vector2 direction;

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

        attackCounter -= Time.deltaTime;
        if (attackCounter <= 0)
        {
            attackCounter = stats[weaponLevel].fireRate;
            direction = PlayerController.instance.playerMovement;
            if (direction != null)
            {
                if (direction.x > 0)
                {
                    damager.transform.rotation = Quaternion.identity;
                }
                else
                {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
            }
            Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;
                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f, damager.transform.rotation.eulerAngles.z + rot), transform).gameObject.SetActive(true);
            }
            SFXManager.instance.PlaySFXPitched(1);
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;       // Damage scaler
        damager.lifeTime = stats[weaponLevel].duration;     // Lifetime scaler
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;         // Size scaler
        attackCounter = 0f;
    }
}
