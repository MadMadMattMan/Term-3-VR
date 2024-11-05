using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedByDismantlee : MonoBehaviour
{
    [SerializeField] List<GameObject> anchors;
    [SerializeField] GameObject hammer;
    Rigidbody rb;
    //Grabbable handGrab;
    //GrabFreeTransformer grabRestriction;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        //handGrab = GetComponentInChildren<Grabbable>();
        //grabRestriction = GetComponent<GrabFreeTransformer>();
    }

    void UnlockObject()
    {
        //When the object is unlocked
        StartCoroutine(OpenScrewDraw());

        //rb.isKinematic = false;
        //handGrab.InjectOptionalOneGrabTransformer(grabRestriction);
        //handGrab.InjectOptionalTwoGrabTransformer(grabRestriction);
    }

    public void AnchorUnlocked(GameObject dismantlee)
    {
        anchors.Remove(dismantlee);

        //If there are no anchors (screws) holding the object closed
        if (anchors.Count <= 0)
        {
            UnlockObject();
        }
    }

    [SerializeField] float step = 5f;
    [SerializeField] float rot = 15f;
    [SerializeField] float distance = 0.19f;
    [SerializeField] float time = 0.5f;

    //The opening draw animation 
    IEnumerator OpenScrewDraw()
    {
        //Enables hammer for use
        hammer.SetActive(true);

        //Plays win sound
        GameObject.Find("Event Sound").GetComponent<AudioSource>().Play();

        //Physically animates the opening of the draw with code
        ///Debug.Log("Started OpenDraw");
        for (int i = 0; i < step; i++)
        {
            ///Debug.Log(distance + " " + step + " " + time);
            transform.eulerAngles += new Vector3(rot / step, 0, 0);
            transform.localPosition += new Vector3(0, 0, distance / step);
            yield return new WaitForSeconds(time / step);
        }
    }
}
