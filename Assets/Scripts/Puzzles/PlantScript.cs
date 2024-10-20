using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class PlantScript : MonoBehaviour
{
    [Header("Tree")]
    [SerializeField] bool growing = false;
    bool grown;
    [SerializeField] float rate = 0.00001f;
    [SerializeField] Transform tf;

    [Header("Key")]
    [SerializeField] GameObject key;



    void LateUpdate()
    {
        if (growing && tf.localScale.x < 0.0007f)
        {
            tf.localScale += growVector();
            //growing = false;
        }
        else if (growing && tf.localScale.x >= 0.0007f && !grown)
        {
            grown = true;
            TreeGrowingFinished();
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

    void TreeGrowingFinished()
    {
        key.SetActive(true);
    }

}
