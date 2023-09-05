using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public static ExperienceController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] int currentExp;
    [SerializeField] ExpPickup pickup;

    public void GetExp(int amountToGet)
    {
        currentExp += amountToGet;
    }

    public void SpawnExp(Vector3 position)
    {
        Instantiate(pickup, position, Quaternion.identity);
    }
}
