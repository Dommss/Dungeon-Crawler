using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    [SerializeField] public float damageAmount;
    [SerializeField] public float lifeTime, growSpeed = 2.5f;
    [SerializeField] bool shouldKnockback;
    [SerializeField] bool destroyParent;

    Vector3 targetSize;

    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockback);
        }
    }
}
