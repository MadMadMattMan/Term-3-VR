using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject lockObj;
    [SerializeField] Transform snapPos;

    ConfigurableJoint joint;
    Collider thisCollider;
    Rigidbody rb;
    bool inLock = false;

    private void Start()
    {
        thisCollider = GetComponentInChildren<MeshCollider>();
        joint = GetComponent<ConfigurableJoint>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Lock"))      
        {
            Debug.Log("Hit Lock");

            thisCollider.isTrigger = true;
            rb.isKinematic = true;

            transform.parent = snapPos;
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(new Vector3(270, 180, 270));

            Destroy(GetComponent<ObjectPhasePrevention>());

            inLock = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Key hit a trigger");
    }

    private void Update()
    {
        if (inLock)
        {
            Debug.Log(transform.localEulerAngles);

            //If in the lock and turned
            if (transform.localRotation.eulerAngles.y > 295f)
            {
                lockObj.GetComponentInParent<LockScript>().Unlock();
            }
        }
    }
}
