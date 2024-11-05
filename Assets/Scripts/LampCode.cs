using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampCode : MonoBehaviour
{
    bool lightState;
    [SerializeField] GameObject bulb;

    public void Toggle()
    {
        if (lightState)
        {
            lightState = false;
            bulb.SetActive(false);
        }
        else
        {
            lightState = true;
            bulb.SetActive(true);
        }
    }
}
