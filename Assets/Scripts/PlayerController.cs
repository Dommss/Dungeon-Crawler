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
        // Vector3 moveInput = new Vector3(0f, 0f, 0f);
        // moveInput.x = Input.GetAxisRaw("Horizontal");
        // moveInput.y = Input.GetAxisRaw("Vertical");
        // moveInput.Normalize();
        // transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (movement != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void OnMove(InputValue value)
    {
        rb.velocity = value.Get<Vector2>() * moveSpeed;
        movement = rb.velocity;
    }
}
