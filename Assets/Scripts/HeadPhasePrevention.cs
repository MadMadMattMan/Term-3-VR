using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HeadPhasePrevention : MonoBehaviour
{
    [SerializeField] PostProcessVolume visualEffect;

    private void OnTriggerEnter(Collider other)
    {
        visualEffect.weight = 1.0f;
    }
}
