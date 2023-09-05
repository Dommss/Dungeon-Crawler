using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private CircleCollider2D trigger;

    [Header("Stats")]
    [SerializeField] float health = 5f;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;
    [SerializeField] float hitWaitTime = 1f;
    [SerializeField] float knockBackTime = .5f;

    [Header("Duplication")]
    [SerializeField] bool shouldDuplicate;
    [SerializeField] GameObject toDuplicate;

    [Header("Immunity")]
    [SerializeField] public bool shouldImmune;
    public float immuneCounter;

    private float knockBackCounter;
    private float hitCounter;

    void Awake()
    {
        trigger = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        target = PlayerHealth.instance.transform;
    }

    private void Start()
    {
        if (shouldImmune)
        {
            immuneCounter = 0.75f;
        }
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

        if (immuneCounter > 0f)
        {
            immuneCounter -= Time.deltaTime;
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
        if (immuneCounter <= 0)
        {
            health -= damageToTake;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            ExperienceController.instance.SpawnExp(transform.position);

            if (shouldDuplicate)
            {
                for (var i = 0; i < 3; i++)
                {
                    Instantiate(toDuplicate, this.transform.position * .5f, Quaternion.identity);
                }
            }
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if (shouldKnockback && immuneCounter <= 0f)
        {
            knockBackCounter = knockBackTime;
        }
    }
}
