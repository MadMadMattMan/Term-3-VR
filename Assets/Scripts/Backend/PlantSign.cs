using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSign : MonoBehaviour
{
    Grabbable plantSign;
    GrabFreeTransformer free;

    private void Start()
    {
        plantSign = GetComponentInChildren<Grabbable>();
        free = GetComponent<GrabFreeTransformer>();
    }

    private void Update()
    {
        if (transform.localPosition.x > 0.94f)
        {
            plantSign.InjectOptionalOneGrabTransformer(free);
            plantSign.InjectOptionalTwoGrabTransformer(free);
        }
    }
}
