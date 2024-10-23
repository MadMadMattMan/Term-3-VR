using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject lockObj;
    [SerializeField] Transform snapPos;
    [SerializeField] GrabFreeTransformer lockedConstraint;

    Grabbable handGrab;
    Collider thisCollider;
    Rigidbody rb;
    bool inLock = false;

    private void Start()
    {
        //Setup
        thisCollider = GetComponentInChildren<MeshCollider>();
        handGrab = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Lock"))      
        {
            LockFunction();
        }
    }

    void LockFunction()
    {
        ///Debug.Log("Key hit lock");

        //Disables collisions for key and prevents free movement
        thisCollider.isTrigger = true;
        rb.isKinematic = true;

        //Snaps key into lock
        transform.parent = snapPos;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.Euler(new Vector3(270, 180, 270));

        //Makes the key fully attach to the lock, prevent key from respawing if lock faces into ground
        Destroy(GetComponent<ObjectPhasePrevention>());

        //Cause constained grab
        handGrab.InjectOptionalOneGrabTransformer(lockedConstraint);
        handGrab.InjectOptionalTwoGrabTransformer(lockedConstraint);

        //Bool update
        inLock = true;
    }

    private void Update()
    {
        //If the key is in the lock, check for the rotation of the key and if the key has been turned
        if (inLock)
        {
            rb.isKinematic = true;
            transform.localPosition = Vector3.zero;

            Debug.Log(transform.localEulerAngles);

            //If in the lock and turned, unlock the lock
            if (transform.localRotation.eulerAngles.y > 295f)
            {
                lockObj.GetComponentInParent<LockScript>().Unlock();
            }
        }
        if (rb.isKinematic && !inLock)
        {
            float dist = Vector3.Distance(transform.position, lockObj.transform.position);

            if (dist < 0.05f)
            {
                LockFunction();
            }
        }
    }
}
