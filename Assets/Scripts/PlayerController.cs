using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 movement;

    void Awake()
    {
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
            // rb.velocity = new Vector3(0f, 0f);
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
