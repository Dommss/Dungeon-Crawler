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

    public void GetExp(int amountToGet)
    {
        currentExp += amountToGet;
    }
}
