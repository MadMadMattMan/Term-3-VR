using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    [SerializeField] Grabbable grabComponent;
    [SerializeField] GrabFreeTransformer unlockedTransformer;

    public void Unlock()
    {
        GetComponent<Rigidbody>().isKinematic = false;

        grabComponent.InjectOptionalOneGrabTransformer(unlockedTransformer);
        grabComponent.InjectOptionalTwoGrabTransformer(unlockedTransformer);
    }
}
