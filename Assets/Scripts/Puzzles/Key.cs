using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject lockObj;
    [SerializeField] Transform snapPos;
    [SerializeField] GrabFreeTransformer keyContrainTransformer;
    [SerializeField] TransformerUtils.RotationConstraints lockRotationalConstraints = new TransformerUtils.RotationConstraints()
    {
        XAxis = new TransformerUtils.ConstrainedAxis(),
        YAxis = new TransformerUtils.ConstrainedAxis(),
        ZAxis = new TransformerUtils.ConstrainedAxis()
    };

    [Header("Dev Tools")]
    [SerializeField] bool turnBypass = false;
    Grabbable handGrab;
    Collider thisCollider;
    Rigidbody rb;

    bool inLock = false;

    private void Start()
    {
        //Setup
        thisCollider = GetComponentInChildren<MeshCollider>();
        handGrab = GetComponentInChildren<Grabbable>();
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
        transform.localRotation = Quaternion.Euler(new Vector3(180, 270, 90));

        //Makes the key fully attach to the lock, prevent key from respawing if lock faces into ground
        Destroy(GetComponent<ObjectPhasePrevention>());

        //Cause constained grab by replacing the rotation constraints
        keyContrainTransformer.InjectOptionalRotationConstraints(lockRotationalConstraints);

        //Bool update
        inLock = true;

        //Temp fix
        if (turnBypass)
            lockObj.GetComponentInParent<LockScript>().Unlock();
    }

    void Update()
    {
        //If the key is in the lock, check for the rotation of the key and if the key has been turned
        if (inLock)
        {
            transform.localPosition = Vector3.zero;

            Debug.Log(transform.localEulerAngles);

            //If in the lock and turned, unlock the lock
            if (transform.localRotation.eulerAngles.y > 300f)
            {
                lockObj.GetComponentInParent<LockScript>().Unlock();
                rb.freezeRotation = true;
                Destroy(handGrab.gameObject);
            }
        }


        if (rb.isKinematic && !inLock)
        {
            float dist = Vector3.Distance(transform.position, lockObj.transform.position);
            ///Debug.Log($"Distance = {dist}");
            if (dist < 0.1f)
            {
                LockFunction();
            }
        }
    }
}
