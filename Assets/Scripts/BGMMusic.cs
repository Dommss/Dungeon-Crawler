using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMusic : MonoBehaviour
{
    public static BGMMusic instance;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] backgroundMusic;

    private void Start()
    {
        audioSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
        audioSource.Play();
    }
}
