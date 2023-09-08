using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
    [SerializeField] float throwPower;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(rb.velocity.x)));
    }
}
