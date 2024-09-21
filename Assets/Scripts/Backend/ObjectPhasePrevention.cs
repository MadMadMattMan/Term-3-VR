using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhasePrevention : MonoBehaviour
{
    [SerializeField] bool nonImportant;
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

    private void OnCollisionEnter(Collision collision)
    {
        startPos = new Vector3(transform.position.x, 0.1f, transform.position.z);
        startRot = transform.rotation;
    }

}
