using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class PlantScript : MonoBehaviour
{
    [SerializeField] bool growing = false;
    [SerializeField] float rate = 0.00001f;
    [SerializeField] Transform tf;

    void LateUpdate()
    {
        if (growing && tf.localScale.x < 0.0007f)
        {
            tf.localScale += growVector();
            growing = false;
        }
    }

    public void ParticleTrigger()
    {
        growing = true;
    }

    Vector3 growVector()
    {
        return new Vector3(rate * Time.deltaTime, rate * Time.deltaTime, rate * Time.deltaTime);
    }
}
