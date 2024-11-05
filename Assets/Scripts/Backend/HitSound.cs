using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    [SerializeField] AudioClip hitClip;
    AudioSource hitSource;

    private void Start()
    {
        try
        {
            hitSource = GetComponent<AudioSource>();
            hitSource.playOnAwake = false;
            hitSource.clip = hitClip;
            hitSource.volume = 0.8f;
        }
        catch
        {
            Debug.LogWarning("No Audio for object");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitSource != null)
        {
            hitSource.Play();
        }
    }
}
