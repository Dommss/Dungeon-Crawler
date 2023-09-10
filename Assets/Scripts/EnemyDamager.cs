using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    [SerializeField] public float damageAmount;
    [SerializeField] public float lifeTime, growSpeed = 2.5f;
    [SerializeField] bool shouldKnockback;
    [SerializeField] bool destroyParent;
    [SerializeField] bool destroyOnImpact;

    [Header("DOT")]
    [SerializeField] public bool damageOverTime;
    [SerializeField] public float timeBetweenSpawn;
    [SerializeField] private float damageCounter;
    [SerializeField] private List<EnemyController> enemiesInRange = new List<EnemyController>();

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

                if (destroyParent == true)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if (damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {
                damageCounter = timeBetweenSpawn;

                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockback);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (damageOverTime == false)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockback);

                if (destroyOnImpact == true)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (other.tag == "Enemy")
            {
                enemiesInRange.Add(other.GetComponent<EnemyController>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (damageOverTime == true)
        {
            if (other.tag == "Enemy")
            {
                enemiesInRange.Remove(other.GetComponent<EnemyController>());
            }
        }
    }
}
