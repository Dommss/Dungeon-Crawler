using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    private Transform target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
    }


    void Update()
    {
        rb.velocity = (target.position - transform.position).normalized * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
    }
}
