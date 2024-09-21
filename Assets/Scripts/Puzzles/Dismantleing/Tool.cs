using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public List<GameObject> objectsInZone = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        objectsInZone.Add(other.gameObject);
        try
        {
            StartDismantle(other.gameObject.GetComponent<Dismantlee>());
            Debug.Log("Started dismantle of " + other.gameObject.name);
        }
        catch
        {
            Debug.Log("Cannot start dismantle - target isn't dismantlee");
        }

    }
    void OnTriggerExit(Collider other)
    {
        objectsInZone.Remove(other.gameObject);
        try
        {
            StopDismantle(other.gameObject.GetComponent<Dismantlee>());
            Debug.Log("Stopped dismantle of " + other.gameObject.name);
        }
        catch
        {
            Debug.Log("Cannot stop dismantle - target isn't dismantlee");
        }
    }

    void StartDismantle(Dismantlee target)
    {
        target.Dismantle(this);
    }
    void StopDismantle(Dismantlee target)
    {
        target.dismantleing = false;
    }
}
