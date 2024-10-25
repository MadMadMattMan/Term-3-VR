using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    [Header("Lock")]
    [SerializeField] Grabbable grabComponent;
    [SerializeField] GrabFreeTransformer unlockedTransformer;

    [Header("Box")]
    [SerializeField] Animator boxLid;
    [SerializeField] GameObject screwdriver;

    public void Unlock()
    {
        //when unlocked, start physics and make lock grab and moveable freely
        GetComponent<Rigidbody>().isKinematic = false;

        grabComponent.InjectOptionalOneGrabTransformer(unlockedTransformer);
        grabComponent.InjectOptionalTwoGrabTransformer(unlockedTransformer);

        //Shift lid to make clear
        boxLid.SetBool("Open", true);
        screwdriver.SetActive(true);
    }
}
