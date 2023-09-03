using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;

    [SerializeField] float health = 5f;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;
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
        }
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
