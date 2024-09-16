using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusterTipMover : MonoBehaviour
{
    [SerializeField] float step = 25f;
    [SerializeField] bool dir;

    void Update()
    {
        float amount = step * Time.deltaTime;

        if (dir)
            transform.localPosition += new Vector3(0, 0, amount);
        else
            transform.localPosition += new Vector3(0, 0, -amount);


        if (transform.localPosition.z >= (2 + amount))
        {
            dir = false;
            transform.localPosition = new Vector3(0, 0, 2);
        }
        else if (transform.localPosition.z <= -2 - amount)
        {
            dir = true;
            transform.localPosition = new Vector3(0, 0, -2);
        }
    }
}
