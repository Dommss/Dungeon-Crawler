using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private Transform target;
    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
