using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float hitWaitTime = 1f;

    private float hitCounter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerHealth.instance.transform;
    }


    void Update()
    {
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
}
