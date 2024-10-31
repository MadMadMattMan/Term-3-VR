using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHit : MonoBehaviour
{
    ParticleSystem ParticleSystem;
    AudioSource AudioSource;

    private void Start()
    {
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Get Color
        //ParticleSystem.startColor = Color.black;
        ParticleSystem.Play();
        AudioSource.Play();
    }
}
