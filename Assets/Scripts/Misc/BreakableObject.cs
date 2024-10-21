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

    [SerializeField] GameObject brokenObject, stationaryTool;
    [SerializeField] Rigidbody[] brokenRbs = new Rigidbody[0];

    [Header("Private")]
    Rigidbody mainObjRb, toolObjRb;
    [SerializeField] float speed;
    [SerializeField] float brokenSpeed;
    Vector3 pastPos;
    Vector3 collisionNormal = Vector3.zero;


    void Start()
    {
        startObject.SetActive(true);
        brokenObject.SetActive(false);

        mainObjRb = GetComponent<Rigidbody>();
        if (stationary)
            toolObjRb = stationaryTool.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (mainObjRb != null && !stationary)
        {
            if (mainObjRb.isKinematic)
            {
                //Calculate velocity if the object isn't moving through physics rigidbody (ie is kinematic)
                //v = d/t
                speed = Mathf.Abs((transform.position - pastPos).magnitude) / Time.deltaTime;
                pastPos = transform.position;
            }
            else
            {
                //Get velocity if using physics rigidbody
                speed = mainObjRb.velocity.magnitude;
            }
        }
        else if (startObject != null)
        {
            //Get velocity of tool if the smashing object is staionary
            if (!toolObjRb.isKinematic)
            {
                //Calculate velocity if the object isn't moving through physics rigidbody (ie is kinematic)
                //v = d/t
                speed = Mathf.Abs((stationaryTool.transform.position - pastPos).magnitude) / Time.deltaTime;
                pastPos = stationaryTool.transform.position;

                ///Debug.Log("Speed via calc: " + speed + "  " + pastPos);
            }
            else
            {
                //Get velocity if using physics rigidbody
                speed = toolObjRb.velocity.magnitude;

                ///Debug.Log("Speed via physics");
            }
        }
        else
        {
            //Debugging error message
            Debug.LogError("Invalid breakable object settings");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug Test
        ///Debug.Log("Detected breakable collision with " + collision.gameObject.name);

        //If the object has a speed above the break value
        if (speed >= breakSpeed && !stationary)
        {
            //Find the normal directon of the contact point object and set collisionNormal, then start Smash()
            collisionNormal = collision.contacts[0].normal.normalized;
            SmashObject();
        }
        //else if the breaking tool has a large enough speed
        else if (speed >= breakSpeed && stationary && collision.gameObject.name == stationaryTool.name)
        {
            SmashObject();
        }
    }

    void SmashObject()
    {
        brokenSpeed = speed;
        //Ininital values
        Vector3 relativePos = startObject.transform.position;

        //Hide whole object and show broken object
        startObject.SetActive(false);
        brokenObject.SetActive(true);

        //Physics adjustments if not stationary
        if (!stationary)
            mainObjRb.isKinematic = true; //Disable main object falling

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

            smashDir += collisionNormal * 1.5f; //Adjusts the direction to have a bit of overall bounce with collision

            //Scale the vector by the final speed calculation and apply as velocity
            brokenRbs[i].velocity = smashDir * fSpeed;
        }
    }
}
