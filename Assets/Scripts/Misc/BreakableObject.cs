using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("speed required to break")]   [SerializeField] float breakSpeed;
    [Tooltip("% of energy conserved")]     [SerializeField] float smashPercent = 0.4f;
    [Tooltip("is this object stationary")] [SerializeField] bool stationary;

    [Header("References")]
    [SerializeField] GameObject startObject;

    [SerializeField] GameObject brokenObject;
    [SerializeField] Rigidbody[] brokenRbs = new Rigidbody[0];

    [Header("Private")]
    Rigidbody mainObjRb;
    [SerializeField] float speed;
    [SerializeField] float brokenSpeed;
    Vector3 pastPos;
    Vector3 collisionNormal = Vector3.zero;


    private void Start()
    {
        startObject.SetActive(true);
        brokenObject.SetActive(false);

        mainObjRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (mainObjRb != null && !stationary)
        {
            if (mainObjRb.isKinematic)
            {
                //v = d/t
                speed = Mathf.Abs((transform.position - pastPos).magnitude) / Time.deltaTime;
                pastPos = transform.position;
            }
            else
            {
                speed = mainObjRb.velocity.magnitude;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Crash speed " + mainObjRb.velocity.magnitude);

        //If the object has a speed above the break value
        if (speed >= breakSpeed && !stationary)
        {
            //Find the normal directon of the contact point object and set collisionNormal, then start Smash()
            collisionNormal = collision.contacts[0].normal.normalized;
            SmashObject();
        }
        else if (speed >= breakSpeed)
        {
            SmashObjectStationary();
        }
    }

    void SmashObject()
    {
        brokenSpeed = speed;
        //Ininital values
        Vector3 relativePos = startObject.transform.position;

        //Breaking
        startObject.SetActive(false);
        mainObjRb.isKinematic = true; //Disable main object falling


        brokenObject.SetActive(true);

        //Final values
        float fSpeed = speed * smashPercent;

        //Launch Objects
        for (int i = 0; i < brokenRbs.Length; i++)
        {
            //Gets a vector of the broken peice relative to origin point
            float dx = brokenRbs[i].gameObject.transform.position.x - relativePos.x;
            float dy = brokenRbs[i].gameObject.transform.position.y - relativePos.y;
            float dz = brokenRbs[i].gameObject.transform.position.z - relativePos.z;
            Vector3 smashDir = new Vector3(dx, dy, dz).normalized;

            smashDir += collisionNormal; //Adjusts the direction to have a bit of bounce

            //Scale the vector by the final speed calculation
            brokenRbs[i].velocity = smashDir * fSpeed;
        }
    }
    void SmashObjectStationary()
    {

    }
}
