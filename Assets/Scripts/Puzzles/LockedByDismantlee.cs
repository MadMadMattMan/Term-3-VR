using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedByDismantlee : MonoBehaviour
{
    [SerializeField] List<GameObject> anchors;
    [SerializeField] bool locked;
    Rigidbody rb;
    Grabbable handGrab;
    GrabFreeTransformer grabRestriction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        handGrab = GetComponentInChildren<Grabbable>();
        grabRestriction = GetComponent<GrabFreeTransformer>();
    }

    void UnlockObject()
    {
        rb.isKinematic = false;
        handGrab.InjectOptionalOneGrabTransformer(grabRestriction);
        handGrab.InjectOptionalTwoGrabTransformer(grabRestriction);
    }

    public void AnchorUnlocked(GameObject dismantlee)
    {
        anchors.Remove(dismantlee);

        if (anchors.Count <= 0)
        {
            UnlockObject();
        }
    }
}
