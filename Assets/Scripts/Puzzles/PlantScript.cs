using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    [SerializeField] bool growing;
    [SerializeField] float rate = 0.00001f;
    [SerializeField] Transform tf;
    void Update()
    {
        if (growing && tf.localScale.x < 0.0007f)
        {
            tf.localScale += growVector();
        }
    }

    public void RecevingWaterChange(bool started)
    {
       growing = started;
    }

    Vector3 growVector()
    {
        return new Vector3(rate * Time.deltaTime, rate * Time.deltaTime, rate * Time.deltaTime);
    }
}
