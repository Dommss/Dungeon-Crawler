using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Stats")]
    [SerializeField] public float pickupRange = 1.5f;
    [SerializeField] public float moveSpeed;
    [SerializeField] public Weapon activeWeapon;

    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 movement;

    void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movement != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + (Vector3)movement * Time.deltaTime * moveSpeed);
    }

    void OnMove(InputValue value)
    {
        Vector3 moveInput = value.Get<Vector2>() * moveSpeed;
        movement = moveInput;
    }
}
