using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhasePrevention : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;

    float minY = -0.1f;

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Update()
    {
        if (transform.position.y < minY)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            transform.position = startPos;
            transform.rotation = startRot;
        }
    }

}
