using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{

    bool pouring;
    Rigidbody rb;
    ParticleSystem ptc;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        ptc = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (Tipping() && rb.isKinematic)
            PourCan();
        else if (pouring)
        {
            ptc.Stop();
            pouring = false;
        }
    }

    void PourCan()
    {
        pouring = true;
        ptc.Play();
    }

    bool Tipping()
    {
        Debug.Log(transform.localEulerAngles.x);
        if (transform.localEulerAngles.x >= 140 || transform.localEulerAngles.x <= -55)
            return true;
        else
            return false;
    }
}
