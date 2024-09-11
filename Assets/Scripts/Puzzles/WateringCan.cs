using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{

    [SerializeField] bool pouring, tipped;
    [SerializeField] Vector3 angle;
    Rigidbody rb;
    ParticleSystem ptc;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>(); 
        ptc = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        tipped = Tipping();

        //If the can is being tipped over and is held by the player, pour the can
        if (Tipping() && rb.isKinematic && !pouring)
        {
            pouring = true;
            ptc.Play();
        }

        //If the player lets go of the can or stops tipping it over, stop pouring
        else if (!Tipping() || !rb.isKinematic)
        {
            ptc.Stop();
            pouring = false;
        }
    }

    bool Tipping()
    {
        angle = transform.localEulerAngles;
        if (transform.localEulerAngles.x <= 355 && transform.localEulerAngles.x > 90)
            return true;
        else
            return false;
    }
}
