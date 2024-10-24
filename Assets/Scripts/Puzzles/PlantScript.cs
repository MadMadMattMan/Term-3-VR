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

    [Header("Dev Tools")]
    [SerializeField] bool pourBypass;



    void LateUpdate()
    {
        //if growing enabled and object smaller than max size, increase size
        if (growing && tf.localScale.x < 0.0007f)
        {
            tf.localScale += growVector();
            if (!pourBypass)
                growing = false;
        }
        //else if the first frame after size is bigger than max size set state to grown
        else if (growing && tf.localScale.x >= 0.0007f && !grown)
        {
            grown = true;
            TreeGrowingFinished();
        }
    }

    //Get from watering can, if a patricle hits the soil
    public void ParticleTrigger()
    {
        growing = true;
    }

    //Grow calculation
    Vector3 growVector()
    {
        return new Vector3(rate * Time.deltaTime, rate * Time.deltaTime, rate * Time.deltaTime);
    }

    void TreeGrowingFinished()
    {
        key.SetActive(true);
    }

}
