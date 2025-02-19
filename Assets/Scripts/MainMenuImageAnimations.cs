using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuImageAnimations : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    [SerializeField] private float speed;
    [SerializeField] private float minSize, maxSize;
    private float activeSize;

    void Start()
    {
        activeSize = maxSize;
        speed *= Random.Range(0.75f, 1.25f);
    }


    void Update()
    {
        sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * activeSize, speed * Time.deltaTime);

        if (sprite.localScale.x == activeSize)
        {
            if (activeSize == maxSize)
            {
                activeSize = minSize;
            }
            else
            {
                activeSize = maxSize;
            }
        }
    }
}

