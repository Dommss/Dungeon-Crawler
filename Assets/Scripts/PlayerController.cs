using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

    }

    void OnMove(InputValue value)
    {
        _rigidbody.velocity = value.Get<Vector2>() * moveSpeed;
    }
}
