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
    [SerializeField] public List<Weapon> unassignedWeapons, assignedWeapons;
    [SerializeField] public int maxWeapons = 3;

    [HideInInspector] public List<Weapon> fullyLevelledWeapons = new List<Weapon>();

    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 movement;

    void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        AddWeapon(Random.Range(0, unassignedWeapons.Count));
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

    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);       // Assigns random weapon from the list to player
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);     // Sets the weapon active in the scene
            unassignedWeapons.RemoveAt(weaponNumber);       // Removes the weapon from unassignedWeapons list
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);
        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
