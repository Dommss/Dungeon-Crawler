using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public int coinAmount;
    [SerializeField] float moveSpeed;
    private bool movingToPlayer;

    [Header("Checks")]
    [SerializeField] float timeBetweenChecks;
    private float checkCounter;

    private PlayerController player;

    private void Start()
    {
        player = PlayerController.instance;
    }

    private void Update()
    {
        if (movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CoinController.instance.AddCoins(coinAmount);
            Destroy(gameObject);
        }
    }
}
