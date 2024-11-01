using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    [SerializeField] AudioClip hitClip;
    AudioSource hitSource;

    private void Start()
    {
        hitSource = Instantiate(new AudioSource());
        hitSource.playOnAwake = false;
        hitSource.clip = hitClip;
        hitSource.volume = 0.8f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSource.Play();
    }
}
