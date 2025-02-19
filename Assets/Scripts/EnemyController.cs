using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Transform target;
    private Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] float health = 5f;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;
    [SerializeField] int expToGive = 1;
    [SerializeField] int coinValue = 1;
    [SerializeField] float coinDropRate = .5f;
    [SerializeField] float hitWaitTime = 1f;
    [SerializeField] float knockBackTime = .5f;

    private float knockBackCounter;
    private float hitCounter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerHealth.instance.transform;
    }

    void Update()
    {
        if (PlayerController.instance.gameObject.activeSelf == true)
        {
            if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }

                if (knockBackCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed * .5f);
                }
            }

            rb.velocity = (target.position - transform.position).normalized * moveSpeed;

            if (hitCounter > 0f)
            {
                hitCounter -= Time.deltaTime;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealth.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            Destroy(gameObject);
            ExperienceController.instance.SpawnExp(transform.position, expToGive);

            if (Random.value <= coinDropRate)
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }

            SFXManager.instance.PlaySFXPitched(10);
        }
        else
        {
            SFXManager.instance.PlaySFXPitched(9);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if (shouldKnockback)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
