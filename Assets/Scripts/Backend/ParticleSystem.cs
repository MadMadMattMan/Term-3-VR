using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] PlantScript plant;

    public void OnParticleTrigger()
    {
        plant.ParticleTrigger();
    }
}